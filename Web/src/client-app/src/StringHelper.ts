export default class StringHelper {
    private static re = /{(.(?!})*)}/g;

    static template(strings: TemplateStringsArray | string[], ...keys: Array<string | number>) {
        return (...values: any[]) => {
            const dict = (values[values.length - 1] || {}) as { [key in string]: string };
            const result = [strings[0]];
            keys.forEach((key, i) => {
                const value = Number.isInteger(key) ? values[key as number] : dict[key as string];
                result.push(value, strings[i + 1]);
            });
            return result.join('');
        };
    }

    static format(format: string, ...values: any[]) {
        const strings: string[] = [];
        const keys: Array<string | number> = [];
        let prevIndex = 0;
        let result: ReturnType<typeof StringHelper.re.exec>;
        while ((result = StringHelper.re.exec(format)) != null) {
            let name = result[1];
            let n = Number.parseInt(name)
            keys.push(Number.isInteger(n) ? n : name);
            strings.push(format.substring(prevIndex, result.index))
            prevIndex = result.index + result[0].length;
        }

        if (prevIndex !== format.length) {
            strings.push(format.substring(prevIndex, format.length))
        }

        return StringHelper.template(strings, ...keys)(...values);
    }

    static isWhiteSpace(str: string){
        return /^\s*$/.test(str);
    }
}
