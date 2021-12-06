<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="adminweekendschool.Admin.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div align="center">
    <div class="container">
          <div class="row" >
              <div class="col">
              </div>

              <div class="col">
                  <div class="loginPage  rounded">
                    <div class="loginMargin">
                         <h4 class="form-signin-heading"> Login <span class="requiredMessage"> (* Required Field)</span></h4>
                        <div align="left">
                        <label for="txtUsername" class="form-signin-heading loginSeparation"> Username or Email<span class="requiredMessage"> *</span></label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"  value=""  placeholder="Enter Username or Email" />
                      

                        <label for="txtPassword" class="form-signin-heading loginSeparation">Password<span class="requiredMessage"> *</span></label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" value="" CssClass="form-control" placeholder="Enter Password"  />
     
                      <br />

                        <asp:Button ID="btnLogin" Text="Login" runat="server" OnClick="btnLogin_Click" Class="btn btn-primary" />
                        <br /> <br />
                        <asp:LinkButton runat="server"  CssClass="loginLink" ID="ForgetPassword" OnClick="ForgetPassword_Click">Forgot your password? </asp:LinkButton>
                        <br />
                         <br />
                        <%-- <asp:LinkButton runat="server"  CssClass="loginLink" ID="ForgrtUsername" OnClick="ForgrtUsername_Click">Forgot your Username? </asp:LinkButton>
                        <br />
                         <br />--%>
                        <div id="dvMessage"  runat="server"   class="alert alert-danger">
                            <strong>Error!</strong>
                            <asp:Label ID="lblMessage" runat="server"  />
                        </div>
                            </div>
                     </div>
                  </div>
              </div>

              <div class="col">
             
              </div>
          
         </div>

    </div>
    </div>
    
<script type = "text/javascript">

    function ValidateLogin()
    {
        var txtUserName = document.getElementById("<%=txtUsername.ClientID%>").value;

        if (txtUserName.trim() == "")
        {
            alert("Username or Email is required field.");
            document.getElementById("<%=txtUsername.ClientID%>").focus();
                return false;
        }

        var txtPassword = document.getElementById("<%=txtPassword.ClientID%>").value;

        if (txtPassword.trim() == "") {
            alert("Password is required field.");
            document.getElementById("<%=txtPassword.ClientID%>").focus();
            return false;
        }

        return true;
    }
</script>

</asp:Content>
