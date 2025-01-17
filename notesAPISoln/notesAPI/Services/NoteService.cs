using Microsoft.EntityFrameworkCore.Storage;
using notesAPI.DbContext;
using notesAPI.Models.Entities;
using notesAPI.Services.Abstractions;

namespace notesAPI.Services
{
    public class NoteService:INoteService
    {
        private readonly INotesDbContext _dbContext;
        public NoteService(INotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Note CreateNote(Note note)
        {
            ValidateNoteData(note);
            using IDbContextTransaction transaction= _dbContext.BeginTransaction();
            try
            {
                Note newNote = new Note
                {
                    Title = note.Title,
                    Description = note.Description,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Tags=note.Tags
                };
                _dbContext.Notes.Add(newNote);
                _dbContext.SaveChanges();
                transaction.Commit();
                return newNote;
            }catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
        public Note UpdateNote(Guid noteId,Note note)
        {
            ValidateNoteData (note);
            Note existingNote=GetNoteById(noteId);
            var properties = typeof(Note).GetProperties().Where(p => p.CanWrite && p.Name != "Id").ToList();
            properties.ForEach(property =>
            {
                var newValue = property.GetValue(note);
                if (newValue != null)
                {
                    property.SetValue(existingNote, newValue);
                }
            });
            existingNote.UpdatedAt = DateTime.Now;
            _dbContext.SaveChanges();
            return existingNote;
        }
        public bool DeleteNote(Guid noteId)
        {
            Note note= GetNoteById(noteId);
            _dbContext.Notes.Remove(note);
            _dbContext.SaveChanges();
            return true;
        }
        public Note GetNoteById(Guid id)
        {
            Note note = _dbContext.Notes.Where(n => n.Id == id).FirstOrDefault()??throw new Exception("Note not found");
            return note;
        }
        public List<Note> GetAllNotes()
        {
            return _dbContext.Notes.ToList();
        }
        private void ValidateNoteData(Note note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));
        }
    }
}
