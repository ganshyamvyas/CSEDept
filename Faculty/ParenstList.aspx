<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/Faculty.master" AutoEventWireup="true" CodeFile="ParenstList.aspx.cs" Inherits="Faculty_ParenstList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="content-header">
        <h1>Parents List</h1>       
 </section>
 <section class="content">
    <div class="row">
              <div class="col-md-12" style="padding:10px;">
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
                       <div class="col-md-6">
                        <asp:Button ID="btnSearch" CssClass="btn btn-success" runat="server" Text="Search" onclick="btnSearch_Click"></asp:Button>
                    </div>
                    </div>
                   
                  
        <!-- right column -->
            <div class="col-md-12">
                <!-- general form elements disabled -->
                <div class="box box-primary" style="height:400px;">
                    <div class="box-header">
                        <h3 class="box-title" id="listheading" runat="server"></h3> 
                    </div><!-- /.box-header -->
                    <div class="box-body">      
                           
                        <div style="margin-top:5px;" class="col-md-12">                                        
                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                        <div class="table-responsive">
                            <asp:GridView ID="grdDetails" AllowPaging="true" PageSize="5"
                                CssClass="table table-striped table-bordered table-hover" runat="server" 
                                onrowdatabound="grdDetails_RowDataBound" onpageindexchanging="grdDetails_PageIndexChanging">
                                <HeaderStyle Wrap="False" />
                                <PagerStyle CssClass="grd" HorizontalAlign="Left" />
                           </asp:GridView>
                         </div>
                        </asp:Panel>  
                        </div>   
                    </div><!-- /.box-body -->
                </div><!-- /.box -->
            </div><!--/.col (right) -->     
        </div>
    </section>
</asp:Content>

