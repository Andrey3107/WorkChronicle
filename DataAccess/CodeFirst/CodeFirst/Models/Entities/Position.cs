namespace CodeFirst.Models.Entities
{
    using System.Collections.Generic;

    public class Position : IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        #region Navigation properties

        public virtual List<User> Users { get; set; }

        #endregion
    }
}
