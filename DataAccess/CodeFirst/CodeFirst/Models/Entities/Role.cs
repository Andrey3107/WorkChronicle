namespace CodeFirst.Models.Entities
{
    using System.Collections.Generic;

    public class Role : IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        #region Navigation properties

        public virtual List<UserRole> UserRoles { get; set; }

        #endregion
    }
}
