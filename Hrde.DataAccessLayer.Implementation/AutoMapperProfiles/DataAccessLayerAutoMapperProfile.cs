using AutoMapper;
using DalModels = Hrde.DataAccessLayer.Implementation.Models;
using InterfaceModels = Hrde.DataAccessLayer.Interfaces.Models;

namespace Hrde.DataAccessLayer.Implementation.AutoMapperProfiles
{
    public class DataAccessLayerAutoMapperProfile : Profile
    {
        public DataAccessLayerAutoMapperProfile()
        {
            CreateMap<DalModels.Account, InterfaceModels.Account>().ReverseMap();
        }
    }
}
