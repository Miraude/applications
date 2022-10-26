
namespace SuggestionAppLibrary.Models;

public class Suggestion
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string Id { get; set; }
   public string Title { get; set; }
   public string Description { get; set; }
   public DateTime DateCreated { get; set; } = DateTime.UtcNow;
   public Category Category { get; set; }
   public BasicUser Author { get; set; }
   public HashSet<string> UserVotes { get; set; } = new();
   public Status SuggestionStatus { get; set; }
   public string OwnerNotes { get; set; }
   public bool ApprovedForRelease { get; set; } = false;
   public bool Archived { get; set; } = false;
   public bool Rejected { get; set; } = false;
}
