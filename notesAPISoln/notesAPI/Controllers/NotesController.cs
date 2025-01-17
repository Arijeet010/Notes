using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using notesAPI.Models.DTOs.Notes;
using notesAPI.Models.Entities;
using notesAPI.Services.Abstractions;

namespace notesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INoteService _noteService;
        private readonly ITagService _tagService;
        public NotesController(INoteService noteService,IMapper mapper, ITagService tagService)
        {
            _noteService = noteService;
            _mapper = mapper;
            _tagService = tagService;
        }
        [HttpGet]
        public ActionResult<List<Note>> GetAllNotes()
        {
            List<Note> notes = _noteService.GetAllNotes();
            return Ok(notes);
        }
        [HttpGet("{id}")]
        public ActionResult<Note> GetNote(Guid id)
        {
            Note note=_noteService.GetNoteById(id);
            return Ok(note);
        }
        [HttpPost]
        public ActionResult AddNote(AddNoteDTO note)
        {
            List<Tag> tags = new List<Tag>();
            if (note.TagNames != null && note.TagNames.Any())
            {
                tags = _tagService.GetFilteredTags(note.TagNames);
                List<string> missingTagNames = note.TagNames.Where(tagName => !tags.Any(tag => tag.Name == tagName)).ToList();
                if (missingTagNames.Any())
                {
                    missingTagNames.ForEach(tagName => _tagService.CreateTag(tagName));
                    tags.AddRange(_tagService.GetFilteredTags(missingTagNames));
                }
            }else if (note.TagIds != null && note.TagIds.Any())
            {
                tags = _tagService.GetFilteredTags(note.TagIds);
            }
            Note newNote = new Note()
            {
                Title = note.Title,
                Description = note.Description,
                Tags = tags
            };
            Note createdNote = _noteService.CreateNote(newNote);
            return Ok(createdNote);
        }
        [HttpPatch("{id}")]
        public ActionResult UpdateNote(Guid id, UpdateNoteDTO note)
        {
            Note updatedNote=_noteService.UpdateNote(id, _mapper.Map<UpdateNoteDTO,Note>(note));
            return Ok(updatedNote);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteNote(Guid id)
        {
            bool isNoteDeleted=_noteService.DeleteNote(id);
            return NoContent();
        }
    }
}
