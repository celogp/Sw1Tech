export class ClassificacaoValueConverter {
    toView(value) {
        if (!value)
            return value;
        let formatted = '';
        if (value == 1) {
            formatted = 'FINAL';
        } else if (value == 2) {
            formatted = 'BASE';
        } else if (value == 3) {
            formatted = 'ACABAMENTOS';
        } else if (value == 4) {
            formatted = 'ACESSÓRIOS';
        } else {
            formatted = 'SERVIÇOS';
        }
        return formatted;
    }

}
