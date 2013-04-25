using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Helpers;
using Core.Services;

namespace ListenedList.Controls
{
    public partial class MonthBoxes : System.Web.UI.UserControl
    {
        public bool Tutorial { get; set; }
        public string Month { get; set; }
        public IList<ShowStatus> Shows { get; set; }
        
        protected void Page_Load( object sender, EventArgs e ) {
            phPlus.Visible = !Tutorial;

            rptMonth.DataSource = Shows;
            rptMonth.DataBind();
        }

        public string GetCssClass( int status, bool attended ) {
            string cssClass = string.Empty;

            switch ( status ) {
                case (int)ListenedStatus.InProgress:
                    cssClass = "defaultButtonYellow";
                    break;
                case (int)ListenedStatus.Finished:
                    cssClass = "defaultButtonOrange";
                    break;
                case (int)ListenedStatus.NeedToListen:
                    cssClass = "defaultButtonGreen";
                    break;
                default:
                    cssClass = "defaultButtonWhite";
                        break;
            }

            if ( attended ) {
                cssClass += " attendedButton";
            }

            return cssClass;
        }

        public string GetStyle() {
            int month = 0;

            if (!int.TryParse( Month, out month ) ) return "display : none;";

            return string.Empty;
        }
    }
}