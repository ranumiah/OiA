using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            foreach (var files in GetFileToProcess().AsParallel())
            {
                var fileDetails = GetFileDetail(files);

                processFiles.Add(fileDetails);
                if (processFiles.Count >= 50000)
                {
                    Logger.Debug($"Save to database 50000 Processed File");
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
                context.FileSystem.AddRange(files);
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

        static List<string> GetFileToProcess()
        {
            using (OiAContextcs context = new OiAContextcs())
            {
                var files = context.PendingFile.Select(x => x.FileFullName).ToList();
                return files;
            }
        }
    }
}
