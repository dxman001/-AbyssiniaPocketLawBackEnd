using AbyssiniaPocketLaw.API.Persistance;
using AbyssiniaPocketLaw.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<ISearchService,SearchService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PocketLawDbContext>(
    opt =>
         //opt.UseMySql(builder.Configuration.GetConnectionString("PocketLawDB"),
         //ServerVersion.Parse("8.0.30-mysql")
         opt.UseMySql(builder.Configuration.GetConnectionString("PocketLawDB"),
        ServerVersion.Parse("5.7.40-mysql")
    ));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
            .WithOrigins("http://localhost:3000", "https://localhost:3000", "http://localhost:4200", "http://pocketlaw.abyssinialaw.com/", "https://pocketlaw.abyssinialaw.com/", "https://abyssinialaw.com/")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
