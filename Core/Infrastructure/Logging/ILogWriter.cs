using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Core.Infrastructure.Logging
{
    public interface ILogWriter
    {
        TextWriter Get();
    }
}
