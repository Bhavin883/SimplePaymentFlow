using Microsoft.EntityFrameworkCore;
using SimplePaymentFlow.Domain;
using SimplePaymentFlow.Persistence;
using SimplePaymentFlow.UseCases.GetSitesUseCase;
using SimplePaymentFlow.UseCases.LockPumpUseCase;
using SimplePaymentFlow.UseCases.GetReceiptUseCase;
using SimplePaymentFlow.UseCases.UnlockPumpUseCase;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IGetSitesUseCase, GetSitesUseCase>();
builder.Services.AddScoped<IUnlockPumpUseCase, UnlockPumpUseCase>();
builder.Services.AddScoped<ILockPumpUseCase, LockPumpUseCase>();
builder.Services.AddScoped<IGetReceiptUseCase, GetReceiptUseCase>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "SimplePaymentFlowDb"));

var app = builder.Build();
AddData(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

static void AddData(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<AppDbContext>();

    var sites = new List<Site>
            {
            new Site(id: 101, name: "Site 101"),
            new Site(id: 102, name: "Site 102"),
            new Site(id: 103, name: "Site 103"),
            new Site(id: 104, name: "Site 104"),
            new Site(id: 105, name: "Site 105"),

            };
    var pumps = new List<Pump> {
            new Pump(1,"Pump1",true,101),
            new Pump(2,"Pump2",true,101),
            new Pump(3,"Pump3",true,102),
            new Pump(4,"Pump4",true,102),
            new Pump(5,"Pump5",true,103),
            new Pump(6,"Pump6",true,103),
            new Pump(7,"Pump7",true,104),
            new Pump(8,"Pump8",true,104),
            new Pump(9,"Pump9",true,105),
            new Pump(10,"Pump10",true,105),

            };
    db.Sites.AddRange(sites);
    db.Pumps.AddRange(pumps);

    db.SaveChanges();
}