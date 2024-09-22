using Artur_Apirozhkov.BdModels;
using Artur_Apirozhkov.Mapper;
using Artur_Apirozhkov.VkApiCore.Models;
using AutoMapper;
using AutoMapper.Execution;
using System.Globalization;
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
           var config = new MapperConfiguration(cfg => cfg.AddProfile<AppMappingProfile>());
           var _mapper= config.CreateMapper();

        }

        public static int? CalculateAge(string dateOfBirthString)
        {
            if(string.IsNullOrWhiteSpace(dateOfBirthString)) return null;
            // Проверяем, содержит ли строка год рождения
            string[] dateParts = dateOfBirthString.Split('.');

            if (dateParts.Length < 3)
            {
                // Год отсутствует, возраст невозможно вычислить
                return null;
            }

            // Преобразуем строку в дату
            DateTime dateOfBirth;
            bool parsed = DateTime.TryParseExact(dateOfBirthString, "d.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth);

            if (!parsed)
            {
                // Если дата не была корректно распознана
                return null;
            }

            // Вычисляем возраст
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;

            // Если день рождения ещё не наступил в этом году, уменьшаем возраст на 1
            if (dateOfBirth > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
        public async Task<UserData> GetUserAsync(long vkId)
        {
            var user = _vkClient.GetUserProfile(vkId);
            if (user.IsClosed is true)
            {
                return null;
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
        
            return userDto;
        }

        private UserData ConvertToUserModel(User user, List<WallPostDTO> posts, List<GroupDTO> groups, List<FriendDTO> friends, List<UserPhotoDTO> photos)
        {
            var postLikesCount = posts.Sum(p => p.CountLikes);
            var averageLikes = postLikesCount / (posts.Count == 0 ? 1 : posts.Count);
            var postsList = new List<BdModels.WallPost>();

            for (int i = 0; i < photos.Count; i++)
            {
                postsList.Add(new BdModels.WallPost(posts[i]));
            }
            using (VkContext vkParserContext = new VkContext())
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<AppMappingProfile>());
                var _mapper = config.CreateMapper();

                vkParserContext.UserModels.Add(new BdModels.UserModel
                {
                    AvatarPhotoUrl = _vkClient.GetAvatarPhotoUrl(user.PhotoId, user.Id),
                    PostsCount = photos.Count,
                    CountLikes = postLikesCount,
                    AverageLikes = averageLikes,
                    Quotes = user.Quotes,
                    Status = user.Status,
                    Vkid = user.Id,
                    About = user.About,
                    Activities = user.Activities,
                    Interest = user.Interests,
                    FriendsCount = friends.Count,
                    Educations = _mapper.Map<ICollection<Artur_Apirozhkov.BdModels.Education>>(user.Education),
                    Groups = _mapper.Map<ICollection<Artur_Apirozhkov.BdModels.Group>>(groups),
                    Friends = _mapper.Map<ICollection<Friend>>(friends),
                    UserPhotos = _mapper.Map<ICollection<UserPhoto>>(photos),
                    WallPosts = postsList,
                    DataOfBirth=user.BirthDate,
                    Age= CalculateAge(user.BirthDate)

                });
                vkParserContext.SaveChanges();

            }
            return new UserData
            {
                FilterUserModel = new Models.UserModel
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

