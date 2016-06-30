app.service('agendaItemsService', ['$http', function ($http) {

    this.getAgendaItems = function (visitId) {
        return $http.get(baseUrl + 'AgendaItems/GetAgendaItemsForVisit/' + visitId);
    };

    this.agendaItems = '';

    this.selectedVisit = '';
    this.agendaItemToAdd = '';

    this.saveAgendaItem = function () {
        return $http.post(baseUrl + 'agendaItems/AddAgendaItem', this.agendaItemToAdd);
    }
}]);
