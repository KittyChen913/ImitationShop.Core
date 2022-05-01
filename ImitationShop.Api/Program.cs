var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ImitationShopDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ImitationShopDB")));

// Autofac Register
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); 

var libraryAssemblies = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "ImitationShop.*.dll", 
    SearchOption.AllDirectories).Select(Assembly.LoadFrom);

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterAssemblyTypes(libraryAssemblies.ToArray()).AsImplementedInterfaces().InstancePerLifetimeScope()
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
