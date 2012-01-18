using System.Web.UI.WebControls;
using System.ComponentModel;

namespace ListenedList.Controls
{
    [ToolboxItem(true)]
    public class ShowTextBox : TextBox
    {
        [Browsable(true)]
        public string ShowId{
			get{return ((string)ViewState["ShowId"]);}
			set{ViewState["ShowId"] = value;}
		}

        [Browsable(true)]
        public string Status{
			get{return ((string)ViewState["Status"]);}
			set{ViewState["Status"] = value;}
		}
    }
}