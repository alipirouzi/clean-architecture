namespace Persistence.Outbox;

public sealed class OutboxMessage
{
    public required Guid Id { get; init; }
    public required string Type { get; init; } = string.Empty;
    public required DateTime CreatedAt { get; init; }
    public required string Content { get; init; } = string.Empty;
    public string Error { get; private set; } = string.Empty;
    public DateTime? ProcessedAtUtc { get; private set; }
    public void MarkAsProcessed(DateTime dateTime) => ProcessedAtUtc = dateTime;

    public void MarkAsError(DateTime dateTime, string error)
    {
        Error = error;
        ProcessedAtUtc = dateTime;
    }
}