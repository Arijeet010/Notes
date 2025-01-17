
namespace notesAPI.Models.DTOs.Notes
{
    public class UpdateNoteDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? TagNames { get; set; }
        public List<Guid>? TagIds { get; set; }
    }
}
