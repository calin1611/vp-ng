app.service('dateService', ['$http', '$rootScope', function ($http, $rootScope) {

    this.getDateTime = function (originalDate) {
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
        // return returnObj;
        return this.printDateTime(returnObj.date, returnObj.time);
    };

    this.printDate = function (date) {
        return date.day + " " + date.monthName;
    };

    this.printTime = function (time) {
        if (time.minute < 10) {
            time.minute = "0" + time.minute;
        }
        return '<i class="material-icons tiny">schedule</i> ' + time.hour + ":" + time.minute;
    };
//                        + printDate(dateTime.date) +"  " + printTime(dateTime.time) +

    this.printDateTime = function (date, time) {
        return this.printDate(date) + ' ' + this.printTime(time);
    };
//----------------------

    this.getDateTime2 = function (originalDate) {
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
    };
//                    getDateTime2(data[i].Date) +


}]);
