import { IFormItem } from "../form-item/form-item.types";

export interface ICatalogUser {
    id: number,
    name: IFormItem<string>,
    password: IFormItem<string>,
    phoneNumber: IFormItem<string>,
    email: IFormItem<string>,
    isDeactivated: IFormItem<boolean>
}

export interface ICatalogUserData {
    id: number,
    name: IFormItem<string>,
    password: IFormItem<string>,
    phoneNumber: IFormItem<string>,
    email: IFormItem<string>,
    isDeactivated: IFormItem<boolean>
}

export interface ICatalogUserDto {
    data: ICatalogUserData
}

export interface ICatalogUserDtoJournalItem {
    id: number,
    name: string
}

export interface ICatalogUserDtoJournal {
    query: string,
    totalCount: number,
    items: ICatalogUserDtoJournalItem[],
}

export interface CatalogUserCombobox {
    id: number;
    name: string;
}