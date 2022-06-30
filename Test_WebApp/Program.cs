
using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Test_WebApp.Models;

namespace Test_WebApp
{
    class Program
    {

        static string address = "https://localhost:44345/";
        static void Main(string[] args)
        {
            string id_query = test_Add();
            Thread.Sleep(30000);
            ConsoleWriteLine(test_Get(id_query));
            Thread.Sleep(15000);
            ConsoleWriteLine(test_Get(id_query));
            Thread.Sleep(15000);
            ConsoleWriteLine(test_Get(id_query));
        }

        private static string test_Add()
        {
            HttpRequest request = addHeaders();

            RequestBody body = new RequestBody()
            {
                user_id = Guid.NewGuid(),
                period_start = new DateTime(2022, 06, 29),
                period_end = new DateTime(2022, 06, 30)
            };
            string jsonbody = JsonConvert.SerializeObject(body, Formatting.None);

            string answer = request.Post(address + "report/user_statistics", jsonbody, "application/json").ToString();
            return JObject.Parse(answer)["id_query"].ToString();
        }

        private static string test_Get(string id_query)
        {
            try
            {
                HttpRequest request = addHeaders();
                string answer = request.Get(address + "report/info/" + id_query).ToString();
                return answer;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private static void ConsoleWriteLine(string answerBody)
        {
            Console.WriteLine(answerBody);
        }


        private static HttpRequest addHeaders()
        {
            HttpRequest request = new HttpRequest();
            request.UserAgent = Http.RandomUserAgent();
            return request;
        }
    }
}