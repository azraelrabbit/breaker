﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebClient.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="formLogin" runat="server">
        Name: <asp:TextBox runat="server" ID="txbName"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txbName">*</asp:RequiredFieldValidator>
        <asp:Button runat="server" ID="btnLogin"
            OnClick="btnLogin_Click" Text="Start Chat" />
    </form>
</body>
</html>

