using PavelNikolaevKt_31_23.Interfaces;
using PavelNikolaevKt_31_23.Services;

namespace PavelNikolaevKt_31_23.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<IGradeService, GradeService>();
            return services;
        }
    }
}