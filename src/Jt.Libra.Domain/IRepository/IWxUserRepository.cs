using Jt.Libra.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Libra.Domain.IRepository
{
    public interface IWxUserRepository
    {
        Task<WxUserInfo> GetAsync(string openId);

        Task<int> InsertAsync(WxUserInfo entity);

        Task<int> UpdateAsync(WxUserInfo entity);
    }
}