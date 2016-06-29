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
        return $http.delete(baseUrl + 'visits/DeleteVisit/' + this.selectedVisit);
    };

    this.removeItemFromService = function (itemContainer, itemId) {
        var i=0, len=itemContainer.length;
        for (; i<len; i++) {
          if (+itemContainer[i].Id == +itemId) {
            itemContainer.splice(i, 1);
          }
        }
    };

}]);

// this.getAgendaItemById = function (input, id) {
//     var i=0, len=input.length;
//     for (; i<len; i++) {
//       if (+input[i].Id == +id) {
//         return input[i];
//       }
//     }
//     return null;
// };
