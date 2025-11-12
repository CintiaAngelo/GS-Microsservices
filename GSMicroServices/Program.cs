using GSMicroServices.Services;
using Microsoft.EntityFrameworkCore;
using GSMicroServices.Data;
using GSMicroServices.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<PromptContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection") ??
        "server=localhost;database=promptdb;user=root;password=123;",
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

builder.Services.AddScoped<IPromptService, PromptService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
