namespace WebApp_UserStatisticsInfo.Models
{
    public class RequestBody
    {
        public Guid user_id { get; set; }
        public DateTime period_start { get; set; }
        public DateTime period_end { get; set; }
    }
}
