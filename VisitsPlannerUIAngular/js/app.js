var app = angular.module('visitsPlanner', ['ngRoute', 'ngResource']);

app.config(function ($routeProvider, $httpProvider, $provide) {
    $routeProvider
        .when('/', {
            templateUrl: 'pages/home.html',
            controller: 'homeController'
        })
        .when('/visits', {
            templateUrl: 'pages/visits.html',
            controller: 'visitsController'
        });

    $httpProvider.defaults.headers.common['Accept'] = 'application/json, text/plain, */*';
    $httpProvider.defaults.headers.common['Content-Type'] = 'application/json';
    $httpProvider.defaults.headers.common['Authorization'] = localStorage.getItem('encodedCredentials');

    $provide.decorator('$templateRequest', ['$delegate', function($delegate) {

      var fn = $delegate;

      $delegate = function(tpl) {

        for (var key in fn) {
          $delegate[key] = fn[key];
        }

        return fn.apply(this, [tpl, true]);
      };

      return $delegate;
    }]);
});

// app.config(['$httpProvider', function ($httpProvider) {
//     $httpProvider.defaults.headers.common['Accept'] = 'application/json';
//     $httpProvider.defaults.headers.common['Content-Type'] = 'application/json';
//     $httpProvider.defaults.headers.common['Authorization'] = localStorage.getItem('encodedCredentials');
//     // $httpProvider.defaults.useXDomain = true;
// }]);
