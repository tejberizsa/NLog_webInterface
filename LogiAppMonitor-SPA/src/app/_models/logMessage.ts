export interface LogMessage {
    id: number;
    createdAt: Date;
    instance?: string;
    logLevel?: string;
    logger?: string;
    loggerMessage?: string;
}
