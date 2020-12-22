export const CFG_API = {
  Base: 'https:',
  // Servidor: '//40.71.27.44',
  Servidor: '//localhost',
  //Porta: ':49891',
  //Porta: ':44313',
  Porta: ':9090',
  PastaApi: '',
  // Servidor: '//sw1tech.com.br.asp.hostazul.com.br',
  // Porta: '',
  // PastaApi: '/api',
  Token: 'tkapp',
  imagesOrcamento: './image/client/orcamento/',
  imagesOcorrencia: './image/client/orcamento/ocorrencia/'
}

export const CFG_URLAPI = {
  PesquisaTabelaUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/PesquisaTabela',
  UsuarioUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/Usuario',
  ParceiroUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/Parceiro',
  LocalizacaoUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/Localizacao',
  VendedorUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/Vendedor',
  ProdutoUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/Produto',
  ProdutoModeloUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/ProdutoModelo',
  ModeloUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/Modelo',
  OrcamentoUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/Orcamento',
  OrcamentoItemUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/OrcamentoItem',
  OrcamentoOcorrenciaUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/OrcamentoOcorrencia',
  OrcamentoAnexoUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/OrcamentoAnexo',
  FinanceiroUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/Financeiro',
  FilesUrl: CFG_API.Base + CFG_API.Servidor + CFG_API.Porta + CFG_API.PastaApi + '/api/Files',
  ViaCepUrl: 'https://viacep.com.br/ws/'
};

export const CFG_USUARIO = {
  Id: "ID",
  Nome: "Nome"
}
