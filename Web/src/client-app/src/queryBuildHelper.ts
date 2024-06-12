import { QueryType } from "./api/base/ApiServiceBase";

export const queryBuildHelper = <T extends object>(
    options?: T,
    specialHandler?: (options: T) => Partial<T>
): QueryType<T> | undefined => {
    if (options === undefined || Object.keys(options).length === 0) {
        return;
    }

    let query: Partial<T> = { ...options };

    if (specialHandler) {
        query = { ...query, ...specialHandler(options) };
    }

    return query as QueryType<T>;
};