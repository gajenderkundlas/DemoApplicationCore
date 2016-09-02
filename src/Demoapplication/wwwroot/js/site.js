// Write your Javascript code.
function deleteStudent(id) {
    if (confirm("Are you sure you want to delete")) {
        $.ajax({
            URL: "Student/Delete",
            method:"Get",
            contentType: "application/json",
            data: { ID: id },
            success: function () {
                location.reload();
            },
            error: function (xhrstatus) {
                alert(xhrstatus.errorMessage);
            }
        })
    }
}
function Search(type) {
    var total = 10000;
    var pageSize = 1;
    var orderBy = "";
    if (type == "top") {
        orderBy = "desc"
        total = 10;
        pageSize = 1;
    } else if (type == "bottom") {
        orderBy = "asc"
        total = 10;
        pageSize = 1;
    }
    else {
        orderBy = "asc"
    }
    $.ajax({
        URL: "Student/Index",
        method: "Get",
        contentType: "application/json",
        data: {
            dataType: "View",
            searchStudent: $("#Search").val(),
            pagesize: pageSize,
            total: total,
            orderby:orderBy
        },
        success: function (data) {
            $("#SearchResult").html(data);
        },
        error: function (xhrstatus) {
            alert(xhrstatus.errorMessage);
        }
    })
}