app.service('visitService', ['$http', '$rootScope', function ($http, $rootScope) {
    var baseUrl = "http://localhost:59557/api/";

    this.addVisit = function (visitObject) {
        var onSuccess = function () {
            $scope.$broadcast('visitAdded', { success: true});
        };

        var onError = function () {
            $scope.$broadcast('visitAdded', { success: false});
        };

        $http({
            method: 'POST',
            url: baseUrl + 'visits/add'
        })
        .then(onSuccess, onError);

    }
}]);
