import { Base } from "./base";

export class CnpjValueConverter extends Base {
    toView(value) {
        if (!value)
            return value;
        value = this.removeNonDigits(value);
        let formatted = value;
        if (value.length >= 14) {
            formatted = value.substring(0, 2) + '.';
            formatted += value.substring(2, 5) + '.';
            formatted += value.substring(5, 8) + '/';
            formatted += value.substring(8, 12) + '-';
            formatted += value.substring(12);
        }
        return formatted;
    }

}
