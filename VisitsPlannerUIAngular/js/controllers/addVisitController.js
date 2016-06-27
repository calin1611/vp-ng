app.controller('addVisitController', ['$scope', '$rootScope', 'visitService', '$http', function ($scope, $rootScope, visitService, $http) {
    var baseUrl = "http://localhost:59557/api/";

    $scope.title;
    $scope.date;
    $scope.selectedEmployee;
    $scope.employees;


    $('.modal-trigger').leanModal();
    $('select').material_select();

    // $scope.openModal = function () {
    //     var promise = visitService.getEmployees();
    //
    //     promise.then(
    //         function (answer) {
    //             console.log("The employees:");
    //             // console.log($scope.employees);
    //             $scope.employees = answer;
    //             console.log($scope.employees);
    //         },
    //         function () {
    //             console.log('error?');
    //         }
    //     )
    //     .finally(function () {
    //         console.log("Xsdaasdasd:");
    //         console.log($scope.employees);
    //         $scope.x = "ceva";
    //
    //     });
    // };

    $scope.$on('gotEmployees', function (event, data) {
        console.log("got the employees");
        console.log(data);
        $scope.employees = data.data;
    });

    $scope.addVisit = function () {
        var visitObject = {
            Title: $scope.title,
            Date: $scope.date,
            OrganiserId: $scope.selectedEmployee
        };

        $http({
            method: 'POST',
            url: baseUrl + 'visits/add',
            data: visitObject
        })
        .then(function successCallback(data, status, headers, config) {
            alert("Sent");
        },
        function errorCallback(data, status, headers, config) {
            alert("Not Sent");
        });
    };
}]);
