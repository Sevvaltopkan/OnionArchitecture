using OnionVb02.Application.DependencyResolvers;
using OnionVb02.InnerInfrastructure.DependencyResolvers;
using OnionVb02.Persistence.DependencyResolvers;
using OnionVb02.WebApi.DependencyResolvers;
using OnionVb02.ValidatorStructor.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Policy - Angular Frontend bağlantısı için
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContextService();
builder.Services.AddDtoMapperService();
builder.Services.AddManagerService();
builder.Services.AddRepositoryService();
builder.Services.AddVmMapperService();
builder.Services.AddHandlerService();
builder.Services.AddValidatorService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS Middleware
app.UseCors("AllowAngular");

app.UseAuthorization();
app.MapControllers();
app.Run();
