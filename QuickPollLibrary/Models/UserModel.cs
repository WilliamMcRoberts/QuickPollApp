
namespace QuickPollLibrary.Models;

public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string ObjectIdentifier { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public List<Guid> PollIds { get; set; } = new();
}
