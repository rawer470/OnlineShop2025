var datatable;
$(document).ready(function () {
    loadDataTable("GetInquiryList","#tblData")
})

function loadDataTable(url, idTable) {
    dataTable = $(idTable).DataTable({   // проверяем наличие синтаксических ошибок
        "ajax": {                           // описание правил загрузки данных в таблицу - загрузка через запрос ajax
            "url": "/inquiry/" + url
        },
        "columns": [
            { "data": "fullName", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "email", "width": "15%" },

            // добавляем кнопку
            {
                "data": "id",
                "render": function (data) {
                    // при нажатии на кнопку нужно перейти на конкретную страницу
                    return `
                        
                            <a href="/Inquiry/Details?Id=${data}" class="btn btn-success text-white"
                            style="cursor:pointer" >
                                EDIT
                            </a>
                      
                    `;
                },

                "width": "5%"  // зададим ширину вывода на экран
            }
        ]
    });
}