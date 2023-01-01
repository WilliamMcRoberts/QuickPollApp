﻿
namespace QuickPollLibrary.Models;

public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public string ObjectIdentifier { get; set; }

    public string DisplayName { get; set; }

    public List<string> PollIds { get; set; } = new();
}
