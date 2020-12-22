import { Base } from "./base";

export class CpfValueConverter extends Base {
    toView(value) {
        if (!value)
            return value;
        value = this.removeNonDigits(value);
        let formatted = value;
        if (value.length >= 11) {
            formatted = value.substring(0, 3) + '.';
            formatted += value.substring(3, 6) + '.';
            formatted += value.substring(6, 9) + '-';
            formatted += value.substring(9);
        }
        return formatted;
    }

}
