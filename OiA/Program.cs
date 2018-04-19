using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NLog;
using OiA.Repository;

namespace OiA
{
    class Program
    {
        private static readonly LogFactory LogFactory = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config");
        static readonly Logger Logger = LogFactory.GetCurrentClassLogger();
        static string testDirectory = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\..\TestData"));

        static void Main(string[] args)
        {
            try
            {
                Logger.Debug($"OiA Started on {Environment.MachineName}");
                var directories = Directory.GetDirectories("C:\\TestData", "*", SearchOption.AllDirectories).ToList();
                var foo = Directory.GetFiles(@"C:\TestData", "*", SearchOption.AllDirectories).ToList();
                Logger.Debug($"Number of Files: {foo.Count}");

                DbStuffProcess();

                Logger.Debug("DB Stuff Done");
                Logger.Debug("OiA Finished");
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

        private static void DbStuffProcess()
        {
            using (OiAContextcs context = new OiAContextcs())
            {
                ReadFile readFile = new ReadFile();

                var file = Path.Combine(testDirectory, "Aquatic Flowers.jpg");
                var fileDetail = readFile.GetFileDetails(file);

                var readAllBytes = File.ReadAllBytes(file);

                fileDetail.Md5Hash = HashFile.GenerateMd5Hash(readAllBytes);
                fileDetail.Sha256Hash = HashFile.GenerateSHA256String(readAllBytes);
                fileDetail.Sha512Hash = HashFile.GenerateSHA512String(readAllBytes);

                context.FileSystem.Add(fileDetail);
                context.SaveChanges();
            }
        }
    }
}
