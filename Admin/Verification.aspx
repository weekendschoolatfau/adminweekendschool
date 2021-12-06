<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="adminweekendschool.Admin.Verification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <div class="row ">
        <div class="col-md-12 ">
                <div class="headerMessage" id="headerMessage" runat="server" >
                    Notification: You can make one or multiple selctions on search creterias below.
                </div>
        </div>
    </div>


<div align="center">
 <div class="AccountTitle">Student Verification</DIV>
<br />
<table class="TblParentInformation" border="1"> 
<tr>
<td  colspan="2" > <div class="AccountSubTitle">
    Search By</div> </td></tr>
<tr>

 <tr>
<td  class="TblLabelRight">Last Name:</td><td  align="left">
        <asp:TextBox ID="txtParentLastName" runat="server" Width="162px" Columns="50" 
                    MaxLength="50" TabIndex="2" ToolTip="Enter last name"></asp:TextBox>
        </td>
</tr>
    <td  class="TblLabelRight">Email:</td><td  align="left">
        <asp:TextBox ID="txtEmail" runat="server" Width="162px" Columns="100" 
                    MaxLength="100" TabIndex="2" ToolTip="Enter Email"></asp:TextBox>
        </td>
</tr>
 <tr>
<td  class="TblLabelRight">Phone Number:</td><td  align="left">
        <span class="style1">(</span><asp:TextBox ID="txtHomePhone1" runat="server" 
         onkeyup="MM_PH('txtHomePhone1', '3', 'txtHomePhone2')"
            Width="45px" Columns="3" MaxLength="3" TabIndex="3" 
            ToolTip="Enter Area Code of the phone number"></asp:TextBox>
        <span class="style1">)<asp:TextBox ID="txtHomePhone2" runat="server" 
        onkeyup="MM_PH('txtHomePhone2', '3', 'txtHomePhone3')"
            Width="45px" Columns="3" MaxLength="3" TabIndex="4" 
            ToolTip="Enter Parent Phone Number"></asp:TextBox>
        </span><span class="style2">-</span><asp:TextBox ID="txtHomePhone3" 
        onkeyup="MM_PH('txtHomePhone3', '4', 'txtHomePhone3')"
            runat="server" Width="45px" Columns="4" MaxLength="4" TabIndex="5" 
            ToolTip="Enter Parent Phone Number"></asp:TextBox>
        </td>
</tr>

 
<tr>
<td  class="TblLabelRight">School Year:</td><td  align="left">
                <asp:DropDownList ID="ddbSchoolYear" runat="server">
                </asp:DropDownList>
        </td>
</tr>

<tr>
<td  class="TblLabelRight">Student Status:</td><td  align="left">
                <asp:DropDownList ID="ddbStudentStatus" runat="server">
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
									CellPadding="4" AutoGenerateColumns="False" DataKeyField="Parent_ID"
								    BorderColor="Black" BorderWidth="2px"   OnDeleteCommand="dg_Delete" 
            CellSpacing="1" >
<AlternatingItemStyle Font-Size="10pt" Font-Names="Arial" BackColor="#CCCCCC"></AlternatingItemStyle>
<ItemStyle Font-Size="10pt" Font-Names="Arial" ForeColor="Black"></ItemStyle>
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

<asp:Label runat="server" id="lblStudentLevel1" Text='<%# DataBinder.Eval(Container,"DataItem.First_NAME") + " " + DataBinder.Eval(Container,"DataItem.LAST_NAME") %>'>
</asp:Label></a>
</ItemTemplate>
</asp:TemplateColumn>





<asp:TemplateColumn HeaderText="Created On">
<ItemTemplate>
<asp:Label runat="server" id="lblSubmittedOn" Text='<%# DataBinder.Eval(Container,"DataItem.CREATION_DATE") %>'>
</asp:Label>
</ItemTemplate>
</asp:TemplateColumn>

<asp:HyperLinkColumn DataNavigateUrlField="Parent_Id" DataNavigateUrlFormatString="UserInformation.aspx?parentId={0}"
						 Text="Verify"	SortExpression=""></asp:HyperLinkColumn>
</Columns>
</asp:datagrid>

</asp:Content>
