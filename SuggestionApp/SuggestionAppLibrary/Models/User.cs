namespace SuggestionAppLibrary.Models;

public class User
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   //MongoDB ID
   public string Id { get; set; }
   //Azure ID
   public string ObjectIdentifier { get; set; }
   public string FirstName{ get; set; }
   public string LastName { get; set; }
   public string DisplayName { get; set; }
   public string Email { get; set; }
   public List<BasicSuggestion> AuthoredSuggestions { get; set; } = new();
   public List<BasicSuggestion> VotedOnSuggestions { get; set; } = new();
}
