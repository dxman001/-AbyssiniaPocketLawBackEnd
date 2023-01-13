using AbyssiniaPocketLaw.API.CacheService;
using AbyssiniaPocketLaw.API.Middlewares;
using AbyssiniaPocketLaw.API.Persistance;
using AbyssiniaPocketLaw.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ISearchService,SearchService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPocketLawDbContext(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, CacheService>();
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

app.UseMiddleware<ExceptionHandler>();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
