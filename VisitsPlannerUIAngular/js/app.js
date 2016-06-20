var app = angular.module('visitsPlanner', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'pages/home.html',
            controller: 'homeController'
        })
        .when('/visits', {
            templateUrl: 'pages/visits.html',
            controller: 'visitsController'
        });
});
