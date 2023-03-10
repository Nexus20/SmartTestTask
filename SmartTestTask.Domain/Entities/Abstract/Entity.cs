namespace SmartTestTask.Domain.Entities.Abstract;

public abstract class Entity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}