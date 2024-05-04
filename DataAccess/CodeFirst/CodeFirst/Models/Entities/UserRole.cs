namespace CodeFirst.Models.Entities
{
    public class UserRole : IEntity<long>
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long RoleId { get; set; }

        #region Navigation properties

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }

        #endregion
    }
}
