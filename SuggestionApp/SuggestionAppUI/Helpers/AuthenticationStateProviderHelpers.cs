
namespace SuggestionAppUI.Helpers;

public static class AuthenticationStateProviderHelpers
{
   public static async Task<User> GetUserFromAuth(
      this AuthenticationStateProvider provider, 
      IUser user)
   {
      var authState = await provider.GetAuthenticationStateAsync();
      string objectId = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("objectidentifier"))?.Value;

      return await user.GetUserFromAuthentication(objectId);
   }
}
