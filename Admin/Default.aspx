<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
	
	<title>CSE DEPARTMENT</title>
    <!-- Bootstrap core CSS -->
    <link href="../assets/login/css/login.css" rel="stylesheet" />
    <link href="../assets/login/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/login/css/animate-custom.css" rel="stylesheet" type="text/css" /> 

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="js/html5shiv.js"></script>
      <script src="js/respond.min.js"></script>
    <![endif]-->
    
     <script src="../assets/login/js/custom.modernizr.html" type="text/javascript" ></script>
   
</head>
<body>
    <form id="form1" runat="server">
   <!-- start Login box -->
    	<div class="container" id="login-block">
    		<div class="row">
			    <div class="col-sm-6 col-md-4 col-sm-offset-3 col-md-offset-4">			    	 
			       <div class="login-box clearfix animated flipInY">
			       		<div class="page-icon animated bounceInDown">
			       			<img class="img-responsive" src="../assets/login/img/login-key-icon.png" alt="Key icon" />
			       		</div>
			        	<div class="login-logo">      
			        		<a href="#"><img src="../assets/images/GITS-Full.png" height="50px" width="200px" /></a>
			        	</div> 
			        	<hr />
			        	<div class="login-form">
			        		<%--<div class="alert alert-error hide">
								  <button type="button" class="close" data-dismiss="alert">&times;</button>
								  <h4>Error!</h4>
								   Your Error Message goes here
							</div>--%>
			        		<div action="#" method="get" style="text-align:center;">
                                <h2 class="text-center">CSE Department</h2><br />
                                <div class="col-md-12">
                                    <a href="AdminLogin.aspx" class="btn btn-primary">Admin</a>
                                </div><br />
                                <div class="col-md-12">
                                    <a href="../Faculty/Default.aspx" class="btn btn-primary">Faculty</a>
                                </div><br />
                                <div class="col-md-12">
                                    <a href="../Parents/Default.aspx" class="btn btn-primary">Parents</a>
                                </div><br />
                                <div class="col-md-12">
                                <a href="../Student/Default.aspx" class="btn btn-primary">Student</a>
                                </div>
							</div>								     		
			        	</div> 			        	
			       </div>
			    </div>
			</div>
    	</div>
     
      	<!-- End Login box -->

        <script type="text/javascript" src="../../../../ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
        <script>window.jQuery || document.write('<script src="js/jquery-1.9.1.min.js"><\/script>')</script> 
        <script type="text/javascript" src="../assets/login/js/bootstrap.min.js"></script> 
        <script type="text/javascript" src="../assets/login/js/placeholder-shim.min.js"></script>        
        <script type="text/javascript" src="../assets/login/js/custom.js"></script>
    </form>
</body>
</html>
