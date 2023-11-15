using System.Security.Claims;
using Api.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Api.Services.AuthHandlers;

public class ContentUpdateHandler
    : AuthorizationHandler<AuthRequirements.Author, ContentBase>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AuthRequirements.Author requirement,
        ContentBase resource
    )
    {
        var isAuthor = context.User
            .HasClaim(ClaimTypes.NameIdentifier, resource.AuthorId.ToString());

        if (isAuthor)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}