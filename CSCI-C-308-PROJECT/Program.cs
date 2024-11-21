using CSCI_308_TEAM5.API.Security;
using CSCI_308_TEAM5.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
    .AddTeam5Jwt();

builder.Services.AddAuthorization();
builder.Services.AddSecurities();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddControllers();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
