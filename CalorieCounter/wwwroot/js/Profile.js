document.addEventListener("DOMContentLoaded", function () {
    var ctx = document.getElementById("weight-chart").getContext("2d");
    

    var labels = weightHistory.map(function (record) {
        // return new Date(record.RecordedDate).toLocaleDateString();
        return record.RecordedDate;
    });

    var data = weightHistory.map(function (record) {
        return record.Weight;
    });

    var chart = new Chart(ctx, {
        type: "line",
        data: {
            labels: labels,
            datasets: [
                {
                    label: "Weight",
                    data: data,
                    borderColor: "rgba(75, 192, 192, 1)",
                    borderWidth: 1,
                    fill: false,
                },
            ],
        },
        options: {
            responsive: true,
            scales: {
                x: {
                    type: "time",
                    time: {
                        unit: "day",
                    },
                    title: {
                        display: true,
                        text: "Date",
                    },
                },
                y: {
                    title: {
                        display: true,
                        text: "Weight (kg)",
                    },
                },
            },
        },
    });
});
