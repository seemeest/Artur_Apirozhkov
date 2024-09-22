using System;
using System.Collections.Generic;

namespace Artur_Apirozhkov.BdModels;

public partial class UserModel
{
    public int Id { get; set; }

    public int? PostsCount { get; set; }

    public int? CountLikes { get; set; }

    public int? AverageLikes { get; set; }

    public string? AvatarPhotoUrl { get; set; }

    public string? Quotes { get; set; }

    public string? Status { get; set; }

    public long? Vkid { get; set; }

    public string? About { get; set; }

    public string? Activities { get; set; }

    public string? Interest { get; set; }

    public int? FriendsCount { get; set; }
    public int? Age { get; set; }
    public string DataOfBirth { get; set; }

    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();

    public virtual ICollection<Friend> Friends { get; set; } = new List<Friend>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<UserPhoto> UserPhotos { get; set; } = new List<UserPhoto>();

    public virtual ICollection<WallPost> WallPosts { get; set; } = new List<WallPost>();
}
