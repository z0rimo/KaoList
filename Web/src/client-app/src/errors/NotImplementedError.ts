/**
 * The error that is thrown when a requested method or operation is not implemented.
 */
export default class NotImplementedError extends Error {
    /**
     * Initializes a new instance of the NotImplementedError class with default properties.
     * @param message The error message that explains the reason for the exception.
     * @param options unknown
     */
    constructor(message?: string, options?: ErrorOptions) {
        super(message ?? 'The method or operation is not implemented.', options);
        this.name = "NotImplementedError";
    }
}