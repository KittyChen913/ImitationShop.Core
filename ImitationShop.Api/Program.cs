var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option => option.Filters.Add<LogActionFilter>())
    .AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.SetCorsPolicy(builder.Configuration);

builder.Services.AddDbContext<ImitationShopDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ImitationShopDB")));

builder.Services.SetModelValidation();

// Autofac Register
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var libraryAssemblies = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "ImitationShop.*.dll",
    SearchOption.AllDirectories).Select(Assembly.LoadFrom);

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterAssemblyTypes(libraryAssemblies.ToArray()).AsImplementedInterfaces().InstancePerLifetimeScope()
);

builder.Host.UseNLog();

// Set JWT Token Model
builder.Services.Configure<JwtTokenModel>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddJwtSetup(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestRewindMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
