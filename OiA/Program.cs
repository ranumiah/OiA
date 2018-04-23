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

                    ParrellSearch(args[0]);

                    Logger.Debug("DB Stuff Done");
                    Logger.Debug("OiA Finished");
                }
                else
                {
                    Logger.Error("Must provide with root folder path");
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

        static void ParrellSearch(string rootFolder)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            var directories = Directory.GetDirectories(rootFolder, "*", SearchOption.AllDirectories).ToList();
            long fileCount = 0;
            var processFiles = new List<FileDetail>();
            foreach (var directory in directories.AsParallel())
            {
                var files = Directory.GetFiles(directory, "*", SearchOption.TopDirectoryOnly);
                var fileDetails = ProcessFiles(files);
                processFiles.AddRange(fileDetails);
                fileCount += processFiles.Count;
                if (processFiles.Count >= 10000)
                {
                    SaveToDb(processFiles);
                    processFiles = new List<FileDetail>();
                }
            }

            SaveToDb(processFiles);

            timer.Stop();
            Logger.Debug($"Number of Files (ParrellSearch): {fileCount} in {timer.Elapsed}");
        }

        static List<FileDetail> ProcessFiles(IEnumerable<string> files)
        {
            var fileDetails = new List<FileDetail>();
            ReadFile readFile = new ReadFile();
            foreach (var file in files.AsParallel())
            {
                var fileDetail = readFile.GetFileDetails(file);
                var readAllBytes = File.ReadAllBytes(file);

                fileDetail.Md5Hash = HashFile.GenerateMd5Hash(readAllBytes);
                fileDetail.Sha256Hash = HashFile.GenerateSHA256String(readAllBytes);
                fileDetail.Sha512Hash = HashFile.GenerateSHA512String(readAllBytes);

                fileDetails.Add(fileDetail);
            }

            return fileDetails;
        }

        static void SaveToDb(List<FileDetail> files)
        {
            using (OiAContextcs context = new OiAContextcs())
            {
                context.FileSystem.AddRange(files);
                context.SaveChanges();
            }
        }
    }
}
