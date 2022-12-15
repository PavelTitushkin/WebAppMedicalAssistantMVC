using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Data.Abstractions.Repositories;
using WebAppMedicalAssistant_Data.Abstractions;
using WebAppMedicalAssistant_DataBase;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistant_Data.Repositories;
using System.Text.Json.Serialization;
using WebAppMedicalAssistant_Data.CQS.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAppMedicalAssistantAPI.Utils;
using WebAppMedicalAssistant_Bussines.EmailService;
using WebAppMedicalAssistant_Core.Abstractions.EmailService;
using Hangfire;
using Hangfire.SqlServer;

namespace WebAppMedicalAssistantAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add Serilog
            builder.Host.UseSerilog((ctx, lc) =>
                lc.WriteTo.File(@"D:\IT-Academy\Projects\WebAppMedicalAssistantMVC\dataApi.log",
                    LogEventLevel.Information).WriteTo.Console(LogEventLevel.Verbose));

            // Add services to the container.


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            //Add EmailConfig
            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);

            //Connection Db
            var connectionString = builder.Configuration.GetConnectionString("DbMedicalAssistant");
            builder.Services.AddDbContext<MedicalAssistantContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(builder.Configuration["DocXml"]);
            });

            //Add Authentication with Jwt
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        ValidAudience = builder.Configuration["Token:Issuer"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:JwtSecret"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            //Add Dependencies
            builder.Services.AddScoped<IFluorographyService, FluorographyService>();
            builder.Services.AddScoped<IAnalysisService, AnalysisService>();
            builder.Services.AddScoped<IVaccinationService, VaccinacionService>();
            builder.Services.AddScoped<IMedicalExaminationService, MedicalExaminationService>();
            builder.Services.AddScoped<ITransferredDiseaseService, TransferredDiseaseService>();
            builder.Services.AddScoped<IDoctorVisitService, DoctorVisitService>();
            builder.Services.AddScoped<IPrescribedMedicationService, PrescribedMedicationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IMedicalInstitutionService, MedicalInstitutionService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IPhysicalTherapyService, PhysicalTherapyService>();
            builder.Services.AddScoped<IMedicineService, MedicineService>();

            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IEmailService, EmailService>();

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
            builder.Services.AddScoped<IRepository<Role>, Repository<Role>>();
            builder.Services.AddScoped<IRepository<ScanOfAnalysisDocument>, Repository<ScanOfAnalysisDocument>>();
            builder.Services.AddScoped<IRepository<ScanOfMedicalExamination>, Repository<ScanOfMedicalExamination>>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IJwtUtil, JwtUtilSha256>();

            //Add Automapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Add MediatR
            builder.Services.AddMediatR(typeof(GetAllDoctorVisitQuery).Assembly);

            // Add Hangfire services.
            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
             builder.Services.AddHangfireServer();

            var app = builder.Build();
            app.UseStaticFiles();
            app.UseHangfireDashboard();
            
            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();
            app.MapHangfireDashboard();

            app.Run();
        }
    }
}