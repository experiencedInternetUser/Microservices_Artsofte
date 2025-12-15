namespace Logic.Trace
{
    public interface ITraceWriter
    {
        string Name { get; }
        string GetValue();
    }
}
