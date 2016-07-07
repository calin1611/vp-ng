app.controller('addVisitController', ['$scope', 'visitsService', 'employeesService', function ($scope, visitsService, employeesService) {

    $scope.vm = {
        visit: {
            Title: '',
            Date: '',
            EmployeeData: {
                Id: ''
            }
        },
        employees: {}
    };

    employeesService.getEmployees()
        .then(function(response){
            $scope.vm.employees = response.data;
        },
        function(error){
            console.error('Dammit! Error: ', error);
        });

    $scope.addVisit = function () {
        var visitObject = {
            Title: $scope.visit.title,
            Date: $scope.visit.date,
            EmployeeData: { 
                Id: $scope.visit.employeeId 
            }
        };

        visitsService.addVisit($scope.vm.visit)
            .then(function successCallback(data, status, headers, config) {
                Materialize.toast('Visit added', 5000);
            },
            function errorCallback(data, status, headers, config) {
                Materialize.toast('ERROR Visit not added', 5000);
            });
    };
}]);
