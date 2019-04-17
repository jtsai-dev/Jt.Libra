using Jt.Libra.Infrastructure.Enums;
using Jt.Libra.Infrastructure.Exceptions;
using Jt.Libra.Infrastructure.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Libra.WebApi.MiddleWare
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private IHostingEnvironment environment;
        private readonly IConfiguration configuration;
        private readonly JsonSerializerSettings jsonSerializerSettings;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostingEnvironment environment,
            IConfiguration configuration,
            JsonSerializerSettings jsonSerializerSettings)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
            this.configuration = configuration;
            this.jsonSerializerSettings = jsonSerializerSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private async Task HandleException(HttpContext context, Exception e)
        {
            string error = "";
            void ReadException(Exception ex)
            {
                error += $"{ex.Message} | {ex.StackTrace} | {ex.InnerException}";
                if (ex.InnerException != null)
                {
                    ReadException(ex.InnerException);
                }
            }
            ReadException(e);

            // wrap friendly result for exception
            string message = e.Message;
            int code = AnalyzingException(e, ref message);
            var result = new Models.Result(code, message);
            var json = JsonConvert.SerializeObject(result, jsonSerializerSettings);

            var input = AnalyzingRequestParameters(context);
            logger.LogError(e, $"msg: {message} \r\n- input: {input} \r\n- err: {error}");
            
            context.Response.Clear();
            context.Response.ContentType = "text/json;charset=utf-8;";
            await context.Response.WriteAsync(json);
        }

        private string AnalyzingRequestParameters(HttpContext context)
        {
            var input = new StringBuilder();
            if (context.Request.QueryString.HasValue)
            {
                input.AppendLine();
                input.Append($" queryString: {context.Request.QueryString.Value}");
            }
            Microsoft.Extensions.Primitives.StringValues token;
            context.Request.Headers.TryGetValue("token", out token);
            if (token.Count > 0)
            {
                input.AppendLine();
                input.Append($" token: {token[0]}");
            }
            if (context.Request.ContentLength.HasValue)
            {
                using (var request = new StreamReader(context.Request.Body))
                {
                    //request.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyText = request.ReadToEnd();
                    input.AppendLine();
                    input.Append($"body: {bodyText}");
                }
            }

            return input.ToString();
        }

        private int AnalyzingException(Exception ex, ref string message)
        {
            int code = (int)ResultCodeEnum.UnknownError;
            if (ex is JtException)
            {
                var codeEnum = (ex as JtException).Code;
                code = (int)codeEnum;
                if (string.IsNullOrEmpty(message))
                {
                    switch (codeEnum)
                    {
                        case ResultCodeEnum.TokenOvertime:
                            message = "认证超时，请重新登陆";
                            break;
                        case ResultCodeEnum.Unauthorized:
                            message = "认证失败，请确认凭证或重新登陆";
                            break;
                        case ResultCodeEnum.BussinessError:
                            message = "操作失败，请确认流程";
                            break;
                        case ResultCodeEnum.UnValid:
                            message = "操作失败，请确认请求的数据无异常";
                            break;
                        case ResultCodeEnum.NoPermission:
                            message = "操作失败，当前账号无权限";
                            break;
                        case ResultCodeEnum.UnknownError:
                        default:
                            message = "操作失败，出现未知异常";
                            break;
                    }
                }
            }

            return code;
        }
    }
}