export interface ICrud {
    heading: string;
    txtPesquisa: string;
    message: string;
    isVisibleForm: boolean;
    isVisibleGrid: boolean;
    isAjaxServer: boolean;
    LstCpoPesquisa :any;
    CpoPesquisa : string;
    

    DoAtivaGrade()
    /*Alternar entre formulario principal e grade de pesquisa, Exemplo:
        if (this.isVisibleForm == true) {
            this.isVisibleGrid = true;
            this.isVisibleForm = false;
        } else {
            this.isVisibleGrid = false;
            this.isVisibleForm = true;
        }
    */
    DoMontaFiltro() /*Montar filtro para pesquisa*/
    DoPesquisar() /* Apresentar uma lista com os dados na grade */
    DoEditar(obj: any) /* Pegar os dados da grade/banco e jogar para o formulario, botao fica na grade*/
    DoApagar() /* Apaga o registro no banco de dados */
    DoSalvar() /* Salva o registro no banco de dados */
    DoAdicionar() /* Inclui o registro no banco de dados */
    DoLimparFormulario() /* Limpar os dados do formulario */
}