app.controller('visitsController', ['$scope', 'visitsService', 'employeesService', function ($scope, visitsService, employeesService) {

    // $scope.visits = visitsService.visits;
    $scope.vm = {
        visits: visitsService.visits,
        visitsLoaded: false,
        temporaryData: {
            employeeId: ''
        },
        employees:{}
    };

    $scope.$on('ajaxLoading', function (event, data) {
        $scope.ajaxLoading = data.loading;
    });

    $scope.getVisitsFromCurrentMonth = function () {
        getVisits('month');
    };

    $scope.getVisitsFromCurrentWeek = function () {
        getVisits('week');
    };

    var setOutcomeIcon = function () {
        for (var i = 0; i < $scope.vm.visits.length; i++) {
            if ($scope.vm.visits[i].EmployeeData.Email === localStorage.getItem('email')) {
                $scope.vm.visits[i].showOutcomeIcon = true;
            }
            else {
                $scope.vm.visits[i].showOutcomeIcon = false;
            }
        }
    };
    
    var getVisits = function (unitOfTime) { // unitOfTime: 'week' or 'month'
        visitsService.getVisits(unitOfTime)
        .then(onVisits, onError);
    };

    var onVisits = function (response) {
        $scope.vm.visitsLoaded = true;
        $scope.vm.visits = response.data;
        setOutcomeIcon();
        visitsService.visits = $scope.vm.visits;
    };

    var onError = function (response) {
        console.error("Angular XHR error: ", response);
        Materialize.toast('There was a problem when connecting to the server.', 20000);
    };

    var resetTemporaryData = function () {
        $scope.vm.temporaryData = {};
        visitsService.selectedVisit = {};
    };
    

    $scope.showDeleteButton = function (visit) {
        if (visit.EmployeeData.Email === localStorage.getItem('email')) {
            return true;
        }
        return false;
    };

    $scope.showOutcome = function (visit) {
        if (visit.EmployeeData.Email === localStorage.getItem('email')) {
            if (visit.Outcome === null) 
                Materialize.toast("This visit has no outcome.", 6000);
            else
                Materialize.toast(visit.Outcome, 6000);
        }
        else {
            Materialize.toast("You don't have the required permissions", 6000);
        }
    };

    $scope.openAgendaItemsModal = function (visit) {
        visitsService.selectedVisit = visit;
    };
    
    $scope.deleteVisit = function (visit) {
        visitsService.selectedVisit = visit;
        visitsService.deleteVisit()
            .then(function (response) {
                visitsService.removeItemFromService(visitsService.visits, visitsService.selectedVisit.Id);

                Materialize.toast("The visit was deleted and you can't do anything about this.", 6000);
                $('#agendaItems-modal').closeModal();

            }, function (error) {
                Materialize.toast("You don't have the required permissions", 6000);
            });
    };

    var getEmployees = function () {
        employeesService.getEmployees()
        .then(function (success) {
            $scope.vm.employees = success.data;
        }, function (error) {
            console.error("Error when trying to get employees");
        });
    };

    $scope.editVisit = function (visit) {
        visitsService.selectedVisit = visit;
        $scope.visitToEdit = visitsService.selectedVisit;
        getEmployees();
    };

    $scope.saveEditsToVisit = function () {
        visitsService.selectedVisit.EmployeeData.Id = $scope.vm.temporaryData.employeeId;

        visitsService.saveEditsToVisit()
        .then(function (success) {
            visitsService.updateCurrentVisitInService(success.data);
            Materialize.toast("Visit updated.", 6000);

            resetTemporaryData();
        }, function (error) {
            Materialize.toast("Visit updated.", 6000);
        });
    };
}]);
