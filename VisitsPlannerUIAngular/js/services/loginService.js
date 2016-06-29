app.service('loginService', ['$http', '$rootScope', function ($http, $rootScope) {

    this.checkIfLoggedIn = function () {
        if (localStorage.getItem("encodedCredentials")) {
            this.setUser(localStorage.getItem("email"));
            return this.user;
        } else {
            this.clearUser();
            return this.user;
        }
    };

    this.setUser = function(email) {
        if(email)
            this.user = {'logged': true, 'email': email};
        else
            throw new Exception('Email not set');
    };

    this.clearUser = function() {
        this.user = {'logged': false, 'email': ''};
    };

    var broadcastLogin = function () {
        $rootScope.$broadcast('loginService-logged');
    };

    var encodeCredentials = function (email, password) {
        return 'Basic ' + btoa(email + ":" + password);
    };

    this.login = function (email, password) {
        var self = this;
        var encodedCredentials = encodeCredentials(email, password);

        $http({
            method: "GET",
            url: baseUrl + "employees/Authenticate",
            headers: {'Authorization' : encodedCredentials}
        }).then(function () {
            localStorage.setItem('encodedCredentials', encodedCredentials);
            localStorage.setItem('email', email);
            $http.defaults.headers.common.Authorization = encodedCredentials;
            self.setUser(email);
            broadcastLogin(true);
        }, function () {
            return "Error while trying to LOG IN";
        });
    };

    this.logout = function () {
        localStorage.removeItem("encodedCredentials");
        localStorage.removeItem("email");
        $http.defaults.headers.common.Authorization = null;
        this.clearUser();
        broadcastLogin(false);
    };

}]);
