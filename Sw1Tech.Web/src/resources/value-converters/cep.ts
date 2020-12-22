import { Base } from "./base";

export class CepValueConverter extends Base {
    toView(value) {
        if (!value)
            return value;
        value = this.removeNonDigits(value);
        let formatted = value;
        if (value.length >= 8) {
            formatted = value.substring(0, 2) + '-';
            formatted += value.substring(2, 5) + '-';
            formatted += value.substring(5, 8);
        }
        return formatted;
    }

}
