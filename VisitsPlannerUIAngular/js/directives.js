// app.directive('navBar', function () {
//     return {
//         restrict: 'E',
//         templateUrl: 'directives/navbar.html',
//         replace: true
//     };
// });
//
// app.directive('loginModal', function () {
//     return {
//         restrict: 'E',
//         templateUrl: 'directives/loginModal.html',
//         replace: true
//     };
// });

app.directive('visitsTable', function () {
    return {
        restrict: 'E',
        templateUrl: '../directives/visits-table.html',
        replace: true
    };
});

app.directive('agendaItemsTable', function () {
    return {
        restrict: 'E',
        templateUrl: '../directives/agenda-items-table.html',
        replace: true
    };
});
