using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helpers.Script
{
    public class PromptHelper : IScriptHelper
    {
        //Used when Registering a Startup Script
        public string ScriptName { get; private set; }

        public string Message { get; private set; }

        public PromptHelper( string message ) {
            Message = message;
        }

        public string GetSuccessScript() {
            ScriptName = "success";
            return GenerateScript();
        }

        public string GetErrorScript() {
            ScriptName = "failure";
            return GenerateScript();
        }

        private string GenerateScript() {
            return "<script type=\"text/javascript\"> $.prompt('{0}'); </script>".FormatWith(Message);
        }
    }
}
