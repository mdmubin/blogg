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
        ConfigureUserMappings();
    }

    public void ConfigureBlogMappings()
    {
        CreateMap<BlogCreateRequest, Blog>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.PostedDateTime, opt => opt.MapFrom(_ => DateTime.Now));

        CreateMap<BlogUpdateRequest, Blog>()
            // from https://github.com/AutoMapper/AutoMapper/issues/2335
            .ForAllMembers(opt => opt.Condition((src, dest, srcVal, destVal) => srcVal != null));

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

    public void ConfigureUserMappings()
    {
        CreateMap<UserRegistrationRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.Normalize()))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.Normalize()));

        CreateMap<UserUpdateRequest, User>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcVal, destVal) => srcVal != null));

        CreateMap<User, UserResponse>();
    }
}