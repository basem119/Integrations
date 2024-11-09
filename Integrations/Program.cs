using Integrations;
using Integrations.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<AppendQueryInUrlHandler>();

builder.Services.AddHttpClient("universities", x=>
{
    x.BaseAddress = new Uri("http://universities.hipolabs.com/");
});
builder.Services.AddHttpClient("jokes", x =>
{
    x.BaseAddress = new Uri("https://official-joke-api.appspot.com/");
});
builder.Services.AddHttpClient<JokeService>( x =>
{
    x.BaseAddress = new Uri("https://official-joke-api.appspot.com/");
}).AddHttpMessageHandler<AppendQueryInUrlHandler>(); //foreach request call the handler


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
