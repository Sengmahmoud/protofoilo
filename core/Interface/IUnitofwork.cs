using System;
using System.Collections.Generic;
using System.Text;

namespace core.Interface
{
   public interface IUnitofwork<T> where T :class
    {
        IGenericRepository<T> Entity { get; }
        void Save();

    }
}
