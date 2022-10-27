
namespace SuggestionAppLibrary.DataAccess;

public interface ISuggestion
{
   Task CreateSuggestion(Suggestion suggestion);
   Task DeleteSuggestion(Suggestion suggestion);
   Task<List<Suggestion>> GetAllPublicSuggestions();
   Task<List<Suggestion>> GetAllRejectedSuggestions();
   Task<List<Suggestion>> GetAllSuggestions();
   Task<Suggestion> GetSuggestion(string id);
   Task<List<Suggestion>> GetWaitingForApprovalSuggestions();
   Task UpdateSuggestion(Suggestion suggestion);
   Task UpvoteSuggestion(string suggestionId, string userId);
}