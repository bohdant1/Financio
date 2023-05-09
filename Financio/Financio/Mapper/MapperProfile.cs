using AutoMapper;

namespace Financio
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ArticleInputDTO, Article>().ReverseMap();
            CreateMap<Article, ArticleOutputDTO>().ReverseMap();
            CreateMap<Collection, CollectionOutputDTO>().ReverseMap();
            CreateMap<CollectionInputDTO, Collection>().ReverseMap();
            CreateMap<UserInputDTO, User>().ReverseMap();
            CreateMap<User, UserOutputDTO>().ReverseMap();
        }
    }
}
