export default class ClassNameHelper {
    static concat(...names: Array<string | null | undefined | false>): string {
        return names.filter(item => !!item).join(" ");
    }

}