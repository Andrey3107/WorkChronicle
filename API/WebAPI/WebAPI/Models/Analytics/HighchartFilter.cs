namespace WebAPI.Models.Analytics
{
    public class HighchartFilter
    {
        public long ProjectId { get; set; }

        public long AssigneeId { get; set; }

        public int PlaceId { get; set; }

        public int StatusId { get; set; }

        public int PriorityId { get; set; }

        public int TypeId { get; set; }
    }
}
