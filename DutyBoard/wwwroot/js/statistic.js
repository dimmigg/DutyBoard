﻿function fillPolarArea(labelsArr, dataArr) {
    const ctx = document.getElementById('polarArea').getContext('2d');
    const chart = new Chart(ctx, {
        type: 'polarArea',
        data: {
            labels: labelsArr,
            datasets: [{

                data: dataArr,
                backgroundColor: [
                    '#F44336',
                    '#9C27B0',
                    '#3F51B5',
                    '#03A9F4',
                    '#4CAF50',
                    '#FFEB3B',
                    '#FFC107',
                    '#26C6DA',
                    '#673AB7',
                ],
                borderWidth: 1
            }],

        },
        options: {
            plugins: {
                legend: {
                    display: true,
                    position: 'bottom',
                }
            },
            responsive: true,
            //scales: {
            //    r: {
            //        pointLabels: {
            //            display: true,
            //            centerPointLabels: true,
            //            font: {
            //                size: 14,
            //            }
            //        }
            //    }
            //}
        }
    });

    return chart;
}

function fillBar(labelsArr, dataArrWork, dataArrHoli) {
    const ctx = document.getElementById('bar').getContext('2d');
    const chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labelsArr,
            datasets: [
                {
                    label: 'Будни',
                    data: dataArrWork,
                    backgroundColor: '#1E88E5',
                },
                {
                    label: 'Выходные',
                    data: dataArrHoli,
                    backgroundColor: '#F9A825',
                },
            ]


        },
        options: {
            indexAxis: 'y',
            responsive: true,
            scales: {
                x: {
                    stacked: true,
                    beginAtZero: false,
                    grid:
                    {
                        display: false
                    }
                },
                y: {
                    stacked: true,
                    ticks: {
                        //display: false,
                        font: {
                            size: 12
                        }
                    },
                    grid:
                    {
                        display: false
                    }
                },

            },
            plugins: {
                legend: {
                    display: true,
                    position: 'bottom',
                    stackWeight: 10
                    /*  labels: {*/
                    // This more specific font property overrides the global property
                    //font: {
                    //size: 14
                    //}
                },
                title: {
                    font: { size: 50 },
                },

            },
        }
    });
    console.log(chart);
    return chart;
}