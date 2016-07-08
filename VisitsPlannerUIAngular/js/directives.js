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

app.directive('ngOpenAgendaItemsModal', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element) {
            $(element).on('click', function () {
                    // $('select').material_select(); //possibly useless, possibly not

                $('#agendaItems-modal').openModal();
            });
        }     
    };
});

app.directive('ngOpenEditVisitModal', function ($timeout, visitsService) {
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
                    }, 60);
                });
            });
        }
    };
});

app.directive('ngOpenEditAgendaItemsModal', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element) {
            $(element).on('click', function () {
                $('#edit-agendaItem-modal').openModal();
            });
        }     
    };
});

app.directive('ngOpenAddVisitModal', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element) {
            $(element).on('click', function () {
                $('#add-visit-modal').openModal();
                $('select').material_select();
            });
        }     
    };
});
