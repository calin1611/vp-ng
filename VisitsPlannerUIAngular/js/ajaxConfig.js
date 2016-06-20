(function() {

    //Global headers for ajax calls
    $.ajaxSetup({
        headers: {
            'Authorization': localStorage.getItem('encodedCredentials'),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    });
})();
