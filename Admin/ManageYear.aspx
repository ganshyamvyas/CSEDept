<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true" CodeFile="ManageYear.aspx.cs" Inherits="Admin_ManageYear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
    function Confirm() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Do you really want to Delete this Record?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }
    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-header">
        <h1>Manage Year<small>(add, update, delete Year)</small></h1>       
</div>
 <div class="content">
    <div class="row">
        <!-- left column -->
        <div class="col-md-8">
            <!-- general form elements -->  
            <div id="msgsuccess" runat="server"  class="alert alert-success">
                Well done! You have successfully Done Operation.
            </div>  
            <div id="msgFails" runat="server" class="alert alert-danger">
                Error! Something goes wrong.
            </div>           
        <div class="box box-warning">
            <!-- form start -->                   
            <div class="box-body"> 
                <div class="form-group">                          
                    <label>Year</label>
                    <asp:TextBox ID="txtYear" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div> 
                                                                          
                </div> 
                    <div class="box-footer">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" onclick="btnSave_Click"/>
                    <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update" onclick="btnUpdate_Click"/>                             
                </div> 
            </div><!-- /.box-body -->   
        </div><!-- /.box --> 
     </div>
    <div class="row">
    <!-- right column -->
        <div class="col-md-12">
            <!-- general form elements disabled -->
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title" id="catheading" runat="server"></h3> 
                </div><!-- /.box-header -->
                <div class="box-body" id="list" runat="server">   
                                               
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                    <div class="table-responsive">
                        <asp:GridView ID="grdDetails" 
                            CssClass="table table-striped table-bordered table-hover" runat="server" 
                            CellPadding="4" OnRowDataBound="grdDetails_RowDataBound"
                            onselectedindexchanging="grdDetails_SelectedIndexChanging" 
                            onrowdeleting="grdDetails_RowDeleting">
                            <Columns>
                                <asp:ButtonField CommandName="Select" HeaderText="Edit" ShowHeader="True" Text="Edit">
                                <ControlStyle CssClass="btn btn-xs btn-warning" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Delete" HeaderText="Delete" ShowHeader="True" Text="Delete">
                                <ControlStyle CssClass="btn btn-xs btn-danger"/>
                                </asp:ButtonField>
                           
                            </Columns>
                            <HeaderStyle CssClass="grd" />
                            <PagerStyle CssClass="grd" HorizontalAlign="Right" />
                            </asp:GridView>
                            </div>
                    </asp:Panel>     
                                   
                </div><!-- /.box-body -->
            </div><!-- /.box -->
        </div><!--/.col (right) -->     
        </div>
        </div>
</asp:Content>

