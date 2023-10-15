namespace Api.Models.Dto.Requests;

public class TagCreateRequest
{
    public string TagName { get; set; } = null!;
}

public class TagUpdateRequest
{
    public string TagName { get; set; } = null!;
}