namespace Artur_Apirozhkov.VkApiCore.Models
{
    public class UserModel
    {
        public int postsCount { get; set; }
        public int CountLikes { get; set; }
        public int AverageLikes { get; set; }
        public string AvatarPhotoUrl { get; set; }
        public string Quotes { get; set; }
        public string Status { get; set; }
        public long Vkid { get; set; }
        public string About { get; set; }
        public string Activities { get; set; }
        public string Interest { get; set; }
        public int friendsCount { get; set; }
        public EducationDTO Education { get; set; }
        public List<GroupDTO> Grouplist { get; set; }
        public List<FriendDTO> FriendsOfTheUser { get; set; }
        public List<UserPhotoDTO> UserPhoto { get; set; }
        public List<WallPostDTO> UserPosts { get; set; }
    }
}
