using Microsoft.EntityFrameworkCore;
using OverTime.API.Extentions.CSV;
using OverTime.Application.Contracts.IRepositories;
using OverTime.Application.Contracts.MicroORM;
using OverTime.Infrastructure.DATA;
using OverTime.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region SqlServer Dependencies

//// use in-memory database
//services.AddDbContext<OrderContext>(c =>
//    c.UseInMemoryDatabase("OrderConnection"));

// use real database
builder.Services.AddDbContext<OverTimeDbContext>(c =>
	c.UseSqlServer(builder.Configuration.GetConnectionString("OverTimeConnection")), ServiceLifetime.Singleton); // we made singleton this in order to resolve in mediatR when consuming Rabbit

#endregion

#region Project Dependencies

// Add Infrastructure Layer
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped(typeof(IPersonRepository), typeof(PersonRepository));
builder.Services.AddScoped(typeof(ICSVService), typeof(CSVService));
//Register dapper in scope    
builder.Services.AddScoped<IDapper, OverTime.Infrastructure.MicroORM.Dapper>();
//services.AddScoped(typeof(IArtistRepository), typeof(ArtistRepository));
//services.AddTransient<IMusicRepository, MusicRepository>(); // we made transient this in order to resolve in mediatR when consuming Rabbit

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//// Add MediatR
//services.AddMediatR(typeof(CreateMusicCommand).GetTypeInfo().Assembly);
//services.AddMediatR(typeof(UpdateMusicCommand).GetTypeInfo().Assembly);
//services.AddMediatR(typeof(DeleteMusicCommand).GetTypeInfo().Assembly);
//services.AddMediatR(typeof(CreateArtistCommand).GetTypeInfo().Assembly);
//services.AddMediatR(typeof(UpdateArtistCommand).GetTypeInfo().Assembly);
//services.AddMediatR(typeof(DeleteArtistCommand).GetTypeInfo().Assembly);

////Domain Level Validation
//services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

#endregion



builder.Services.AddControllers()
	.AddXmlSerializerFormatters()
	.AddXmlDataContractSerializerFormatters(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

CreateAndSeedDatabase(app);

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



static void CreateAndSeedDatabase(IHost host)
{
	using (var scope = host.Services.CreateScope())
	{
		var services = scope.ServiceProvider;
		var loggerFactory = services.GetRequiredService<ILoggerFactory>();

		try
		{
			var overTimeContext = services.GetRequiredService<OverTimeDbContext>();
			OverTimeDbContextSeed.SeedAsync(overTimeContext, loggerFactory).Wait();
		}
		catch (Exception exception)
		{
			var logger = loggerFactory.CreateLogger<Program>();
			logger.LogError(exception, "An error occurred seeding the DB.");
		}
	}
}