using SharedKernel;

namespace Domain.Entities;

public class Invitation:Entity
{
    public Invitation(Guid id, Guid gatheringId, Guid memberId, InvitationStatus status, DateTime createdAt) : base(id)
    {
        GatheringId = gatheringId;
        MemberId = memberId;
        Status = status;
        CreatedAt = createdAt;
    }

    public Guid GatheringId { get; private set; }
    public Guid MemberId { get; private set; }
    public InvitationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
}