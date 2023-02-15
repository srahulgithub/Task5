using MongoDB.Driver;

public class UsersContext
{
    private readonly IMongoCollection<User> _users;

    public UsersContext(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("UsersDB"));
        var database = client.GetDatabase("test");
        _users = database.GetCollection<User>("users");
    }

    public async Task<List<User>> GetUsers()
    {
        return await _users.Find(user => true).ToListAsync();
    }

    public async Task<User> GetUser(string id)
    {
        return await _users.Find<User>(user => user.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User> CreateUser(User user)
    {
        await _users.InsertOneAsync(user);
        return user;
    }

    public async Task UpdateUser(string id, User userIn)
    {
        await _users.ReplaceOneAsync(user => user.Id == id, userIn);
    }

    public async Task DeleteUser(string id)
    {
        await _users.DeleteOneAsync(user => user.Id == id);
    }
}
