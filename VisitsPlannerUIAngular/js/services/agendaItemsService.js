app.service('agendaItemsService', ['$http', function ($http) {

    this.agendaItems = '';

    this.agendaItemToAdd = '';

    this.selectedAgendaItem = {};

    this.getAgendaItems = function (visitId) {
        return $http.get(baseUrl + 'AgendaItems/GetAgendaItemsForVisit/' + visitId);
    };

    this.saveAgendaItem = function () {
        return $http.post(baseUrl + 'AgendaItems/AddAgendaItem', this.agendaItemToAdd);
    };

    this.getRelatedData = function () {
        return $http.get(baseUrl + 'AgendaItems/GetRelatedData');
    };

    this.saveEditsToAgendaItem = function () {
        return $http.put(baseUrl + 'AgendaItems/UpdateAgendaItem', this.selectedAgendaItem);
    };

    this.updateCurrentAgendaItemInService = function (newValue) {
        for (var i=0; i<this.agendaItems.length; i++) {
          if (this.agendaItems[i].Id == this.selectedAgendaItem.Id) {
            this.agendaItems[i] = newValue;
          }
        }
    };

}]);
