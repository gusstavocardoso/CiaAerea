using CiaAerea.Contexts;
using CiaAerea.Middlewares;
using CiaAerea.Services;
using CiaAerea.Validators.Aeronave;
using CiaAerea.Validators.Piloto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CiaAereaContext>();
builder.Services.AddTransient<AeronaveService>();
builder.Services.AddTransient<PilotoService>();
builder.Services.AddTransient<AdicionarAeornaveValidator>();
builder.Services.AddTransient<AtualizarAeronaveValidator>();
builder.Services.AddTransient<ExcluirAeronaveValidator>();
builder.Services.AddTransient<AdicionarPilotoValidator>();
builder.Services.AddTransient<AtualizarPilotoValidator>();
builder.Services.AddTransient<ExcluirPilotoValidator>();
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

app.UseMiddleware<ValidationExceptionHandlerMiddleware>();

app.Run();