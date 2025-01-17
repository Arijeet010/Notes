using notesAPI.Services;
using notesAPI.Services.Abstractions;

namespace notesAPI.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddNotesServices(this IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<ITagService, TagService>();
            return services;
        }
    }
}
