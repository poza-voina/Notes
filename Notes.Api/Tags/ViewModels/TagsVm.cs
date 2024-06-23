using Notes.Core.Entities;

namespace Notes.Api.Tags.ViewModels;

public class TagsVm
{
    public ICollection<Tag> Tags { get; init; }

    public TagsVm(ICollection<Tag> tags)
    {
        Tags = tags;
    }

    public TagsVm()
    {
        Tags = new List<Tag>();
    }
}