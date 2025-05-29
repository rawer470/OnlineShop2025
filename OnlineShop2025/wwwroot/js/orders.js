$(document).ready(function() {
    console.log('Initializing DataTable...');
    
    try {
        $('#tblData').DataTable({
            processing: true,
            serverSide: false,
            ajax: {
                url: '/Order/GetOrderUser',
                dataSrc: function(json) {
                    console.log('Raw API response:', json);
                    
                    // 1. Проверка если данные пришли в свойстве "data"
                    if (json && json.data) {
                        console.log('Using data from "data" property');
                        return json.data;
                    }
                    // 2. Проверка если данные пришли как массив
                    else if (Array.isArray(json)) {
                        console.log('Using direct array data');
                        return json;
                    }
                    // 3. Если структура не распознана
                    else {
                        console.error('Unknown data structure:', json);
                        return [];
                    }
                },
                error: function(xhr, status, error) {
                    console.error('AJAX Error:', status, error);
                    alert('Error loading data. See console for details.');
                }
            },
            columns: [
                { 
                    data: function(row) {
                        return row.OrderDate || row.orderDate || row.date || 'N/A';
                    },
                    title: 'Order Date',
                    width: '15%'
                },
                { 
                    data: function(row) {
                        return row.OrderStatus || row.orderStatus || row.status || 'N/A';
                    },
                    title: 'Status',
                    width: '15%'
                },
                { 
                    data: function(row) {
                        const amount = row.FinalOrderTotal || row.finalOrderTotal || row.total;
                        return amount ? '$' + parseFloat(amount).toFixed(2) : '$0.00';
                    },
                    title: 'Total',
                    width: '15%'
                },
                {
                    data: 'id',
                    render: function(data) {
                        return data ? `<a href="/Order/Details?Id=${data}" class="btn btn-primary">Detail</a>` : '';
                    },
                    title: 'Actions',
                    width: '5%',
                    orderable: false
                }
            ],
            language: {
                emptyTable: "No data available",
                loadingRecords: "Loading...",
                processing: "Processing..."
            },
            initComplete: function() {
                console.log('DataTable initialized successfully');
            }
        });
    } catch (e) {
        console.error('DataTable initialization error:', e);
    }
});