using System;
using System.Collections.Generic;

namespace Artur_Apirozhkov.BdModels;

public partial class Group
{
    public int Id { get; set; }

    public long? VkUserid { get; set; }

    public long? VkGroupId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? UserMidelId { get; set; }

    public virtual UserModel? UserMidel { get; set; }
}
