import { Base } from "./base";

export class PhoneValueConverter extends Base {
    toView(value) {
        if (!value)
            return value;
        value = this.removeNonDigits(value);
        let formatted = value;
        if (value.length >= 12) {
            formatted = '(' + value.substring(0, 3);
            if (value.length >= 3){
                formatted += ') ' + value.substring(3, 8);
            }
            if (value.length >= 8) {
                formatted += '-' + value.substring(8);
            }
        }
        return formatted;
    }
}
