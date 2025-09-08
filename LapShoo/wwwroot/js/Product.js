var dtble;
$(document).ready(function () {
    loaddata();
    loadCategories();
});

function loaddata() {
    dtble = $("#mytable").DataTable({
        "ajax": {
            "url": "/Admin/Product/GetData",
            "type": "GET",
            "datatype": "json",
            "data": function (d) {
                d.categoryId = $('#categoryFilter').val(); // 👈 هنا بعت الفئة المختارة
            }
        },
        "columns": [
            { "data": "name" },
            { "data": "price" },
            { "data": "description" },
            { "data": "category.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                     <button class="btn btn-success btn-sm" onclick="Show(${data})">Show</button>
                    `;
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Admin/Product/Edit/${data}" class="btn btn-success btn-sm">Edit</a>
                        <a onClick="DeleteItem('/Admin/Product/Delete/${data}')" class="btn btn-danger btn-sm">Delete</a>
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

function Show(id) {
    $.ajax({
        url: `/Admin/Product/GetPro`,   
        type: "GET",
        data: { id: id },               
        success: function (res) {
            if (res.success) {
                var p = res.data;

                // Fill modal with data
                $("#ShowName").text(p.name);
                $("#ShowDescription").text(p.description);
                $("#ShowPrice").text(p.price);
                $("#ShowCategory").text(p.category ? p.category.name : "No Category");
                $("#ShowImg").attr("src", p.imgUrl);
                $("#showProductModal").modal("show");
            } else {
                Swal.fire("Error", res.message, "error");
            }
        },
        error: function () {
            Swal.fire("Error", "Failed to load product details", "error");
        }
    });
}


function loadCategories() {
    $.ajax({
        url: '/Admin/Category/getdata',
        type: 'GET',
        success: function (response) {
            var $catFilter = $('#categoryFilter');


            if ($catFilter.hasClass("select2-hidden-accessible")) {
                $catFilter.select2('destroy');
            }

            $catFilter.empty();
            $catFilter.append('<option value="null" selected>----------- Choose Category -----------</option>');
            $catFilter.append('<option value="0">Get Product With No Category</option>');
            $catFilter.append('<option value="-1">Get all</option>');

            $.each(response.data, function (i, cat) {
                $catFilter.append(
                    $('<option>', {
                        value: cat.id,
                        text: cat.name
                    })
                );
            });


            $catFilter.select2({
                placeholder: "----------- Choose Category2 -----------",
                allowClear: true,
                width: '100%'
            });
        },
        error: function (xhr, status, error) {
            console.error("Error loading categories:", error);
        }
    });
}

function ReLoad() {
    $('#mytable').DataTable().ajax.reload();
    console.log('Hi iam done')
}
