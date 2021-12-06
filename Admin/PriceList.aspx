<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PriceList.aspx.cs" Inherits="adminweekendschool.Admin.PriceList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     
    <div class="row ">
        <div class="col-md-12 ">
                <div class="headerMessage" id="headerMessage" runat="server" >
                    Notification: Student Fees For Year 2021 / 2022.
                </div>
        </div>
    </div>
    <div class="row ">
        <div class="col-md-12 ">
                <div class="AccountTitle">
                    <div id="titleMsg" runat="server" > Student Fees</div>
                    </div>

                </div>
         </div>
    
 

      
<br />
<br /><div align="center">
      <asp:datagrid id="dgTuitionFee" runat="server" ToolTip="Document List" Width="100%"
									                    CellPadding="4" AutoGenerateColumns="False" DataKeyField="level_Id"
                                    OnEditCommand  ="dg_Edit" 
                                    OnCancelCommand="dg_Cancel" 
                                    OnUpdateCommand="dg_Update"
									                            BorderColor= "Black" BorderWidth="2px"  CellSpacing="1" >

                    <AlternatingItemStyle Font-Size="10pt" Font-Names="Arial" BackColor="#CCCCCC"></AlternatingItemStyle>
                    <ItemStyle Font-Size="10pt" Font-Names="Arial" ForeColor="Black"></ItemStyle>
                    <HeaderStyle Font-Size="10pt" Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="#003366"></HeaderStyle>
                    <Columns>

                    <asp:TemplateColumn HeaderText="Grade">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblDocument_Description" Text='<%# DataBinder.Eval(Container, "DataItem.Level_id") %>'>
                    </asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Tuition Fee">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblTuitionFee" Text='<%# DataBinder.Eval(Container, "DataItem.TUITION_FEE") %>'>
                    </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox runat="server"  ReadOnly="false" id="txtTuitionFee" Text='<%# DataBinder.Eval(Container, "DataItem.TUITION_FEE") %>'>
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTuitionFee" ControlToValidate="txtTuitionFee" ErrorMessage="Tuition fee is required" Runat="server"
													                    EnableClientScript="true"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="T-Shirt Price">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblTShirtPrice" Text='<%# "$" +DataBinder.Eval(Container, "DataItem.T-Shirt_Price") %>'>
                    </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox runat="server"  ReadOnly="false" id="txtTShirtPrice" Text='<%# DataBinder.Eval(Container, "DataItem.T-Shirt_Price") %>'>
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTShirtPrice" ControlToValidate="txtTShirtPrice" ErrorMessage="TShirt price is required" Runat="server"
													                    EnableClientScript="true"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Book Price">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblBookPrice" Text='<%# "$" +DataBinder.Eval(Container, "DataItem.Book_Price") %>'>
                    </asp:Label>
                    </ItemTemplate>
                     <EditItemTemplate>
                    <asp:TextBox runat="server"  ReadOnly="false" id="txtBookPrice" Text='<%# DataBinder.Eval(Container, "DataItem.Book_Price") %>'>
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBookPrice" ControlToValidate="txtBookPrice" ErrorMessage="Book price is required" Runat="server"
													                    EnableClientScript="true"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    </asp:TemplateColumn>
                   

                   <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="Edit"></asp:EditCommandColumn>
                   </Columns>
                    </asp:datagrid>
                    <br />
    </div>
  
   
</asp:Content>
