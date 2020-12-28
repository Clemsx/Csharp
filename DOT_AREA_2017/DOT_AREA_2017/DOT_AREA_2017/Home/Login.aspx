<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DOT_AREA_2017.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
         <center>
            <h1>Register Page</h1>
             <p>

                 <h2><asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                    </h2>

             </p>
                <table ID="Table" runat="server" Height="350px" Width="450px" border="1">
                    <tr>
                        <td><center>Login : </center></td>
                        <td>
                           <center><asp:TextBox ID="TextLogin" runat="server"></asp:TextBox> </center>
                        </td>
                    </tr>
                    <tr>
                        <td><center>Password : </center></td>
                        <td>
                            <center><asp:TextBox ID="TextPass" TextMode="Password" runat="server"></asp:TextBox> </center>
                        </td>
                    </tr>
                    <tr>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>
                        </td>
                    <td>
                        <center><asp:Button ID="ButtonLogin" runat="server" OnClick="ButtonLogin_Click" Text="Login" /></center>
                    </td>
                    </tr>
                </table>
        </center>
    </div>
</asp:Content>
