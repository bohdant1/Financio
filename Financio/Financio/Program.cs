using Financio;

var builder = WebApplication.CreateBuilder(args);
// Logging 
builder.Logging.ClearProviders();
builder.Logging.AddConsole(options => 
{
    options.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
});

// Add services to the container.

builder.Services.Configure<DBContext>(
    builder.Configuration.GetSection("MongoDb"));

builder.Services.Configure<BlobStorageContext>(
    builder.Configuration.GetSection("BlobStorage"));


builder.Services.AddScoped<DBContext>();
builder.Services.AddScoped<BlobStorageContext>();
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<CollectionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

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
