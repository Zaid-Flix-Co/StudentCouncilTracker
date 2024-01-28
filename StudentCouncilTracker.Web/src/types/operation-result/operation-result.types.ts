export interface IOperationResult<T> {
    value: T,
    ok: boolean,
    reasons: IReason[]
}

export interface ICanPermission {
    create: boolean,
    edit: boolean,
    delete: boolean
}

export interface IReason {
    message: string,   
    metadata: object,   
    type: ReasonType   
}

export enum ReasonType {
    Info = 0,
    Success = 1,
    Warning = 2,
    Error = 3,
} 