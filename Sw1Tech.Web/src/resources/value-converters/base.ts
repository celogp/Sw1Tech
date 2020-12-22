export class Base {
    public removeNonDigits(input) {
        let digits = '';
        // Remove non-digits. i.e. '(', ')', ' ' and '-'
        for (let i = 0; i < input.length; i++) {
            let char = input.charAt(i);
            if ('0' <= char && char <= '9')
                digits += char;
        }
        return digits;
    }
}