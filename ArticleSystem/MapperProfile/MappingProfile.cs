using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using AutoMapper;

namespace ArticleSystem.MapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDto, User>();

            CreateMap<ArticleAddDto, Article>();

            CreateMap<Comment, ArticleCommentDto>()
                .ForMember(dto => dto.AuthorLogin, src => src.MapFrom(com => com.Author.Login ?? "Deleted User"));

            CreateMap<Article, ArticleGetDto>()
                .ForMember(dto => dto.AuthorLogin, src => src.MapFrom(art => art.Author.Login))
                .ForMember(dto => dto.Tags, src => src.MapFrom(art => art.Tags.Select(y => new Tag() { Id = y.Id, Value = y.Value, ArticleId = y.ArticleId} ).ToList()));

            CreateMap<Article, ArticleGetWithCommentsDto>()
                .ForMember(dto => dto.AuthorLogin, src => src.MapFrom(art => art.Author.Login))
                .ForMember(dto => dto.Tags, src => src.MapFrom(art => art.Tags.Select(y => new Tag() { Id = y.Id, Value = y.Value, ArticleId = y.ArticleId }).ToList()))
                .ForMember(dto => dto.Comments, src => src.MapFrom(art => art.Comments));

            CreateMap<Article, ArticleModifyDto>();

            CreateMap<ArticleModifyDto, Article>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<TagAddDto, Tag>();

            CreateMap<string, Tag>()
                .ForMember(tag => tag.Value, src => src.MapFrom(str => str));

            CreateMap<TagListAddDto, List<Tag>>()
                .AfterMap((src, dest) =>
                    dest.AddRange(src.Value.Select(x => new Tag() { Value = x }))
                );

            CreateMap<CommentAddDto, Comment>();

            
        }
    }
}
