namespace Shared;

public interface ISoftDeletable
{
    public bool IsDeleted { get; }

    public DateTime? DeletedOn { get; }

    void Delete();

    void Restore();
}