using System;
using System.Collections.Generic;

namespace Artur_Apirozhkov.BdModels;

public partial class Friend
{
    public int Id { get; set; }

    public long? VkUserid { get; set; }

    public long? VkFriendId { get; set; }

    public string? City { get; set; }

    public int? EducationId { get; set; }

    public string? Job { get; set; }

    public int UserMidelId { get; set; }

    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();

    public virtual UserModel UserMidel { get; set; } = null!;
}
