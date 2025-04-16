using System.Collections.Generic;
using System.Linq;

public class UserRepository : IUserRepository
{
    private readonly List<User> users = new();

    public List<User> GetAllUsers() => users;

    public User GetUserById(int id) => users.FirstOrDefault(u => u.Id == id);

    public void AddUser(User user) => users.Add(user);

    public void UpdateUser(User user)
    {
        var existingUser = GetUserById(user.Id);
        if (existingUser != null)
        {
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
        }
    }

    public void DeleteUser(int id) => users.RemoveAll(u => u.Id == id);
}
