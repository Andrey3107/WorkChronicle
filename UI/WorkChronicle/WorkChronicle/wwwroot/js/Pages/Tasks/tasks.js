class Tasks {
    constructor() {
        
    }

    Init() {
        const me = this;

        this.InitProjectsDropdown();
        this.InitTaskTypesDropdown();
        this.InitTaskStatusesDropdown();
        this.InitAsigneeDropdown();
        this.InitNameField();
        this.InitDescriptionField();
        this.InitEstimateField();
        this.InitStartAndEndDatepickers();
        this.InitCompletenessField();
        this.InitPriorityDropdown();

        $("#btn-create-task").on("click", () => me.InitCreateTask());
    }

    // Init fields
    InitProjectsDropdown() {
        const me = this;

        const projectsDropdown = new Dropdown("#projects-ddl", "name", "id");

        this.GetDataList(null, "GET", "/Tasks/GetProjectList").then((response) => {
            if (response && response.success) {
                projectsDropdown.SetData(response.data);
                me.InitAsigneeDropdown();
            }
            else if (response && !response.success) {
                projectsDropdown.SetData([]);
            }
        });

        $("#projects-ddl").on("change", () => {
            me.InitAsigneeDropdown();
            me.RecheckNotNullFields("#projects-ddl");
        });
    }

    InitTaskTypesDropdown() {
        const me = this;
        const taskTypesDropdown = new Dropdown("#ticket-type-ddl", "description", "id");

        this.GetDataList(null, "GET", "/Tasks/GetTicketTypes").then((response) => {
            if (response && response.success) {
                taskTypesDropdown.SetData(response.data);
            }
            else if (response && !response.success) {
                taskTypesDropdown.SetData([]);
            }
        });

        $("#ticket-type-ddl").on("change", () => { me.RecheckNotNullFields("#ticket-type-ddl"); });
    }

    InitTaskStatusesDropdown() {
        const me = this;
        const taskStatusesDropdown = new Dropdown("#ticket-status-ddl", "description", "id");

        this.GetDataList(null, "GET", "/Tasks/GetTicketStatuses").then((response) => {
            if (response && response.success) {
                taskStatusesDropdown.SetData(response.data);
            }
            else if (response && !response.success) {
                taskStatusesDropdown.SetData([]);
            }
        });

        $("#ticket-status-ddl").on("change", () => { me.RecheckNotNullFields("#ticket-status-ddl"); });
    }

    InitPriorityDropdown() {
        const me = this;
        const priorityDropdown = new Dropdown("#priority-ddl", "description", "id");

        this.GetDataList(null, "GET", "/Tasks/GetPriorities").then((response) => {
            if (response && response.success) {
                priorityDropdown.SetData(response.data);
            }
            else if (response && !response.success) {
                priorityDropdown.SetData([]);
            }
        });

        $("#priority-ddl").on("change", () => { me.RecheckNotNullFields("#priority-ddl"); });
    }

    InitAsigneeDropdown() {
        const me = this;
        const projectId = $("#projects-ddl").val();
        const assigneeDropdown = new Dropdown("#assignee-ddl", "fullName", "id");

        assigneeDropdown.SetData([]);

        if (projectId) {
            this.GetDataList(null, "GET", "/Tasks/GetAssignees?projectId=" + projectId).then((response) => {
                if (response && response.success) {
                    assigneeDropdown.SetData(response.data);
                }
                else if (response && !response.success) {
                    assigneeDropdown.SetData([]);
                }
            });
        }

        $("#assignee-ddl").on("change", () => { me.RecheckNotNullFields("#assignee-ddl"); });
    }

    InitNameField() {
        const me = this;
        $("#task-name").on("change", () => { me.RecheckNotNullFields("#task-name"); });
    }

    InitDescriptionField() {
        const me = this;
        $("#description").on("change", () => { me.RecheckNotNullFields("#description"); });
    }

    InitEstimateField() {
        const me = this;
        $("#estimated-time").on("change", () => { me.RecheckNotNullFields("#estimated-time"); });
    }

    InitCompletenessField() {
        const me = this;
        $("#completeness-ddl").on("change", () => { me.RecheckNotNullFields("#completeness-ddl"); });
    }

    InitStartAndEndDatepickers() {
        const me = this;
        $("#start-datepicker").datepicker();
        $("#end-datepicker").datepicker();

        $("#start-datepicker").on("change", () => { me.RecheckDateFields(); });
        $("#end-datepicker").on("change", () => { me.RecheckDateFields(); });
    }

    InitCreateTask() {
        const isValid = this.Validate();

        if (isValid) {
            const data = this.CollectDataModel();

            this.GetDataList(data, "POST", "/Tasks/CreateTask").then((response) => {
                if (response && response.success) {
                    alert("Task was created successfully");
                    window.location.href = "/Tasks/Index";
                }
                else if (response && !response.success) {
                    alert("Something went wrong");
                }
            });
        }
    }

    Validate() {
        let isValid = true;

        const projectDdl = $("#projects-ddl");
        const ticketTypeDdl = $("#ticket-type-ddl");
        const ticketStatusDdl = $("#ticket-status-ddl");
        const taskName = $("#task-name");
        const description = $("#description");
        const assigneeDdl = $("#assignee-ddl");
        const startDate = $("#start-date");
        const endDate = $("#end-date");
        const completenessDdl = $("#completeness-ddl");
        const estimate = $("#estimated-time");
        const priorityDdl = $("#priority-ddl");

        if (!projectDdl.val()) {
            projectDdl.addClass("invalid-input");
            isValid = false;
        }

        if (!ticketTypeDdl.val()) {
            ticketTypeDdl.addClass("invalid-input");
            isValid = false;
        }

        if (!priorityDdl.val()) {
            priorityDdl.addClass("invalid-input");
            isValid = false;
        }

        if (!completenessDdl.val()) {
            completenessDdl.addClass("invalid-input");
            isValid = false;
        }

        if (!ticketStatusDdl.val()) {
            ticketStatusDdl.addClass("invalid-input");
            isValid = false;
        }

        if (!taskName.val()) {
            taskName.addClass("invalid-input");
            isValid = false;
        }

        if (!description.val()) {
            description.addClass("invalid-input");
            isValid = false;
        }

        if (!assigneeDdl.val()) {
            assigneeDdl.addClass("invalid-input");
            isValid = false;
        }

        if (!startDate.val()) {
            startDate.addClass("invalid-input");
            isValid = false;
        }

        if (!endDate.val()) {
            endDate.addClass("invalid-input");
            isValid = false;
        }

        if (startDate.val() && endDate.val()) {
            const startDateValue = new Date(startDate.val());
            const endDateValue = new Date(endDate.val());

            if (startDateValue > endDateValue) {
                startDate.addClass("invalid-input");
                endDate.addClass("invalid-input");

                isValid = false;
            }
        }

        if (!estimate.val()) {
            estimate.addClass("invalid-input");
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

    RecheckDateFields() {
        if (!$("#start-date").val() && !$("#start-date").hasClass("invalid-input")) {
            $("#start-date").addClass("invalid-input");
        }

        if (!$("#end-date").val() && !$("#end-date").hasClass("invalid-input")) {
            $("#end-date").addClass("invalid-input");
        }

        if ($("#start-date").val() && $("#end-date").val()) {
            const startDateValue = new Date($("#start-date").val());
            const endDateValue = new Date($("#end-date").val());

            if (startDateValue <= endDateValue) {
                $("#start-date").removeClass("invalid-input");
                $("#end-date").removeClass("invalid-input");
            }
            else {
                $("#start-date").addClass("invalid-input");
                $("#end-date").addClass("invalid-input");
            }
        }
    }

    CollectDataModel() {
        const projectId = $("#projects-ddl").val();
        const ticketTypeId = $("#ticket-type-ddl").val();
        const ticketStatusId = $("#ticket-status-ddl").val();
        const taskName = $("#task-name").val();
        const description = $("#description").val();
        const assigneeId = $("#assignee-ddl").val();
        const startDate = $("#start-date").val();
        const endDate = $("#end-date").val();
        const estimate = $("#estimated-time").val();
        const completeness = $("#completeness-ddl").val();
        const priorityId = $("#priority-ddl").val();


        const model = {
            ProjectId: projectId,
            TicketTypeId: ticketTypeId,
            TicketStatusId: ticketStatusId,
            TaskName: taskName,
            Description: description,
            AssigneeId: assigneeId,
            StartDate: startDate,
            EndDate: endDate,
            Estimate: estimate,
            Completeness: completeness,
            PriorityId: priorityId
        };

        return model;
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