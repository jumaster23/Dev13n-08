using Okane.Application.Auth;
using Okane.Domain;

namespace Okane.Storage.EntityFramework;

public class UsersRepository(OkaneDbContext db) : IUsersRepository
{
    public void Add(User entity)
    {
        db.Users.Add(entity);
        db.SaveChanges();
    }

    public User? ById(int id) => 
        db.Users.Find(id);

    public IEnumerable<User> All() => db.Users;

    public void Remove(int id)
    {
        var user = db.Users.Find(id);
        db.Users.Remove(user);
        db.SaveChanges();
    }

    public bool Exists(int id) => 
        db.Users.Any(u => u.Id == id);

    public User? ByUsername(string username) => 
        db.Users.FirstOrDefault(u => u.Username == username);
}