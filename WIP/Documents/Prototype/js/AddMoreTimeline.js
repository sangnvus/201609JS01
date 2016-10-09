$(document).ready(function(){

    var counter = 1;

    $("#addButton").click(function () {

	if(counter>10){
            alert("Chỉ có thể tạo tối đa 10 lịch trình");
            return false;
	}

	var newTextBoxDiv = $(document.createElement('div'))
	     .attr("id", 'TextBoxDiv' + counter);

	newTextBoxDiv.after().html('<div class="row-fluid" >' 
						 +'<span class="span6">'
						 +'	 <span class="span3" style="padding-top:10px;">Từ ngày</span>'
						 +'	 <span class="span9"><input type="date" name="fromDate'+counter+'" id="fromDate'+counter+'" style="padding:10px; border-color:rgb(224, 95, 3);border-radius:10px;"/></span>'
						 +'</span>'
						 +'<span class="span6">'
						 +'	 <span class="span2" style="padding-top:10px;">đến ngày</span>'
						 +'	 <span class="span10"><input type="date" name="toDate'+counter+'" id="toDate'+counter+'"style="padding:10px; border-color:rgb(224, 95, 3);border-radius:10px;"/></span>'
						 +'</span>'
						 +'</div>'
						 +'<div class="row-fluid" >'
						 +'<p></p>	'			 
						 +'<div class="row-fluid" >'
						 +'<span class="span12">'
						 +'	 <span style="padding-top:25px;">Mô tả</span>'
						 +'	 <span style="margin-left:66px"><textarea name="descrip'+counter+'" id="descrip'+counter+'" class="form-control"rows="4" style="width:72%;">'
						 +'	 </textarea>'
						 +'	 </span>'
						 +'</span>'
						 +'</div>'
						 +'</div>'
						 +'<hr>');

	newTextBoxDiv.appendTo("#TextBoxesGroup");

	counter++;
     });

     $("#removeButton").click(function () {
	if(counter==1){
          alert("Không thể xóa thêm lịch trình");
          return false;
       }
	counter--;
        $("#TextBoxDiv" + counter).remove();

     });

  });