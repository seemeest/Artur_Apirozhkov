using Artur_Apirozhkov.VkApiCore.Models;
using System;
using System.Collections.Generic;

namespace Artur_Apirozhkov.BdModels;

public partial class WallPost
{
    public int Id { get; set; }

    public long? VkUserid { get; set; }

    public long? VkPostId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Text { get; set; }

    public long? Author { get; set; }

    public int? CountLikes { get; set; }

    public int? CountReposts { get; set; }

    public bool? Friend { get; set; }

    public int? UserMidelId { get; set; }

    public virtual UserModel? UserMidel { get; set; }
    public WallPost(WallPostDTO dto)
    {
        VkUserid = dto.VkUserid;
        VkPostId = dto.vkPostID;

        Date = dto.Date.HasValue ? DateOnly.FromDateTime(dto.Date.Value) : null;

        Text = dto.Text;
        Author = dto.author;
        CountLikes = dto.CountLikes;
        CountReposts = dto.CountReposts;
        Friend = dto.friend;
    }
    public WallPost() { }
}
