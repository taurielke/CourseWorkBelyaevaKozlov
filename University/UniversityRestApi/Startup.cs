using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UniversityDataBaseImplement.Implements;
using UniversityBusinessLogic.OfficePackage;
using UniversityBusinessLogic.OfficePackage.Implements;

namespace UniversityRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services
        //to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStudentStorage, StudentStorage>();
            services.AddTransient<IAttestationStorage, AttestationStorage>();
            services.AddTransient<IDeaneryStorage, DeaneryStorage>();
            services.AddTransient<ILearningPlanStorage, LearningPlanStorage>();
            services.AddTransient<IDisciplineStorage, DisciplineStorage>();
            services.AddTransient<ITeacherStorage, TeacherStorage>();
            services.AddTransient<IStudentLogic, StudentLogic>();
            services.AddTransient<IAttestationLogic, AttestationLogic>();
            services.AddTransient<IDeaneryLogic, DeaneryLogic>();
            services.AddTransient<ILearningPlanLogic, LearningPlanLogic>();
            services.AddTransient<IDisciplineLogic, DisciplineLogic>();
            services.AddTransient<ITeacherLogic, TeacherLogic>();
            services.AddTransient<IReportLogic, ReportLogic>();
            services.AddTransient<AbstractSaveToWord, SaveToWord>();
            services.AddTransient<AbstractSaveToExcel, SaveToExcel>();
            services.AddTransient<AbstractSaveToPdf, SaveToPdf>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "University" +
                    "RestApi",
                    Version = "v1"
                });
            });
        }
        // This method gets called by the runtime. Use this method to configure the
        //HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniversityRestApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}