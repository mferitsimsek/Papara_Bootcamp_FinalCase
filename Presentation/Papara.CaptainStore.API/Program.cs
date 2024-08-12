using Papara.CaptainStore.Application;
using Papara.CaptainStore.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection(); // HTTPS yönlendirmesi ekleyin
app.UseRouting(); // Rotalama middleware'ini ekleyin
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
