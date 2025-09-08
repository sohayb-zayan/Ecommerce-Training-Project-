var dtble;
$(document).ready(function () {
    loaddata();
});
function loaddata() {
    dtble = $("#mytable").DataTable({
        "ajax": {
            "url": "/Admin/Category/GetData",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name" },
            { "data": "description" },
            {
                "data": "createdTime",
                "render": function (data) {
                    return new Date(data).toLocaleString();
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <button class="btn btn-success btn-sm" onclick="EditItem(${data})">Edit</button>
                        <a onClick="DeleteItem('/Admin/Category/Delete/${data}')" class="btn btn-danger btn-sm">Delete</a>
                    `;
                }
            }
        ]
    });
}
function DeleteItem(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "This record will be permanently deleted!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE", 
                success: function (data) {
                    Swal.fire(
                        "Deleted!",
                        "Your record has been deleted.",
                        "success"
                    );
                    dtble.ajax.reload(); 
                },
                error: function (xhr, status, error) {
                    Swal.fire(
                        "Error!",
                        "Something went wrong while deleting.",
                        "error"
                    );
                }
            });
        }
    });
}
function EditItem(id) {
    $.ajax({
        url: `/Admin/Category/GetById/${id}`,  
        type: "GET",
        success: function (res) {
            if (res) {
                
                $("#CategoryId").val(res.id);
                $("#Name").val(res.name);            
                $("#Description").val(res.description); 
                $("#editModal").modal("show");
            }
        }
    });
}
function SaveEdit() {
    let category = {
        Id: $("#CategoryId").val(),
        Name: $("#Name").val(),
        Description: $("#Description").val()
    };

    $.ajax({
        url: "/Admin/Category/Edit",
        type: "POST",
        data: category,
        success: function (res) {
            if (res.success) {
                Swal.fire("Updated!", res.message, "success");
                $("#editModal").modal("hide");
                dtble.ajax.reload(); 
            } else {
                Swal.fire("Error!", res.message, "error");
            }
        }
    });
}