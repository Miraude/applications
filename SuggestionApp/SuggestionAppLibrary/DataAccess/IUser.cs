
namespace SuggestionAppLibrary.DataAccess;

public interface IUser
{
   Task CreateUser(User user);
   Task<User> GetUser(string id);
   Task<User> GetUserFromAuthentication(string objectId);
   Task<List<User>> GetUsersAsync();
   Task UpdateUser(User user);
}