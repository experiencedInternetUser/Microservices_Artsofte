namespace User.Api.Trace
{
    public interface ITraceReader
    {
        string Name { get; }
        string GetValue();
        void WriteValue(string? value);
    }
}
