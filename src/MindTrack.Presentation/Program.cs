using MindTrack.Infrastructure;
using MindTrack.Application.Mappings;
using MindTrack.Presentation.Middlewares;
using AutoMapper; 
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructure(conn);

// 🔄 AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var erros = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new
            {
                Campo = e.Key,
                Mensagens = e.Value.Errors.Select(x => x.ErrorMessage)
            });

        var resposta = new
        {
            status = 400,
            erro = "BadRequest",
            mensagem = "Um ou mais campos estão inválidos.",
            detalhes = erros
        };

        return new BadRequestObjectResult(resposta);
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
