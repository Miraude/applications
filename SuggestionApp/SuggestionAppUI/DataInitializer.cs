using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace SuggestionAppUI;

public static class DataInitializer
{
   public static void ConfigureServices(this WebApplicationBuilder builder)
   {
      builder.Services.AddRazorPages();
      //Add consent handler to blazor so auth works
      builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
      builder.Services.AddMemoryCache();
      //Adds the identity handling and mvc controllers functions into the app.
      builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();

      //read from appsettings and add it to authentication
      builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
         .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

      builder.Services.AddAuthorization(options =>
      {
         //Create new policy
         options.AddPolicy("Admin", policy =>
         {
            //To get this policcy "admin", you need this claim
            policy.RequireClaim("jobTitle", "Admin");
         });
      });

      builder.Services.AddSingleton<IDbConnection, DbConnection>();
      builder.Services.AddSingleton<ICategory, MongoCategory>();
      builder.Services.AddSingleton<IStatus, MongoStatus>();
      builder.Services.AddSingleton<IUser, MongoUserData>();
      builder.Services.AddSingleton<ISuggestion, MongoSuggestion>();
   }
}
