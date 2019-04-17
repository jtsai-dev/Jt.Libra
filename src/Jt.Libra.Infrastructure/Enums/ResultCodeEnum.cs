
namespace Jt.Libra.Infrastructure.Enums
{
    public enum ResultCodeEnum : int
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// Token超时，需要重新认证
        /// </summary>
        TokenOvertime = 1,

        /// <summary>
        /// 认证失败
        /// </summary>
        Unauthorized = 2,

        /// <summary>
        /// 业务执行失败
        /// </summary>
        BussinessError = 3,

        /// <summary>
        /// 参数异常
        /// </summary>
        UnValid = 4,

        /// <summary>
        /// 未知异常
        /// </summary>
        UnknownError = 5,

        /// <summary>
        /// 无权限
        /// </summary>
        NoPermission = 6,
    }
}
