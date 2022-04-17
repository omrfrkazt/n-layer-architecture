using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Cache
{
    public interface ICacheService
    {

        T Get<T>(string key, Func<T> acquire);


        Task<T> GetAsync<T>(string key, Func<Task<T>> acquire);


        void Remove(string key);


        void Set(string key, object data);

    }
}
