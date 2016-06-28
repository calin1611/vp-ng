app.service('loginService', ['$http', '$rootScope', function ($http, $rootScope) {
    var loggedEmail = '';

    this.checkIfLoggedIn = function () {
        if (localStorage.getItem("encodedCredentials")) {
            this.loggedIn = {
                'logged': true,
                'user': loggedEmail
            };
            return true;
        } else {
            this.loggedIn = {
                'logged': false,
                'user': ''
            };
            return false;
        }
    };


    var broadcastLogin = function (loginBoolean) {
        $rootScope.$broadcast('loginService-logged', {
            'logged': loginBoolean,
            'user': loggedEmail
        });
    };

    this.encodeCredentials = function (email, password) {
        loggedEmail = email;
        return 'Basic ' + btoa(email + ":" + password);
    };

    this.login = function (email, password) {
        var encodedCredentials = this.encodeCredentials(email, password);

        var onSuccess = function () {
            broadcastLogin(true);
            localStorage.setItem('encodedCredentials', encodedCredentials);
            localStorage.setItem('email', email);
            $http.defaults.headers.common.Authorization = localStorage.getItem('encodedCredentials');

            this.loggedIn = {
                'logged': true,
                'user': email
            };
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
        $http.defaults.headers.common.Authorization = localStorage.getItem('encodedCredentials');

        broadcastLogin(false);

        this.loggedIn = {
            'logged': false,
            'user': ''
        };
    };

}]);
