using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NLog;
using OiA.Repository;

namespace OiA
{
    class Program
    {
        private static readonly LogFactory LogFactory = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config");
        static readonly Logger Logger = LogFactory.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {

                if (args.Length == 1)
                {
                    Logger.Debug($"OiA Started on {Environment.MachineName}");

                    FileSearch(args[0]);

                    Logger.Debug("OiA Finished");
                }
                else if (args.Length == 2)
                {
                    var deDupFile = GetDeDupFile();
                    var hashYear = new HashSet<int>();
                    var hashMonth = new HashSet<int>();
                    var hashDay = new HashSet<int>();
                    foreach (var fileDetail in deDupFile)
                    {
                        hashYear.Add(fileDetail.FileCreationTimeUtc.Year);
                        hashMonth.Add(fileDetail.FileCreationTimeUtc.Month);
                        hashDay.Add(fileDetail.FileCreationTimeUtc.Day);

                        // go off and create the folder structure for copy
                        // then make a list of files to copy to
                        // then actually copy. in what mode? override or database verified.
                    }
                }
                else
                {
                    Logger.Debug($"OiA Started on {Environment.MachineName}");

                    ProcessFiles();

                    Logger.Debug("OiA Finished");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        static void FileSearch(string rootFolder)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            var directories = Directory.GetDirectories(rootFolder, "*", SearchOption.AllDirectories).ToList();
            long fileCount = 0;
            var pendingFiles = new List<PendingFile>();
            foreach (var directory in directories.AsParallel())
            {
                var files = Directory.GetFiles(directory, "*", SearchOption.TopDirectoryOnly);

                foreach (var file in files)
                {
                    pendingFiles.Add(new PendingFile {FileFullName = file});
                }

                fileCount += files.Length;
                if (pendingFiles.Count >= 10000)
                {
                    Logger.Debug($"Save to database 10000 Processed File");
                    SaveToDb(pendingFiles);
                    Logger.Debug($"Database operation completed");
                    pendingFiles = new List<PendingFile>();
                }
            }

            SaveToDb(pendingFiles);

            timer.Stop();
            Logger.Debug($"Number of Files (ParrellSearch): {fileCount} in {timer.Elapsed}");
        }


        static void ProcessFiles()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var processFiles = new List<FileDetail>();
            foreach (var file in GetFileToProcess().AsParallel())
            {
                try
                {
                    var fileDetails = GetFileDetail(file);
                    processFiles.Add(fileDetails);
                }
                catch (Exception e)
                {
                    SaveToDb(file, e.Message);
                    Logger.Error(e, "Stopped program because of exception");
                }

                if (processFiles.Count >= 10000)
                {
                    Logger.Debug($"Save to database 10000 Processed File");
                    SaveToDb(processFiles);
                    Logger.Debug($"Database operation completed");
                    processFiles = new List<FileDetail>();
                }
            }

            SaveToDb(processFiles);

            timer.Stop();
        }

        static FileDetail GetFileDetail(string file)
        {
            var readFile = new ReadFile();
            var fileDetail = readFile.GetFileDetails(file);

            if (fileDetail.Length < int.MaxValue)
            {
                var readAllBytes = File.ReadAllBytes(file);
                var hashFile = new HashFile();
                fileDetail.Md5Hash = hashFile.GenerateHash(readAllBytes);
            }

            return fileDetail;
        }

        static void SaveToDb(IEnumerable<FileDetail> files)
        {
            using (OiAContextcs context = new OiAContextcs())
            {
                var fileDetails = files.ToList();
                foreach (var file in fileDetails)
                {
                    context.PendingFile.Single(x => x.FileFullName == file.FullName).Status = ProcessStatus.Complete;
                }
                context.FileSystem.AddRange(fileDetails);
                context.SaveChanges();
            }
        }

        static void SaveToDb(IEnumerable<PendingFile> files)
        {
            using (OiAContextcs context = new OiAContextcs())
            {
                context.PendingFile.AddRange(files);
                context.SaveChanges();
            }
        }

        static void SaveToDb(string file, string message)
        {
            using (OiAContextcs context = new OiAContextcs())
            {
                context.PendingFile.Single(x => x.FileFullName == file).Status = ProcessStatus.Error + message;
                context.SaveChanges();
            }
        }

        static List<string> GetFileToProcess()
        {
            using (OiAContextcs context = new OiAContextcs())
            {
                var files = context.PendingFile.Where(x => x.Status == ProcessStatus.New).Select(x => x.FileFullName).ToList();
                return files;
            }
        }

        static List<FileDetail> GetDeDupFile()
        {
            Dictionary<string, List<FileDetail>> database = new Dictionary<string, List<FileDetail>>();
            using (OiAContextcs context = new OiAContextcs())
            {
                foreach (var fileDetail in context.FileSystem.OrderBy(x => x.FileLastWriteTimeUtc).ThenBy(x => x.FileCreationTimeUtc).ThenBy(x => x.FileLastAccessTimeUtc))
                {
                    if (fileDetail.Md5Hash != null)
                    {
                        if (database.ContainsKey(fileDetail.Md5Hash))
                        {
                            database[fileDetail.Md5Hash].Add(fileDetail);
                        }
                        else
                        {
                            database.Add(fileDetail.Md5Hash, new List<FileDetail> {fileDetail});
                        }
                    }
                }
            }

            List<FileDetail> uniqueData = new List<FileDetail>();
            foreach (var fileDetails in database.Values)
            {
                if (fileDetails.Count >= 1)
                {
                    uniqueData.Add(fileDetails.First());
                }
            }

            return uniqueData;
        }
    }
}
