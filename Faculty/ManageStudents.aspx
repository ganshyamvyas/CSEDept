<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/Faculty.master" AutoEventWireup="true" CodeFile="ManageStudents.aspx.cs" Inherits="Faculty_ManageStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script>
      function previewFile() {
        var preview = document.querySelector('#<%=imgPerson.ClientID %>');
        var file = document.querySelector('#<%= Flpimages.ClientID %>').files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            preview.src = reader.result;
        }
        if (file) {
            reader.readAsDataURL(file);
            document.getElementById("imgPerson").style.visibility = "visible";
        }
        else {
            preview.src = "";
            document.getElementById("imgPerson").style.visibility = "hidden";
        }
    }
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
       <div class="modal fade" id="modal-update<%#Eval("Id")%>">
    <div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title">Upload Students List from Excel</h4>
        </div>
        <asp:UpdatePanel ID="panel" runat="server">
            <ContentTemplate>
                <div class="modal-body">
                    <div class="form-group">                          
                            <label>Department :</label>
                            <asp:DropDownList ID="ddlStudentDepartment" CssClass="form-control" runat="server">
                       
                            </asp:DropDownList>    
                        </div>
                         <div class="form-group">                          
                            <label>Year :</label>
                            <asp:DropDownList ID="ddlStudentYear" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStudentYear_SelectedIndexChanged">
                       
                            </asp:DropDownList>    
                        </div>
                    <div class="form-group">                          
                            <label>Semester :</label>
                            <asp:DropDownList ID="ddlStudentSemester" CssClass="form-control" runat="server">
                        
                            </asp:DropDownList>
                        </div>
                <div class="form-group">                          
                    <label>Select Excel file :</label>
                    <asp:FileUpload ID="flpFile" runat="server" /> 
                </div> 
                </div>
             </ContentTemplate>
        </asp:UpdatePanel>
        <div class="modal-footer">
        <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
        <asp:Button ID="btnUpload" CssClass="btn btn-primary" runat="server" Text="Upload" 
                onclick="btnUpload_Click" />
        </div>
    </div>
    <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
<!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>Manage Students<small>(add, update, delete Student)</small></h1>
      <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Manage Students</li>
      </ol>
    </section>
    <!-- Main content -->
    <section class="content">
    <div class="row">   
        <div class="col-md-12">
            <div id="msgsuccess" runat="server" class="alert alert-success">
                Well done! You have successfully Done Operation.
            </div>  
            <div id="msgFails" runat="server" class="alert alert-danger">
                Error! Something goes wrong.
            </div> 
        </div>   
    </div>
    <div style="margin-bottom:20px;" class="row"> 
        <div class="col-md-12">    
            <asp:LinkButton ID="btnAdd" CssClass="btn btn-primary" runat="server" Text="Add New" onclick="btnAdd_Click">
                <i class="fa fa-edit"></i> Add New Students
            </asp:LinkButton>
            <button type="button" class="btn btn-facebook" data-toggle="modal" data-target="#modal-update">
                Upload Students from Excel
            </button>
        </div>
    </div>
    <div id="EntryForm" runat="server" class="row">  
        <div class="col-md-12">
          <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="form-group">                          
                    <label>Department :</label>
                    <asp:DropDownList ID="ddldept" CssClass="form-control" runat="server">
                       
                    </asp:DropDownList>      
                    <asp:RequiredFieldValidator ID="req1" runat="server" ErrorMessage="Select Department" ControlToValidate="ddldept" Display="Dynamic" InitialValue="0">
                    </asp:RequiredFieldValidator>  
                </div>
                <div class="form-group">                          
                    <label>Year :</label>
                    <asp:DropDownList ID="ddlyear" CssClass="form-control" runat="server">
                        <asp:ListItem Text="-------Select Year------" Value="0"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    </asp:DropDownList>      
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Year" ControlToValidate="ddlyear" Display="Dynamic" InitialValue="0">
                    </asp:RequiredFieldValidator>  
                </div>
                  <div class="form-group">                          
                    <label>Semester :</label>
                    <asp:DropDownList ID="ddlsemester" CssClass="form-control" runat="server">
                        <asp:ListItem Text="-------Select Semester------" Value="0"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                    </asp:DropDownList>      
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Semester" ControlToValidate="ddlsemester" Display="Dynamic" InitialValue="0">
                    </asp:RequiredFieldValidator>  
                </div>
                 <div class="form-group">                          
                    <label>Roll No :</label>
                    <asp:TextBox ID="txtrollno" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>
                <div class="form-group">                          
                    <label>Name :</label>
                    <asp:TextBox ID="txtName" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>
                <div class="form-group">                          
                    <label>Father's Name :</label>
                    <asp:TextBox ID="txtfathername" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>
                <div class="form-group">                          
                    <label>Mother's Name :</label>
                    <asp:TextBox ID="txtmothername" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>
                <div class="form-group">                          
                    <label>Email ID :</label>
                    <asp:TextBox ID="txtEmailID" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>
                <div class="form-group">                          
                    <label>Contact No :</label>
                    <asp:TextBox ID="txtContactNo" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>   
                <div class="form-group">                          
                    <label>Father's Contact No :</label>
                    <asp:TextBox ID="txtfcontactno" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>              
                <div class="form-group">                          
                    <label>Mother's Contact No :</label>
                    <asp:TextBox ID="txtmcontactno" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>
                 <div class="form-group">                          
                    <label>Parents's Login Password :</label>
                    <asp:TextBox ID="txtparentspassword" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>
                <div class="form-group">                          
                    <label>Password :</label>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>
                 
                <div class="form-group">                          
                    <label>Address :</label>
                    <asp:TextBox ID="txtaddr" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div> 
                  <div class="form-group">                          
                    <label>Student Image:(Recommended Size -> Width=180px)</label>
                    <asp:FileUpload ID="Flpimages" onchange="previewFile()" runat="server" />
                    <label id="imgpath" runat="server"></label>
                    <br />
                    <asp:Image ID="imgPerson"  Height="100px" Width="100px" alt="Faculty Photo" runat="server"></asp:Image>
                </div> 
                <div class="box-footer">
                    <asp:Button ID="btnSave"  CssClass="btn btn-primary" runat="server" Text="Save" onclick="btnSave_Click"/>
                    <asp:Button ID="btnUpdate"  CssClass="btn btn-primary" runat="server" Text="Update" onclick="btnUpdate_Click" /> 
                </div>
                <!-- /.box-body -->
              </div>
            <!-- /.box-body -->
          </div><!-- /.box -->
        </div>
        
    </div>
    <div class="row">  
        <div class="col-md-12 col-xs-12">
            <!-- general form elements disabled -->
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Students List</h3> 
                </div><!-- /.box-header -->
                <div class="box-body">  
                    <%--<div class="form-group">                          
                        <label>Select Category :</label>
                        <asp:DropDownList ID="ddlCategorySearch" CssClass="form-control" runat="server" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlCategorySearch_SelectedIndexChanged">
                            <asp:ListItem Text="-------Select Category------" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Trading" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Manufactured Finish Goods" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Raw material" Value="3"></asp:ListItem>
                        </asp:DropDownList>      
                    </div>--%> 
                    <div class="form-group">                          
                        <label>Search Student :</label>
                        <asp:TextBox ID="txtSearchName" CssClass="SearchTextbox" Placeholder="Student Name" runat="server"></asp:TextBox> 
                        <asp:LinkButton ID="btnSearchName" CssClass="btn btn-primary" runat="server" onclick="btnSearchName_Click">
                        <i class="fa fa-search"></i> 
                        Search 
                        </asp:LinkButton> 
                        <asp:Button ID="btnAllExport" CssClass="btn btn-success right" style="float:right" runat="server" 
                        Text="Export All to Excel" onclick="btnAllExport_Click"></asp:Button>
                    </div> 
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                    <div class="table-responsive">
                        <asp:GridView ID="grdDetails" 
                            CssClass="table table-bordered table-striped table-hover" runat="server" onrowdeleting="grdDetails_RowDeleting" 
                            onselectedindexchanging="grdDetails_SelectedIndexChanging" 
                            onrowdatabound="grdDetails_RowDataBound" AllowPaging="True" 
                            onpageindexchanging="grdDetails_PageIndexChanging" PageSize="50" >
                            <Columns>                                             
                                <asp:ButtonField CommandName="Select" HeaderText="Edit" ShowHeader="True" Text="Edit">
                                    <ControlStyle CssClass="btn btn-xs btn-warning" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Delete" HeaderText="Delete" ShowHeader="True" Text="Delete">
                                <ControlStyle CssClass="btn btn-xs btn-danger"/>
                                </asp:ButtonField>
                                 <asp:TemplateField>
                                    <HeaderTemplate>Image</HeaderTemplate>
                                    <ItemTemplate>                               
                                            <a target="_blank" href='../<%# Eval("Photo") %>'>
                                            <img alt="Image" style="width:100px" 
                                                src='../<%# Eval("Photo") %>' />
                                            </a>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                            </Columns>
                            <HeaderStyle Wrap="false" CssClass="grd" />
                            <PagerStyle CssClass="grd" HorizontalAlign="Right" />
                            </asp:GridView>
                            </div>
                    </asp:Panel>     
                </div><!-- /.box-body -->
            </div><!-- /.box -->
        </div>
    </div>
    <!-- /.row -->
</section>
</asp:Content>

