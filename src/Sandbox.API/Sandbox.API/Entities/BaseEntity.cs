namespace Sandbox.API.Entities;

public abstract class BaseEntity
{
    public BaseEntity()
    {
        Uid = Guid.NewGuid();
    }

    public int Id { get; set; }
    public Guid Uid { get; set; }
    public DateTime CreateTimeStamp { get; set; }
    public DateTime AmendTimeStamp { get; set; }
}