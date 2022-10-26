using MongoDB.Driver;

namespace SuggestionAppLibrary.DataAccess;

public interface IDbConnection
{
   IMongoCollection<Category> CategoryCollection { get; }
   string CategoryCollectionName { get; }
   MongoClient Client { get; }
   string DbName { get; }
   IMongoCollection<Status> StatusCollection { get; }
   string StatusCollectionName { get; }
   IMongoCollection<Suggestion> SuggestionCollection { get; }
   string SuggestionCollectionName { get; }
   IMongoCollection<User> UserCollection { get; }
   string UserCollectionName { get; }
}