using System.Collections;

namespace Notes.Core.Entities;

public interface ITaggable
{
    public ICollection<Tag>? Tags { get; set; }
}