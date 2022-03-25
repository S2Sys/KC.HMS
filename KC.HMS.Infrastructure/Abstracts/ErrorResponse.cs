using System;
using System.Text.Json;

namespace KC.HMS.Infrastructure.Abstracts
{
    public struct ErrorResponse
    {
        public int StatusCode;
        public string Message;
        public Exception Exception;
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

