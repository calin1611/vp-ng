app.service('agendaItemsService', ['$http', function ($http) {
    var baseUrl = "http://localhost:59557/api/";

    this.getAgendaItems = function (visitId) {
        return $http.get(baseUrl + 'AgendaItems/GetAgendaItemsForVisit/' + visitId);
    };
}]);
