$(document).ready(function () {




(function() {

  //looks in LocalStorage to see if there are any stored credentials and decide which button to show (Log In/Out)
  function checkIfLoggedIn() {
      if (localStorage.getItem("encodedCredentials")) {
          $("#login-btn").hide();
          $("#hello").show();
          $('#logout-btn').show();
      } else {
          $("#login-btn").show();
          $('#logout-btn').hide();
          ajaxLogin();
      }
  }
  checkIfLoggedIn();

  //retrieves the credentials and encodes them
  var getCredentials = function () {
      var email = $('#email').val();
      var password = $('#password').val();
      return {
          encode: btoa(email+":"+password),
          email: email,
          password: password
      };
  }

  //Log In function
  function ajaxLogin() {
      $('#login').on("click", function(e){
          $.ajax({
              url: "http://localhost:59557/api/employees/Authenticate",
              type: "get",
              headers: { 'Authorization': 'Basic ' + getCredentials().encode },
              success: function () {
                  localStorage.setItem('encodedCredentials', 'Basic ' + getCredentials().encode);
                  $('#modal1').closeModal();
                  checkIfLoggedIn();
                  location.reload();
              },
              error: function () {
                  alert("aww...");
              }
          });
      });
  }

  //Log Out function
  $('#logout-btn').on('click', function () {
      localStorage.removeItem("encodedCredentials");
      checkIfLoggedIn();
  });

  //shows the Log In modal
  $('.modal-trigger').leanModal();
}());

});
