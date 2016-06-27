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
    };

    this.getEmployees = function () {
        var onEmployees = function (response) {
            $rootScope.$broadcast('gotEmployees', { success: true, data: response.data});
            // return response.data;
        };

        var onError = function (response) {
            // $scope.$broadcast('error', { success: false});
            console.log("In ERROR");
            console.log(response);
        };

        return $http({
            method: 'GET',
            url: baseUrl + 'employees/getall'
        })
        .then(onEmployees, onError);
    };
}]);
