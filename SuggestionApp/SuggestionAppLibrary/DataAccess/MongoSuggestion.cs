namespace SuggestionAppLibrary.DataAccess;

public class MongoSuggestion : ISuggestion
{
   private readonly IDbConnection _db;
   private readonly IUserData _userData;
   private readonly IMemoryCache _cache;
   private readonly IMongoCollection<Suggestion> _suggestions;
   private const string CacheName = "SuggestionData";

   public MongoSuggestion(IDbConnection db, IUserData userData, IMemoryCache cache)
   {
      _db = db;
      _userData = userData;
      _cache = cache;
      _suggestions = db.SuggestionCollection;
   }

   public async Task<List<Suggestion>> GetAllSuggestions()
   {
      var output = _cache.Get<List<Suggestion>>(CacheName);
      if (output is null)
      {
         var suggestions = await _suggestions.FindAsync(s => s.Archived == false);

         output = suggestions.ToList();
         _cache.Set(CacheName, output, TimeSpan.FromMinutes(1));
      }
      return output;
   }
   public async Task<List<Suggestion>> GetAllPublicSuggestions()
   {
      var output = await GetAllSuggestions();
      return output.Where(s => s.ApprovedForRelease).ToList();
   }
   public async Task<List<Suggestion>> GetAllRejectedSuggestions()
   {
      var output = await GetAllSuggestions();
      return output.Where(s => s.Rejected).ToList();
   }
   public async Task<Suggestion> GetSuggestion(string id)
   {
      var suggestion = await _suggestions.FindAsync(s => s.Id == id);
      return suggestion.FirstOrDefault();
   }
   public async Task<List<Suggestion>> GetWaitingForApprovalSuggestions()
   {
      var output = await GetAllSuggestions();
      return output.Where(s => s.ApprovedForRelease == false && s.Rejected == false).ToList();
   }
   public async Task CreateSuggestion(Suggestion suggestion)
   {
      var client = _db.Client;
      using var session = await client.StartSessionAsync();
      session.StartTransaction();

      try
      {
         var db = client.GetDatabase(_db.DbName);
         var suggestionsInTransaction = db.GetCollection<Suggestion>(_db.SuggestionCollectionName);
         await suggestionsInTransaction.InsertOneAsync(suggestion);

         var usersInTransaction = db.GetCollection<User>(_db.UserCollectionName);
         var user = await _userData.GetUser(suggestion.Author.Id);
         user.AuthoredSuggestions.Add(new BasicSuggestion(suggestion));
         await usersInTransaction.ReplaceOneAsync(u => u.Id == user.Id, user);
         await session.CommitTransactionAsync();
      }
      catch (Exception e)
      {
         await session.AbortTransactionAsync();
         throw;
      }
   }
   public async Task UpdateSuggestion(Suggestion suggestion)
   {
      await _suggestions.ReplaceOneAsync(s => s.Id == suggestion.Id, suggestion);
      _cache.Remove(CacheName);
   }
   public async Task DeleteSuggestion(Suggestion suggestion)
   {
      await _suggestions.DeleteOneAsync(s => s.Id == suggestion.Id);
      _cache.Remove(CacheName);
   }
   public async Task UpvoteSuggestion(string suggestionId, string userId)
   {
      var client = _db.Client;
      using var session = await client.StartSessionAsync();
      session.StartTransaction();

      try
      {
         var db = client.GetDatabase(_db.DbName);
         var suggestionsInTransaction = db.GetCollection<Suggestion>(_db.SuggestionCollectionName);
         var suggestion = (await suggestionsInTransaction.FindAsync(s => s.Id == suggestionId)).First();

         bool isUpvoted = suggestion.UserVotes.Add(userId);
         if (isUpvoted == false)
         {
            suggestion.UserVotes.Remove(userId);
         }
         await suggestionsInTransaction.ReplaceOneAsync(s => s.Id == suggestionId, suggestion);

         var usersInTransaction = db.GetCollection<User>(_db.UserCollectionName);
         var user = await _userData.GetUser(suggestion.Author.Id);

         if (isUpvoted)
         {
            user.VotedOnSuggestions.Add(new BasicSuggestion(suggestion));
         }
         else
         {
            var suggestionToRemove = user.VotedOnSuggestions.Where(s => s.Id == suggestionId).First();
            user.VotedOnSuggestions.Remove(suggestionToRemove);
         }
         await usersInTransaction.ReplaceOneAsync(u => u.Id == userId, user);

         await session.CommitTransactionAsync();

         _cache.Remove(CacheName);
      }
      catch (Exception e)
      {
         await session.AbortTransactionAsync();
         throw;
      }
   }
}
