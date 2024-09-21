using Artur_Apirozhkov.VkApiCore.Models;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;

namespace Artur_Apirozhkov.VkApiCore.Services
{
    public class VkClient
    {
        private readonly VkApi _api;

        public VkClient(VkApi api)
        {
            _api = api;
        }
        public User GetUserProfile(long vkId)
        {
            var res = _api.Users.Get([vkId],
        ProfileFields.FirstName |
        ProfileFields.LastName |
        ProfileFields.Sex |
        ProfileFields.FriendLists |
        ProfileFields.Education |
        ProfileFields.Universities |
        ProfileFields.Schools |
        ProfileFields.CanPost |
        ProfileFields.Status |
        ProfileFields.Relation |
        ProfileFields.Relatives |
        ProfileFields.Counters |
        ProfileFields.Nickname |
        ProfileFields.Timezone |
        ProfileFields.Language |
        ProfileFields.OnlineMobile |
        ProfileFields.OnlineApp |
        ProfileFields.RelationPartner |
        ProfileFields.Activities |
        ProfileFields.About |
        ProfileFields.Quotes |
        ProfileFields.Career |
        ProfileFields.FriendStatus |
        ProfileFields.IsFriend |
        ProfileFields.ScreenName |
        ProfileFields.IsHiddenFromFeed |
        ProfileFields.IsFavorite |
        ProfileFields.CanSendFriendRequest |
        ProfileFields.WallComments |
        ProfileFields.Verified |
        ProfileFields.FollowersCount |
        ProfileFields.Exports |
        ProfileFields.MaidenName |
        ProfileFields.PhotoId |
        ProfileFields.HomeTown |
        ProfileFields.Interests |
        ProfileFields.Occupation
    );
            return res.FirstOrDefault();
        }
        public  List<FriendDTO> GetUserFriends(long vkId)
        {
            var friends = _api.Friends.Get(new FriendsGetParams
            {
                UserId = vkId,
                Order = VkNet.Enums.StringEnums.FriendsOrder.Hints,
                Fields = ProfileFields.All,
                Count = 100
            });

            return friends.Select(f => new FriendDTO
            {
                vkUserid = vkId,
                vkFriendId = f.Id,
                City = f.City?.Title ?? "Не указано",
                Job = f.Occupation?.Name ?? "Не указано"
            }).ToList();
        }
        public List<WallPostDTO> GetUserPosts(long vkId)
        {
            var posts = _api.Wall.Get(new WallGetParams
            {
                OwnerId = vkId,
                Count = 100,
                Filter = VkNet.Enums.StringEnums.WallFilter.Owner
            });

            return posts.WallPosts.Select(p => new WallPostDTO
            {
                VkUserid = p.FromId,
                Date = p.Date,
                Text = p.Text,
                CountLikes = p.Likes.Count,
                CountReposts = p.Reposts.Count
            }).ToList();
        }
        public List<GroupDTO> GetUserGroups(long vkId)
        {
            var groups = _api.Groups.Get(new GroupsGetParams
            {
                UserId = vkId,
                Extended = true,
                Fields = GroupsFields.Description,
                Count = 200
            });

            return groups.Select(g => new GroupDTO
            {
                vkUserid = vkId,
                vkGroupId = g.Id,
                Name = g.Name,
                Description = g.Description
            }).ToList();
        }
        public List<UserPhotoDTO> GetUserPhotos(long vkId)
        {
            var photos = _api.Photo.GetAll(new PhotoGetAllParams
            {
                OwnerId = vkId,
                Extended = true,
                Count = 20
            });

            return photos.Select(p => new UserPhotoDTO
            {
                vkUserid = vkId,
                vkPhotoId = p.Id,
                LikeCount = p.Likes.Count,
                RepostCount = p.Reposts.Count,
                Url = p.Sizes.Last().Url.ToString(),
                CreateTime = p.CreateTime,
                Text = p.Text,
            }).ToList();
        }
        public string GetAvatarPhotoUrl(string photoId, long vkId)
        {
            if (string.IsNullOrEmpty(photoId)) return string.Empty;

            var photos = _api.Photo.Get(new PhotoGetParams
            {
                PhotoIds = new[] { photoId.Split("_")[1] },
                AlbumId = PhotoAlbumType.Profile,
                OwnerId = vkId,
                Extended = true
            });

            return photos.FirstOrDefault()?.Sizes.Last().Url.ToString();
        }
    }
}
