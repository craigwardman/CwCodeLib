using System;
using System.Text;

namespace CwCodeLib.ExceptionHandling
{
    internal static class ExceptionExtensions
    {
        public static string MessageEx(this Exception ex)
        {
            StringBuilder errorMessageBuilder = new StringBuilder();

            bool firstLine = true;
            Exception exception = ex;
            while (exception != null)
            {
                if (firstLine)
                {
                    firstLine = false;
                }
                else
                {
                    errorMessageBuilder.Append(Environment.NewLine);
                }

                errorMessageBuilder.Append(exception.Message);
                exception = exception.InnerException;
            }

            return errorMessageBuilder.ToString();
        }
    }
}