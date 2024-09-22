using Artur_Apirozhkov.BdModels;
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

            // Запускаем задачи параллельно
            var friendsTask = _vkClient.GetUserFriends(vkId);
            var postsTask = _vkClient.GetUserPosts(vkId);
            var groupsTask = _vkClient.GetUserGroups(vkId);
            var photosTask = _vkClient.GetUserPhotos(vkId);
         

            // Ожидаем завершения всех задач
            await Task.WhenAll(friendsTask, postsTask, groupsTask, photosTask);

            // Получаем результаты
            var friends = await friendsTask;
            var posts = await postsTask;
            var groups = await groupsTask;
            var photos = await photosTask;

            var userDto = ConvertToUserModel(user, posts, groups, friends, photos);
            //using (VkParserContext vkParserContext = new VkParserContext())
            //{

            //    vkParserContext.UserModels.Add(new BdModels.UserModel
            //    {
            //        UserFriends= friends,
            //    }

            //}
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
                    UserPosts = posts,
                    Education=new EducationDTO(user.Education)
                    
                },
                UserPhoto = photos
            };
        }
    }
}

