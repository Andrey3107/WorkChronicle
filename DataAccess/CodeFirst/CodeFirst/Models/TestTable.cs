namespace CodeFirst.Models
{
    public class TestTable : IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? Number { get; set; }
    }
}
