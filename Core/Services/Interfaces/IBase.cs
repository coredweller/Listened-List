using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services.Interfaces
{
    public interface IBase<T>
    {
        void SaveCommit( T item, out bool success );
        void Save( T item, out bool success );
        void Delete( T item );
    }
}
