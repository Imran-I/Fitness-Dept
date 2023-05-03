'use strict';
const FromDate = document.querySelector('.FromDate');
const ToDate = document.querySelector('.ToDate');

function BindFromDate() {

    if (FromDate == null)
        return;

    let FromDateOnload = FromDate.value == null ? "today" : FromDate.value;
    let ToDateOnload;

    flatpickr
        (
            FromDate,
            {
                enableTime: false,
                dateFormat: "d-m-Y",
                maxDate: "today",
                onReady: function (_0, dateStr, _1) {
                    ToDateOnload = ToDate.value == null ? dateStr : ToDate.value;
                    BindToDate(ToDateOnload);
                },
                onOpen: function () {
                    const numInput = document.querySelectorAll('.numInput');
                    numInput.forEach((input) => input.type = '');
                },
                onChange: function (_0, dateStr, _1) {
                    BindToDate(dateStr);
                }
            }
        )
};

function BindToDate(date) {
    flatpickr
        (
            ToDate,
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
        );
};
BindFromDate();