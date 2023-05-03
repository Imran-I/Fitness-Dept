'use strict';
const DashFromDate = document.querySelector('.DashFromDate');
const DashToDate = document.querySelector('.DashToDate');

function BindDashFromDate() {
    if (DashFromDate == null)
        return;

    let FromDateOnload = DashFromDate.value == null ? "today" : DashFromDate.value;
    let ToDateOnload;

    flatpickr
        (
            DashFromDate,
            {
                enableTime: false,
                dateFormat: "d-m-Y",
                maxDate: "today",
                onReady: function (_0, dateStr, _1) {
                    ToDateOnload = DashToDate.value == null ? dateStr : DashToDate.value;
                    BindDashToDate(ToDateOnload);
                },
                onOpen: function () {
                    const numInput = document.querySelectorAll('.numInput');
                    numInput.forEach((input) => input.type = '');
                },
                onChange: function (_0, dateStr, _1) {
                    BindDashToDate(dateStr);
                }
            }
        )
};

function BindDashToDate(date) {
    flatpickr
        (
            DashToDate,
            {
                enableTime: false,
                dateFormat: "d-m-Y",
                defaultDate: date,
                minDate: date,
                onOpen: function () {
                    const numInput = document.querySelectorAll('.numInput');
                    numInput.forEach((input) => input.type = '');
                }
            }
        )
};
BindDashFromDate();