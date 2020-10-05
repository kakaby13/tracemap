using System;

namespace TraceMap.Common.Exceptions
{
    public class ParseTraceLineException : Exception
    {
        private string Text { get; }

        public ParseTraceLineException(string text)
        {
            Text = text;
        }
    }
}
