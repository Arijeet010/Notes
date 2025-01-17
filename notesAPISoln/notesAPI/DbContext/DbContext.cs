using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using notesAPI.Models.Entities;

namespace notesAPI.DbContext
{
    public class NotesDbContext:Microsoft.EntityFrameworkCore.DbContext, INotesDbContext
    {

        public NotesDbContext(DbContextOptions<NotesDbContext> options):base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public IDbContextTransaction BeginTransaction()
        {
            return this.Database.BeginTransaction();
        }
    }
}
