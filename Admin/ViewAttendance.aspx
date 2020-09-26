<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true" CodeFile="ViewAttendance.aspx.cs" Inherits="Admin_ViewAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
        <h1>View Attendance</h1>       
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
                            <asp:DropDownList ID="ddlStudentDepartment" CssClass="form-control" runat="server">
                             
                            </asp:DropDownList>  
                            <asp:RequiredFieldValidator ID="req3" ForeColor="Red" ControlToValidate="ddlStudentDepartment"  Display="Dynamic"
                                InitialValue="0" runat="server" ErrorMessage="Select Department"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div  class="col-md-6">
                        <div class="form-group">      
                            <asp:DropDownList ID="ddlStudentYear" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStudentYear_SelectedIndexChanged">
                                
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="ddlStudentYear"  Display="Dynamic"
                                InitialValue="0" runat="server" ErrorMessage="Select Year"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div  class="col-md-6">
                       <div class="form-group"> 
                            <asp:DropDownList ID="ddlStudentSemester" CssClass="form-control" runat="server">
                              
                            </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="ddlStudentSemester"  Display="Dynamic"
                                InitialValue="0" runat="server" ErrorMessage="Select Semester"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                       <div  class="col-md-6">
                        <div class="form-group">      
                            <asp:DropDownList ID="ddlSubjects" CssClass="form-control" runat="server"></asp:DropDownList>    
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="ddlSubjects"  Display="Dynamic"
                                InitialValue="0" runat="server" ErrorMessage="Select Subject"></asp:RequiredFieldValidator>
                        </div>
                               </div>
                         <div class="col-md-6">
                        <asp:TextBox ID="txtsearch" class="form-control" runat="server" placeholder="Select Date"></asp:TextBox>
                             
                              <asp:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" TargetControlID="txtsearch" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="txtsearch" ErrorMessage="Please Select Date" ForeColor="Red"></asp:RequiredFieldValidator>     
                    </div> 
                 <div class="col-md-6">
                        <asp:Button ID="btnSearch" CssClass="btn btn-success" runat="server" Text="Search" onclick="btnSearch_Click"></asp:Button>
                    </div>  
                    </div>
                     
                     
                <div class="box-body"> 
                     <asp:Button ID="btnAllExport" CssClass="btn btn-success right" style="float:right" runat="server" 
                        Text="Export All to Excel" onclick="btnAllExport_Click" Visible="false"></asp:Button>
                        <div style="margin-top:5px;" class="col-md-12">   
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

