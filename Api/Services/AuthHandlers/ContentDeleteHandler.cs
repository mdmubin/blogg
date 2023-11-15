using System.Security.Claims;
using Api.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Api.Services.AuthHandlers;

public class ContentDeleteHandler
    : AuthorizationHandler<AuthRequirements.AuthorAndModerator, ContentBase>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AuthRequirements.AuthorAndModerator requirement,
        ContentBase resource
    )
    {
        var isAuthor = context.User
            .HasClaim(ClaimTypes.NameIdentifier, resource.AuthorId.ToString());

        var isModerator = context.User.IsInRole("moderator");

        if (isAuthor || isModerator)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}