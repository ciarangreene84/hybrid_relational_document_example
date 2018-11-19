using DalModels = Hrde.DataAccessLayer.Interfaces.Models;
using RepoModels = Hrde.RepositoryLayer.Interfaces.Models;
using AutoMapper;


namespace Hrde.RepositoryLayer.Implementation.AutoMapperProfiles
{
    public class RepositoryLayerAutoMapperProfile : Profile
    {
        public RepositoryLayerAutoMapperProfile()
        {
            CreateMap<DalModels.Account, RepoModels.Account>().ReverseMap();
        }
    }
}
