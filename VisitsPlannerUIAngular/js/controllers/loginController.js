app.controller('loginController', ['$scope', 'loginService', '$window', '$rootScope',function ($scope, loginService, $window, $rootScope) {
    $('.modal-trigger').leanModal();

    (function() {
        if(loginService.checkIfLoggedIn() === true) {
            $scope.logged = true;
            $scope.user = localStorage.getItem("email");
        } else {
            $scope.logged = false;
            $scope.user = '';
        }
    }());

    $scope.$on('loginService-logged', function (event, data) {
        if (loginService.user.logged === true) {
            $scope.logged = true;
            $scope.user = loginService.user.email;
            $('#loginModal').closeModal();
        } else {
            $scope.logged = false;
            $scope.user = '';
        }
    });

    $scope.encodeCredentials = function () {
        return loginService.encodeCredentials($scope.email, $scope.password);
    };

    $scope.login = function () {
        loginService.login($scope.email, $scope.password);
    };

    $scope.logout = function () {
        loginService.logout();
        $window.location.href = '/#';
    };

}]);
