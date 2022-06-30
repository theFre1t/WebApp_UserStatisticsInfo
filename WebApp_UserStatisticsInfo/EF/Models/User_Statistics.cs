namespace WebApp_UserStatisticsInfo.EF.Models
{
    public class User_Statistics
    {
        public Guid id_query { get; set; }
        public Guid user_id { get; set; }
        public DateTime period_start { get; set; }
        public DateTime period_end { get; set; }
        public int? count_auth { get; set; }
        public DateTime dt_create_query { get; set; }
        public DateTime dt_finish_process { get; set; }
        public int percent { 

            get 
            {
                int percent_now;
                if(dt_finish_process <= DateTime.Now)
                {
                    percent_now = 100;
                }
                else
                {
                    double time_full_processing = dt_finish_process.Subtract(dt_create_query).TotalMilliseconds;
                    double time_now_processing = DateTime.Now.Subtract(dt_create_query).TotalMilliseconds;
                    percent_now = Convert.ToInt32(Math.Floor(time_now_processing / time_full_processing * 100));
                }
                return percent_now;
            } 
        }
    }
}
