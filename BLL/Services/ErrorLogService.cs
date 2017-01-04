using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Web.Core;
using Web.Core.ErrorHandler;

namespace BLL
{
    public interface IErrorLogService 
    {
        void LogException(Exception ex);
    }
    public class ErrorLogService : IErrorLogService
    {
        private readonly IErrorLogger errorLogger;
        public ErrorLogService(IErrorLogger errorLogger)
        {
            this.errorLogger = errorLogger;
        }

        public void LogException(Exception ex)
        {
            errorLogger.LogException(ex);
        }
    }
}
