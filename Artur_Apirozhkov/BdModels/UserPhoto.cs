using System;
using System.Collections.Generic;

namespace Artur_Apirozhkov.BdModels;

public partial class UserPhoto
{
    public int Id { get; set; }

    public long? VkUserid { get; set; }

    public long? VkPhotoId { get; set; }

    public string? Url { get; set; }

    public string? Text { get; set; }

    public int? LikeCount { get; set; }

    public int? RepostCount { get; set; }

    public DateTime? CreateTime { get; set; }

    public int? UserMidelId { get; set; }

    public virtual UserModel? UserMidel { get; set; }
}
