namespace Notes.Api;

public enum ProcessingErrors
{
    Conflict,
    NotFound
}
public class ProcessingError
{
    public string Message { get; init; } = default!;
    public ProcessingErrors Type { get; set; } = ProcessingErrors.NotFound;

    public ProcessingError(string message)
    {
        Message = message;
    }

    public ProcessingError(string message, ProcessingErrors type) : this(message)
    {
        Type = type;
    }
    
    public ProcessingError() {}
}