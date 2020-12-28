<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DOT_AREA_2017.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <center>
            <h1>Register Page</h1>
            <p>
                <h2><asp:Label ID="Label1" runat="server" ForeColor="#33CC33" Text="Label" Visible="False"></asp:Label></h2>
            </p>
            <p>
                <table ID="Table1" runat="server" Height="350px" Width="450px" border="1">
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
                        <td><center>Email : </center></td>
                        <td>
                            <center><asp:TextBox ID="TextEmail" runat="server"></asp:TextBox></center>
                        </td>
                    </tr>
                    <tr>
                        <td><center>Username : </center></td>
                        <td>
                            <center><asp:TextBox ID="TextUser" runat="server"></asp:TextBox></center>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [User]"></asp:SqlDataSource>
                        </td>
                        <td><center>
                            <asp:Button ID="ButtonRegister" runat="server" OnClick="ButtonRegister_Click" Text="Register" />
                            </center></td>
                    </tr>
                </table>
            </p>
        </center>
    </div>

</asp:Content>
