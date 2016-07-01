app.service('agendaItemsService', ['$http', function ($http) {

    this.agendaItems = '';

    this.agendaItemToAdd = '';

    this.getAgendaItems = function (visitId) {
        return $http.get(baseUrl + 'AgendaItems/GetAgendaItemsForVisit/' + visitId);
    };

    this.saveAgendaItem = function () {
        return $http.post(baseUrl + 'agendaItems/AddAgendaItem', this.agendaItemToAdd);
    };

    this.getRelatedData = function () {
        return $http.get(baseUrl + 'AgendaItems/GetRelatedData');
    };
}]);
