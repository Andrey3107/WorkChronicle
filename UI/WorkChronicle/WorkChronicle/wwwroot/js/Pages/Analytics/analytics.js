class Analytics {
    constructor() {
        this.chart;
    }

    Init() {
        const me = this;

        this.InitProjectsDropdown();
        this.InitTaskTypesDropdown();
        this.InitTaskStatusesDropdown();
        this.InitPriorityDropdown();
        this.InitPlace();

        this.InitHighchart();

        $("#update-chart").on("click", () => me.GetHighchartData());
    }

    InitProjectsDropdown() {
        const me = this;

        const projectsDropdown = new Dropdown("#projects-ddl", "name", "id");

        this.GetDataList(null, "GET", "/Analytics/GetProjectList").then((response) => {
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
        });
    }

    InitAsigneeDropdown() {
        const me = this;
        const projectId = $("#projects-ddl").val();
        const assigneeDropdown = new Dropdown("#assignee-ddl", "fullName", "id");

        assigneeDropdown.SetData([]);

        if (projectId) {
            this.GetDataList(null, "GET", "/Analytics/GetAssignees?projectId=" + projectId).then((response) => {
                if (response && response.success) {
                    assigneeDropdown.SetData(response.data);
                }
                else if (response && !response.success) {
                    assigneeDropdown.SetData([]);
                }

                me.GetHighchartData();
            });
        }
    }

    InitTaskTypesDropdown() {
        const me = this;
        const taskTypesDropdown = new Dropdown("#ticket-type-ddl", "description", "id");

        this.GetDataList(null, "GET", "/Analytics/GetTicketTypes").then((response) => {
            if (response && response.success) {
                taskTypesDropdown.SetData(response.data);
            }
            else if (response && !response.success) {
                taskTypesDropdown.SetData([]);
            }
        });
    }

    InitTaskStatusesDropdown() {
        const me = this;
        const taskStatusesDropdown = new Dropdown("#ticket-status-ddl", "description", "id");

        this.GetDataList(null, "GET", "/Analytics/GetTicketStatuses").then((response) => {
            if (response && response.success) {
                taskStatusesDropdown.SetData(response.data);
            }
            else if (response && !response.success) {
                taskStatusesDropdown.SetData([]);
            }
        });
    }

    InitPriorityDropdown() {
        const me = this;
        const priorityDropdown = new Dropdown("#priority-ddl", "description", "id");

        this.GetDataList(null, "GET", "/Analytics/GetPriorities").then((response) => {
            if (response && response.success) {
                priorityDropdown.SetData(response.data);
            }
            else if (response && !response.success) {
                priorityDropdown.SetData([]);
            }
        });
    }

    InitPlace() {
        const me = this;
        const placeDropdown = new Dropdown("#place-ddl", "description", "id");

        this.GetDataList(null, "GET", "/Analytics/GetPlaces").then((response) => {
            if (response && response.success) {
                placeDropdown.SetData(response.data);
            }
            else if (response && !response.success) {
                placeDropdown.SetData([]);
            }
        });
    }

    InitHighchart() {
        this.chart = Highcharts.chart('chart', {
            xAxis: {
                type: 'datetime'
            },
            title: {
                text: ''
            },
            yAxis: {
                title: {
                    text: 'Spent time, h'
                }
            },
            series: [
                {
                    name: 'name',
                    data: [
                        [((new Date("2019-01-01")).getTime()), 1]
                    ]
                }
            ],
            tooltip: {
                formatter: function () {
                    return (new Date(this.x)).toLocaleDateString() + '<br>Spent time: <b>' + this.y + '</b>';
                }
            }
        });

        this.RemoveHighchartData();

        //const data = [
        //    {
        //        name: 'Andrey',
        //        data: [
        //            [((new Date("2024-05-01")).getTime()), 5.5]
        //        ]
        //    }
        //];

        //this.SetChartData(data);
    }

    GetHighchartData() {
        const me = this;
        const data = {
            ProjectId: $("#projects-ddl").val(),
            AssigneeId: $("#assignee-ddl").val(),
            PlaceId: $("#place-ddl").val(),
            StatusId: $("#ticket-status-ddl").val(),
            PriorityId: $("#priority-ddl").val(),
            TypeId: $("#ticket-type-ddl").val()
        };

        this.GetDataList(data, "POST", "/Analytics/GetAnalyticsHighchartData").then((response) => {
            if (response && response.success) {
                me.RemoveHighchartData();
                me.SetChartData(response.data);
            }
            else if (response && !response.success) {
                me.RemoveHighchartData();
            }
        });
    }

    SetChartData(data) {
        //const me = this;

        //me.RemoveHighchartData();

        //data.forEach((element) => me.chart.addSeries(element));

        const me = this;

        me.RemoveHighchartData();

        data.forEach((element) => {
            const item = {
                name: element.name,
                data: []
            };

            element.data.forEach((e) => {
                const i = [((new Date(e.createDate)).getTime()), e.value];
                item.data.push(i);
            });

            me.chart.addSeries(item)
        });
    }

    RemoveHighchartData() {
        this.chart.series.forEach((element) => element.remove(true));
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