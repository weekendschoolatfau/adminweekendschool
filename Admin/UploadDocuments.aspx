<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadDocuments.aspx.cs" Inherits="adminweekendschool.Admin.UploadDocuments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="row ">
        <div class="col-md-12 ">
                <div class="headerMessage" id="headerMessage" runat="server" >
                   All uploaded files must be with PDF extension
                </div>
        </div>
    </div>
     <div id="processMessage" style="display:none; position:absolute; top:60px; left:20px;    z-index: 1000;">
                Loading data... <img alt="Loading" src="./images/ajax-loader.gif" width="50" height="50" />
     </div>
    <br />

    <div align="center">
  
<div class="row ">
<div class="col-md-12 ">
    <div class="AccountTitle">
        <div id="titleMsg" runat="server" >Upload Files </div>
        </div>

    </div>
</div>
   
<br />

<table class="TblParentInformation" border="1"> 
<tr>
<td  colspan="2" > <div class="AccountSubTitle">
    Student Information</div> </td></tr>
<tr>
<td  class="TblLabelRight">First Name:</td><td >
    <asp:TextBox ID="txtStudentFirstName" runat="server" Width="200px" Columns="50" 
                    MaxLength="50" TabIndex="1" ToolTip="Student First Name"></asp:TextBox>
    </td>
 </tr>
 <tr>
<td  class="TblLabelRight">Last Name:</td><td >
        <asp:TextBox ID="txtStudentLastName" runat="server" Width="200px" Columns="50" 
                    MaxLength="50" TabIndex="2" ToolTip="Student Last Name"></asp:TextBox>
        </td>
</tr>
 <tr>
<td  class="TblLabelRight">Age:</td><td >
    <asp:TextBox ID="txtStudentAge" runat="server" Width="200px" Columns="50" 
                MaxLength="50" TabIndex="2" ToolTip="Student Age"></asp:TextBox>
    </td>
</tr>
<tr>
    <td  class="TblLabelRight">Level:</td><td >
    <asp:TextBox ID="txtStudentLevel" runat="server" Width="200px" Columns="50" 
                MaxLength="50" TabIndex="2" ToolTip="Student Level"></asp:TextBox>
    </td>
</tr>
</table>
<br />
<br />
<table width="70%"  class="TblParentInformation" border="1">
<tr> 
    <td> <asp:DropDownList ID="ddlDocumentsType" runat="server" TabIndex="11" 
        > </asp:DropDownList>&nbsp;&nbsp;&nbsp;</td>
    <td>
        <asp:FileUpload ID="assetsFileUpload"   runat="server"  />
    </td>
    <td>&nbsp;
        <asp:Button ID="btnUploadDocument" Text="Upload PDF Template" runat="server" OnClick="btnUploadDocument_Click"  ></asp:Button>
    </td>
</tr>
</table>

        <br />
        <br />
        <br />
        <asp:Button ID="btnBack" runat="server" Text="Back" 
           TabIndex="12" 
            ToolTip="Return To The Previous Page" OnClick="btnBack_Click" />  

</div>

<br />
    <br />



<asp:datagrid id="dgDocuments" runat="server" ToolTip="Document List" Width="100%"
									CellPadding="4" AutoGenerateColumns="False" DataKeyField="document_Id"
									
									OnDeleteCommand="dg_Delete" BorderColor="Black" BorderWidth="2px" 
            CellSpacing="1" >
<AlternatingItemStyle Font-Size="10pt" Font-Names="Arial" BackColor="#CCCCCC"></AlternatingItemStyle>
<ItemStyle Font-Size="10pt" Font-Names="Arial" ForeColor="Black"></ItemStyle>
<HeaderStyle Font-Size="10pt" Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="#003366"></HeaderStyle>
<Columns>

<asp:TemplateColumn HeaderText="Documet Type">
<ItemTemplate>
<asp:Label runat="server" id="lblDocument_Description" Text='<%# DataBinder.Eval(Container, "DataItem.Document_Description") %>'>
</asp:Label>
</ItemTemplate>
</asp:TemplateColumn>

<asp:TemplateColumn HeaderText="Documet Name">
<ItemTemplate>
<asp:Label runat="server" id="lblDocument_Name" Text='<%# DataBinder.Eval(Container, "DataItem.Document_Name") %>'>
</asp:Label>
</ItemTemplate>
</asp:TemplateColumn>

<asp:TemplateColumn HeaderText="Added By">
<ItemTemplate>
<asp:Label runat="server" id="lblDocument_Added_By" Text='<%# DataBinder.Eval(Container, "DataItem.ADDED_BY") %>'>
</asp:Label>
</ItemTemplate>
</asp:TemplateColumn>


<asp:TemplateColumn HeaderText="Source">
<ItemTemplate>
<asp:Label runat="server" id="lblDocument_Source" Text='<%# DataBinder.Eval(Container, "DataItem.IS_PARENT") %>'>
</asp:Label>
</ItemTemplate>
</asp:TemplateColumn>

<asp:TemplateColumn HeaderText="Added Date">
<ItemTemplate>
<asp:Label runat="server" id="lblDocument_Added_Date" Text='<%# DataBinder.Eval(Container, "DataItem.ADDED_Date") %>'>
</asp:Label>
</ItemTemplate>
</asp:TemplateColumn>



<asp:ButtonColumn Text="Delete" CommandName="Delete"></asp:ButtonColumn>

</Columns>
</asp:datagrid>
<br />


<br />
    <div align="center" >
        <table>
<tr>
<td  class="TblLabelRight">Application Status:</td><td  align="left">
                <asp:DropDownList ID="ddbStudentStatus" runat="server">
                </asp:DropDownList>
        </td>
    <td>
         <asp:Button ID="btnApplicationStatus" runat="server" Text="Process"  ToolTip="Application Status" OnClick="btnApplicationStatus_Click"  />  
    </td>

</tr>
</table>
        <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" ></asp:Label> 
    </div>

 
<script type = "text/javascript">

    function UploadDocument_Click() {

      
        var sel = document.getElementById("<%=ddlDocumentsType.ClientID%>").value;
        if (sel == 0)
        {
            alert("You must select document type");
            document.getElementById("<%=ddlDocumentsType.ClientID%>").focus();

            return false;
        }
        return true;

    }

</script>


</asp:Content>
