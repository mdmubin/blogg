using Api.Models.Dto.Requests;
using Api.Models.Dto.Responses;
using Api.Models.Entities;
using AutoMapper;

namespace Api.Services;

public class DataMapper : Profile
{
    public DataMapper()
    {
        ConfigureBlogMappings();
        ConfigureCommentMappings();
        ConfigureReplyMappings();
        ConfigureTagMappings();
    }

    public void ConfigureBlogMappings()
    {
        CreateMap<BlogCreateRequest, Blog>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.PostedDateTime, opt => opt.MapFrom(_ => DateTime.Now));

        CreateMap<BlogUpdateRequest, Blog>();

        CreateMap<Blog, BlogResponse>();
    }

    public void ConfigureCommentMappings()
    {
        CreateMap<CommentCreateRequest, Comment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.PostedDateTime, opt => opt.MapFrom(_ => DateTime.Now));

        CreateMap<CommentUpdateRequest, Comment>();

        CreateMap<Comment, CommentResponse>();
    }

    public void ConfigureReplyMappings()
    {
        CreateMap<ReplyCreateRequest, Reply>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

        CreateMap<ReplyUpdateRequest, Reply>();

        CreateMap<Reply, ReplyResponse>();
    }

    public void ConfigureTagMappings()
    {
        CreateMap<TagCreateRequest, Tag>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.TagName.Normalize()));

        CreateMap<TagUpdateRequest, Tag>()
            .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.TagName.Normalize()));

        CreateMap<Tag, TagResponse>();
    }
}