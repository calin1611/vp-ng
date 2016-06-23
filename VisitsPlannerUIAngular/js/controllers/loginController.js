app.controller('loginController', ['$scope', 'loginService', '$window', '$rootScope',function ($scope, loginService, $window, $rootScope) {
    var baseUrl = "http://localhost:59557/api/";

    if(loginService.checkIfLoggedIn() === true) {
        $scope.logged = true;
        $scope.user = localStorage.getItem("email");
    } else {
        $scope.logged = false;
        $scope.user = '';
    }

    $scope.triggerModal = function () {
        $('.modal-trigger').leanModal();
    };

    $scope.$on('loginService-logged', function (event, data) {
        if (data.logged === true) {
            // alert('logged IN');
            $scope.logged = true;
            $scope.user = data.user;
        } else {
            // alert('logged out');
            $scope.logged = false;
            $scope.user = '';
        }
    });

    $scope.encodeCredentials = function () {
        return loginService.encodeCredentials($scope.email, $scope.password);
    };
    $scope.login = function () {
        console.log('in login');
        var encodedCredentials = '';
        if (localStorage.getItem("encodedCredentials")) {
            encodedCredentials = localStorage.getItem("encodedCredentials");
        } else {
            encodedCredentials  = $scope.encodeCredentials();
        }
        loginService.login($scope.email, $scope.password);
    };

    $scope.logout = function () {
        loginService.logout();
        $window.location.href = '/#';
    };

}]);
