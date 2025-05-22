using CapgeminiQ5.Application.Contracts;
using CapgeminiQ5.Application.Features.Movimentacao.Commands;
using CapgeminiQ5.Infrastructure.Repositories;
using MediatR;
namespace CapgeminiQ5.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddControllers().AddJsonOptions(options =>{
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });

            builder.Services.AddMediatR(typeof(MovimentarContaCommandHandler).Assembly);
            builder.Services.AddScoped<IContaRepository, ContaRepositoryDapper>();
            builder.Services.AddScoped<IMovimentoRepository, MovimentoRepositoryDapper>();


            builder.Services.AddControllers();

            builder.Services.AddMediatR(typeof(MovimentarContaCommandHandler).Assembly);
            builder.Services.AddScoped<IContaRepository, ContaRepositoryDapper>();
            builder.Services.AddScoped<IMovimentoRepository, MovimentoRepositoryDapper>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            DatabaseBootstrap.Initialize();
            Console.WriteLine(DatabaseBootstrap.GetConnectionString());

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}