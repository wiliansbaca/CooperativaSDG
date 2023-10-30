<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Cooperativa.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <h2>Login</h2>
            <asp:Label runat="server" Text="Username" />
            <asp:TextBox runat="server" ID="txtUsername" />
            <br />
            <asp:Label runat="server" Text="Password" />
            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
            <br />
            <asp:Button runat="server" ID="btnLogin" Text="Login" OnClick="btnLogin_Click" />
            <br />
            <asp:Label runat="server" ID="lblMessage" />
            <br />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
         </div>
    </form>
</body>
</html>
