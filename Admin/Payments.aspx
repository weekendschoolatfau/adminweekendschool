<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="adminweekendschool.Admin.Payments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br /><br />
   <div align="center">
<table class="TblParentInformation" border="1"   width="40%"> 
<tr>
<td  colspan="2" > <div class="AccountSubTitle">
    Payments<span class="requiredMessage"> ( * Required Field)</span></div> </td>

</tr>
<%--<tr>
<td  colspan="2" > <div class="AccountSubTitle">
    <asp:Label ID="lblSemester" runat="server" ></asp:Label></div> </td>

</tr>--%>

<tr>
<td  class="TblLabelRight">Student Name:</td><td >
    <asp:TextBox ID="txtStudentName" runat="server" Width="200px" Columns="100" 
                    MaxLength="100" TabIndex="1" ReadOnly="true" Enabled="false" ></asp:TextBox>
    </td></tr>

<tr>
<td  class="TblLabelRight">Receive SMS Notification:</td><td >
   <asp:CheckBox ID="chbSmsNotification" runat="server" />
    </td>
</tr>



<tr>
<td  class="TblLabelRight">Phone Number:</td><td >
    <asp:TextBox ID="txtPhoneNumber" runat="server" Width="200px" Columns="50" 
                    MaxLength="50" TabIndex="1" ToolTip="Enter phone number"></asp:TextBox>
    </td></tr>

     
<tr>
<td  class="TblLabelRight">Payment Plan<span class="RequiredFields">*</span></td><td>
<asp:RadioButton ID="chbPaymentFull" runat="server" GroupName="PaymentPlan"  Text="Full Payment"  AutoPostBack="true" OnCheckedChanged="chbPaymentFull_CheckedChanged"/>
<asp:RadioButton ID="chbPaymentPlan" runat="server" GroupName="PaymentPlan"  Text="Partial Payment"  AutoPostBack="true" OnCheckedChanged="chbPaymentPlan_CheckedChanged"/>
</td>
</tr>
<tr>
<td  class="TblLabelRight">Payment Type<span class="RequiredFields">*</span></td><td>
        <asp:RadioButton ID="chbPaymentTypeDebit" runat="server" GroupName="PaymentType"  Text="Debit" OnCheckedChanged="chbPaymentTypeDebit_CheckedChanged" AutoPostBack="true"/>
         <asp:RadioButton ID="chbPaymentTypeCredit" runat="server" GroupName="PaymentType"  Text="Credit" OnCheckedChanged="chbPaymentTypeCredit_CheckedChanged"  AutoPostBack="true"/>
          <asp:RadioButton ID="chbPaymentTypeCash" runat="server" GroupName="PaymentType"  Text="Cash" OnCheckedChanged="chbPaymentTypeCash_CheckedChanged"  AutoPostBack="true"/>
     <asp:RadioButton ID="chbPaymentTypeCheck" runat="server" GroupName="PaymentType"  Text="Check" OnCheckedChanged="chbPaymentTypeCheck_CheckedChanged"  AutoPostBack="true"/>
  </td>
</tr>
<tr>
<td  class="TblLabelRight">Payment Date:</td><td >
        <asp:TextBox ID="txtPaymentDate" runat="server" Width="200px" Columns="50" 
                    MaxLength="50" TabIndex="2" ReadOnly="true" Enabled ="false"></asp:TextBox>
        </td>
</tr>

<tr>

    <td  class="TblLabelRight">Tuition Fee:</td><td>
        <asp:TextBox ID="txtTuitionFee" runat="server" Width="150px" Columns="10" 
                    MaxLength="10" TabIndex="6" ToolTip="Enter Tuition Fee" Enabled="false"  style="text-align: right"></asp:TextBox>
        </td>
</tr>
<tr>
    <td  class="TblLabelRight">Uniform Price:</td><td>
        <asp:TextBox ID="txtUniformPrice" runat="server" Width="150px" Columns="10" 
                    MaxLength="10" TabIndex="6" ToolTip="Uniform Price"  Enabled="false"  style="text-align: right"></asp:TextBox>
        </td>
</tr>
<tr>
    <td  class="TblLabelRight">Book Price:</td><td>
        <asp:TextBox ID="txtBookPrice" runat="server" Width="150px" Columns="10" 
                    MaxLength="10" TabIndex="6" ToolTip="Uniform Price"  Enabled="false"  style="text-align: right"></asp:TextBox>
        </td>
</tr>

<tr>
<td  class="TblLabelRight">Total Amount Payed:</td><td>
        <asp:TextBox ID="txtTotalAmountPayed" runat="server" Width="150px" Columns="10"
                    MaxLength="10" TabIndex="6" ToolTip="Amount Payed"  Enabled="false" style="text-align: right"></asp:TextBox>
        </td>
</tr>

 <tr>
<td  class="TblLabelRight">Total Amount To Pay:</td><td>
        <asp:TextBox ID="txtTotalAmountToPay" runat="server" Width="150px" Columns="10"
                    MaxLength="10" TabIndex="6" ToolTip="Amount To Pay"  Enabled="false" style="text-align: right"></asp:TextBox>
        </td>
</tr>
</table>
<asp:Panel id="pnlCheck" runat="server" >

<table class="TblCashformation" border="1"   width="40%" > 
 <tr>
<td  class="TblLabelRight">Check Number:</td><td>
        <asp:TextBox ID="txtCheckNumber" runat="server" Width="150px" Columns="10"
                    MaxLength="10" TabIndex="6" ToolTip="Enter Check Number"   style="text-align: right"></asp:TextBox>
        </td>
</tr>
<tr>
<td  class="TblLabelRight">Check Amount:</td><td>
        <asp:TextBox ID="txtCheckAmount" runat="server" Width="150px" Columns="10"
                    MaxLength="10" TabIndex="6" ToolTip="Enter Check Amount"   style="text-align: right"></asp:TextBox>
        </td>
</tr>
</table>
</asp:Panel>
<asp:Panel id="pnlCash" runat="server" >
<table class="TblCashformation" border="1"   width="40%" > 
<tr>
<td  class="TblLabelRight">Cash Amount:</td><td>
        <asp:TextBox ID="txtCashAmount" runat="server" Width="150px" Columns="10"
                    MaxLength="10" TabIndex="6" ToolTip="Enter Cash Amount"   style="text-align: right"></asp:TextBox>
        </td>
</tr>
</table>
</asp:Panel>
<asp:Panel id="pnlCardHolder" runat="server" >
<table class="TblPaymentformation" border="1"   width="40%" > 
<tr>
<td  class="TblLabelRight">Card Holder Name:<span class="RequiredFields">*</span></td><td>
        <asp:TextBox ID="txtCardHolderName" runat="server" Width="200px" Columns="50" 
                    MaxLength="50" TabIndex="6" ToolTip="Enter Card Holder Name"></asp:TextBox>
        </td>
</tr>
</table>
</asp:Panel>
<asp:Panel id="pnlPaymentformation" runat="server" >
<table class="TblPaymentformation" border="1"   width="40%" > 
<tr>
<td  class="TblLabelRight">Card Number (16 digits):<span class="RequiredFields">*</span></td><td>
        <asp:TextBox ID="txtCardNumber" runat="server" Width="200px" Columns="16" 
                    MaxLength="16" TabIndex="6" ToolTip="Enter Card Number"></asp:TextBox>
        </td>
</tr>
<tr>
<td  class="TblLabelRight">Card Number (16 digits):<span class="RequiredFields">*</span></td><td>
        <asp:TextBox ID="txtThreeDigits" runat="server" Width="30px" Columns="3" 
                    MaxLength="3" TabIndex="6" ToolTip="Enter 3-digits"></asp:TextBox>
        </td>
</tr>

<tr>
<td  class="TblLabelRight">Expiration Date:<span class="RequiredFields">*</span></td><td>
        <asp:TextBox ID="txtExpirationDate" runat="server" Width="70px" Columns="7" 
                    MaxLength="7" TabIndex="6" ToolTip="Enter Expiration Date"></asp:TextBox> eg: MM/YYYY
        </td>
</tr>


</table>
    </asp:Panel>
    <br />
    <br />
        <asp:Button ID="btnBack" runat="server" Text="Back" 
           TabIndex="12" 
            ToolTip="Return To The Previous Page" OnClick="btnBack_Click" />  
     <asp:Button ID="btnProcessPayment" runat="server" Text="Process Payment" 
           TabIndex="12" 
            ToolTip="Process Payment" OnClick="btnProcessPayment_Click" />  

      

       </div>
    <br />
    <div align="center">
   <asp:Label ID="lblPaymentMessage" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" style="text-align: left" ></asp:Label>
</div>

       
<script type = "text/javascript">

    function StudentPayment()
    {

        var debitChecked = document.getElementById("<%=chbPaymentTypeDebit.ClientID%>").checked;
        var creditChecked = document.getElementById("<%=chbPaymentTypeCredit.ClientID%>").checked;
        var cachChecked = document.getElementById("<%=chbPaymentTypeCash.ClientID%>").checked;
        var checkChecked = document.getElementById("<%=chbPaymentTypeCheck.ClientID%>").checked;

        if ((!debitChecked) && (!creditChecked) && (!cachChecked) && (!checkChecked))
        {
            alert("The payment type must be selected");
            document.getElementById("<%=chbPaymentTypeDebit.ClientID%>").focus();
            return false;
        }

        var holderName = document.getElementById("<%=txtCardHolderName.ClientID%>").value;

        if ((holderName == "") || (holderName.length == 0))
        {
            alert("Card Holder Name is required");
            document.getElementById("<%=txtCardHolderName.ClientID%>").focus();
            return false;
        }

        var cardNumber = document.getElementById("<%=txtCardNumber.ClientID%>").value;

        if ((cardNumber == "") || (cardNumber.length == 0)) {
            alert("Card Number is required");
            document.getElementById("<%=txtCardNumber.ClientID%>").focus();
            return false;
        }

        if ( (cardNumber.length < 16)) {
            alert("Card Number must have 16 digits");
            document.getElementById("<%=txtCardNumber.ClientID%>").focus();
             return false;
        }

        var threeDigitsCard = document.getElementById("<%=txtThreeDigits.ClientID%>").value;

        if ((threeDigitsCard == "") || (threeDigitsCard.length == 0)) {
            alert("Three Digits Number is required");
            document.getElementById("<%=txtThreeDigits.ClientID%>").focus();
            return false;
        }

        var expirationDate = document.getElementById("<%=txtExpirationDate.ClientID%>").value;

        if ((expirationDate == "") || (expirationDate.length == 0)) {
            alert("Card Expiration Date is required");
            document.getElementById("<%=txtExpirationDate.ClientID%>").focus();
            return false;
        }

       
        return true;
    }

</script>

</asp:Content>
