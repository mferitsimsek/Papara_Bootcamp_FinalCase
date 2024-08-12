using Hangfire;
using Papara.CaptainStore.API.Middleware;
using Papara.CaptainStore.Application;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Services.Message;
using Papara.CaptainStore.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
//builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<MessageServiceHostedService>();

var app = builder.Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();
app.UseMiddleware<ErrorHandlerMiddleware>();
Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
{
    Log.Information("-------------Request-Begin------------");
    Log.Information(requestProfilerModel.Request);
    Log.Information(Environment.NewLine);
    Log.Information(requestProfilerModel.Response);
    Log.Information("-------------Request-End------------");
};
app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);

app.UseHttpsRedirection(); // HTTPS yönlendirmesi ekleyin
app.UseRouting(); // Rotalama middleware'ini ekleyin
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
