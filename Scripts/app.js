$(document).ready(function () {
    $('#login').click(function () {
        var email = $('#email').val();
        var password = $('#password').val();

        $.ajax({
            url: 'api/Employees/Login',
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify({
                email: email,
                password: password
            }),
            contentType: 'application/json',
            success: function (response) {
                if (response.IsHR) {
                    window.location.href = 'HREmployeeMaintenance.html';
                } else {
                    window.location.href = 'EmployeesList.html';
                }
            },
            error: function (error) {
                var errorMessage = error.responseJSON.message;

                if (errorMessage) {
                    alert('Error: ' + errorMessage);
                } else {
                    alert('ERROR');
                }
            }
        });
    });
});


$(document).ready(function () {
    $.ajax({
        url: 'api/Employees/GetEmployees',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $.each(data, function (key, value) {
                var row = $('<tr>');
                row.append($('<td>').text(value.EmployeeID));
                row.append($('<td>').text(value.EmployeeName));
                row.append($('<td>').text(value.Email));
                row.append($('<td>').text(value.HireFiscalYear));
                $('#employees tbody').append(row);
            });

            // DataTablesプラグインを適用します。
            $('#employees').DataTable();
        },
        error: function (error) {
            alert('ERROR');
        }
    });
});

