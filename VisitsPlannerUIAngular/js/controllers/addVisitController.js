app.controller('addVisitController', ['$scope', 'visitsService', 'employeesService', function ($scope, visitsService, employeesService) {

    $('.modal-trigger').leanModal();
    $('select').material_select();

    $scope.visit = {
        title: '',
        date: '',
        employeeId: ''
    };

    $scope.employees = '';

    employeesService.getEmployees()
        .then(function(response){
            $scope.employees = response.data;
        },
        function(error){
            console.error(error);
        });

    $scope.addVisit = function () {
        var visitObject = {
            Title: $scope.visit.title,
            Date: $scope.visit.date,
            OrganiserId: $scope.visit.employeeId
        };

        visitsService.addVisit(visitObject)
            .then(function successCallback(data, status, headers, config) {
                Materialize.toast('Visit added', 5000);
            },
            function errorCallback(data, status, headers, config) {
                Materialize.toast('ERROR Visit not added', 5000);
            });
    };
}]);
