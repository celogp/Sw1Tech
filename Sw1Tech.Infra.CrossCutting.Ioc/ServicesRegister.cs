using Microsoft.Extensions.DependencyInjection;
using Sw1Tech.App;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Service;
using Sw1Tech.Infra.Context.EF;
using Sw1Tech.Infra.Context.Interfaces.EF;
using Sw1Tech.Infra.Repository.EF;
using Microsoft.Extensions.Configuration;

namespace Sw1Tech.Infra.CrossCutting.Ioc
{
    public static class ServicesRegister
    {
        private static IConfigurationRoot Configuration { get; }

        public static void DoRegistrarServicos(IServiceCollection services)
        {
            //Repositorios
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
            services.AddScoped<IParceiroRepository, ParceiroRepository>();
            services.AddScoped<ISexoRepository, SexoRepository>();
            services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();
            services.AddScoped<IUfRepository, UfRepository>();
            services.AddScoped<IFormaPagamentoRepository, FormaPagamentoRepository>();
            services.AddScoped<IFinanceiroRepository, FinanceiroRepository>();
            services.AddScoped<IVendedorRepository, VendedorRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoModeloRepository, ProdutoModeloRepository>();
            services.AddScoped<IModeloRepository, ModeloRepository>();
            services.AddScoped<IModeloKitRepository, ModeloKitRepository>();
            services.AddScoped<IOrcamentoRepository, OrcamentoRepository>();
            services.AddScoped<IOrcamentoItemRepository, OrcamentoItemRepository>();
            services.AddScoped<IOrcamentoOcorrenciaRepository, OrcamentoOcorrenciaRepository>();
            services.AddScoped<IOrcamentoAnexoRepository, OrcamentoAnexoRepository>();
            services.AddScoped<IRegistroExportacaoRepository, RegistroExportacaoRepository>();

            //Serviços da classe
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ILocalizacaoService, LocalizacaoService>();
            services.AddScoped<IParceiroService, ParceiroService>();
            services.AddScoped<ISexoService, SexoService>();
            services.AddScoped<IOcorrenciaService, OcorrenciaService>();
            services.AddScoped<IUfService, UfService>();
            services.AddScoped<IFormaPagamentoService, FormaPagamentoService>();
            services.AddScoped<IFinanceiroService, FinanceiroService>();
            services.AddScoped<IVendedorService, VendedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IProdutoModeloService, ProdutoModeloService>();
            services.AddScoped<IModeloService, ModeloService>();
            services.AddScoped<IModeloKitService, ModeloKitService>();
            services.AddScoped<IOrcamentoService, OrcamentoService>();
            services.AddScoped<IOrcamentoItemService, OrcamentoItemService>();
            services.AddScoped<IOrcamentoOcorrenciaService, OrcamentoOcorrenciaService>();
            services.AddScoped<IOrcamentoAnexoService, OrcamentoAnexoService>();
            services.AddScoped<IRegistroExportacaoService, RegistroExportacaoService>();

            //Serviços da aplicação
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<ILocalizacaoAppService, LocalizacaoAppService>();
            services.AddScoped<IParceiroAppService, ParceiroAppService>();
            services.AddScoped<ISexoAppService, SexoAppService>();
            services.AddScoped<IOcorrenciaAppService, OcorrenciaAppService>();
            services.AddScoped<IUfAppService, UfAppService>();
            services.AddScoped<IFormaPagamentoAppService, FormaPagamentoAppService>();
            services.AddScoped<IFinanceiroAppService, FinanceiroAppService>();
            services.AddScoped<IVendedorAppService, VendedorAppService>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IProdutoModeloAppService, ProdutoModeloAppService>();
            services.AddScoped<IModeloAppService, ModeloAppService>();
            services.AddScoped<IModeloKitAppService, ModeloKitAppService>();
            services.AddScoped<IOrcamentoAppService, OrcamentoAppService>();
            services.AddScoped<IOrcamentoItemAppService, OrcamentoItemAppService>();
            services.AddScoped<IOrcamentoOcorrenciaAppService, OrcamentoOcorrenciaAppService>();
            services.AddScoped<IOrcamentoAnexoAppService, OrcamentoAnexoAppService>();
            services.AddScoped<IRegistroExportacaoAppService, RegistroExportacaoAppService>();
        }
    }
}
