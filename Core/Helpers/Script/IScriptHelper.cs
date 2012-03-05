using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helpers.Script
{
    public interface IScriptHelper
    {
        string ScriptName { get; }
        string Message { get; }

        string GetSuccessScript();
        string GetErrorScript();
    }
}
