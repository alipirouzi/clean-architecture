using System.Security.AccessControl;
using SharedKernel;

namespace Domain.Entities;

public class Gathering : Entity
{
    private readonly List<Attendee> _attendees = [];
    private readonly List<Invitation> _invitations = [];

    private Gathering(
        GatheringId id,
        Member creator,
        GatheringType type,
        string name,
        DateTime scheduledAtUtc,
        string? location)
        : base(id.Value)
    {
        Creator = creator;
        Type = type;
        Name = name;
        ScheduledAtUtc = scheduledAtUtc;
        Location = location;
    }

    public Member Creator { get; private set; }
    public GatheringType Type { get; private set; }
    public string Name { get; set; }
    public DateTime ScheduledAtUtc { get; set; }
    public string? Location { get; set; }
}

public record GatheringId(Guid Value);