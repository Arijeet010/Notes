using notesAPI.Models.Entities;

namespace notesAPI.Services.Abstractions
{
    public interface ITagService
    {
        Tag CreateTag(string tagName);
        Tag UpdateTag(Guid tagId, string tagName);
        bool DeleteTag(Guid tagId);
        Tag GetTagById(Guid id);
        Tag GetTagByName(string tagName);
        List<Tag> GetFilteredTags(List<Guid>? tagIds);
        List<Tag> GetFilteredTags(List<string>? tagNames);
        List<Tag> GetAllTags();
    }
}
