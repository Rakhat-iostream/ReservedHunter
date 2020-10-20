using IndustrialStudentPositionHunters.Models;
using IndustrialStudentPositionHunters.Repositories;
using IndustrialStudentPositionHunters.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StudentPositionHunters.Models;
using StudentPositionHunters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Repositories;

namespace IndustrialStudentPositionHunters
{
    public static class ServicesDependencies
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEntityRepository<Student>, StudentRepository>();
            services.AddScoped<IEntityRepository<News>, NewsRepository>();
            services.AddScoped<IEntityRepository<Advertisement>, AdvertisementsRepository>();
            services.AddScoped<ICompanyRepository, CompaniesRepository>();
            services.AddScoped<IEntityRepository<Employer>, EmployersRepository>();
            services.AddScoped<IEntityRepository<Article>, ArticlesRepository>();
            services.AddScoped<IEntityRepository<Company>, CompaniesRepository>();
            services.AddScoped<IEntityRepository<Position>, PositionsRepository>();
            services.AddScoped<IPositionsRepository, PositionsRepository>();
            services.AddScoped<IEntityRepository<Resume>, ResumesRepository>();
            services.AddScoped<IUserAuthRepository<Student>, StudentRepository>();
            services.AddScoped<IUserAuthRepository<Employer>, EmployersRepository>();
        }
    }
}
