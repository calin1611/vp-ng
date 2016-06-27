app.controller('visitsController', ['$scope', 'visitsService', 'agendaItemsService', function ($scope, visitsService, agendaItemsService) {
    var baseUrl = "http://localhost:59557/api/";

    $scope.visitsLoaded = false;
    $scope.$on('ajaxLoading', function (event, data) {
        $scope.ajaxLoading = data.loading;
    });

    var onVisits = function (response) {
        $scope.visitsLoaded = true;
        $scope.visits = response.data;
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


    var getVisits = function (unitOfTime) { // unitOfTime: 'week' or 'month'
        visitsService.getVisits(unitOfTime)
        .then(onVisits, onError);
    };

    $scope.getVisitsFromCurrentMonth = function () {
        getVisits('month');
    };
    $scope.getVisitsFromCurrentWeek = function () {
        getVisits('week');
    };


    $scope.openAgendaItemsModal = function (visit) {
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
}]);
