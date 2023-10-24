using Api.Models.Entities;

namespace Api.Data;

public class RepositoryManager
{
    private readonly BloggContext context;


    private readonly Lazy<GenericDataRepository<Blog>> BlogsRepository;

    private readonly Lazy<GenericDataRepository<Comment>> CommentsRepository;

    private readonly Lazy<GenericDataRepository<Reply>> RepliesRepository;

    private readonly Lazy<GenericDataRepository<Tag>> TagsRepository;

    private readonly Lazy<GenericDataRepository<User>> UserRepository;

    private readonly Lazy<GenericDataRepository<UserRole>> UserRoleRepository;


    public RepositoryManager(BloggContext context)
    {
        this.context = context;

        BlogsRepository = new(() => new(this.context));
        CommentsRepository = new(() => new(this.context));
        RepliesRepository = new(() => new(this.context));
        TagsRepository = new(() => new(this.context));
        UserRepository = new(() => new(this.context));
        UserRoleRepository = new(() => new(this.context));
    }


    public GenericDataRepository<Blog> Blogs => BlogsRepository.Value;

    public GenericDataRepository<Comment> Comments => CommentsRepository.Value;

    public GenericDataRepository<Reply> Replies => RepliesRepository.Value;

    public GenericDataRepository<Tag> Tags => TagsRepository.Value;

    public GenericDataRepository<User> Users => UserRepository.Value;

    public GenericDataRepository<UserRole> UserRoles => UserRoleRepository.Value;


    public async Task SaveChanges()
    {
        await context.SaveChangesAsync();
    }
}