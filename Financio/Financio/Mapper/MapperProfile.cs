using AutoMapper;

namespace Financio
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ArticleInputDTO, Article>().ReverseMap();
            CreateMap<Article, ArticleOutputDTO>().ReverseMap();
        }
    }
}
