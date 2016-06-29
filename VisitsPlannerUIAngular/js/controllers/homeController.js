app.controller('homeController', ['$scope', 'loginService', function ($scope, loginService) {

    (function() {
        if(loginService.checkIfLoggedIn().logged === true) {
            $scope.logged = true;
        } else {
            $scope.logged = false;
        }

    }());

    $scope.$on('loginService-logged', function (event, data) {
        if (loginService.user.logged === true) {
            $scope.logged = true;
        } else {
            $scope.logged = false;
        }
    });
}]);
