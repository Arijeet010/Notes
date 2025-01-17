using Microsoft.EntityFrameworkCore.Storage;
using notesAPI.DbContext;
using notesAPI.Models.Entities;
using notesAPI.Services.Abstractions;

namespace notesAPI.Services
{
    public class TagService:ITagService
    {
        private readonly INotesDbContext _context;
        public TagService(INotesDbContext context)
        {
            _context = context;
        }
        public Tag CreateTag(string tagName)
        {
            ValidateTag(tagName);
            using IDbContextTransaction transaction = _context.BeginTransaction();
            try
            {
                var newTag = new Tag()
                {
                    Name = tagName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt=DateTime.Now
                };
                _context.Tags.Add(newTag);
                _context.SaveChanges();
                transaction.Commit();
                return newTag;
            }catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
        public Tag UpdateTag(Guid tagId,string tagName)
        {
            ValidateTag(tagName);
            Tag existingTag=GetTagById(tagId);
            existingTag.Name = tagName;
            existingTag.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return existingTag;
        }
        public bool DeleteTag(Guid tagId)
        {
            Tag tag = GetTagById(tagId);
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return true;
        }
        public Tag GetTagById(Guid id)
        {
            return _context.Tags.Where(t => t.Id == id).FirstOrDefault()??throw new Exception("Tag not found");
        }
        public Tag GetTagByName(string tagName)
        {
            return _context.Tags.Where(t => t.Name == tagName).FirstOrDefault() ?? throw new Exception("Tag not found");
        }
        public List<Tag> GetFilteredTags(List<Guid>? tagIds)
        {
            List<Tag> tags = new List<Tag>();
            if (tagIds != null && tagIds.Any())
            {
                tags = _context.Tags.Where(t => tagIds.Contains(t.Id)).ToList();
            }
            return tags;
        }
        public List<Tag> GetFilteredTags(List<string>? tagNames)
        {
            List<Tag> tags = new List<Tag>();
            if (tagNames != null && tagNames.Any())
            {
                tags = _context.Tags.Where(t => tagNames.Contains(t.Name)).ToList();
            }
            return tags;
        }
        public List<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }
        private void ValidateTag(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName)) throw new ArgumentNullException(nameof(tagName));
            bool tagExists = _context.Tags.Any(t => t.Name == tagName);
            if (tagExists) throw new InvalidOperationException("Tag already exists");
        }
    }
}
