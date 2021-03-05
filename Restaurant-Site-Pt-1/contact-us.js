//
//CANNOT GET THE BELOW JAVASCRIPT TO WORK :(
//

/*
function clearErrors() {
	for (var loopCounter = 0; 
		loopCounter < document.forms["contactUs"].elements.length;
		loopCounter++) {
		if (document.forms["contactUs"].elements[loopCounter]
			.parentElement.className.indexOf("has-") != -1) {
			
			document.forms["contactUs"].elements[loopCounter]
				.parentElement.className = "form-group";
			}
		}
}

function validateItems() {
	clearErrors();


	var fullName = document.forms["contactUs"]["fullName"].value;
	var email =  document.forms["contactUs"]["email"].value;
	if (fullName == "") {
		alert("Must enter your name.");
		document.forms["contactUs"]["fullName"]
			.parentElement.className = "form-group has-error";
		document.forms["contactUs"]["fullName"].focus();
		return false;
	}
   if (email == "") {
       alert("Must enter an email.");
       document.forms["contactUs"]["email"]
          .parentElement.className = "form-group has-error"
       document.forms["contactUs"]["email"].focus();
       return false;
}

	alert("Thank you for your inquiry. We'll get back to you soon");
	return false;
}

*/