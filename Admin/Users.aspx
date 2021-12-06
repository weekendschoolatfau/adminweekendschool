<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="adminweekendschool.Admin.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


     
    <div class="row ">
        <div class="col-md-12 ">
                <div class="headerMessage" id="headerMessage" runat="server" >
                    Notification: 
                </div>
        </div>
    </div>
    <div class="row ">
        <div class="col-md-12 ">
                <div class="AccountTitle">
                    <div id="titleMsg" runat="server" > Weekend School Users</div>
                    </div>

                </div>
         </div>
    
 

      
<br />
<br /><div align="center">
      <asp:datagrid id="dgUsersList" runat="server" ToolTip="Weekend School Users " Width="80%"
									                    CellPadding="4" AutoGenerateColumns="False" DataKeyField="STAFF_USERS_ID"
                                    OnEditCommand  ="dg_Edit" 
                                    OnCancelCommand="dg_Cancel" 
                                    OnUpdateCommand="dg_Update"
									                            BorderColor= "Black" BorderWidth="2px"  CellSpacing="1" >

                    <AlternatingItemStyle Font-Size="10pt" Font-Names="Arial" BackColor="#CCCCCC"></AlternatingItemStyle>
                    <ItemStyle Font-Size="10pt" Font-Names="Arial" ForeColor="Black"></ItemStyle>
                    <HeaderStyle Font-Size="10pt" Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="#003366"></HeaderStyle>
                    <Columns>

                    <asp:TemplateColumn HeaderText="User Id">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblUserId" Text='<%# DataBinder.Eval(Container, "DataItem.STAFF_USERS_ID") %>'>
                    </asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Name">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblUserName" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
                    </asp:Label>
                    </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Role Name">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblRoleName" Text='<%#  DataBinder.Eval(Container, "DataItem.ROLE_NAME") %>'>
                    </asp:Label>
                    </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Active">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblActive" Text='<%#  DataBinder.Eval(Container, "DataItem.Active") %>'>
                    </asp:Label>
                    </ItemTemplate>
                    </asp:TemplateColumn>

                  

                   </Columns>
                    </asp:datagrid>
                    <br />
    <br />

         <table class="TblParentInformation" border="1" width="40%"> 
        <tr><td colspan="2"><div class="AccountSubTitle">Add New User</div> </td></tr>
        <tr><td  class="TblLabelLeft">User Name</td><td  class="TblLabelLeft">Role</td></tr>
        <tr><td><asp:DropDownList runat="server" ID="ddbUsersList" ></asp:DropDownList></td><td><asp:DropDownList runat="server" ID="ddbRolesList" ></asp:DropDownList></td></tr>
        <tr><td colspan="2" align="center"><asp:Button runat="server" ID="btnAddNewUser" Text="Add New User" OnClick="btnAddNewUser_Click" /></td></tr>
    </table>
     <br />
    <br />
    <asp:Label runat="server" ID="lblMessage" Font-Bold="True" ForeColor="Red"></asp:Label>
    </div>
   
  
  
<script type = "text/javascript">

    function addNewUser()
    {
        var sel = document.getElementById("<%=ddbUsersList.ClientID%>").value;
        if (sel == 0) {
            alert("User Name is required field");
            document.getElementById("<%=ddbUsersList.ClientID%>").focus();

            return false;
        }

        var sel = document.getElementById("<%=ddbRolesList.ClientID%>").value;
        if (sel == 0) {
            alert("Role is required field");
            document.getElementById("<%=ddbRolesList.ClientID%>").focus();

            return false;
        }


      
    }

</script>

</asp:Content>
