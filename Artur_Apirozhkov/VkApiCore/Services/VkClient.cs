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
        public async Task<List<FriendDTO>> GetUserFriends(long vkId)
        {
            var friends = await _api.Friends.GetAsync(new FriendsGetParams
            {
                UserId = vkId,
                Order = VkNet.Enums.StringEnums.FriendsOrder.Hints,
                Fields = ProfileFields.All,
                Count = 100
            });

            var friendList = new List<FriendDTO>(friends.Count);
            for (int i = 0; i < friends.Count; i++)
            {
                var friend = friends[i];
                friendList.Add(new FriendDTO
                {
                    vkUserid = vkId,
                    vkFriendId = friend.Id,
                    City = friend.City?.Title ?? "Не указано",
                    Job = friend.Occupation?.Name ?? "Не указано"
                });
            }
            return friendList;
        }

        public async Task<List<WallPostDTO>> GetUserPosts(long vkId)
        {
            var posts = await _api.Wall.GetAsync(new WallGetParams
            {
                OwnerId = vkId,
                Count = 100,
                Filter = VkNet.Enums.StringEnums.WallFilter.Owner
            });

            var postList = new List<WallPostDTO>(posts.WallPosts.Count);
            for (int i = 0; i < posts.WallPosts.Count; i++)
            {
                var post = posts.WallPosts[i];
                postList.Add(new WallPostDTO
                {
                    VkUserid = post.FromId,
                    Date = post.Date,
                    Text = post.Text,
                    CountLikes = post.Likes.Count,
                    CountReposts = post.Reposts.Count
                });
            }
            return postList;
        }


        //хз как оптимизировать 1к мл жрёт
        public async Task<List<GroupDTO>> GetUserGroups(long vkId)
        {
            var groups = await _api.Groups.GetAsync(new GroupsGetParams
            {
                UserId = vkId,
                Extended = true,
                Fields = GroupsFields.Description,
                Count = 200
            });

            var groupList = new List<GroupDTO>(groups.Count);
            for (int i = 0; i < groups.Count; i++)
            {
                var group = groups[i];
                groupList.Add(new GroupDTO
                {
                    vkUserid = vkId,
                    vkGroupId = group.Id,
                    Name = group.Name,
                    Description = group.Description
                });
            }
            return groupList;
        }

        public async Task<List<UserPhotoDTO>> GetUserPhotos(long vkId)
        {
            var photos = await _api.Photo.GetAllAsync(new PhotoGetAllParams
            {
                OwnerId = vkId,
                Extended = true,
                Count = 20
            });

            var photoList = new List<UserPhotoDTO>(photos.Count);
            for (int i = 0; i < photos.Count; i++)
            {
                var photo = photos[i];
                photoList.Add(new UserPhotoDTO
                {
                    vkUserid = vkId,
                    vkPhotoId = photo.Id,
                    LikeCount = photo.Likes.Count,
                    RepostCount = photo.Reposts.Count,
                    Url = photo.Sizes.Last().Url.ToString(),
                    CreateTime = photo.CreateTime,
                    Text = photo.Text,
                });
            }
            return photoList;
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

            if (photos.Count > 0)
            {
                var photo = photos[0];
                return photo.Sizes[photo.Sizes.Count - 1].Url.ToString();
            }

            return string.Empty;
        }

    }
}
