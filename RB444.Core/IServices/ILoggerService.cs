using System;
using System.Collections.Generic;
using System.Text;

namespace RB444.Core.IServices
{
    public interface ILoggerService
    {
        void LogException(string message, Exception ex);
    }
}
