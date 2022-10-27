
namespace SuggestionAppLibrary.DataAccess;

public class MongoUserData : IUser
{
   private readonly IMongoCollection<User> _users;
   public MongoUserData(IDbConnection db)
   {
      _users = db.UserCollection;
   }

   public async Task<List<User>> GetUsersAsync()
   {
      var users = await _users.FindAsync(u => true);

      return users.ToList();
   }

   public async Task<User> GetUser(string id)
   {
      var user = await _users.FindAsync(u => u.Id == id);
      return user.FirstOrDefault();
   }

   public async Task<User> GetUserFromAuthentication(string objectId)
   {
      var user = await _users.FindAsync(u => u.ObjectIdentifier == objectId);
      return user.FirstOrDefault();
   }

   public Task CreateUser(User user)
   {
      return _users.InsertOneAsync(user);
   }
   public Task UpdateUser(User user)
   {
      var filteredUser = Builders<User>.Filter.Eq("Id", user.Id);
      return _users.ReplaceOneAsync(filteredUser, user, new ReplaceOptions { IsUpsert = true });
   }
}
