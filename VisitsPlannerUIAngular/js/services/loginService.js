app.service('loginService', ['$http', '$rootScope', function ($http, $rootScope) {
    var baseUrl = "http://localhost:59557/api/";
    var loggedEmail = '';
    
    this.checkIfLoggedIn = function () {
        console.log("In checkIfLoggedIn()");
        if (localStorage.getItem("encodedCredentials")) {
            console.log("True");
            return true;
        } else {
            console.log("false");
            return false;
        }
    };

    this.encodeCredentials = function (email, password) {
        loggedEmail = email;
        return 'Basic ' + btoa(email + ":" + password);
    };

    this.login = function (email, password) {
        var encodedCredentials = this.encodeCredentials(email, password)
        var onSuccess = function () {
            $rootScope.$broadcast('loginService-logged', {
                'logged': true,
                'user': loggedEmail
            });
            localStorage.setItem('encodedCredentials', encodedCredentials);
            localStorage.setItem('email', email);
            $('#loginModal').closeModal(); //de mutat
            // checkIfLoggedIn();
            $http.defaults.headers.common.Authorization = localStorage.getItem('encodedCredentials');

        };

        var onError = function () {
            return "Error while trying to LOG IN";
        };

        $http({
            method: "GET",
            url: baseUrl + "employees/Authenticate",
            headers: {'Authorization' : encodedCredentials}
        })
        .then(onSuccess, onError);
    };

    this.logout = function () {
        localStorage.removeItem("encodedCredentials");
        localStorage.removeItem("email");
        $rootScope.$broadcast('loginService-logged', {
            'logged': false,
            'user': ''
        });
    };

}]);
