using Elmah;
using System;
using System.Web;

namespace Web.Core.ErrorHandler
{
 
    public class ElmahLogger : IErrorLogger
    {
        public void LogException(Exception ex)
        {
            try
            {
                var context = HttpContext.Current;
                if (context == null)
                    return;
                var signal = ErrorSignal.FromContext(context);
                if (signal == null)
                    return;
                signal.Raise(ex);
            }
            catch (Exception exception)
            {
                // swallow or handle with something else...
                if (exception is HttpRequestValidationException)
                {
                    return;
                }
                throw exception;
            }
        }
    }
}
