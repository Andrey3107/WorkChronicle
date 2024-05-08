class LogTime {
    constructor() {
        this.newColumnBody = $("#new .column-body");
        this.inProgressColumnBody = $("#in-progress .column-body");
        this.resolvedColumnBody = $("#resolved .column-body");

    }

    Init() {
        const me = this;

        me.InitPlace();

        $("#btn-save-record").on("click", () => me.InitCreateTask());
    }

    InitPlace() {
        const me = this;
        const placeDropdown = new Dropdown("#place-ddl", "description", "id");

        this.GetDataList(null, "GET", "/Tasks/GetPlaces").then((response) => {
            if (response && response.success) {
                placeDropdown.SetData(response.data);
            }
            else if (response && !response.success) {
                placeDropdown.SetData([]);
            }
        });

        $("#place-ddl").on("change", () => { me.RecheckNotNullFields("#place-ddl"); });
    }

    InitCreateTask() {
        const isValid = this.Validate();

        if (isValid) {
            const data = this.CollectDataModel();

            this.GetDataList(data, "POST", "/Tasks/CreateTimeTrack").then((response) => {
                if (response && response.success) {
                    window.location.href = "/Tasks/Index";
                }
                else if (response && !response.success) {
                    alert("Something went wrong");
                }
            });
        }
    }

    CollectDataModel() {
        const duration = $("#duration").val();
        const comment = $("#comment").val();
        const placeId = $("#place-ddl").val();
        const ticketId = $("#task-id").val();

        const model = {
            Duration: duration,
            Comment: comment,
            TicketId: ticketId,
            PlaceId: placeId
        };

        return model;
    }

    Validate() {
        let isValid = true;

        const duration = $("#duration");
        const comment = $("#comment");
        const placeDdl = $("#place-ddl");

        if (!duration.val()) {
            duration.addClass("invalid-input");
            isValid = false;
        }

        if (!comment.val()) {
            comment.addClass("invalid-input");
            isValid = false;
        }

        if (!placeDdl.val()) {
            placeDdl.addClass("invalid-input");
            isValid = false;
        }

        return isValid;
    }

    RecheckNotNullFields(input) {
        if (!$(input).val() && !$(input).hasClass("invalid-input")) {
            $(input).addClass("invalid-input");
        }

        if ($(input).val()) {
            $(input).removeClass("invalid-input");
        }
    }

    // Get data
    GetDataList(data, type, url) {
        return $.ajax({
            type: type,
            url: url,
            data: data,
            success: function (response) {
                if (response.Success == false) {
                    alert(response.Message);
                }
            },
            error: function () {
                alert("Something went wrong during getting data");
            }
        });
    }
}