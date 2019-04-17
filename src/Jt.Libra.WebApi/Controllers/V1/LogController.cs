using Jt.Libra.Application.Log;
using Jt.Libra.WebApi.Attrbutes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Jt.Libra.WebApi.Controllers
{
    [WrapResult]
    public class LogController : JtControllerBase
    {
        private readonly ILogger logger;

        public LogController(
             ILogger<LogController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// 消息上报
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public void Log(LogInput input)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            logger.LogInformation($"消息上报: {json}");
        }
    }
}
