<%@ Page Title="Facebook" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Facebook.aspx.cs" Inherits="DOT_AREA_2017.Facebook" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <div>
        <center>

        <h2>
            Post message on Facebook
        </h2>
            </br></br>
            <p>
                <h3><asp:Label ID="LabelResult" runat="server" Text="Label" Visible="False"></asp:Label></h3>
            </p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Destination :"></asp:Label>
            &nbsp;
           <asp:TextBox ID="TextDestination" runat="server"></asp:TextBox>
            </br></br>
            <asp:Label ID="Label2" runat="server" Text="Message :"></asp:Label>
            &nbsp;
            <textarea ID="TextFacebook" name="S1" style="width: 370px; height: 150px" runat="server" placeholder="Type here..."></textarea>
        </p>
        <p>
            &nbsp;
        </p>
              <table class="table-fill">
                    <thead>
                        <tr>
                            <th class="text-left">Where</th>
                            <th class="text-left">Reaction</th>
                        </tr>
                    </thead>
                    <tbody class="table-hover">
                        <tr>
                            <td class="text-left">On destination mail</td>
                            <td class="text-left"><asp:Button ID="ButtonMail" runat="server" Text="Send" OnClick="ButtonMail_Click"></asp:Button></td>
                        </tr>
                        <tr>
                            <td class="text-left">On your phone</td>
                            <td class="text-left"><asp:Button ID="ButtonPhone" runat="server" Text="Send" OnClick="ButtonPhone_Click"></asp:Button></td>
                        </tr>
                        <tr>
                            <td class="text-left">Post the same message on Twitter too</td>
                            <td class="text-left"><asp:Button ID="ButtonFb" runat="server" Text="Post" OnClick="ButtonFb_Click"></asp:Button></td>
                        </tr>
                    </tbody>
                </table>
        </center>
    </div>

</asp:Content>
