using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Libra.Application
{
    public interface IUserService: IServices
    {
        Task<AccessTokenOutput> SessionKey(AccessTokenInput input);
    }
}
