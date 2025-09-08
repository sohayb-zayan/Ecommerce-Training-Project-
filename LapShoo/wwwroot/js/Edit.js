$(document).ready(function () {
    loadCategories();
});


function loadCategories() {
    $.ajax({
        url: '/Admin/Category/getdata',
        type: 'GET',
        success: function (response) {
            $('#categoryFilter').empty();
            $.each(response.data, function (i, cat) {
                $('#categoryFilter').append(
                    $('<option>', {
                        value: cat.id,
                        text: cat.name
                    })
                );
            });
        },
        error: function (xhr, status, error) {
            console.error("Error loading categories:", error);
        }
    });
}