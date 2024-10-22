using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Services.Contract
{
    public interface ICasheService
    {
        Task SetCasheKeyAsync(string key, object rasponse, TimeSpan expireTime);
        Task<string> GetCasheKeyAsync (string key);
    }
}
