namespace CodeFirst.Models.Entities
{
    public class UserToProject : IEntity<long>
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long ProjectId { get; set; }

        #region Navigation properties

        public virtual User User { get; set; }

        public virtual Project Project { get; set; }

        #endregion
    }
}
