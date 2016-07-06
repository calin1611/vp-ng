app.service('modalDisplayService', [ '$rootScope', function ($rootScope) {
    this.showAgendaItemsModal = false;

    this.showModal = function () {
        // this.showAgendaItemsModal = true;
    };

    this.hideModal = function () {
        // this.showAgendaItemsModal = false;
    };
}]);
