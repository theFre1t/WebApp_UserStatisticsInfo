namespace Test_WebApp.Models
{
    public class AnswerBody
    {
        public Guid id_query { get; set; }
        public int percent { get; set; }
        public Result? result { get; set; }
    }

    public class Result
    {
        public Guid user_id { get; set; }
        public DateTime period_start { get; set; }
        public DateTime period_end { get; set; }
        public int? count_auth { get; set; }
    }
}
