app.service('visitsService', ['$http','$rootScope', function ($http, $rootScope) {
    var baseUrl = "http://localhost:59557/api/";

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
}]);
