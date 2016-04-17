using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Devevil.Blog.Logger.Log4Net
{
    public static class Logger<T>
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(T));

        public static void Configure()
        {
            BasicConfigurator.Configure();
        }

        public static void Error(string prmMsg)
        {
            logger.Error(prmMsg);
        }

        public static void Error(Exception prmMsg)
        {
            logger.Error(prmMsg);
        }

        public static void Info(string prmMsg)
        {
            logger.Info(prmMsg);
        }

        public static void Info(Exception prmMsg)
        {
            logger.Info(prmMsg);
        }

        public static void Warning(string prmMsg)
        {
            logger.Warn(prmMsg);
        }

        public static void Warning(Exception prmMsg)
        {
            logger.Warn(prmMsg);
        }

        public static void Debug(string prmMsg)
        {
            logger.Debug(prmMsg);
        }

        public static void Debug(Exception prmMsg)
        {
            logger.Debug(prmMsg);
        }
    }
}
