namespace Artur_Apirozhkov.VkApiCore.Models
{
    public class FriendDTO
    {
        public long vkUserid { get; set; } //- Внутренний id вк Владельца страницы (используется только для проверки точности результата участника)
        public long vkFriendId { get; set; }
        public string City { get; set; }
        public EducationDTO education { get; set; }
        public string Job { get; set; }
    }
}
