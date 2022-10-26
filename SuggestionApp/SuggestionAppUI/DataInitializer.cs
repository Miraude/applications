namespace SuggestionAppUI;

public static class DataInitializer
{
   public static void ConfigureServices(this WebApplicationBuilder builder)
   {
      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor();
      builder.Services.AddMemoryCache();
   }
}
