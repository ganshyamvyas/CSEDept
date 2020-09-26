<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeFile="MyInternalMarks.aspx.cs" Inherits="Student_MyInternalMarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <section class="content-header">
        <h1>View Internal Marks</h1>       
 </section>
 <section class="content"> 
    <div class="row">
        <div class="col-md-12">
            <div id="msgsuccess" runat="server" class="alert alert-success">
                Well done! You have successfully Done Operation.
            </div>  
            <div id="msgFails" runat="server" class="alert alert-danger">
                Error! Something goes wrong.
            </div> 
            <div class="box box-primary" style="min-height:400px">
               <%-- <div class="box-header">
                    <h3 class="box-title">All Product <span style="font-size:14px">(Sale and Deal Product not Included here)</span>  <a href="AddProduct.aspx" style="float:right" class="btn btn-dropbox">Add New Product</a> </h3> 
                </div>--%>
                <div id="divProducts" runat="server">
                   <div class="row" style="padding:10px;">
                
                       <div  class="col-md-6">
                        <div class="form-group">      
                            <asp:DropDownList ID="ddlSubjects" CssClass="form-control" runat="server"></asp:DropDownList>    
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="ddlSubjects"  Display="Dynamic"
                                InitialValue="0" runat="server" ErrorMessage="Select Subject"></asp:RequiredFieldValidator>
                        </div>
                               </div>
                       
                 <div class="col-md-6">
                        <asp:Button ID="btnSearch" CssClass="btn btn-success" runat="server" Text="Search" onclick="btnSearch_Click"></asp:Button>
                    </div>  
                    </div>
                     
                     
                <div class="box-body">                                 
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                  
                     
                    <div class="table-responsive">
                       
                        
                        <asp:GridView ID="grdDetails" CssClass="table table-striped table-bordered table-hover" runat="server" 
                            AllowPaging="true" PageSize="25" OnRowDataBound="grdDetails_RowDataBound" onpageindexchanging="grdDetails_PageIndexChanging">
                           
                            <HeaderStyle Wrap="false" CssClass="grd" />
                            <PagerStyle CssClass="grd" HorizontalAlign="Left" />
                        </asp:GridView>
                        </div>
                    </asp:Panel>     
                </div><!-- /.box-body -->
                </div>
            </div><!-- /.box -->
        </div><!--/.col (right) -->     
    </div>
    </section>
</asp:Content>

