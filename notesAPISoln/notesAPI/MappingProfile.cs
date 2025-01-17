using AutoMapper;
using notesAPI.Models.DTOs.Notes;
using notesAPI.Models.Entities;

namespace notesAPI
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<AddNoteDTO, Note>();
            CreateMap<UpdateNoteDTO, Note>();
        }
    }
}
