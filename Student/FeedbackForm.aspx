<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeFile="FeedbackForm.aspx.cs" Inherits="Student_FeedbackForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="content-header">
        <h1>Feedback Form </h1>       
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
                    
                    <div class="box-body">  
                        <h4 id="feedbackheading" runat="server"></h4>
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
                                               
                                                
                                                 <asp:BoundField DataField="FeedBackQues" HeaderText="FeedBackQues">
                                                       
                                                    <ItemStyle Font-Size="Medium" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Excellent (81 to 100%)">
                                                    <ItemTemplate>
                                                       
                                                        <asp:HiddenField ID="hdQuestionID" runat="server" Value='<%#Eval("ID") %>'></asp:HiddenField>
                                                       
                                                        <asp:RadioButton ID="rdexecellent" runat="server" GroupName="feedback"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>        
                                                <asp:TemplateField HeaderText="Good (61 to 80 %)">
                                                    <ItemTemplate>
                                                       <asp:RadioButton ID="rdgood" runat="server" GroupName="feedback"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>        
                                                <asp:TemplateField HeaderText="Fair (41 to 60 %)">
                                                    <ItemTemplate>
                                                       
                                                        <asp:RadioButton ID="rdfair" runat="server" GroupName="feedback"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Less than 40 %">
                                                    <ItemTemplate>
                                                       <asp:RadioButton ID="rdbad" runat="server" GroupName="feedback"/>
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
                </div><!-- /.box -->
            </div><!--/.col (right) -->
    </section>
</asp:Content>

