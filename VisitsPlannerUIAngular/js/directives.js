app.directive('navBar', function () {
    return {
        restrict: 'E',
        templateUrl: 'directives/navbar.html',
        replace: true
    };
});

app.directive('loginModal', function () {
    return {
        restrict: 'E',
        templateUrl: 'directives/loginModal.html',
        replace: true
    };
});
