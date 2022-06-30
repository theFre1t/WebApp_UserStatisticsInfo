using Microsoft.AspNetCore.Mvc;
using WebApp_UserStatisticsInfo.EF.Context;
using WebApp_UserStatisticsInfo.EF.Models;
using WebApp_UserStatisticsInfo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp_UserStatisticsInfo.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        IConfiguration configuration;
        public ReportController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpGet("{id_query}")]
        public ActionResult info(Guid? id_query)
        {
            try
            {
                if (id_query == null)
                {
                    return BadRequest("Идентификатор отсутствует");
                }

                using (UserStaticContext db = new UserStaticContext())
                {
                    var source = db.User_Statistics
                        .Where(w => w.id_query == id_query).FirstOrDefault() ?? null;

                    if (source != null)
                    {
                        int percent = source.percent; //Что бы не получить два разный %, сразу запоминаем, иначе при 99% можно получить результат
                        var result = new AnswerBody()
                        {
                            id_query = source.id_query, 
                            percent = percent,
                            result = percent < 100 ? null : new Result()
                            {
                                user_id = source.user_id,
                                period_start = source.period_start,
                                period_end = source.period_end,
                                count_auth = source.count_auth
                            }
                        };
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("По данному идентификатору ничего не найдено");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> user_statistics(RequestBody body)
        {
            DateTime dt_create_query = DateTime.Now; // Сразу записываем дату, что бы не получить разницу в миллисекундах
            try
            {
                if (body == null)
                {
                    return BadRequest("Тело запроса пустое");
                }

                using (UserStaticContext db = new UserStaticContext())
                {
                    var user_static = new User_Statistics()
                    {
                        id_query = Guid.NewGuid(),
                        user_id = body.user_id,
                        period_start = body.period_start,
                        period_end = body.period_end,
                        count_auth = new Random().Next(0, 25),
                        dt_create_query = dt_create_query,
                        dt_finish_process = dt_create_query.AddMilliseconds(double.Parse(configuration["TimeProcessing"])),
                    };
                    db.Add(user_static);
                    bool result = await db.SaveChangesAsync() > 0;

                    if (result)
                    {
                        return Ok("{\"id_query\": \"" + user_static.id_query + "\" }");
                    }
                    else
                    {
                        return BadRequest("Не удалось внести статистику");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
