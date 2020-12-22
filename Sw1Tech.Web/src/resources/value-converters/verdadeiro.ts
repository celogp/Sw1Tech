export class VerdadeiroValueConverter {
    toView(value) {
        if (value){
            return 'Sim';
        }
        else{
            return 'Não';
        }
    }
}
