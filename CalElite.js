/*
 * [Outdated. Out of use.]
 * Скрипт для GoogleScript. Запускается по триггеру в понедельник.
 * Забирает пары в Этонате на неделю с сайта расписания meow и добавляет соответсвующие события.
 */

function doTrigger() {
    var response = UrlFetchApp.fetch("http://meow.am.tpu.ru/__dev/etonat.php").getContentText();
    var json = (new RegExp(/"\[.*?\]"/).exec(response))[0];
    var model = JSON.parse(JSON.parse(json));

    for (var i = 0; i < model.length; i++) {
        addEvent(model[i]);
    }
}

function addEvent(eventData) {
    CalendarApp.getCalendarsByName("Пары3")[0].createEvent(
        eventData.short,
        getTimeFromModel(eventData, "start"),
        getTimeFromModel(eventData, "end"));
}

function getTimeFromModel(eventData, s) {
    var periods = [{
            start: "08:30:00",
            end: "10:05:00"
        }, {
            start: "10:25:00",
            end: "12:00:00"
        }, {
            start: "12:20:00",
            end: "13:55:00"
        }, {
            start: "14:15:00",
            end: "15:50:00"
        }, {
            start: "16:10:00",
            end: "17:45:00"
        }, {
            start: "18:05:00",
            end: "19:40:00"
        }, {
            start: "20:00:00",
            end: "21:35:00"
        }
    ];

    var date = getMonday(new Date());
    var currentDate = new Date(date.setDate(date.getDate() + eventData.weekday));
    var eventDate = new Date(currentDate.toDateString() + " " + (periods[eventData.time][s]) + " GMT+07:00");

    return new Date(eventDate);
}

function getMonday(d) {
    d = new Date(d);

    var day = d.getDay();
    var diff = d.getDate() - day + (day === 0 ? -6 : 1);
    var date = new Date(d.setDate(diff));
    var date1 = new Date(date.setHours(0, 0, 0, 0));

    return date1;
}