var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();


app.UseSwaggerUI(opciones =>
{
    opciones.SwaggerEndpoint("/swagger/estudiantes/v1/swagger.json", "Estudiantes API");
    opciones.SwaggerEndpoint("/swagger/cursos/v1/swagger.json", "Cursos API");
});

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapReverseProxy();

app.Run();