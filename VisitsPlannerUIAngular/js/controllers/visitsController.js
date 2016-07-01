app.controller('visitsController', ['$scope', 'visitsService', function ($scope, visitsService) {

    $scope.visits = visitsService.visits;
    $scope.visitsLoaded = false;

    $scope.$on('ajaxLoading', function (event, data) {
        $scope.ajaxLoading = data.loading;
    });

    $scope.getVisitsFromCurrentMonth = function () {
        getVisits('month');
    };
    $scope.getVisitsFromCurrentWeek = function () {
        getVisits('week');
    };

    var getVisits = function (unitOfTime) { // unitOfTime: 'week' or 'month'
        visitsService.getVisits(unitOfTime)
        .then(onVisits, onError);
    };

    var onVisits = function (response) {
        $scope.visitsLoaded = true;
        $scope.visits = response.data;
        visitsService.visits = response.data;
    };

    $scope.openAgendaItemsModal = function (visit) {
        visitsService.selectedVisit = visit;
    };

    var onError = function (response) {
        console.error("Angular XHR error: ");
        console.log(response);
        Materialize.toast('There was a problem when connecting to the server.', 20000);
    };

    $scope.showOutcome = function (outcome) {
        if (outcome === null) {
            Materialize.toast("You don't have the required permissions", 6000);
        } else {
            Materialize.toast(outcome, 6000);
        }
    };

    $scope.deleteVisit = function () {
        visitsService.deleteVisit()
            .then(function (response) {
                visitsService.removeItemFromService(visitsService.visits, visitsService.selectedVisit.Id);

                Materialize.toast("The visit was deleted and you can't do anything about this.", 6000);
                $('#agendaItems-modal').closeModal();

            }, function (error) {
                Materialize.toast("You don't have the required permissions", 6000);
            });
    };
}]);
