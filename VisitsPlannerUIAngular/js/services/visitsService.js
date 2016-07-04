app.service('visitsService', ['$http','$rootScope', function ($http, $rootScope) {

    this.selectedVisit = '';

    this.visits = [];

    this.addVisit = function (visitObject) {
        return $http({
            method: 'POST',
            url: baseUrl + 'visits/add',
            data: visitObject
        });
    };

    this.getVisits = function (unitOfTime) { // unitOfTime: 'week' or 'month'
        $rootScope.$broadcast('ajaxLoading', {
            loading: true
        });

        return $http.get(baseUrl + 'Visits/Current' + unitOfTime)
            .finally(function() {
                $rootScope.$broadcast('ajaxLoading', {
                    loading: false
                });
            });
    };

    this.deleteVisit = function () {
        return $http.delete(baseUrl + 'visits/DeleteVisit/' + this.selectedVisit.Id);
    };

    this.removeItemFromService = function (itemContainer, itemId) {
        for (var i=0; i<itemContainer.length; i++) {
          if (itemContainer[i].Id == itemId) {
            itemContainer.splice(i, 1);
          }
        }
    };

    this.saveEditsToVisit = function () {
        return $http.put(baseUrl + 'visits/UpdateVisit', this.selectedVisit);
    };

    this.updateCurrentVisitInService = function (newValue) {
        for (var i=0; i<this.visits.length; i++) {
          if (this.visits[i].Id == this.selectedVisit.Id) {
            this.visits[i] = newValue;
          }
        }
    };

}]);