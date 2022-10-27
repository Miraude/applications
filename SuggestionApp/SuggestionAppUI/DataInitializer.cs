using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SuggestionAppUI;

public static class DataInitializer
{
   public static void ConfigureServices(this WebApplicationBuilder builder)
   {
      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor();
      builder.Services.AddMemoryCache();
      builder.Services.AddSingleton<IDbConnection, DbConnection>();
      builder.Services.AddSingleton<ICategory, MongoCategory>();
      builder.Services.AddSingleton<IStatus, MongoStatus>();
      builder.Services.AddSingleton<IUserData, MongoUserData>();
      builder.Services.AddSingleton<ISuggestion, MongoSuggestion>();
   }
}
