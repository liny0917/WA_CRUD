<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="WA_CRUD.CategoryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>類別清單</title>
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
                      if (confirm("是否新增類別？")) {
                          return true;
                      } else { return false; }
                  }
              }

              function checkDelete() {
                  if (confirm("是否刪除類別？")) {
                      return true;

                  } else { return false; }
              }
          </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table>
                    <tr>
                        <td>全名：
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>簡稱：
                        </td>
                        <td>
                            <asp:TextBox ID="txtSimple" runat="server" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_OnClick" OnClientClick="return formCheck();" />
                        </td>
                    </tr>
                </table>

                <asp:Panel runat="server" ID="pl_List">
                    <asp:GridView ID="GVCategory" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="False" OnRowDataBound="GVCategory_OnRowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="編號" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lBtnEdit" runat="server" Text="編輯"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lBtnDelete" runat="server" Text="刪除" OnClick="lBtnDelete_OnClick" OnClientClick="return checkDelete();"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="全名" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="簡稱" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSimpleName" runat="server" Text='<%#Eval("SimpleName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Label ID="lblNodata" runat="server" Text="No Data Found！" Visible="False"></asp:Label>
            </div>
        </div>
    </form>
  
</body>

</html>
