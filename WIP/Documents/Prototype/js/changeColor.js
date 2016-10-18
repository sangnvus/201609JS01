var count = 1;
function changeLikeStt() {
	var property = document.getElementById('like');
	if (count == 0) {
		property.style.color = "#b7b7b7"
		count = 1;        
		}
	else {
		property.style.color = "blue"
		count = 0;
		}
}