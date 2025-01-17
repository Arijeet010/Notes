using notesAPI.Models.Entities;

namespace notesAPI.Services.Abstractions
{
    public interface INoteService
    {
        Note CreateNote(Note note);
        Note UpdateNote(Guid noteId,Note note);
        bool DeleteNote(Guid noteId);
        Note GetNoteById(Guid id);
        List<Note> GetAllNotes();
    }
}
