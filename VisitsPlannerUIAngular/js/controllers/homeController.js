app.controller('homeController', ['$scope', 'loginService', function ($scope, loginService) {

    (function() {
        if(loginService.checkIfLoggedIn() === true) {
            $scope.logged = true;
        } else {
            $scope.logged = false;
        }

    }());

    $scope.$on('loginService-logged', function (event, data) {
        if (data.logged === true) {
            $scope.logged = true;
        } else {
            $scope.logged = false;
        }
    });
}]);
