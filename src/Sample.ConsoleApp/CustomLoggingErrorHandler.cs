using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;

namespace Sample.ConsoleApp
{
    public class CustomLoggingErrorHandler : IErrorHandler
    {
        private Action<string, Exception, ErrorCode> _action;

        public CustomLoggingErrorHandler()
        {
        }

        public CustomLoggingErrorHandler(Action<string, Exception, ErrorCode> action)
        {
            _action = action;
        }

        public void Error(string message)
        {
            if (_action == null)
            {
                return;
            }

            _action(message, null, ErrorCode.GenericFailure);
        }

        public void Error(string message, Exception e)
        {
            if (_action == null)
            {
                return;
            }

            _action(message, e, ErrorCode.GenericFailure);
        }

        public void Error(string message, Exception e, ErrorCode errorCode)
        {
            if (_action == null)
            {
                return;
            }

            _action(message, e, errorCode);
        }
    }
}
