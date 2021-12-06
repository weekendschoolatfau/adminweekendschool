<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Finances.aspx.cs" Inherits="adminweekendschool.Admin.Finances" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <div align="center">
<br /><br />
<table class="TblParentInformation" border="1"> 
<tr>
<td  colspan="2" > <div class="AccountSubTitle">
    Financial Report</div> </td></tr>

 
 
<tr>
<td  class="TblLabelRight">School Year:</td><td  align="left">
                <asp:DropDownList ID="ddbSchoolYear" runat="server">
                </asp:DropDownList>
        </td>
</tr>

<tr>
<td  class="TblLabelRight">Payment Status:</td><td  align="left">
                <asp:DropDownList ID="ddlStudentPaymentStatus" runat="server">
                </asp:DropDownList>
        </td>
</tr>

<tr>
<td  class="TblLabelRight">Student Level </td><td  align="left">
                <asp:DropDownList ID="ddbAddStudentLevel" runat="server">
                </asp:DropDownList>
        </td>
</tr>
</table>
<br />
<br>
<asp:button id="btnProcess" accessKey="P" onclick="btnProcess_Click" runat="server" Width="100px"
					Text="Process" ToolTip="Click on process to Get Verification List."></asp:button><asp:button id="btnReset" accessKey="R" onclick="btnReset_Click" runat="server" Width="100px"
					Text="Reset" ToolTip="Click on reset button to cancel all the selections."></asp:button><br>
<br>

    
<asp:datagrid id="dgVerificationList" runat="server" ToolTip="Verification List" Width="80%"
									CellPadding="4" AutoGenerateColumns="False" DataKeyField="STUDENT_ID"
								    BorderColor="Black" BorderWidth="2px"   OnDeleteCommand="dg_Delete" 
            CellSpacing="1" >
<AlternatingItemStyle Font-Size="10pt" Font-Names="Arial" BackColor="#CCCCCC"></AlternatingItemStyle>
<ItemStyle Font-Size="10pt" Font-Names="Arial" ForeColor="Black" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
<HeaderStyle Font-Size="10pt" Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="#003366"></HeaderStyle>
<Columns>

<asp:TemplateColumn HeaderText="School Year">
<ItemTemplate>
<asp:Label runat="server" id="lblStudentLevel" Text='<%# DataBinder.Eval(Container,"DataItem.ENROLLMENT_YEAR") %>'>
</asp:Label>
</ItemTemplate>
</asp:TemplateColumn>


<asp:TemplateColumn HeaderText="Student  Name">
<ItemTemplate>

<asp:Label runat="server" id="lblStudentName" Text='<%# DataBinder.Eval(Container,"DataItem.NAME") %>'>
</asp:Label></a>
</ItemTemplate>
</asp:TemplateColumn>





<asp:TemplateColumn HeaderText="Payment Date">
<ItemTemplate>
<asp:Label runat="server" id="lblPaymentDate" Text='<%# DataBinder.Eval(Container,"DataItem.PAYMENT_DATE") %>'>
</asp:Label>
</ItemTemplate>
</asp:TemplateColumn>


<%--
<asp:TemplateColumn HeaderText="Payment Status">
<ItemTemplate>
<asp:Label runat="server" id="lblPaymentStatus" Text='<%# DataBinder.Eval(Container,"DataItem.PAYMENT_STATUS") %>'>
</asp:Label>
</ItemTemplate>
</asp:TemplateColumn>--%>


<asp:TemplateColumn HeaderText="Amount Due">
<ItemTemplate>
<asp:Label runat="server" id="lblAmountDue" Text='<%# "$" + DataBinder.Eval(Container,"DataItem.TOTAL_AMOUNT_DUE") %>'>
</asp:Label>
</ItemTemplate>
    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Middle" />
</asp:TemplateColumn>


<asp:TemplateColumn HeaderText="Amount Payed">
<ItemTemplate>
<asp:Label runat="server" id="lblAmountPayed" Text='<%# "$" + DataBinder.Eval(Container,"DataItem.TOTAL_AMOUNT_PAYED") %>' >
</asp:Label>
</ItemTemplate>
    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Middle" />
</asp:TemplateColumn>



<asp:TemplateColumn HeaderText="Amount Remaining">
<ItemTemplate>
<asp:Label runat="server" id="lblAmountRemaining" Text='<%# "$" + DataBinder.Eval(Container,"DataItem.REMAINING") %>'>
</asp:Label>
</ItemTemplate>
    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Middle" />
</asp:TemplateColumn>



<asp:HyperLinkColumn DataNavigateUrlField="Parent_Id" DataNavigateUrlFormatString="UserInformation.aspx?parentId={0}"
						 Text="Verify"	SortExpression="">
    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Middle" />
    </asp:HyperLinkColumn>
</Columns>
</asp:datagrid>
          <br /><br />
         <asp:Panel ID="pnlTotal" runat="server">
         <table class="TblParentInformation" border="1"> 
              <tr>
                  <td class="TblLabelCenter">Amount Due</td> <td  class="TblLabelCenter">Amount Payed</td> <td  class="TblLabelCenter"> Amount Remaining</td>
              </tr>
               <tr>
                  <td  class="TblLabelCenter"><asp:Label runat="server" ID="lblAmountDue"></asp:Label></td> 
                   <td  class="TblLabelCenter"><asp:Label runat="server" ID="lblAmountPayed"></asp:Label></td> 
                   <td  class="TblLabelCenter"><asp:Label runat="server" ID="lblAmountRemaining"></asp:Label></td>
              </tr>
          </table>
          </asp:Panel>
          </div>
</asp:Content>
