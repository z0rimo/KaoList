export type AreaNameType = 'root' | 'customer';

export default class PathHelper {
    static getAreaNameByPath(path: string): AreaNameType | null {
        if (path.startsWith('/customer')) {
            return 'customer'
        } else if (path === '/') {
            return 'root';
        }

        return null;
    } 
}