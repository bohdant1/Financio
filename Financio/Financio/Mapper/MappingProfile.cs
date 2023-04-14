using AutoMapper;
using MongoDB.Bson;

namespace Financio.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BsonDocument, Article>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src["_id"].ToString()))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src["Title"].AsString))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src["Date"].ToUniversalTime()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src["Text"].AsString))
                .ForMember(dest => dest.CollectionIds, opt => opt.MapFrom(src => src["CollectionIds"].AsBsonArray.Select(x => x.ToString())))
                .ForMember(dest => dest.Collections, opt => opt.Ignore()); // Ignore this property since it's not being mapped from BsonDocument
        }
    }
}
