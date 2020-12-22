export abstract class OrcamentoModel {
    Id: number = 0;
    ParceiroId: number = 0;
    NomeParceiro: string = "";
    Projeto: string = "";
    DtMovimento: string;
    DtEntrega: string;
    UsuarioId: number = 0;
    DiaValidade: number = 3;
    Numero: number = 0;
    Status: number = 0;

    //Variaveis do total do orçamento
    TotBases: number = 0;
    TotProdutos: number = 0;
    TotAcabamentos: number = 0;
    TotAcessorios: number = 0;
    TotServicos: number = 0;
    VlrDesconto: number = 0;
    PerDesconto: number = 0;
    TotOrcamento: number = 0;

}