using System;
using System.IO;

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
