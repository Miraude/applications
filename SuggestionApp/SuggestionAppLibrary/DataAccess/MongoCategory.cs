﻿

namespace SuggestionAppLibrary.DataAccess;

public class MongoCategory : ICategory
{
   private readonly IMongoCollection<Category> _categories;
   private readonly IMemoryCache _cache;
   private const string CacheName = "CategoryData";

   public MongoCategory(IDbConnection db, IMemoryCache cache)
   {
      _cache = cache;
      _categories = db.CategoryCollection;
   }

   public async Task<List<Category>> GetAllCategories()
   {
      var output = _cache.Get<List<Category>>(CacheName);
      if (output is null)
      {
         var categories = await _categories.FindAsync(c => true);
         output = categories.ToList();

         _cache.Set(CacheName, output, TimeSpan.FromDays(1));
      }
      return output;
   }
   public Task CreateCategory(Category category)
   {
      return _categories.InsertOneAsync(category);
   }
}
