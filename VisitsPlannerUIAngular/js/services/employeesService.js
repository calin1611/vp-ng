app.service('employeesService', ['$http', function ($http) {

    this.getEmployees = function () {
        return $http.get(baseUrl + 'employees/getall');
    };
}]);
