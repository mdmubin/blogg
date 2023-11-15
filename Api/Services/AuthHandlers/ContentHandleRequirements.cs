using Microsoft.AspNetCore.Authorization;

namespace Api.Services.AuthHandlers;


public static class AuthRequirements
{
    public class Author : IAuthorizationRequirement
    {
    }

    public class AuthorAndModerator : IAuthorizationRequirement
    {
    }
}