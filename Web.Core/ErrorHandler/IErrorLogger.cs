using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.ErrorHandler
{
    public interface IErrorLogger
    {
        void LogException(Exception ex);
    }
}
