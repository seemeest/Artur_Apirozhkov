namespace Artur_Apirozhkov.VkApiCore.Models
{
    public class WallPostDTO
    {
        public long? VkUserid { get; set; }
        public long vkPostID { get; set; }

        public DateTime? Date { get; set; }
        public string Text { get; set; }
        public long author { get; set; }
        public int CountLikes { get; set; }
        public int CountReposts { get; set; }

        public bool friend { get; set; } //true\false от друга ли пост?
    }
}
