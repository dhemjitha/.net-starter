using WebApplication6.Models;

namespace WebApplication6.Services
{
    public class UserServices
    {
        private static List<User> users = new List<User>();

        public UserServices()
        {
            if (users.Count == 0)
            {
                users.Add(new User
                {
                    Id = 1,
                    Name = "Alice",
                    Email = "alice@gmail.com"
                });

                users.Add(new User
                {
                    Id = 2,
                    Name = "Bob",
                    Email = "bob@gmail.com"
                });

                users.Add(new User
                {
                    Id = 3,
                    Name = "Charlie",
                    Email = "charlie@gmail.com"
                });
            }
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public User AddUser(User user)
        {
            user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            return user;
        }

        public User DeletableUser(User user)
        {
            var existingUser = users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                users.Remove(existingUser);
                return existingUser;
            }
            return null;
        }

        public User UpdateUser(User user)
        {
            var existingUser = users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                return existingUser;
            }
            return null;
        }
    }
}
