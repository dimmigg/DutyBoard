function fillStatistic(labelsArr, dataArr) {   
    const ctx = document.getElementById('myChart').getContext('2d');
    const myChart = new Chart(ctx, {
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
        '#F44336',
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
        scales: {
        r: {
            pointLabels: {
                display: true,
                    centerPointLabels: true,
                        font: {
                    size: 14,
                            }
            }
        }
    }
}
    });
    return myChart;
}