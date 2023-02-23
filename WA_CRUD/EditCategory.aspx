<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCategory.aspx.cs" Inherits="WA_CRUD.EditCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>編輯類別</title>
    <script type="text/javascript">
        $(function () {

        });

        function formCheck() {
            var fullName = $("#<%=txtName.ClientID%>").val();
              //  var simpleName = $("#<%=txtSimple.ClientID%>").val();
            if (fullName == "") {
                alert("全名請勿空白");
                return false;
            } else {
                if (confirm("是否編輯類別？")) {
                    return true;
                } else { return false; }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>全名：
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>簡稱：
                    </td>
                    <td>
                        <asp:TextBox ID="txtSimple" runat="server" Width="90px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" OnClientClick="return formCheck();" />
                        &nbsp;<asp:Button ID="BtnCanel" runat="server" Text="取消" OnClick="BtnCanel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
