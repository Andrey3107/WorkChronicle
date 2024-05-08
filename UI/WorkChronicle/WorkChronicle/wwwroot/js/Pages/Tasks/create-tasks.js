class CreateTasks {
    constructor() {
        this.queriesCounter = 0;
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
        $("#btn-edit-task").on("click", () => me.InitCreateTask());
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

            me.CheckSetTask();
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

            me.CheckSetTask();
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

            me.CheckSetTask();
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

            me.CheckSetTask();
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

                me.CheckSetTask();
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
                    if ($("#task-id").val() == 0) {
                        alert("Task was created successfully");
                    }
                    else {
                        alert("Task was updated successfully");
                    }
                    
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
        const taskId = $("#task-id").val();
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
            Id: taskId,
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

    CheckSetTask() {
        const me = this;
        me.queriesCounter--;

        if (me.queriesCounter == 0 && $("#task-id").val() != 0) {
            this.GetDataList(null, "GET", "/Tasks/GetTaskById?id=" + $("#task-id").val()).then((response) => {
                if (response && response.success) {
                    $("#projects-ddl").val(response.data.projectId);
                    $("#ticket-type-ddl").val(response.data.typeId);
                    $("#ticket-status-ddl").val(response.data.ticketStatusId);
                    $("#task-name").val(response.data.name);
                    $("#description").val(response.data.description);
                    $("#assignee-ddl").val(response.data.assigneeId);
                    $("#start-datepicker").datepicker("update", new Date(response.data.created));
                    $("#end-datepicker").datepicker("update", new Date(response.data.dueDate));
                    $("#estimated-time").val(response.data.estimate);
                    $("#completeness-ddl").val(response.data.completeness);
                    $("#priority-ddl").val(response.data.priorityId);
                }
                else if (response && !response.success) {
                    alert("Something went wong!")
                }
            });
        }
    }
    
    // Get data
    GetDataList(data, type, url) {
        const me = this;
        return $.ajax({
            type: type,
            url: url,
            data: data,
            beforeSend: () => { me.queriesCounter++; },
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