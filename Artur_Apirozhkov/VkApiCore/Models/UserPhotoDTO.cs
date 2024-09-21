namespace Artur_Apirozhkov.VkApiCore.Models
{
    public class UserPhotoDTO
    {
        public long vkUserid { get; set; }
        public long? vkPhotoId { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
        public int LikeCount { get; set; }
        public int RepostCount { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
