using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cardcost.Core.Services.interfaces
{
    public interface IRedisService
    {
        void Connect();
        Task Set(string key, string value);
        Task<string> Get(string key);
    }
}
