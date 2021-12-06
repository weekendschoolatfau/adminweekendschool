<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="audit.aspx.cs" Inherits="adminweekendschool.Admin.audit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Audit</title>
    <link href="../Content/Site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="AccountTitle" align="center">
    Audit Student Application: </div>

           
<br />
<br /><div align="center">
      <asp:datagrid id="dgAudit" runat="server" ToolTip="Audit List" Width="100%"
									                    CellPadding="4" AutoGenerateColumns="False" DataKeyField="AUDIT_DETAIL_ID"
									
									                   
                                CellSpacing="1" >
                    <AlternatingItemStyle Font-Size="10pt" Font-Names="Arial" BackColor="#CCCCCC"></AlternatingItemStyle>
                    <ItemStyle Font-Size="10pt" Font-Names="Arial" ForeColor="Black"></ItemStyle>
                    <HeaderStyle Font-Size="10pt" Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="#003366"></HeaderStyle>
                    <Columns>

                    <asp:TemplateColumn HeaderText="Username">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblDocument_Description" Text='<%# DataBinder.Eval(Container, "DataItem.USERNAME") %>'>
                    </asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Action">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblTuitionFee" Text='<%# "" +DataBinder.Eval(Container, "DataItem.ACTION") %>'>
                    </asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    </asp:TemplateColumn>


                    <asp:TemplateColumn HeaderText="Field Name">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblTuitionFee" Text='<%# "" +DataBinder.Eval(Container, "DataItem.FIELD_NAME") %>'>
                    </asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    </asp:TemplateColumn>


                    <asp:TemplateColumn HeaderText="Old Value">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblOldValue" Text='<%# "" +DataBinder.Eval(Container, "DataItem.OLD_VALUE") %>'>
                    </asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    </asp:TemplateColumn>



                        <asp:TemplateColumn HeaderText="New Value">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblNewValue" Text='<%# "" +DataBinder.Eval(Container, "DataItem.New_VALUE") %>'>
                    </asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    </asp:TemplateColumn>


                     <asp:TemplateColumn HeaderText="Transaction Date">
                    <ItemTemplate>
                    <asp:Label runat="server" id="lblNewValue" Text='<%# "" +DataBinder.Eval(Container, "DataItem.AUDIT_DATE") %>'>
                    </asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                    </asp:TemplateColumn>

                    
                    </Columns>
                    </asp:datagrid>
                    <br />
    </div>
  


    </form>
</body>
</html>
