namespace WebAPI.Test.Comparers
{
    using System.Collections.Generic;

    using CodeFirst.Models.Entities;

    public class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Id == y.Id 
                   && x.FirstName == y.FirstName
                   && x.LastName == y.LastName
                   && x.Email == y.Email
                   && x.Password == y.Password
                   && x.Salt == y.Salt
                   && x.PositionId == y.PositionId;
        }

        public int GetHashCode(User obj)
        {
            var hashId = obj.Id.GetHashCode();
            var hashFirstName = obj.FirstName.GetHashCode();
            var hashLastName = obj.LastName.GetHashCode();
            var hashEmail = obj.Email.GetHashCode();
            var hashPassword = obj.Password.GetHashCode();
            var hashSalt = obj.Salt.GetHashCode();
            var hashPositionId  = obj.PositionId.GetHashCode();

            return hashId ^ hashFirstName ^ hashLastName ^ hashEmail ^ hashPassword ^ hashSalt ^ hashPositionId;
        }
    }
}
