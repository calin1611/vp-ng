app.controller('agendaItemsController', ['$scope', 'agendaItemsService', '$filter', function ($scope, agendaItemsService, $filter) {

    $scope.$on('ajaxLoading', function (event, data) {
        $scope.ajaxLoading = data.loading;
    });

    $scope.agendaItemsService = agendaItemsService;

    $scope.$watch("agendaItemsService.selectedVisit", function(newVal, oldVal){
        // if(newVal !== oldVal){
            $scope.selectedVisit = newVal;
            getAgendaItemsForVisit($scope.selectedVisit.Id);
        // }
    });

    var getAgendaItemsForVisit =  function (visitId) {
        agendaItemsService.getAgendaItems(visitId)
        .then(onAgendaItems, onError);
    };

    var onAgendaItems = function (response) {
        agendaItemsService.agendaItems = response.data;
        $scope.agendaItems = agendaItemsService.agendaItems;

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

    $scope.showOutcome = function (outcome) {
        if (outcome === null) {
            Materialize.toast("You don't have the required permissions", 6000);
        } else {
            Materialize.toast(outcome, 6000);
        }
    };
}]);
