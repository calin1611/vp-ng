app.service('agendaItemsService', ['$http', function ($http) {

    this.agendaItems = '';

    this.agendaItemToAdd = '';

    this.selectedAgendaItem = {};

    this.updateCurrentAgendaItemInService = function (newValue) {
        for (var i=0; i<this.agendaItems.length; i++) {
          if (this.agendaItems[i].Id == this.selectedAgendaItem.Id) {
            this.agendaItems[i] = newValue;
          }
        }
    };
    
    this.removeItemFromService = function (agendaItemId) {
        for (var i=0; i<this.agendaItems.length; i++) {
          if (this.agendaItems[i].Id == agendaItemId) {
            this.agendaItems.splice(i, 1);
          }
        }
    };

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

    this.deleteAgendaItem = function () {
        return $http.delete(baseUrl + 'AgendaItems/DeleteAgendaItem/' + this.selectedAgendaItem.Id);
    };
}]);
