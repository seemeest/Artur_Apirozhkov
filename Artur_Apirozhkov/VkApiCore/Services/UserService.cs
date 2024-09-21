using Artur_Apirozhkov.VkApiCore.Models;
using VkNet;
using VkNet.Model;

namespace Artur_Apirozhkov.VkApiCore.Services
{
    public class UserService
    {
        private readonly VkClient _vkClient;

        public UserService(VkApi api)
        {
            _vkClient = new VkClient(api);
        }
        public async Task<UserData> GetUserAsync(long vkId)
        {
            var user = _vkClient.GetUserProfile(vkId);
            if (user.IsClosed is true)
            {
                throw new Exception("Профиль пользователя скрыт");
            }

            var friends =  _vkClient.GetUserFriends(vkId);
            var posts = _vkClient.GetUserPosts(vkId);
            var groups = _vkClient.GetUserGroups(vkId);
            var photos = _vkClient.GetUserPhotos(vkId);

            // Преобразование данных пользователя в DTO
            var userDto = ConvertToUserModel(user, posts, groups, friends, photos);
            return userDto;
        }
        private UserData ConvertToUserModel(User user, List<WallPostDTO> posts, List<GroupDTO> groups, List<FriendDTO> friends, List<UserPhotoDTO> photos)
        {
            var postLikesCount = posts.Sum(p => p.CountLikes);
            var averageLikes = postLikesCount / (posts.Count == 0 ? 1 : posts.Count);

            return new UserData
            {
                FilterUserModel = new UserModel
                {
                    postsCount = posts.Count,
                    CountLikes = postLikesCount,
                    AverageLikes = averageLikes,
                    AvatarPhotoUrl = _vkClient.GetAvatarPhotoUrl(user.PhotoId, user.Id),
                    Quotes = user.Quotes,
                    Status = user.Status,
                    Vkid = user.Id,
                    About = user.About,
                    Activities = user.Activities,
                    Interest = user.Interests,
                    friendsCount = user.FriendLists.Count,
                    Grouplist = groups,
                    FriendsOfTheUser = friends,
                    UserPhoto = photos,
                    UserPosts = posts
                },
                UserPhoto = photos
            };
        }
    }
}

