class Dropdown {
    constructor(id, textFieldOption, valueFieldOption) {
        this.ddl = $(id)[0];
        this.textField = textFieldOption;
        this.valueField = valueFieldOption;
    }

    SetData(data) {
        $(this.ddl).children('option').remove();

        data.forEach(item => {
            const option = document.createElement('option');
            option.value = item[this.valueField];
            option.textContent = item[this.textField];
            this.ddl.appendChild(option);
        });
    }
}