$(document).ready(function() {
	$("#loginLink").click(function(){
	    // $("#loginDialog").modal("hide");
	 
	    $("#loginDialog").modal("toggle");
	    $("#ac-globalnav").css("padding-right", "15px;");
	});

	$("#register-box").click(function(){
		$("#loginDialog").modal("hide").fadeOut(1200);
		$("#registerDialog").modal("toggle").fadeIn(200);
	});
	$("#forgot-box").click(function(){
		$("#loginDialog").modal("hide").fadeOut(1200);
		$("#forgotPassDialog").modal("toggle").fadeIn(200);
	});
	
		//  // Calling Login Form
		// $("#login-box").click(function(){
		// 	$("#login").delay(1000).show();

		// 	$("#register").hide();
		// 	return false;
		// });
		//Calling Register Form
		// $("#register-box").click(function(){
		// 	$("#loginDialog").modal("hide");
		// 	$("#registerDialog").modal("show");
		// 	return false;
		// });
		// //Calling Forgot Password Form
		// $("#forgot-box").click(function(){
		// 	$("#login").hide();
		// 	$("#forgotpass").show();
		// 	return false;
		// });
		// // Back Login Formae
		// $("#backlogin").click(function(){
		// 	$("#register").hide();
		// 	$("#login").show();
		// 	return false;
		// });
		// // Close Windows
		// $("#close_login").click(function(){
		// 	$("#login").hide();
		// 	return false;
		// });
		// $("#close_register").click(function(){
		// 	$("#register").hide();
		// 	return false;
		// });
		// // Close  ForgotPass Windows 
		// $("#close_forgotpass").click(function(){
		// 	$("#forgotpass").hide();
		// 	return false;
		// });
	/*
	//Open Login Window
	$('a.login-window').click(function() {

        //lấy giá trị thuộc tính href - chính là phần tử "#login-box"
        var loginBox = $(this).attr('href');

        //cho hiện hộp đăng nhập trong 300ms
        $(loginBox).fadeIn("slow");

        // thêm phần tử id="over" vào cuối thẻ body
        $('body').append('<div id="over"></div>');
        $('#over').fadeIn(300);
        
        return false;
    });
	//Open Register Window
	$('a.register-window').click(function() {

        //lấy giá trị thuộc tính href - chính là phần tử "#register-box"
        var registerBox = $(this).attr('href');

        //cho hiện hộp đăng nhập trong 300ms
        $(registerBox).fadeIn("slow");

        // thêm phần tử id="over" vào cuối thẻ body
        $('body').append('<div id="over"></div>');
        $('#over').fadeIn(300);
        
        return false;
    });

    // khi click đóng hộp thoại
    $(document).on('click', "a.close, #over", function() { 
        $('#over, .login').fadeOut(300 , function() {
            $('#over').remove();  
        }); 
        return false;
    });*/
	
});