app.controller('agendaItemsController', ['$scope', '$filter', 'agendaItemsService', 'visitsService', function ($scope, $filter, agendaItemsService, visitsService) {

    $scope.vm = {
        agendaItems: {},
        showAddAgendaItemForm: false,
        agendaItemToAdd: {},
        relatedData: {},
        agendaItemToEdit: {},
        temporaryData:{},
        selectedVisit: {}
    };

    $scope.visitsService = visitsService;

    $scope.$on('ajaxLoading', function (event, data) {
        $scope.ajaxLoading = data.loading;
    });

    $scope.$watch("visitsService.selectedVisit", function (newVal, oldVal) {
        if (newVal.Id !== undefined && newVal !== oldVal) {
            $scope.vm.selectedVisit = newVal;
            getAgendaItemsForVisit($scope.vm.selectedVisit.Id);
        }
    });

    var getAgendaItemsForVisit = function (visitId) {
        agendaItemsService.getAgendaItems(visitId)
            .then(onAgendaItems, onError);
    };

    var checkIfAgendaItemsExist = function () {
        $scope.hideAgendaItemsTable = $scope.vm.agendaItems.length === 0;
    };

    var onAgendaItems = function (response) {
        agendaItemsService.agendaItems = response.data;
        $scope.vm.agendaItems = agendaItemsService.agendaItems;

        $scope.hideAgendaItemsTable = true;
        checkIfAgendaItemsExist();
    };

    var onError = function (response) {
        console.error("Angular XHR error: ", response);
        Materialize.toast('There was a problem when connecting to the server.', 20000);
    };

    var resetTemporaryData = function () {
        $scope.vm.temporaryData = {};
        agendaItemsService.selectedAgendaItem = {};
    };

    $scope.showOutcome = function (outcome) {
        if (outcome === null) {
            Materialize.toast("This agenda item has no outcome.", 6000);
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
        $scope.vm.agendaItemToAdd.VisitId = visitsService.selectedVisit.Id;
        agendaItemsService.agendaItemToAdd = $scope.vm.agendaItemToAdd;

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
        $scope.vm.agendaItemToAdd = {};
        $scope.showAddAgendaItemForm = false;
    };
    
    // Edits

    $scope.editAgendaItem = function (agendaItem) {
        agendaItemsService.selectedAgendaItem = agendaItem;
        $scope.vm.agendaItemToEdit = agendaItemsService.selectedAgendaItem;

        agendaItemsService.getRelatedData()
            .then(function (success) {
                $scope.vm.relatedData = {
                    locations: success.data.Location,
                    visitTypes: success.data.VisitType
                };

            }, function (error) {
                Materialize.toast('Error when getting data from server.', 6000);
                console.error('Error when getting related date');
            });
    };

    $scope.saveEditsToAgendaItem = function () {
        agendaItemsService.selectedAgendaItem.Location.Id = $scope.vm.temporaryData.locationId;
        agendaItemsService.selectedAgendaItem.VisitType.Id = $scope.vm.temporaryData.visitTypeId;


        agendaItemsService.saveEditsToAgendaItem()
            .then(function (success) {
                agendaItemsService.updateCurrentAgendaItemInService(success.data);
                console.log('Success: ', success);
                $('#edit-agendaItem-modal').closeModal();
                Materialize.toast('Agenda item updated.', 6000);

                resetTemporaryData();
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
            }, function (error) {
                console.error(error);
                Materialize.toast('Error! ', 6000);
            });
    };

}]);
