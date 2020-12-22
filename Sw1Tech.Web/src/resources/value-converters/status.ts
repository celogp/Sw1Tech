export class StatusValueConverter {
    toView(value) {
        if (!value){
            return 'Pendente';
        }
        else{
            return 'Bloqueado';
        }
    }
}
