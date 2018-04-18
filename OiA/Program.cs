using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog;

namespace OiA
{
    class Program
    {
        static readonly Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                Logger.Debug($"OiA Started on {Environment.MachineName}");
                var directories = Directory.GetDirectories("C:\\TestData", "*", SearchOption.AllDirectories).ToList();
                var foo = Directory.GetFiles(@"C:\TestData", "*", SearchOption.AllDirectories).ToList();
                Logger.Debug($"Number of Files: {foo.Count}");
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
    }
}
