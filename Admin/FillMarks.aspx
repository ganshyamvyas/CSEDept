<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true" CodeFile="FillMarks.aspx.cs" Inherits="Admin_FillMarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <style type="text/css">
.grd > td > table > tbody > tr > td
{
    padding:5px 10px 5px 8px;
    font-size:16px;
    border:1px #b8b8b8 inset;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
        <h1>Upload Marks </h1>       
 </section>
 <section class="content">
    <div class="row">
        <!-- right column -->
            <div class="col-md-12">
                <div id="msgsuccess" runat="server" class="alert alert-success">
                Well done! You have successfully Done Operation.
            </div>  
            <div id="msgFails" runat="server" class="alert alert-danger">
                Error! Something goes wrong.
            </div>
               
                <!-- general form elements disabled -->
                <div class="box box-primary" style="height:180px;">
                   
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
                       
                 
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnSearch" CssClass="btn btn-success" runat="server" Text="Search" onclick="btnSearch_Click"></asp:Button>
                    </div>
                </div>    
                <div class="box box-primary">
                    <div class="box-body">  
                         
                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">  
                                <ContentTemplate> 
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdProfileData" ShowFooter="true" AutoGenerateColumns="false" AutoGenerateEditButton="false" AutoGenerateSelectButton="false"
                                            CssClass="table table-striped table-bordered table-hover" runat="server">
                                            <Columns> 
                                                <asp:TemplateField HeaderText="SR. No.">

                                                    <ItemTemplate>
                                                        <span><%# Container.DataItemIndex +1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField DataField="RollNo" HeaderText="Roll No">
                                                    <ItemStyle Font-Size="Medium" />
                                                </asp:BoundField>              
                                                <asp:BoundField DataField="Name" HeaderText="Student Name">
                                                    <ItemStyle Font-Size="Medium" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FathersName" HeaderText="Father's Name">
                                                    <ItemStyle Font-Size="Medium" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="I-MID (out of 10)">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdStudentID" runat="server" Value='<%#Eval("StudentID") %>'></asp:HiddenField>
                                                     
                                                        <asp:TextBox ID="txtfirstmarks" CssClass="form-control" runat="server" Text='<%#Eval("FirstMidMarks") %>' AutoPostBack="true" OnTextChanged="txtfirstmarks_TextChanged">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>        
                                                <asp:TemplateField HeaderText="II-MID (out of 10)">
                                                    <ItemTemplate>
                                                   
                                                        <asp:TextBox ID="txtsecondmarks" CssClass="form-control"  AutoPostBack="true" OnTextChanged="txtsecondmarks_TextChanged" runat="server" Text='<%#Eval("SecondMidMarks") %>'>
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>        
                                                <asp:TemplateField HeaderText="AVG">
                                                    <ItemTemplate>
                                                       
                                                        <asp:TextBox ID="txtavg" CssClass="form-control" runat="server" Enabled="false" Text='<%#Eval("AvgMidMarks") %>'>
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ASSIGNMENT (out of 10)">
                                                    <ItemTemplate>
                                                         
                                                        <asp:TextBox ID="txtassigmarks" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="txtassigmarks_TextChanged" Text='<%#Eval("AssiMarks") %>'>
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="TOTAL">
                                                    <ItemTemplate>
                                                        
                                                        <asp:TextBox ID="txttotalmarks" CssClass="form-control" runat="server" Enabled="false" Text='<%#Eval("TotalMarks") %>'>
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                 
                                                </asp:TemplateField>                                 
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                               </ContentTemplate>
                           </asp:UpdatePanel>
                        </asp:Panel>  
                    </div><!-- /.box-body -->
                    <div class="box-footer">
                        <asp:Button ID="btnSave"  CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click"/>
                       
                    </div>  
                    </div>
                    
                </div><!-- /.box -->
            </div><!--/.col (right) -->  
    </section>
</asp:Content>

