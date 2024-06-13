namespace Notes.Api;

public class ProcessingError
{
    public string Message { get; init; } = default!;

    public ProcessingError(string message)
    {
        Message = message;
    }
    
    public ProcessingError() {}
}