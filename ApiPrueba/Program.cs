using ApiPrueba.Implementaciones;
using ApiPrueba.Interfases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var config = builder.Configuration.GetSection("ApiConfiguracion");
//var urlAcceso = config["urlMicroservicioPrueba"];

//builder.Services.AddScoped<IConsumoWS, ConsumoWS>();
builder.Services.AddHttpClient<IConsumoWS,ConsumoWS>
    (
        cliente => {
            cliente.BaseAddress = new Uri("https://api.servicredito.aksingeneo.net/api/v1/");
            cliente.DefaultRequestHeaders.Add("Accept", "Application/json");
        }
    );

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
