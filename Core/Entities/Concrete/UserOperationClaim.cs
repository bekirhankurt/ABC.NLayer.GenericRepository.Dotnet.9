namespace Core.Entities.Concrete;

public class UserOperationClaim : IEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}