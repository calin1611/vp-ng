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

app.directive('ngOpenAgendaItemsModal', function ($timeout, modalDisplayService) {
    return {
        restrict: 'A',
        link: function (scope, element) {
            $(element).on('click', function () {
                $('#agendaItems-modal').openModal();
            });
        }     
    };
});

app.directive('ngOpenEditVisitModal', function ($timeout, modalDisplayService) {
    return {
        restrict: 'A',
        link: function (scope, element) {
            $timeout(function () {
                $(element).on('click', function () {
                    $('#edit-visit-modal').openModal();

                    setTimeout(function(){ 
                        var modalBackground = $('.lean-overlay');
                        if (modalBackground.length > 1) {
                            modalBackground.last().trigger('click');
                        }
                    }, 50);
                });
            });
        }
    };
});
