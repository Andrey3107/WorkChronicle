namespace WorkChronicle.WebApiClients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Analytics;

    public partial class WebApiClient
    {
        public Task<List<AnalyticsChartViewModel>> GetAnalyticsHighchartData(HighchartFilter filter)
        {
            return PostAsync<HighchartFilter, List<AnalyticsChartViewModel>>("/Analytics/GetAnalyticsData", filter);
        }
    }
}
