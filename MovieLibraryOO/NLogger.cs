using System;

namespace MovieLibraryOO
{
    class NLogger
    {

        public void nLog(string actionType)
        {
            string path = "nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            logger.Info(actionType);
        }
    }
}
