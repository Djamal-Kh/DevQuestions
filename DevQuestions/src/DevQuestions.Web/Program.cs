using DevQuestions.Web;
using DevQuestions.Web.Middlewares;
using Framework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies();

builder.Services.AddEndpoints(typeof(DependencyInjection).Assembly);

var app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapEndpoints();

app.Run();