<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true" CodeFile="changePassword.aspx.cs" Inherits="Admin_changePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function Confirm()
        {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you really want to Change Your Password?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<section class="content-header">
    <h1>Change Password</h1>
</section>
        <section class="content">
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
                    <label>Current Password</label>
                    <asp:TextBox ID="txtCurrentPassword" textMode="Password" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>    
                <div class="form-group">                          
                    <label>New Password</label>
                    <asp:TextBox ID="txtNewPassword" textMode="Password" CssClass="form-control" required="require" runat="server" ></asp:TextBox>                          
                </div>  
                <div class="form-group">                          
                    <label>Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" textMode="Password" CssClass="form-control" required="require" runat="server" ></asp:TextBox>    
                    <asp:CompareValidator ID="compPass" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Password Does'nt Matched" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>                       
                </div>            
                </div> 
                    <div class="box-footer">
                       <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update" OnClientClick = "Confirm()" onclick="btnUpdate_Click"/>                             
                   </div>  
            </div><!-- /.box-body -->   
        </div><!-- /.box --> 
     </div>  
    </section>

</asp:Content>

