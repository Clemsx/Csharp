<%@ Page Title="Logged" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Logged.aspx.cs" Inherits="DOT_AREA_2017.Logged" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <center>
            <h2>
                <asp:Label ID="LabelUsername" runat="server" Text="Label" Visible="False"></asp:Label>
            </h2>
            
            <h3>
                Choose an API : ACTION
            </h3>
            <p>

                <table class="table-fill">
                    <thead>
                        <tr>
                            <th><center>API Name</center></th>
                           <!--<th class="text-left">Link</th>-->
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td><center><a href="API/Twitter.aspx">Twitter</a></center></td>
                        </tr>
                        <!--
                        <tr>
                            <td class="text-left">SendInBlue</td>
                            <td class="text-left"><a href="API/SendInBlue.aspx">Click me</a></td>
                        </tr>
                        <tr>
                            <td class="text-left">ViaNett SMS</td>
                            <td class="text-left"><a href="API/Vianett.aspx">Click me</a></td>
                        </tr>
                        <tr>
                            <td class="text-left">Facebook</td>
                            <td class="text-left"><a href="API/Facebook.aspx">Click me</a></td>
                        </tr>
                        <tr>
                            <td class="text-left">May</td>
                            <td class="text-left">$ 98,000.00</td>
                        </tr>
                        -->
                    </tbody>
                </table>
            </p>

        </center>
    </div>

</asp:Content>
