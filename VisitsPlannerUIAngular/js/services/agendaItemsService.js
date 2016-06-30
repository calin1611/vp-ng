app.service('agendaItemsService', ['$http', function ($http) {

    this.getAgendaItems = function (visitId) {
        return $http.get(baseUrl + 'AgendaItems/GetAgendaItemsForVisit/' + visitId);
    };

    this.agendaItems = '';

    this.selectedVisit = '';
}]);
