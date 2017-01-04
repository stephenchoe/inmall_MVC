using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Web.Core.ErrorHandler;

namespace BLL
{
    public interface IErrorHandlerService
    {
        void LogException(Exception ex);
    }
    public class ErrorHandlerService : IErrorHandlerService
    {
        private readonly IErrorLogger errorLogger;
        public ErrorHandlerService(IErrorLogger errorLogger)
        {
            this.errorLogger = errorLogger;            
        }

        public void LogException(Exception ex)
        {
            errorLogger.LogException(ex);
        }

    }
}
