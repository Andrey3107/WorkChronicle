namespace WorkChronicle.Models.Analytics
{
    using System.Collections.Generic;

    public class AnalyticsChartViewModel
    {
        public string Name { get; set; }

        public List<AnalyticsDataModel> Data { get; set; }
    }
}
