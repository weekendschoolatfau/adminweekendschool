<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserInformation.aspx.cs" Inherits="adminweekendschool.Admin.UserInformation" %>
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
<td  class="TblLabelRight">Username:<span class="RequiredFields">*</span></td><td >
    <asp:TextBox ID="txtUsername" runat="server" Width="200px" Columns="50" 
                    MaxLength="50" TabIndex="1" ToolTip="Enter your username"></asp:TextBox>
    </td></tr>
<tr>
<td  class="TblLabelRight">First Name:<span class="RequiredFields">*</span></td><td >
    <asp:TextBox ID="txtParentFirstName" runat="server" Width="200px" Columns="50" 
                    MaxLength="50" TabIndex="1" ToolTip="Enter the first name of parent"></asp:TextBox>
    </td></tr>
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
            Width="45px" Columns="4" MaxLength="4" TabIndex="4" 
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
     <asp:Button ID="btnUpdateParentInformation" runat="server" Text="Update Parent Information" 
            onclick="btnUpdateParentInformation_Click" TabIndex="12" 
            ToolTip="Update Parent Information" />  
<br /><br /><br />
<font color=red><b>List of students information to enroll this year </b> </font><br />

<asp:datagrid id="dgStudents" runat="server" ToolTip="Student List" Width="110%"
									CellPadding="4" AutoGenerateColumns="False" DataKeyField="student_Id"
									OnEditCommand="dg_Edit" 
                                    OnCancelCommand="dg_Cancel" 
                                    OnUpdateCommand="dg_Update"
									OnDeleteCommand="dg_Delete" BorderColor="Black" BorderWidth="2px" 
            CellSpacing="1" >
<AlternatingItemStyle Font-Size="10pt" Font-Names="Arial" BackColor="#CCCCCC"></AlternatingItemStyle>
<ItemStyle Font-Size="10pt" Font-Names="Arial" ForeColor="Black"></ItemStyle>
<HeaderStyle Font-Size="10pt" Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="#003366"></HeaderStyle>
<Columns>

<asp:TemplateColumn HeaderText="Student First Name">
<ItemTemplate>
<asp:Label runat="server" id="lblFirstName" Text='<%# DataBinder.Eval(Container, "DataItem.First_Name") %>'>
</asp:Label>
</ItemTemplate>
<EditItemTemplate>
<asp:TextBox runat="server"  ReadOnly="false" id="txtFN" Text='<%# DataBinder.Eval(Container, "DataItem.First_Name") %>'>
</asp:TextBox>
<asp:RequiredFieldValidator ID="rfvFN" ControlToValidate="txtFN" ErrorMessage="First name is required" Runat="server"
													EnableClientScript="true"></asp:RequiredFieldValidator>
</EditItemTemplate>
</asp:TemplateColumn>


<asp:TemplateColumn HeaderText="Student Last Name">
<ItemTemplate>
<asp:Label runat="server" id="lblLastName" Text='<%# DataBinder.Eval(Container, "DataItem.Last_Name") %>'>
</asp:Label>
</ItemTemplate>

</asp:TemplateColumn>



<asp:TemplateColumn HeaderText="Age">
<ItemTemplate>
<asp:Label runat="server" id="lblStudentAge" Text='<%# GetAgeName(DataBinder.Eval(Container,"DataItem.Age").ToString())%>'>
</asp:Label>
</ItemTemplate>
<EditItemTemplate>
<asp:DropDownList    id="ddlStudentAge" runat="server" DataSource="<%#  getStudentAge() %>" DataValueField="Age_Id" DataTextField="Age_Desc" SelectedIndex='<%# GetSelectedIndexAge(DataBinder.Eval(Container,"DataItem.Age").ToString())%>' >
</asp:DropDownList>
</EditItemTemplate>
</asp:TemplateColumn>


<asp:TemplateColumn HeaderText="Level">
<ItemTemplate>
<asp:Label runat="server" id="lblStudentLevel" Text='<%# GetLevelName(DataBinder.Eval(Container,"DataItem.Level").ToString())%>'>
</asp:Label>
</ItemTemplate>
<EditItemTemplate>
<asp:DropDownList    id="ddlStudentLevel" runat="server" DataSource="<%#  getStudentLevel() %>" DataValueField="Level_Id" DataTextField="Level_Desc" SelectedIndex='<%# GetSelectedIndexLevel(DataBinder.Eval(Container,"DataItem.Level").ToString())%>' >
</asp:DropDownList>
</EditItemTemplate>
</asp:TemplateColumn>





<asp:TemplateColumn HeaderText="Payment Status">
<ItemTemplate>
<asp:Label runat="server" id="lblStudentPaymentStatus" Text='<%# GetSelectedStudentPaymentStatusName(DataBinder.Eval(Container,"DataItem.Payment_Status").ToString())%>'>
</asp:Label>
</ItemTemplate>
<EditItemTemplate>
<asp:DropDownList Enabled="false" id="ddlStudentPaymentStatus" runat="server" DataSource="<%#  getStudentPaymentStatus() %>" DataValueField="PAYMENT_STATUS_ID" DataTextField="PAYMENT_STATUS_DESC" SelectedIndex='<%# GetSelectedStudentPaymentStatus(DataBinder.Eval(Container,"DataItem.PAYMENT_STATUS").ToString())%>' >
</asp:DropDownList>
</EditItemTemplate>
</asp:TemplateColumn>


<asp:TemplateColumn HeaderText="Application Status">
<ItemTemplate>
<asp:Label runat="server" id="lblApplicationStatus" Text='<%# GetStudentStatusName(DataBinder.Eval(Container,"DataItem.Student_Status").ToString())%>'>
</asp:Label>
</ItemTemplate>
<EditItemTemplate>
<asp:DropDownList    id="ddlStudentStatus" runat="server" DataSource="<%#  getStudentStatus() %>" DataValueField="STUDENT_STATUS_CODE" DataTextField="STUDENT_STATUS_DESC" SelectedIndex='<%# GetSelectedIndexStudentStatus(DataBinder.Eval(Container,"DataItem.Student_Status").ToString())%>' >
</asp:DropDownList>
</EditItemTemplate>
</asp:TemplateColumn>



<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="Edit"></asp:EditCommandColumn>
<asp:ButtonColumn Text="Delete" CommandName="Delete"></asp:ButtonColumn>
<asp:HyperLinkColumn DataNavigateUrlField="Student_Id" DataNavigateUrlFormatString="payments.aspx?studentId={0}"
						 Text="Payment"	SortExpression=""></asp:HyperLinkColumn>

<asp:HyperLinkColumn DataNavigateUrlField="Student_Id" DataNavigateUrlFormatString="uploaddocuments.aspx?studentId={0}"
						 Text="Upload Documents"	SortExpression=""></asp:HyperLinkColumn>


<asp:HyperLinkColumn DataNavigateUrlField="Student_Id" 
						 Text="Audit" DataNavigateUrlFormatString="javascript:auditStudent()"	SortExpression="">

      
</asp:HyperLinkColumn>




</Columns>
</asp:datagrid>
<br />
<table  class="TblAddStudentinformation" border="1" > 
<tr>
<td  colspan="3"><div class="AccountSubTitle">
   Add New Student Information</div></td></tr>
<tr>
<td  class="TblLabelCenter">First Name<span class="RequiredFields">*</span></td>
<td  class="TblLabelCenter">Age<span class="RequiredFields">*</span></td>
<td  class="TblLabelCenter">Level<span class="RequiredFields">*</span></td>
</tr>
<tr>
<td  class="TblLabelLeft">
    <asp:TextBox ID="txtStudentFirstName" runat="server" Width="200px" Columns="50" 
        MaxLength="50" TabIndex="7" ToolTip="Enter Student First Name"></asp:TextBox>
    </td>
<td  class="TblLabelLeft">
    <asp:DropDownList ID="ddlAddStudentAge" runat="server" TabIndex="10" 
        ToolTip="Select Student Age">
    </asp:DropDownList>
    </td>
<td  class="TblLabelLeft">
    <asp:DropDownList ID="ddbAddStudentLevel" runat="server" TabIndex="11" 
        ToolTip="Select Student Level">
    </asp:DropDownList>
    </td>
  
</tr>
</table>
    <br />
    <asp:Button ID="btnAddNewStudent" runat="server" Text="Add  Student" 
            onclick="btnAddNewStudent_Click" TabIndex="12" 
            ToolTip="Click on Add Student When You Finish Entering Student Information" />  

    &nbsp;&nbsp;
     <asp:Button ID="btnPrintPayments" runat="server" Text="Print Student Payments Receipt" 
            TabIndex="12" 
            ToolTip="Print Student Payments" OnClick="btnPrintPayments_Click" />  
      <%--  &nbsp;<br /><br />
        <font color=red>
        After finishing entering all the students information above<br />please click on submit information button to process your application
        </font>
        <br />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit Information" 
            onclick="btnSubmit_Click" TabIndex="13"  
            ToolTip="Submit your information After adding All The Student(s)" />  
        &nbsp;  
        
        <input id="hdnStudentsList" type="hidden" runat="server" value="0" />
        
    </span>--%>

</div>


    <asp:DropDownList ID="ddlStudentPaymentStatus" runat="server" TabIndex="11"  Visible="false"
        > </asp:DropDownList>

     <asp:DropDownList ID="ddlStudentStatus" runat="server" TabIndex="11"  Visible="false"
        > </asp:DropDownList>


<asp:HiddenField ID="hdnParentId" runat="server" />
    
<script type = "text/javascript">

    function auditStudent() {
        var parentId = document.getElementById("<%=hdnParentId.ClientID%>").value;
        newwindow = window.open('audit.aspx?parentId=' + parentId, 'name', 'height=900,width=800');
        if (window.focus) { newwindow.focus() }
        return false;
    }

    function AddNewParent() {
        var txtUsername = document.getElementById("<%=txtUsername.ClientID%>").value;

         if (txtUsername.trim() == "") {
             alert("Username is required field.");
             document.getElementById("<%=txtUsername.ClientID%>").focus();
             return false;
         }

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


    function AddNewStudent() {
        var txtFirstName = document.getElementById("<%=txtStudentFirstName.ClientID%>").value;

        if (txtFirstName.trim() == "") {
            alert("First name is required field.");
            document.getElementById("<%=txtStudentFirstName.ClientID%>").focus();
            return false;
        }

        var sel = document.getElementById("<%=ddlAddStudentAge.ClientID%>").value;
        if (sel == 0) {
            alert("Student age is required field");
            document.getElementById("<%=ddlAddStudentAge.ClientID%>").focus();

            return false;
        }

        sel = document.getElementById("<%=ddbAddStudentLevel.ClientID%>").value;
        if (sel == 0) {
            alert("Student level is required field");
            document.getElementById("<%=ddbAddStudentLevel.ClientID%>").focus();

            return false;
        }


        return true;


    }

    function OpenPdfFile(pdffile) {

        window.open('../files/' + pdffile, 'PaymentForm', 'width=750,height=350,resizable=yes');

    }
</script>


</asp:Content>
