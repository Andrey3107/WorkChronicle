namespace CodeFirst.Models
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
