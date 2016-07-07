var baseUrl = "http://localhost:59557/api/";

var app = angular.module('visitsPlanner', ['ngRoute', 'ngResource', 'ngSanitize', 'ui.materialize']);

app.config(function ($routeProvider, $httpProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'pages/home.html',
            controller: 'homeController'
        })
        .when('/visits', {
            templateUrl: 'pages/visits.html',
            controller: 'visitsController'
        });

    $httpProvider.defaults.headers.common.Accept = 'application/json, text/plain, */*';
    $httpProvider.defaults.headers.common['Content-Type'] = 'application/json';
    $httpProvider.defaults.headers.common.Authorization = localStorage.getItem('encodedCredentials');

});
