using System;

namespace User.Api.Trace
{
    public class TraceIdAccessor : ITraceReader, ITraceWriter
    {
        private string? _value;
        public string Name => "TraceId";

        public string GetValue()
        {
            if (string.IsNullOrEmpty(_value))
                _value = Guid.NewGuid().ToString();
            return _value;
        }

        public void WriteValue(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                value = Guid.NewGuid().ToString();
            _value = value;
        }
    }
}
