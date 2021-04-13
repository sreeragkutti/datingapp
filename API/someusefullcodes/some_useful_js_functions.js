convertStringToDateFormat: function (date) {
            return new Date(date.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3"));
        }
		
addMonths: function (date, months) {
            var d = date.getDate();
            date.setMonth(date.getMonth() + parseInt(months));
            if (date.getDate() != d) {
                date.setDate(0);
            }
            return date;
        }
convertStringDateToYYYYMMDD: function (date) {
            let convertDate = date.split("/").reverse().join("-");
            return convertDate;
        }