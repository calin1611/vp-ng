app.controller('visitsController', ['$scope', function ($scope) {
    $scope.visitsFromCurrentMonth = {};
    $scope.visitsFromCurrentWeek = {};

    (function() {
        var baseUrl = "http://localhost:59557/api/"

        function getVisitsFromCurrentMonth() {
            $('#spinner').show();
            $.ajax({
                url: baseUrl + 'Visits/CurrentMonth',
                type: "get",
                success: function (data) {
                    visitsFromCurrentMonth = data;
                    bindData(data);
                },
                error: function () {
                    alert("aww...");
                },
                complete: function () {
                    $('#spinner').hide();
                }
            });
        }

        function getVisitsFromCurrentWeek() {
            $('#spinner').show();
            $.ajax({
                url: baseUrl + 'Visits/CurrentWeek',
                type: "get",
                success: function (data) {
                    visitsFromCurrentMonth = data;
                    bindData(data);
                },
                error: function () {
                    alert("aww...");
                },
                complete: function () {
                    $('#spinner').hide();
                }
            });
        }

        //make sure the page loads with a blank page
        var table = $("#visits-table");
        table.hide();
        //add event handlers for Click
        $('#current-month').on('click', getVisitsFromCurrentMonth);
        $('#current-week').on('click', getVisitsFromCurrentWeek);
        $('#addBtn').on('click', addVisit);

        function bindData(data) {
            var tableBody = getTableBody('visits-table');

            for (var i = 0; i < data.length; i++) {
                var dateTime = getDateTime(data[i].Date)
                tableBody.append(
                    '<tr data-visitId="' + data[i].Id + '">' +
                        '<td>' +
                            data[i].Title +
                        '</td>' +
                        '<td>'
                            + printDate(dateTime.date) +"  " + printTime(dateTime.time) +
                        '</td>' +
                        '<td>' +
                            '<a href="mailto:' + data[i].EmployeeData.Email + '">' + data[i].EmployeeData.FirstName + " " + data[i].EmployeeData.LastName + '</a>' +
                        '</td>' +
                        '<td>' +
                            printOutcome(data[i].Outcome) +
                        '</td>' +
                        '<td>' +
                            '<i class="material-icons modal-trigger pointer" href="#agendaItems-modal">launch</i>' +
                        '</td>' +
                    '</tr>');
            }
            $('.tooltipped').tooltip({delay: 50});

            //after the table has been popultated, add a click event on the agenda items button.
            var agendaItemsButton = $('.modal-trigger');
            agendaItemsButton.leanModal();
            agendaItemsButton.on("click", function(){
                var visitForAgendaItems = {
                    id: this.closest('tr').dataset.visitid,
                    title: $(this).parent().siblings(":first").text()
                };
                getAgendaItemsForVisit(visitForAgendaItems);
            });
        }

        function getTableBody(tableId) {
            var tableBody = $("#" + tableId + " >  tbody:last-child");
            table.show();
            tableBody.html("");

            return tableBody;
        }

        function getAgendaItemsForVisit(visit) {
            $.ajax({
                url: baseUrl + 'AgendaItems/GetAgendaItemsForVisit/' + visit.id,
                method: 'get',
                success: function (data) {
                    $('#visit-title').text(visit.title);
                    populateAgendaItemsTable(data);
                }
            });
        }

        function populateAgendaItemsTable(data){
            var tableBody = getTableBody('agendaItems-table');

            for (var i = 0; i < data.length; i++) {
                if (data[i].Outcome === null) {
                    data[i].Outcome = '-';
                }
                tableBody.append('<tr data-visitId="' + data[i].Id + '">' +
                    '<td>' +
                        data[i].Title +
                    '</td>' +
                    '<td>' +
                        getDateTime2(data[i].Date) +
                    '</td>' +
                    '<td>' +
                        data[i].LocationId +
                    '</td>' +
                    '<td>' +
                        '<i class="material-icons tooltipped pointer" data-position="right" data-delay="50" data-tooltip="' + data[i].Outcome + '">subject</i>' +
                    '</td>' +
                    '<td>' +
                        data[i].VisitTypeId +
                    '</td>' +
                '</tr>');
            }
            $('.tooltipped').tooltip({delay: 50});
        }

        var contentToAdd = '';
        function addVisit() {
            $.ajax({
                url: baseUrl + 'employees/getall',
                type: 'get',
                success: function (data) {
                    console.log(data);
                    for (var i = 0; i < data.length; i++) {
                        contentToAdd += '<option value="' + data[i].Id + '">' + data[i].FirstName +' ' + data[i].LastName + '</option>';
                    }
                    var selectElement = $('#select');
                    selectElement.append(contentToAdd);
                },
                error: function () {
                    console.error('Pe error');
                },
                done: function () {
                    console.log('Pe done!');
                },
                complete: function () {
                    console.log("complete");
                }
            });
            var selectElement = $('#select');
            selectElement.append(contentToAdd);

            $('#submit-visit2').on('click', function (e) {

                var organiserId = $('#select').val();
                var title = $('#visit_title').val();
                var date = $('#datepicker').val();
                dataToSend= {
                    'Title':title,
                    'Date': date,
                    'OrganiserId': organiserId
                }
                dataToSendStringified = JSON.stringify(dataToSend);
                $.ajax({
                    url: baseUrl + 'visits/add',
                    type: 'POST',
                    data:  dataToSendStringified,
                    success: function () {
                        console.log('success');
                    },
                    error: function () {
                        console.error('Pe error');
                    },
                    done: function () {
                        console.log('Pe done!');
                    }
                });
            })
            $('.datepicker').pickadate({
                selectMonths: true, // Creates a dropdown to control month
                selectYears: 15 // Creates a dropdown of 15 years to control year
            });
            $(document).ready(function() {
                $('select').material_select();
            });
            // $('#add-visit-modal > .modal-content > div').html('<h1>Aqui!</h1>')
        }

        function getDateTime(originalDate) {
            var date = new Date(originalDate);
            returnObj = {};
            var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
            returnObj.date = {
                year: date.getUTCFullYear(),
                month: date.getUTCMonth(),
                monthName: monthNames[date.getUTCMonth()],
                day: date.getUTCDate()
            };
            returnObj.time = {
                hour: date.getUTCHours(),
                minute: date.getUTCMinutes()
            };
            return returnObj;

        }

        function printDate(date) {
            return date.day + " " + date.monthName;
        }
        function printTime(time) {
            if (time.minute < 10) {
                time.minute = "0" + time.minute;
            }
            return '<i class="material-icons tiny">schedule</i> ' + time.hour + ":" + time.minute;
        }

        function getDateTime2(originalDate) {
            var date = new Date(originalDate);

            var today = date.toLocaleDateString('en-GB', {
                timeZone: 'UTC',
                day : 'numeric',
                month : 'short',
                // year : 'numeric'
                hour: '2-digit',
                minute: '2-digit'
            }).split(' ').join(' ');
            return today;
        }
        function printOutcome(outcome) {
            if (outcome === null) {
                return '<i class="material-icons tooltipped pointer"data-position="right" data-delay="50" data-tooltip="Insufficient permissions">lock_outline</i>';
            }
            else {
                return '<i class="material-icons tooltipped pointer" data-position="right" data-delay="50" data-tooltip="' + outcome + '">subject</i>';
            }
        }
    })();

}]);
