using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BackendGeneral.Interfaces
{
    public interface ICache
    {
        void Set(string identifier, object item);

        T Get<T>(string expression);

        void Remove(string expression);
    }
}
