using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Core;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Data.Abstractions;
using WebAppMedicalAssistant_Data.Abstractions.Repositories;
using WebAppMedicalAssistant_Data.Repositories;
using WebAppMedicalAssistant_DataBase;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            //Connection Db
            var connectionString = builder.Configuration.GetConnectionString("DbMedicalAssistant");
            builder.Services.AddDbContext<MedicalAssistantContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));

            //Add Auto-mapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Add Dependencies
            builder.Services.AddScoped<IFluorographyService, FluorographyService>();

            builder.Services.AddScoped<IRepository<Analysis>, Repository<Analysis>>();
            builder.Services.AddScoped<IRepository<Appointment>, Repository<Appointment>>();
            builder.Services.AddScoped<IRepository<Disease>, Repository<Disease>>();
            builder.Services.AddScoped<IRepository<Doctor>, Repository<Doctor>>();
            builder.Services.AddScoped<IRepository<DoctorVisit>, Repository<DoctorVisit>>();
            builder.Services.AddScoped<IRepository<Fluorography>, Repository<Fluorography>>();
            builder.Services.AddScoped<IRepository<MedicalExamination>, Repository<MedicalExamination>>();
            builder.Services.AddScoped<IRepository<MedicalInstitution>, Repository<MedicalInstitution>>();
            builder.Services.AddScoped<IRepository<Medicine>, Repository<Medicine>>();
            builder.Services.AddScoped<IRepository<PhysicalTherapy>, Repository<PhysicalTherapy>>();
            builder.Services.AddScoped<IRepository<PrescribedMedication>, Repository<PrescribedMedication>>();
            builder.Services.AddScoped<IRepository<TransferredDisease>, Repository<TransferredDisease>>();
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();
            builder.Services.AddScoped<IRepository<Vaccination>, Repository<Vaccination>>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}