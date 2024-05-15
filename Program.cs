using Microsoft.EntityFrameworkCore;
using WebApiMusicalLibrary;
using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Repository;
using WebApiMusicalLibrary.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(option => {
    option.UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<IGenreRepository,GenreRepository>();
builder.Services.AddScoped<ICountryRepository,CountryRepository>();
builder.Services.AddScoped<IBandSingerRepository,BandSingerRepository>();
builder.Services.AddScoped<IAlbunRepository,AlbunRepository>();
builder.Services.AddScoped<ISongsRepository,SongsRepository>();
builder.Services.AddScoped<IMenuOptionRepository,MenuOptionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();