<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewParent.aspx.cs" Inherits="adminweekendschool.Admin.AddNewParent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="row ">
        <div class="col-md-12 ">
                <div class="headerMessage" id="headerMessage" runat="server" >
                    Notification: The new entered information will be reviewed in 24hrs.
                </div>
        </div>
    </div>
    <div class="row ">
        <div class="col-md-12 ">
                <div class="AccountTitle">
                    <div id="titleMsg" runat="server" > </div><span class="requiredMessage">* Required Field</span>
                    </div>

                </div>
         </div>
    
 

      
<br />
<br /><div align="center">
  
<table class="TblParentInformation" border="1"> 
<tr>
<td  colspan="2" > <div class="AccountSubTitle">
    Parent&nbsp; Contact Information</div> </td></tr>
<tr>
<td  class="TblLabelRight">First Name:<span class="RequiredFields">*</span>

</td>
<td >
    <asp:TextBox ID="txtParentFirstName" runat="server" Width="200px" Columns="50" 
                    MaxLength="50" TabIndex="1" ToolTip="Enter the first name of parent"></asp:TextBox>
    </td>
</tr>
 <tr>
<td  class="TblLabelRight">Last Name:<span class="RequiredFields">*</span></td><td >
        <asp:TextBox ID="txtParentLastName" runat="server" Width="200px" Columns="50" 
                    MaxLength="50" TabIndex="2" ToolTip="Enter last name of the parent"></asp:TextBox>
        </td>
</tr>
 <tr>
<td  class="TblLabelRight">Phone Number:<span class="RequiredFields">*</span></td><td >
        <span class="style1">(</span><asp:TextBox ID="txtHomePhone1" runat="server" 
         onkeyup="MM_PH('txtHomePhone1', '3', 'txtHomePhone2')"
            Width="45px" Columns="3" MaxLength="3" TabIndex="3" 
            ToolTip="Enter Area Code of the phone number"></asp:TextBox>)
        <span class="style1"><asp:TextBox ID="txtHomePhone2" runat="server" 
        onkeyup="MM_PH('txtHomePhone2', '3', 'txtHomePhone3')"
            Width="45px" Columns="3" MaxLength="3" TabIndex="4" 
            ToolTip="Enter Parent Phone Number"></asp:TextBox>
        </span><span class="style2">-</span><asp:TextBox ID="txtHomePhone3" 
        onkeyup="MM_PH('txtHomePhone3', '4', 'txtEmailAddress')"
            runat="server" Width="45px" Columns="4" MaxLength="4" TabIndex="5" 
            ToolTip="Enter Parent Phone Number"></asp:TextBox>
        </td>
</tr>
 <tr>
<td  class="TblLabelRight">Email Address:<span class="RequiredFields">*</span></td><td>
        <asp:TextBox ID="txtEmailAddress" runat="server" Width="250px" Columns="100" 
                    MaxLength="100" TabIndex="6" ToolTip="Enter Parent Email Address"></asp:TextBox>
        </td>
</tr>

</table>
    <br /><asp:Label ID="lblParentMessage" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" ></asp:Label>  <br />
     <asp:Button ID="btnUpdateParentInformation" runat="server" Text="Add New Parent Information" 
            onclick="btnUpdateParentInformation_Click" TabIndex="12" 
            ToolTip="Add New Parent Information" />  


   

</div>


   

<script type = "text/javascript">

    function AddNewParent()
    {
       
        var txtParentFirstName = document.getElementById("<%=txtParentFirstName.ClientID%>").value;

        if (txtParentFirstName.trim() == "") {
            alert("Parent first name is required field.");
            document.getElementById("<%=txtParentFirstName.ClientID%>").focus();
            return false;
        }

        var txtParentLastName = document.getElementById("<%=txtParentLastName.ClientID%>").value;

        if (txtParentLastName.trim() == "") {
            alert("Parent last name is required field.");
            document.getElementById("<%=txtParentLastName.ClientID%>").focus();
            return false;
        }

        var txtHomePhone1 = document.getElementById("<%=txtHomePhone1.ClientID%>").value;

        if (txtHomePhone1.trim() == "") {
            alert("Phone Area Code is required field.");
            document.getElementById("<%=txtHomePhone1.ClientID%>").focus();
            return false;
        }

        var txtHomePhone2 = document.getElementById("<%=txtHomePhone2.ClientID%>").value;

        if (txtHomePhone2.trim() == "") {
            alert("Phone is required field.");
            document.getElementById("<%=txtHomePhone2.ClientID%>").focus();
            return false;
        }

        var txtHomePhone3 = document.getElementById("<%=txtHomePhone3.ClientID%>").value;

        if (txtHomePhone3.trim() == "") {
            alert("Phone is required field.");
            document.getElementById("<%=txtHomePhone3.ClientID%>").focus();
            return false;
        }

        var txtEmailAddress = document.getElementById("<%=txtEmailAddress.ClientID%>").value;

        if (txtEmailAddress.trim() == "") {
            alert("Email address is required field.");
            document.getElementById("<%=txtEmailAddress.ClientID%>").focus();
             return false;
         }

        return true;
    }


</script>



</asp:Content>
