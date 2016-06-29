app.controller('visitsController', ['$scope', 'visitsService', 'agendaItemsService', '$filter', function ($scope, visitsService, agendaItemsService, $filter) {

    $scope.visits = visitsService.visits;

    $scope.visitsLoaded = false;
    $scope.$on('ajaxLoading', function (event, data) {
        $scope.ajaxLoading = data.loading;
    });

    var onVisits = function (response) {
        $scope.visitsLoaded = true;
        $scope.visits = response.data;
        visitsService.visits = response.data;
    };

    var onAgendaItems = function (response) {
        $scope.agendaItems =  response.data;

        $scope.numberOfAgendaItems = true;
        if ($scope.agendaItems.length > 0) {
            $scope.numberOfAgendaItems = false;
        }

        $('#agendaItems-modal').openModal();
    };

    var onError = function (response) {
        console.error("Angular XHR error: ");
        console.log(response);
        Materialize.toast('There was a problem when connecting to the server.', 20000);
    };


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


    $scope.openAgendaItemsModal = function (visit) {
        visitsService.selectedVisit = visit.Id;

        getAgendaItemsForVisit(visit.Id);
        $scope.visitTitle = visit.Title;
    };

    var getAgendaItemsForVisit =  function (visitId) {
        agendaItemsService.getAgendaItems(visitId)
            .then(onAgendaItems, onError);
    };

    $scope.showOutcome = function (outcome) {
        if (outcome === null) {
            Materialize.toast("You don't have the required permissions", 20000);
        } else {
            Materialize.toast(outcome, 20000);
        }
    };

    $scope.deleteVisit = function () {
        visitsService.deleteVisit()
            .then(function (response) {
                visitsService.removeItemFromService(visitsService.visits, visitsService.selectedVisit);

                Materialize.toast("The visit was deleted and you can't do anything about this.", 6000);
            }, function (error) {
                Materialize.toast("You don't have the required permissions", 6000);
            });
    };
}]);
