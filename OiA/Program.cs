using System;
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
                Logger.Debug("init main");
                Logger.Debug("Hello World!");
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
