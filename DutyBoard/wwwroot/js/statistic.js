function fillPolarArea(labelsArr, dataArr) {
    const ctx = document.getElementById('polarArea').getContext('2d');
    const chart = new Chart(ctx, {
        type: 'polarArea',
        data: {
            labels: labelsArr,
            datasets: [{

                data: dataArr,
                backgroundColor: [
                    '#f44336',
                    '#9c27b0',
                    '#3f51b5',
                    '#03a9f4',
                    '#009688',
                    '#8bc34a',
                    '#ffeb3b',
                    '#FFC107',
                    '#ff9800',   
                    '#26C6DA',
                    '#cddc39',
                    '#4CAF50',
                    '#673AB7',
                    '#ffc107',
                    '#795548',
                ],
                borderWidth: 1
            }],

        },
        options: {
            plugins: {
                legend: {
                    display: true,
                    position: 'bottom',
                    onHover: handleHover,
                    onLeave: handleLeave
                }
            },
            responsive: true,
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
                    position: 'bottom'
                },
                title: {
                    font: { size: 50 },
                },

            },
        }
    });
    return chart;
}

function handleHover(evt, item, legend) {
    legend.chart.data.datasets[0].backgroundColor.forEach((color, index, colors) => {
        colors[index] = index === item.index || color.length === 9 ? color : color + '30';
    });
    legend.chart.update();
}
// </block:handleHover>

// <block:handleLeave:2>
// Removes the alpha channel from background colors
function handleLeave(evt, item, legend) {
    legend.chart.data.datasets[0].backgroundColor.forEach((color, index, colors) => {
        colors[index] = color.length === 9 ? color.slice(0, -2) : color;
    });
    legend.chart.update();
}