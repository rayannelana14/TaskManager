using TaskManagerApi.Repository;
using TaskManagerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Task Manager",
        Description = "Api to manager tasks"
    });
});
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 5005;
});

builder.Services.AddScoped<ITaskManagerService, TaskManagerService>();
builder.Services.AddScoped<ITaskManagerRepository, TaskManagerRepository>();

builder.WebHost.UseUrls("https://localhost:5005");

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Manager API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
