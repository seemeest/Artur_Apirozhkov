using System;
using System.Collections.Generic;

namespace Artur_Apirozhkov.BdModels;

public partial class Education
{
    public int Id { get; set; }

    public string? UniversityName { get; set; }

    public string? FacultyName { get; set; }

    public string? EducationForm { get; set; }

    public string? EducationStatus { get; set; }

    public int? UserMidelId { get; set; }

    public int? FriendId { get; set; }

    public virtual Friend? Friend { get; set; }

    public virtual UserModel? UserMidel { get; set; }
}
