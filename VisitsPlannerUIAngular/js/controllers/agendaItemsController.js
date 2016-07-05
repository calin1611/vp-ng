app.controller('agendaItemsController', ['$scope', '$filter', 'agendaItemsService', 'visitsService', function ($scope, $filter, agendaItemsService, visitsService) {

    $('select').material_select();

    $scope.vm = {
        showAddAgendaItemForm: false,
        agendaItemToAdd: {},
        relatedData: {},
        agendaItemToEdit: {}
    };

    $scope.visitsService = visitsService;

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
    
    // Edits

    $scope.editAgendaItem = function (agendaItem) {
        console.log('agenda item: ', agendaItem);
        agendaItemsService.selectedAgendaItem = agendaItem;
        $scope.vm.agendaItemToEdit = agendaItemsService.selectedAgendaItem;

        console.log('agenda item2: ', agendaItemsService.selectedAgendaItem);
        agendaItemsService.getRelatedData()
            .then(function (success) {

                $scope.vm.relatedData = {
                    locations: success.data.Location,
                    visitTypes: success.data.VisitType
                };

                // $scope.vm.relatedData.locations = success.data.Location;
                // $scope.vm.relatedData.visitTypes = success.data.VisitType;

                $('#edit-agendaItem-modal').openModal();
                console.log("Bskja", agendaItemsService.selectedAgendaItem)
            }, function (error) {
                Materialize.toast('Error when getting data from server.', 6000);
                console.error('Error when getting related date');
            });
    };

    $scope.saveEditsToAgendaItem = function () {
        agendaItemsService.saveEditsToAgendaItem()
            .then(function (success) {
                agendaItemsService.updateCurrentAgendaItemInService(success.data);
                console.log('Success: ', success);
                $('#edit-agendaItem-modal').closeModal();
                Materialize.toast('Agenda item updated.', 6000);
            }, function (error) {
                console.error(error);
                Materialize.toast('Error when updating agenda item.', 6000);
            });
    };

    $scope.deleteAgendaItem = function (agendaItem) {
        agendaItemsService.selectedAgendaItem = agendaItem;

        agendaItemsService.deleteAgendaItem()
            .then(function (success) {
                agendaItemsService.removeItemFromService(agendaItem.Id);
                
                Materialize.toast("The agenda item was deleted and you can't do anything about this.", 6000);
                // $('#agendaItems-modal').closeModal();

            }, function (error) {
                console.error(error);
                Materialize.toast('Error! ', 6000);
            });
    }

}]);
