namespace SuggestionAppLibrary.DataAccess;

public class MongoStatus : IStatus
{
   private readonly IMongoCollection<Status> _Status;
   private readonly IMemoryCache _cache;
   private const string cacheName = "StatusData";

   public MongoStatus(IDbConnection db, IMemoryCache cache)
   {
      _cache = cache;
      _Status = db.StatusCollection;
   }

   public async Task<List<Status>> GetAllStatuses()
   {
      var output = _cache.Get<List<Status>>(cacheName);

      if (output is null)
      {
         var statuses = await _Status.FindAsync(s => true);

         output = statuses.ToList();

         _cache.Set(cacheName, output, TimeSpan.FromDays(1));
      }
      return output;
   }

   public Task CreateStatus(Status status)
   {
      return _Status.InsertOneAsync(status);
   }
}
