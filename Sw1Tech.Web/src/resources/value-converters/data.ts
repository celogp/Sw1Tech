import * as moment from "moment";

export class DataValueConverter {
    toView(value, tipo) {
        if (!value)
            return value;
        let ret = moment(value).format('D/M/YYYY HH:MM:SS');
        if (tipo != "H") {
            ret = moment(value).format('D/M/YYYY');
        }
        return ret;
    }
}