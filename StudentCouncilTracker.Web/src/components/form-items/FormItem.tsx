const { TextArea } = Input;
import {
  EmployeeCombobox,
  FormItemLayout,
  IDateRange,
  IFormItem,
  FieldTypes,
} from "@/types/index";
import { DateRange, Employee } from "@/components/editors";
import { ReactNode, useCallback } from "react";
import dayjs from "dayjs";
import styles from "@/components/editors/form-items/FormItem.module.css";
import {
  Button,
  Checkbox,
  DatePicker,
  Form,
  Input,
  InputNumber,
  Popover,
} from "antd";
import { QuestionCircleOutlined } from '@ant-design/icons';

interface IFormItemProps<T> {
  children?: ReactNode;
  control: IFormItem<T>;
  isShowLabel?: boolean;
  layout?: FormItemLayout;
  onChange?: (value: IFormItem<T>) => void;
}

interface IFormItemValueProps<T> {
  value?: IFormItem<T>;
  onChange: (value: IFormItem<T>) => void;
}

interface IFormItemLabelHintProps {
  hint: string;
}

interface IFormItemLabelProps<T> {
  control: IFormItem<T>;
  className: string;
}

export function FormItemLabel<T>({
  control,
  className,
}: IFormItemLabelProps<T>) {
  if (!control) return null;

  const labelClassName = `${className} ${styles["col-form-label"]}`;

  return control.type === FieldTypes.CHECKBOX ? (
    <div className={className}></div>
  ) : (
    <label className={labelClassName}>
      {control.label.label}
      {control.label.hint && <FormItemLabelHint hint={control.label.hint} />}
    </label>
  );
}

export function FormItemLabelHint({ hint }: IFormItemLabelHintProps) {
  if (!hint) return null;

  return (
    <Popover content={hint} trigger="click" placement="bottomLeft">
      <Button type="link" icon={<QuestionCircleOutlined />} size="small" />
    </Popover>
  );
}

export function FormItemValue<T>({
  value: control,
  onChange,
}: IFormItemValueProps<T>) {
  if (!control) return null;

  if (!control.isEditable) return control?.value;

  const handleChange = (newValue: unknown) =>
    onChange({ ...control, value: newValue as T });

  switch (control?.type) {
    case FieldTypes.TEXT:
    case FieldTypes.URL:
    case FieldTypes.EMAIL:
      return (
        <Input
          value={control.value as string}
          onChange={(e) => handleChange(e.target.value)}
          allowClear
        />
      );
    case FieldTypes.TEXTAREA:
      return (
        <TextArea
          value={control.value as string}
          onChange={(e) => handleChange(e.target.value)}
          autoSize={{ minRows: 2 }}
          allowClear
        />
      );
    case FieldTypes.NUMBER:
      return (
        <InputNumber
          value={control.value as number}
          onChange={(value) => handleChange(value)}
        />
      );
    case FieldTypes.DATE:
      return (
        <DatePicker
          placeholder=""
          format="DD.MM.YYYY"
          value={control.value ? dayjs(`${control.value}`) : null}
          onChange={(date) => handleChange(date?.toJSON())}
        />
      );
    case FieldTypes.DATERANGE:
      return (
        <DateRange
          value={control.value as IDateRange}
          onChange={(value) => handleChange(value)}
        />
      );
    case FieldTypes.CHECKBOX:
      return (
        <Checkbox
          checked={control.value as boolean}
          onChange={(value) => handleChange(value.target.checked)}
        >
          {control.label.label}
        </Checkbox>
      );
    case FieldTypes.EMPLOYEE: {
      const employeeCombobox = control.value as EmployeeCombobox;
      return (
        <Employee
          value={employeeCombobox}
          onChange={(value) => handleChange(value)}
        />
      );
    }
  }

  return null;
}

export function FormItem<T>({
  children,
  control,
  onChange,
  isShowLabel = true,
  layout = FormItemLayout.Horizontal,
}: IFormItemProps<T>) {
  const onChangeValue = useCallback(
    (value: IFormItem<T>) => {
      if (onChange) {
        onChange(value);
      }
    },
    [onChange]
  );

  const colDefault = 12;

  const labelCol = layout == FormItemLayout.Horizontal ? 2 : colDefault;

  const valueCol =
    isShowLabel && layout == FormItemLayout.Horizontal
      ? colDefault - labelCol
      : colDefault;

  const labelClassName = `col-sm-${labelCol}`;

  const valueClassName = `col-sm-${valueCol} ${
    control?.isEditable ? "" : styles["col-form-value"]
  }`;

  const validator = (_: any, value: { value: IFormItem<T> }) => {
    return Promise.resolve();

    if (value?.value || control?.type === FieldTypes.CHECKBOX) {
      return Promise.resolve();
    }
    return Promise.reject(new Error("Обѝзательно длѝ заполнениѝ!"));
  };

  return (
    <div className="form-group row">
      <FormItemLabel control={control} className={labelClassName} />

      <div className={valueClassName}>
        {children}

        <Form.Item
          name={control.name}
          rules={[{ validator: validator }]}
          className="mb-0"
        >
          <FormItemValue onChange={onChangeValue} />
        </Form.Item>
      </div>
    </div>
  );
}
