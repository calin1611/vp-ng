app.controller('agendaItemsController', ['$scope', 'agendaItemsService', 'visitsService', function ($scope, agendaItemsService, visitsService) {
    
    $scope.showAddAgendaItemForm = false;

    $scope.$on('ajaxLoading', function (event, data) {
        $scope.ajaxLoading = data.loading;
    });

    $scope.visitsService = visitsService;

    $scope.$watch("visitsService.selectedVisit", function(newVal, oldVal){
        if(newVal !== oldVal){
            $scope.selectedVisit = newVal;
            getAgendaItemsForVisit($scope.selectedVisit.Id);

            $scope.showDeleteButton = function () {
                if ($scope.selectedVisit.EmployeeData.Email === localStorage.getItem('email')) {
                    return true;
                }
                return false;
            };
        }
    });

    var getAgendaItemsForVisit =  function (visitId) {
        agendaItemsService.getAgendaItems(visitId)
        .then(onAgendaItems, onError);
    };

    function checkIfAgendaItemsExist() {
        if ($scope.agendaItems.length > 0) {
            $scope.hideAgendaItemsTable = false;
        }
        else {
        $scope.hideAgendaItemsTable = true;            
        }
    }
    var onAgendaItems = function (response) {
        agendaItemsService.agendaItems = response.data;
        $scope.agendaItems = agendaItemsService.agendaItems;

        $scope.hideAgendaItemsTable = true;
        checkIfAgendaItemsExist();

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

    $scope.agendaItemToAdd = {};
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

    $scope.addAgendaItem = () => {
        $scope.hideAgendaItemsTable = false;
        $scope.showAddAgendaItemForm = true;
    };

    $scope.cancelAddAgendaItem = function () {
        // $scope.hideAgendaItemsTable = true
        checkIfAgendaItemsExist();
        $scope.agendaItemToAdd = {};
        $scope.showAddAgendaItemForm = false;
    };
}]);
