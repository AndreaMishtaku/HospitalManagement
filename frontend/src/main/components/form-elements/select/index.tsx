import React from "react";
import { useState } from "react";
import Select from "react-select";

interface ISelect {
  value: any;
  onChange: (value: any) => void;
  multiple?: boolean;
  options: Array<any>;
  required?: boolean;
}

const FormSelect = (props: ISelect) => {
  const {
    options,
    value,
    required = false,
    multiple = false,
    onChange,
  } = props;

  const [val, setVal] = useState(
    multiple
      ? options.filter((o: any) => o.value === value)
      : options.find((o: any) => o.value === value)
  );

  return (
    <Select
      required={required}
      options={options}
      isMulti={multiple ? true : false}
      value={val}
      onChange={(newValue: any) => {
        setVal(newValue);
        if (multiple) {
          onChange(newValue.map((x: any) => x.value));
        } else {
          onChange(newValue.value);
        }
      }}
    />
  );
};

export default FormSelect;
