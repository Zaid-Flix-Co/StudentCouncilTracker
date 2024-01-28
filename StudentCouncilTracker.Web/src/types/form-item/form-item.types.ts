export interface IFormItem<T> extends IFormItemPermission {
    label: IFormItemLabel,
    name: string,
    type: FieldTypes,
    value: T
}

export enum FormItemLayout {
    Horizontal = 'Horizontal',
    Vertical = "Vertical"
}

export interface IFormItemLabel {
    label: string
    hint: string
}

export interface IFormItemPermission {
    isEditable: boolean,
    isVisible: boolean,
}

export enum FieldTypes {
    TEXT = 0,
    TEXTAREA = 2,
    SELECT = 'select',
    NUMBER = 'number',
    EMAIL = 'email',
    URL = 'url',
    PASSWORD = 'password',
    CHECKBOX = 3,
    RADIO = 'radio',
    DATE = 1,
    DATERANGE = 'date-range',
    MONTHRANGE = 'month-range',

    EMPLOYEE = 20,
    EMPLOYEES = 21,
    REGULATION_TYPE = 47,
    ORGANIZATIONAL_UNIT = 48,
    RISKLEVEL = 49,
    FAULT_TYPE = 51,
    FAULT_GROUP = 52,
    PROFESSION = 53,
    DOCUMENT_ACTION_AREA = 54,
    DOCUMENT_TYPE = 55,
    CONTROL_CARD_TYPE = 56,
    CONTROL_CARD = 50
}
export interface IDateRange {
    From: IFormItem<string>,
    To: IFormItem<string>,
}
