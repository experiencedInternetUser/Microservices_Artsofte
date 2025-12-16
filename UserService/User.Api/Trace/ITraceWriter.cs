namespace User.Api.Trace
{
    public interface ITraceWriter
    {
        string Name { get; }
        string GetValue();
    }
}
