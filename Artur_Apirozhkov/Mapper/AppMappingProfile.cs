using AutoMapper;

namespace Artur_Apirozhkov.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Artur_Apirozhkov.VkApiCore.Models.EducationDTO, Artur_Apirozhkov.BdModels.Education>();
            CreateMap<Artur_Apirozhkov.VkApiCore.Models.FriendDTO, Artur_Apirozhkov.BdModels.Friend>();
            CreateMap<Artur_Apirozhkov.VkApiCore.Models.GroupDTO, Artur_Apirozhkov.BdModels.Group>();
            CreateMap<Artur_Apirozhkov.VkApiCore.Models.UserModel, Artur_Apirozhkov.BdModels.UserModel>();
            CreateMap<Artur_Apirozhkov.VkApiCore.Models.UserPhotoDTO, Artur_Apirozhkov.BdModels.UserPhoto>();
        }
    }
}
