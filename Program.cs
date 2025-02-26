var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// React uygulaması için CORS politikasını ekleyelim
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS politikasını aktifleştirelim
app.UseCors("ReactPolicy");

app.UseAuthorization();

// React statik dosyaları için yapılandırma
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

// React uygulaması için catch-all route
app.MapFallbackToFile("index.html");

app.Run();
