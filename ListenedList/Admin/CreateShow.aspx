<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateShow.aspx.cs" Inherits="ListenedList.Admin.CreateShow"
    MasterPageFile="~/Masters/Genius.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            Create Show</h2>
    </div>
    <br />
    <div>
        <table>
            <tr>
                <td>
                    Venue Name:
                </td>
                <td>
                    <asp:TextBox ID="txtVenue" runat="server" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqVenue" runat="server" ControlToValidate="txtVenue" Text="You must enter a venue"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    City:
                </td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCity" Text="You must enter a city"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    State:
                </td>
                <td>
                    <asp:DropDownList ID="ddlState" runat="server">
                    <asp:ListItem Value="" Selected="true">Select State</asp:ListItem>
                      <asp:ListItem Value="AL">Alabama</asp:ListItem>
                    <asp:ListItem Value="AK">Alaska</asp:ListItem>
                    <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                    <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                    <asp:ListItem Value="CA">California</asp:ListItem>
                    <asp:ListItem Value="CO">Colorado</asp:ListItem>
                    <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                    <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                    <asp:ListItem Value="DE">Delaware</asp:ListItem>
                    <asp:ListItem Value="FL">Florida</asp:ListItem>
                    <asp:ListItem Value="GA">Georgia</asp:ListItem>
                    <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                    <asp:ListItem Value="ID">Idaho</asp:ListItem>
                    <asp:ListItem Value="IL">Illinois</asp:ListItem>
                    <asp:ListItem Value="IN">Indiana</asp:ListItem>
                    <asp:ListItem Value="IA">Iowa</asp:ListItem>
                    <asp:ListItem Value="KS">Kansas</asp:ListItem>
                    <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                    <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                    <asp:ListItem Value="ME">Maine</asp:ListItem>
                    <asp:ListItem Value="MD">Maryland</asp:ListItem>
                    <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                    <asp:ListItem Value="MI">Michigan</asp:ListItem>
                    <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                    <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                    <asp:ListItem Value="MO">Missouri</asp:ListItem>
                    <asp:ListItem Value="MT">Montana</asp:ListItem>
                    <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                    <asp:ListItem Value="NV">Nevada</asp:ListItem>
                    <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                    <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                    <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                    <asp:ListItem Value="NY">New York</asp:ListItem>
                    <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                    <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                    <asp:ListItem Value="OH">Ohio</asp:ListItem>
                    <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                    <asp:ListItem Value="OR">Oregon</asp:ListItem>
                    <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                    <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                    <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                    <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                    <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                    <asp:ListItem Value="TX">Texas</asp:ListItem>
                    <asp:ListItem Value="UT">Utah</asp:ListItem>
                    <asp:ListItem Value="VT">Vermont</asp:ListItem>
                    <asp:ListItem Value="VA">Virginia</asp:ListItem>
                    <asp:ListItem Value="WA">Washington</asp:ListItem>
                    <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                    <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlState" Text="You must choose a state"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Country:
                </td>
                <td>
                    <asp:DropDownList ID="ddlCountry" runat="server">
                     <asp:ListItem Value="" Selected="true">Select Country</asp:ListItem>
                    <asp:ListItem Value="US">United States</asp:ListItem>
                    <asp:ListItem Value="AF">Afghanistan</asp:ListItem>
                    <asp:ListItem Value="AS">American Samoa</asp:ListItem>
                    <asp:ListItem Value="AI">Anguilla</asp:ListItem>
                    <asp:ListItem Value="AQ">Antarctica</asp:ListItem>
                    <asp:ListItem Value="AG">Antigua And Barbuda</asp:ListItem>
                    <asp:ListItem Value="AW">Aruba</asp:ListItem>
                    <asp:ListItem Value="AU">Australia</asp:ListItem>
                    <asp:ListItem Value="AT">Austria</asp:ListItem>
                    <asp:ListItem Value="BS">Bahamas</asp:ListItem>
                    <asp:ListItem Value="BB">Barbados</asp:ListItem>
                    <asp:ListItem Value="BE">Belgium</asp:ListItem>
                    <asp:ListItem Value="BM">Bermuda</asp:ListItem>
                    <asp:ListItem Value="BV">Bouvet Island</asp:ListItem>
                    <asp:ListItem Value="BR">Brazil</asp:ListItem>
                    <asp:ListItem Value="CA">Canada</asp:ListItem>
                    <asp:ListItem Value="CV">Cape Verde</asp:ListItem>
                    <asp:ListItem Value="KY">Cayman Islands</asp:ListItem>
                    <asp:ListItem Value="CL">Chile</asp:ListItem>
                    <asp:ListItem Value="CN">China</asp:ListItem>
                    <asp:ListItem Value="CX">Christmas Island</asp:ListItem>
                    <asp:ListItem Value="CC">Cocos (Keeling) Islands</asp:ListItem>
                    <asp:ListItem Value="CO">Colombia</asp:ListItem>
                    <asp:ListItem Value="CK">Cook Islands</asp:ListItem>
                    <asp:ListItem Value="CR">Costa Rica</asp:ListItem>
                    <asp:ListItem Value="CI">Cote D'Ivoire</asp:ListItem>
                    <asp:ListItem Value="CZ">Czech Republic</asp:ListItem>
                    <asp:ListItem Value="DK">Denmark</asp:ListItem>
                    <asp:ListItem Value="DJ">Djibouti</asp:ListItem>
                    <asp:ListItem Value="DO">Dominican Republic</asp:ListItem>
                    <asp:ListItem Value="EC">Ecuador</asp:ListItem>
                    <asp:ListItem Value="FI">Finland</asp:ListItem>
                    <asp:ListItem Value="FR">France</asp:ListItem>
                    <asp:ListItem Value="DE">Germany</asp:ListItem>
                    <asp:ListItem Value="GH">Ghana</asp:ListItem>
                    <asp:ListItem Value="GR">Greece</asp:ListItem>
                    <asp:ListItem Value="GL">Greenland</asp:ListItem>
                    <asp:ListItem Value="GU">Guam</asp:ListItem>
                    <asp:ListItem Value="GT">Guatemala</asp:ListItem>
                    <asp:ListItem Value="HT">Haiti</asp:ListItem>
                    <asp:ListItem Value="HN">Honduras</asp:ListItem>
                    <asp:ListItem Value="HK">Hong Kong</asp:ListItem>
                    <asp:ListItem Value="HU">Hungary</asp:ListItem>
                    <asp:ListItem Value="IN">India</asp:ListItem>
                    <asp:ListItem Value="ID">Indonesia</asp:ListItem>
                    <asp:ListItem Value="IE">Ireland</asp:ListItem>
                    <asp:ListItem Value="IT">Italy</asp:ListItem>
                    <asp:ListItem Value="JM">Jamaica</asp:ListItem>
                    <asp:ListItem Value="JP">Japan</asp:ListItem>
                    <asp:ListItem Value="KP">Korea, Dem People'S Republic</asp:ListItem>
                    <asp:ListItem Value="KR">Korea, Republic Of</asp:ListItem>
                    <asp:ListItem Value="LB">Lebanon</asp:ListItem>
                    <asp:ListItem Value="MG">Madagascar</asp:ListItem>
                    <asp:ListItem Value="MW">Malawi</asp:ListItem>
                    <asp:ListItem Value="MY">Malaysia</asp:ListItem>
                    <asp:ListItem Value="ML">Mali</asp:ListItem>
                    <asp:ListItem Value="MH">Marshall Islands</asp:ListItem>
                    <asp:ListItem Value="MQ">Martinique</asp:ListItem>
                    <asp:ListItem Value="MX">Mexico</asp:ListItem>
                    <asp:ListItem Value="MA">Morocco</asp:ListItem>
                    <asp:ListItem Value="NP">Nepal</asp:ListItem>
                    <asp:ListItem Value="NL">Netherlands</asp:ListItem>
                    <asp:ListItem Value="AN">Netherlands Ant Illes</asp:ListItem>
                    <asp:ListItem Value="NC">New Caledonia</asp:ListItem>
                    <asp:ListItem Value="NZ">New Zealand</asp:ListItem>
                    <asp:ListItem Value="NF">Norfolk Island</asp:ListItem>
                    <asp:ListItem Value="MP">Northern Mariana Islands</asp:ListItem>
                    <asp:ListItem Value="NO">Norway</asp:ListItem>
                    <asp:ListItem Value="PA">Panama</asp:ListItem>
                    <asp:ListItem Value="PG">Papua New Guinea</asp:ListItem>
                    <asp:ListItem Value="PE">Peru</asp:ListItem>
                    <asp:ListItem Value="PH">Philippines</asp:ListItem>
                    <asp:ListItem Value="PL">Poland</asp:ListItem>
                    <asp:ListItem Value="PT">Portugal</asp:ListItem>
                    <asp:ListItem Value="PR">Puerto Rico</asp:ListItem>
                    <asp:ListItem Value="KN">Saint Kitts And Nevis</asp:ListItem>
                    <asp:ListItem Value="LC">Saint Lucia</asp:ListItem>
                    <asp:ListItem Value="VC">Saint Vincent, The Grenadines</asp:ListItem>
                    <asp:ListItem Value="WS">Samoa</asp:ListItem>
                    <asp:ListItem Value="SG">Singapore</asp:ListItem>
                    <asp:ListItem Value="SB">Solomon Islands</asp:ListItem>
                    <asp:ListItem Value="ES">Spain</asp:ListItem>
                    <asp:ListItem Value="LK">Sri Lanka</asp:ListItem>
                    <asp:ListItem Value="SE">Sweden</asp:ListItem>
                    <asp:ListItem Value="CH">Switzerland</asp:ListItem>
                    <asp:ListItem Value="TW">Taiwan</asp:ListItem>
                    <asp:ListItem Value="TH">Thailand</asp:ListItem>
                    <asp:ListItem Value="TR">Turkey</asp:ListItem>
                    <asp:ListItem Value="TC">Turks And Caicos Islands</asp:ListItem>
                    <asp:ListItem Value="UA">Ukraine</asp:ListItem>
                    <asp:ListItem Value="GB">United Kingdom</asp:ListItem>
                    <asp:ListItem Value="UM">United States Minor Is.</asp:ListItem>
                    <asp:ListItem Value="VG">Virgin Islands (British)</asp:ListItem>
                    <asp:ListItem Value="VI">Virgin Islands (U.S.)</asp:ListItem>
                    <asp:ListItem Value="WF">Wallis And Futuna Islands</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCountry" Text="You must choose a country"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Show Date:
                </td>
                <td>
                    <asp:TextBox ID="txtShowDate" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtShowDate" Text="You must enter a show date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Notes:
                </td>
                <td>
                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
