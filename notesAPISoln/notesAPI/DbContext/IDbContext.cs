using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using notesAPI.Models.Entities;

namespace notesAPI.DbContext
{
    public interface INotesDbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        IDbContextTransaction BeginTransaction();
        int SaveChanges();
    }
}
