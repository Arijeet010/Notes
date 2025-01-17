using Microsoft.AspNetCore.Mvc;
using notesAPI.Models.Entities;
using notesAPI.Services.Abstractions;

namespace notesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController:ControllerBase
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet]
        public ActionResult<List<Tag>> GetAllTags()
        {
            List<Tag> tags= _tagService.GetAllTags();
            return Ok(tags);
        }
        [HttpGet("id")]
        public ActionResult<Tag> GetTag(Guid id)
        {
            Tag tag=_tagService.GetTagById(id);
            return Ok(tag);
        }
        [HttpPost]
        public ActionResult<Tag> AddTag([FromBody] string tagName)
        {
            Tag tag = _tagService.CreateTag(tagName);
            return Ok(tag);
        }
        [HttpPatch("id")]
        public ActionResult<Tag> ModifyTag(Guid id, [FromBody] string tagName)
        {
            Tag tag = _tagService.UpdateTag(id,tagName);
            return Ok(tag);
        }
        [HttpDelete("id")]
        public ActionResult RemoveTag(Guid id)
        {
            bool isTagRemoved = _tagService.DeleteTag(id);
            return NoContent();
        }
    }
}
