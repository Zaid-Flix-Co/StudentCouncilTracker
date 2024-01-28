import { useMemo, useState } from "react";
import { DebounceSelect } from "@/components/editors/debounce-selects/DebounceSelect"
import { EmployeeCombobox, IEmployeeListItem } from "@/types/employees.types";
import { useLazyGetEmployeeListQuery } from "@/store/api/employees.api";
import { IOperationResult } from "@/types/operation-result.types";
import { IList } from "@/types/list.types";
import { HighlightText } from "@/components/displays/highlight-text/HighlightText";
import { CatalogUserCombobox } from "../../../types/catalog-user/catalog-user.types";

export interface CatalogUserValue {
    label: string;
    value: string;
}

interface CatalogUserProps {
    value: CatalogUserCombobox,
    onChange?: (value: CatalogUserCombobox) => void;
}

export function CatalogUser({ onChange, value: valueOption }: CatalogUserProps) {
    const [value, setValue] = useState<CatalogUserValue>({ label: valueOption.name, value: `${valueOption.id}` });
    const [search, setSearch] = useState<string>("");
    const [trigger] = useLazyGetCatalogUserListQuery()

    const fetch = useMemo(() => {
        const load = (search: string) => {
            console.log(search)

            return trigger(search, true).unwrap().then((result: IOperationResult<IList<ICatalogUserListItem>>) => {

                return result.value.items.map(
                    (e) => ({
                        label: `${e.name}`,
                        value: `${e.id}`
                    }),
                )
            })
        }

        return load;
    }, [trigger])

    return (<>
        <DebounceSelect
            popupClassName="ant-select-multiline"
            value={value}
            placeholder=""
            onSearchValueChange={setSearch}
            fetchOptions={fetch}
            onChange={(newValue) => {
                setValue(newValue as CatalogUserValue);

                const temp: any = newValue

                const employeeCombobox = { id: +temp.value, name: temp.label }

                onChange?.(employeeCombobox);

            }}
            style={{ width: '100%' }}
            optionRender={(option) => (
                <div style={{ lineHeight: "1.2" }}>
                    <div style={{ color: "rgb(132, 146, 166)", fontSize: "13px", marginBottom: "0.25rem" }}>
                        {option.data.profession}, {option.data.organizationalUnit}
                    </div>
                    <div><HighlightText search={search} text={option.data.label} /></div>
                </div>
            )} />
    </>)
}
