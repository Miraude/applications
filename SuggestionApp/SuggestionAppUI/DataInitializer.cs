using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace SuggestionAppUI;

public static class DataInitializer
{
   public static void ConfigureServices(this WebApplicationBuilder builder)
   {
      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor();
      builder.Services.AddMemoryCache();

      builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
         .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

      builder.Services.AddSingleton<IDbConnection, DbConnection>();
      builder.Services.AddSingleton<ICategory, MongoCategory>();
      builder.Services.AddSingleton<IStatus, MongoStatus>();
      builder.Services.AddSingleton<IUser, MongoUserData>();
      builder.Services.AddSingleton<ISuggestion, MongoSuggestion>();
   }
}
