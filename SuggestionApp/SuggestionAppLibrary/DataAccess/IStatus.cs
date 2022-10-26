
namespace SuggestionAppLibrary.DataAccess;

public interface IStatus
{
   Task CreateStatus(Status status);
   Task<List<Status>> GetAllStatuses();
}