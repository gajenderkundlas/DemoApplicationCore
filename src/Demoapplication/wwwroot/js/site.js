// Write your Javascript code.
var total = 5;
function deleteStudent(id) {
    if (confirm("Are you sure you want to delete")) {
        alert("enter in delete");
        $.ajax({
            url: "Student/Delete",
            method:"Get",
            contentType: "application/json",
            data: { ID: id },
            success: function (data) {
                if (data == 1) {
                     location.reload();
                } else {
                    alert("Error! There is error occur while deleting the record.")
                }
            },
            error: function (xhrstatus) {
                alert(xhrstatus.errorMessage);
            }
        })
    }
}
function Prev() {
    var pageIndex = $("#hdnPageSize").val();
    if (parseInt(pageIndex) > 1) {
        pageIndex = parseInt(pageIndex) - 1;
        $("#hdnPageSize").val(pageIndex);
    } else {
        pageIndex = 1;
        $("btnPrev").attr("disabled", "disabled");
        $("btnNext").removeAttr("disabled");
    }
    Search("paging");
}
function Next() {
    var pageIndex = $("#hdnPageSize").val();
    alert((parseInt(pageIndex) * parseInt(total))+"total record="+$("#hdnTotalRecord").val());
    if ((parseInt(pageIndex)*parseInt(total)) < parseInt($("#hdnTotalRecord").val())) {
        pageIndex = parseInt(pageIndex) + 1;
        $("#hdnPageSize").val(pageIndex);
        $("btnPrev").removeAttr("disabled");
    } else {
        //pageIndex = 1;
        $("btnNext").attr("disabled", "disabled");
        $("btnPrev").removeAttr("disabled");
    }
    Search("paging");
}

function Search(type) {
   // var total = 5;
    var pageSize = $("#hdnPageSize").val();
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
    else if (type == "normal") {
        pageSize = 1;
        if (total <= parseInt($("#hdnTotalRecord").val())) {
            $("btnNext").attr("disabled", "disabled");
            $("btnPrev").attr("disabled", "disabled");
        } 
        orderBy = "asc"
    } 
    $.ajax({
        url: "Student/Index",
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
        error: function (xhr,status,error) {
          var err=eval("("+xhr.responseText+")")
          alert(err.Message);
        }
    })
}