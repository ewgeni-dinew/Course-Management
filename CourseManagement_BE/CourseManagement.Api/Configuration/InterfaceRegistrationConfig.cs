namespace CourseManagement.Api.Configuration
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using CourseManagement.Data.Factories.Contracts.Assembly;
    using CourseManagement.Data.Models;
    using CourseManagement.Repository;
    using CourseManagement.Repository.Contracts;
    using CourseManagement.Services.Contracts.Assembly;
    using DinkToPdf;
    using DinkToPdf.Contracts;

    public static class InterfaceRegistrationConfig
    {
        public static void RegisterInterfaces(this IServiceCollection services)
        {
            //add automated service provider mappings by following naming conventions>>>
            var assemblies = new List<Assembly>()
            {
               Assembly.GetAssembly(typeof(IServiceAssembly)),
               Assembly.GetAssembly(typeof(IFactoryAssembly)),
            };

            foreach (var assembly in assemblies)
            {
                assembly.GetTypes()
                    .Where(x => x.IsClass && x.GetInterfaces().Any(i => i.Name == $"I{x.Name}"))
                    .Select(x => new ProviderKVP
                    {
                        Implementation = x,
                        Interface = x.GetInterface($"I{x.Name}")
                    })
                    .ToList()
                    .ForEach(x => services.AddScoped(x.Interface, x.Implementation));
            }
            //<<<

            //add custom service provider mappings >>>
            services.AddScoped<IRepository<ApplicationUser>, UserRepository>();
            services.AddScoped<IRepository<Course>, CourseRepository>();
            services.AddScoped<IRepository<FavoriteCourse>, FavoriteCourseRepository>();

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }
    }

    class ProviderKVP
    {
        public Type Implementation { get; set; }

        public Type Interface { get; set; }
    }
}
