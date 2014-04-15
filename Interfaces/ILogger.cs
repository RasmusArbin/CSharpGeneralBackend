using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGeneral.Interfaces
{
    public interface ILogger
    {
        void LogInsert<T>(T entity);

        void LogDelete<T>(T entity);

        void LogUpdate<T>(T entity);

        void Log(string loggingMessage);
    }
}
