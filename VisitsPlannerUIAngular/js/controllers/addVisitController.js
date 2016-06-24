app.controller('addVisitController', ['$scope', '$rootScope', function ($scope, $rootScope) {
    var baseUrl = "http://localhost:59557/api/";

    $scope.title;
    $scope.date;
    $scope.employeeId;

    $scope.openModal = function () {
        $('.modal-trigger').leanModal();
    };

    var visitObject = {
        title: $scope.title,
        date: $scope.date,
        employeeId: $scope.employeeId
    }

    $scope.addVisit = function (visitObject) {

    }
}]);
