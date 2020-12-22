export abstract class OrcamentoItemModel {
    Id: number = 0;
    OrcamentoId: number = 0;
    RootId: number = 0;
    Ambiente: string = "";
    NomeProdutoBase: string = "";
    VolumeBase:string = "UN";
    VlrBase: number = 0;
    ProdutoId: number = 0;
    NomeProduto: string = "";
    Volume: string = "UN";
    Classificacao: number = 0;
    ModeloId: number = 0;
    NomeModelo : string = "";
    Quantidade: number = 1;
    QuantidadeKit : number = 1;
    Largura: number = 1;
    Comprimento: number = 1;
    Area: number = 1;
    VlrUnitario: number = 0;
    VlrCusto : number = 0;
    VlrDesconto: number = 0;
    PerDesconto: number = 0;
    IndDescontoProdutoFinal: number = 1;
    VlrBruto: number = 0;
    VlrTotal: number = 0;
}