using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sw1Tech.App;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Service;
using Sw1Tech.Infra.Context.EF;
using Sw1Tech.Infra.Context.Interfaces.EF;
using Sw1Tech.Infra.CrossCutting.Ioc;
using Sw1Tech.Infra.Repository.EF;
using Sw1Tech.Infra.Repository.EF.Context;
using System;

namespace Sw1Tech.ACIntegracao
{
    public class SexoCtrl
    {
        ISexoAppService _sexoAppService;
        public SexoCtrl(ISexoAppService SexoAppService)
        {
            _sexoAppService = SexoAppService;
        }

        public dynamic DoGetSexo()
        {
            var lst = _sexoAppService.DoObterTodos();
            foreach (var item in lst)
            {
                Console.WriteLine("dentro da classe => " + item.Nome);
            }

            return true;
        }

    }

    class Program
    {
        private static IServiceProvider ServiceProvider { get; set; }
        static ServiceCollection _services;
        static readonly string _connString = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=bdSw1tech; User ID=sw1tech; Password=@dmin123; Connect Timeout=30;";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _services = new ServiceCollection();
            ConfigureServices(_services);
            ServiceProvider = _services.BuildServiceProvider();

            //ConfigureServices();
            SexoCtrl sexoCtrl = new SexoCtrl(ServiceProvider.GetService<ISexoAppService>());

            var bar = ServiceProvider.GetService<ISexoAppService>();
            var lst =  bar.DoObterTodos();
            foreach (var item in lst)
            {
                Console.WriteLine(item.Nome);
            }

            var lst1 = ServiceProvider.GetService<ILocalizacaoAppService>().DoObterTodos();
            foreach (var item in lst1)
            {
                Console.WriteLine(item.Logradouro + item.Localidade);
            }

            sexoCtrl.DoGetSexo();

            Console.ReadKey();

            //sexoCtrl.DoGetSexo();
        }

        static ServiceProvider ConfigureServices()
        {
            
            Console.WriteLine("ConfiguraServices");

            var builder = new ConfigurationBuilder()
                            .SetBasePath(AppContext.BaseDirectory)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .Build();

            builder.GetConnectionString("DefaultConnection");

            //IConfigurationRoot configuration = builder.Build();

            Console.WriteLine(builder.GetConnectionString("DefaultConnection"));

            var services = new ServiceCollection();

            //services.AddDbContext<Sw1TechContext>(
            //               options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContext<Sw1TechContext>(
            //               options => options.UseSqlServer(builder.GetConnectionString("DefaultConnection")));

            services.AddDbContext<Sw1TechContext>(
                           options => options.UseSqlServer("Data Source=localhost\\SQLEXPRESS; Initial Catalog=bdSw1tech; User ID=sw1tech; Password=@dmin123; Connect Timeout=30;"));


            services.AddScoped<SexoCtrl>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISexoAppService, SexoAppService>();
            services.AddScoped<ISexoService, SexoService>();
            services.AddScoped<ISexoRepository, SexoRepository>();

            services.AddScoped<ILocalizacaoAppService, LocalizacaoAppService>();
            services.AddScoped<ILocalizacaoService, LocalizacaoService>();
            services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();

            ServiceProvider = services.BuildServiceProvider();

            return services.BuildServiceProvider();
        }

        static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Sw1TechContext>(
                           options => options.UseSqlServer(_connString));

            ServicesRegister.DoRegistrarServicos(services);

            services.AddScoped<SexoCtrl>();

        }

    }
}
