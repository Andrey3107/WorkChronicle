class Tasks {
    constructor() {
        this.newColumnBody = $("#new .column-body");
        this.inProgressColumnBody = $("#in-progress .column-body");
        this.resolvedColumnBody = $("#resolved .column-body");

    }

    Init() {
        const me = this;

        me.InitColumns();
    }

    InitColumns() {
        const me = this;

        this.GetDataList(null, "GET", "/Tasks/GetTicketsByUser").then((response) => {
            if (response && response.success) {
                response.data.forEach(item => {
                    const card = me.CreateCard(item.id, item.projectName, item.ticketName, item.priority);

                    if (item.ticketStatusId == 1) {
                        me.newColumnBody.append(card);
                    }

                    if (item.ticketStatusId == 2) {
                        me.inProgressColumnBody.append(card);
                    }

                    if (item.ticketStatusId == 3) {
                        me.resolvedColumnBody.append(card);
                    }
                });
            }
            else if (response && !response.success) {
                alert(response.errorMessage);
            }

            //$(".column-card").on("click", function () {
            //    window.location.href = "/Tasks/CreateTask?id=" + $(this).find(".card-id").data("value");
            //});
            me.InitTaskModal();
        });
    }

    InitTaskModal() {
        const me = this;

        $(".column-card").on("click", function () {
            me.SetTaskData(me, this);
        });
    }

    SetTaskData(context, card) {
        const taskId = $(card).find(".card-id").data("value");

        $("#task-modal").find("#task-id").val(taskId);

        this.GetDataList(null, "GET", "/Tasks/GetTicketDetailsById?id=" + $("#task-modal").find("#task-id").val()).then((response) => {
            if (response && response.success) {
                $("#task-modal").find("#project-name-span").text(response.data.projectName);
                $("#task-modal").find("#ticket-type-span").text(response.data.type);
                $("#task-modal").find("#ticket-status-span").text(response.data.status);
                $("#task-modal").find("#ticket-name-span").text(response.data.name);
                $("#task-modal").find("#ticket-description-span").text(response.data.description);
                $("#task-modal").find("#assignee-span").text(response.data.assigneeName);
                $("#task-modal").find("#priority-span").text(response.data.priority);
                $("#task-modal").find("#completeness-span").text(response.data.completeness);
                $("#task-modal").find("#estimate-span").text(response.data.estimate);
                $("#task-modal").find("#spent-time-span").text(response.data.spentTime);
                $("#task-modal").find("#start-date-span").text(response.data.startDate);
                $("#task-modal").find("#end-date-span").text(response.data.endDate);
            }
            else if (response && !response.success) {
                alert(response.errorMessage);
            }
        });

        context.InitModalButtons();

        $("#task-modal").modal("show");
    }

    InitModalButtons() {
        $("#btn-edit-ticket").on("click", () => {
            window.location.href = "/Tasks/CreateTask?id=" + $("#task-modal").find("#task-id").val();
        });

        $("#btn-log-time").on("click", () => {
            window.location.href = "/Tasks/LogTime?ticketId=" + $("#task-modal").find("#task-id").val();
        });
    }

    CreateCard(id, projectName, ticketName, priority) {
        const card = document.createElement("div");
        const cardId = document.createElement("div");
        const cardProject = document.createElement("div");
        const cardName = document.createElement("div");
        const cardPriority = document.createElement("div");
        const boldText = document.createElement("b");
        const spanText = document.createElement("span");

        $(cardId).attr("data-value", id);
        $(cardId).attr("hidden");
        $(card).addClass("column-card");
        $(cardId).addClass("card-id");
        $(cardProject).addClass("card-project");
        $(cardName).addClass("card-name");
        $(cardPriority).addClass("card-priority");

        boldText.textContent = "Priority: ";
        spanText.textContent = priority;

        cardPriority.appendChild(boldText);
        cardPriority.appendChild(spanText);

        cardProject.textContent = projectName;
        cardName.textContent = ticketName;

        card.appendChild(cardId);
        card.appendChild(cardProject);
        card.appendChild(cardName);
        card.appendChild(cardPriority);

        return card;
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