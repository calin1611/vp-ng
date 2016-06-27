app.service('employeesService', ['$http', function ($http) {
    var baseUrl = "http://localhost:59557/api/";

    this.getEmployees = function () {
        return $http({
            method: 'GET',
            url: baseUrl + 'employees/getall'
        });
    };
}]);
