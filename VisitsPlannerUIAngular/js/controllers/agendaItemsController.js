app.controller('agendaItemsController', ['$scope', '$filter', 'agendaItemsService', 'visitsService', function ($scope, $filter, agendaItemsService, visitsService) {

    $scope.vm = {
        showAddAgendaItemForm: false,
        agendaItemToAdd: {},
        relatedData: {}
    };
    $scope.visitsService = visitsService;
    // Add Agenda Item
    $('select').material_select();

    $scope.$on('ajaxLoading', function (event, data) {
        $scope.ajaxLoading = data.loading;
    });

    $scope.$watch("visitsService.selectedVisit", function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.selectedVisit = newVal;
            getAgendaItemsForVisit($scope.selectedVisit.Id);

        }
    });

    var getAgendaItemsForVisit = function (visitId) {
        agendaItemsService.getAgendaItems(visitId)
            .then(onAgendaItems, onError);
    };

    var checkIfAgendaItemsExist = function () {
        $scope.hideAgendaItemsTable = $scope.agendaItems.length === 0;
    };

    var onAgendaItems = function (response) {
        agendaItemsService.agendaItems = response.data;
        $scope.agendaItems = agendaItemsService.agendaItems;

        $scope.hideAgendaItemsTable = true;
        checkIfAgendaItemsExist();

        $('#agendaItems-modal').openModal();
    };

    var onError = function (response) {
        console.error("Angular XHR error: ", response);
        Materialize.toast('There was a problem when connecting to the server.', 20000);
    };

    $scope.showOutcome = function (outcome) {
        if (outcome === null) {
            Materialize.toast("You don't have the required permissions", 6000);
        } else {
            Materialize.toast(outcome, 6000);
        }
    };

    $scope.addAgendaItem = function () {
        $scope.hideAgendaItemsTable = false;
        $scope.showAddAgendaItemForm = true;

        agendaItemsService.getRelatedData()
            .then(function (success) {
                $scope.vm.relatedData.locations = success.data.Location;
                $scope.vm.relatedData.visitTypes = success.data.VisitType;
            });
    };

    $scope.saveAgendaItem = function () {
        $scope.agendaItemToAdd.VisitId = visitsService.selectedVisit.Id;
        agendaItemsService.agendaItemToAdd = $scope.agendaItemToAdd;

        agendaItemsService.saveAgendaItem()
            .then(function (response) {
                console.log(response);
                agendaItemsService.agendaItems.push(response.data);
            }, function (error) {
                console.error(error);
            });
    };

    $scope.cancelAddAgendaItem = function () {
        checkIfAgendaItemsExist();
        $scope.agendaItemToAdd = {};
        $scope.showAddAgendaItemForm = false;
    };
}]);
