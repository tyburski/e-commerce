using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce
{
    internal class ParameterHandler : IDisposable
    {
        bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                return;
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public object GetParam(Item item, string parameter)
        {
             var value = Task.Run(() => item.parameters.GetType().GetProperty($"{parameter}").GetValue(item.parameters, null));
            return value.Result;
        }
    }
}
