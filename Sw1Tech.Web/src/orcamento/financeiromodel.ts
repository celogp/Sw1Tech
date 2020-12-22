export abstract class FinanceiroModel {
    Id: number = 0;
    ParceiroId: number = 0;
    OrcamentoId: number = 0;
    FormaPagamentoId: number = 0;
    DtMovimento: string;
    DtVencimento: string;
    Parcela: number = 0;
    VlrParcela: number = 0;
    VlrTaxa : number = 0;
    VlrSaldo: number = 0;
    Historico: string;
    RecDesp : number = 1;
}