import * as numeral from "numeral";

export class QtdValueConverter {
    toView(value) {
        if (!value)
            return value;
        let formatted = numeral(value).format('0,0.00');
        return formatted;
    }

}
