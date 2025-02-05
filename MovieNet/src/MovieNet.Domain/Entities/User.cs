using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MovieNet.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        [MaxLength(44)]
        public required string Salt { get; set; }
        [MaxLength(44)]
        public required string PasswordHash { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly Birthday { get; set; }
        public Role Role { get; set; }
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<Rating> Ratings { get; set; } = [];
        public ICollection<MovieListEntry> MovieListEntries { get; set; } = [];

        protected User()
        { }

        [SetsRequiredMembers]
        public User(string initialPassword, string userName, string email, DateOnly createdAt, DateOnly birthday, Role role)
        {
            Guid = Guid.NewGuid();
            UserName = userName;
            Email = email;
            CreatedAt = createdAt;
            Birthday = birthday;
            SetPassword(initialPassword);
            Role = role;
        }

        [MemberNotNull(nameof(Salt), nameof(PasswordHash))]
        public void SetPassword(string password)
        {
            Salt = GenerateRandomSalt();
            PasswordHash = CalculateHash(password, Salt);
        }
        public bool CheckPassword(string password) => PasswordHash == CalculateHash(password, Salt);
        private string GenerateRandomSalt(int length = 128)
        {
            byte[] salt = new byte[length / 8];
            using (System.Security.Cryptography.RandomNumberGenerator rnd =
                System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        private string CalculateHash(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            System.Security.Cryptography.HMACSHA256 myHash =
                new System.Security.Cryptography.HMACSHA256(saltBytes);

            byte[] hashedData = myHash.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashedData);
        }
    }
}
