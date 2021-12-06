<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="adminweekendschool.Admin.ForgetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center">   
    <div class="container">
         
          <div class="row" >
              <div class="col">
              </div>

              <div class="col">
                  <div class="loginPage  rounded">
                    <h2 class="form-signin-heading"> Forgot Password</h2>
                    <div class="loginMargin">
                        <label for="txtUsername" class="form-signin-heading">  Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"  value=""  placeholder="Enter your Email" />
                        <br />
                         <asp:Button ID="btnForgotPassword" Text="Submit" runat="server" OnClick="btnForgotPassword_Click" Class="btn btn-primary" />
                         <br />
                         <br />
                              <asp:LinkButton runat="server"  CssClass="loginLink" ID="btnBackToLogin" OnClick="btnBackToLogin_Click">Back to login page </asp:LinkButton>
                        <br />
                       <br />
                        <div id="dvMessage"  runat="server"   class="alert alert-danger">
                            <strong>Message</strong>
                            <asp:Label ID="lblMessage" runat="server" />
                        </div>
                     </div>
                  </div>
              </div>

              <div class="col">
             
              </div>
          
         </div>
   </div>
</div>
</asp:Content>
