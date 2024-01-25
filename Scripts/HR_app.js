$(document).ready(function () {
    var table = $('#employees').DataTable();

    var loadEmployees = function (searchTerm) {
        $.ajax({
            url: 'api/Employees/GetEmployeesWithDetails',
            type: 'GET',
            dataType: 'json',
            data: {
                searchTerm: searchTerm
            },
            contentType: 'application/json',
            success: function (data) {
                table.clear();
                $.each(data, function (key, value) {
                    var row = $('<tr>');
                    row.append($('<td>').text(value.EmployeeID));
                    row.append($('<td>').text(value.EmployeeName));
                    row.append($('<td>').text(value.Email));
                    row.append($('<td>').text(value.Height));
                    row.append($('<td>').text(value.Weight));
                    row.append($('<td>').text(value.HireFiscalYear));
                    row.append($('<td>').text(formatDate(value.Birthday)));
                    row.append($('<td>').text(value.BloodType));
                    row.append($('<td>').text(value.Password));
                    row.append($('<td>').text(value.DepartmentName));
                    row.append($('<td>').text(value.EmploymentId));
                    row.append($('<td>').text(formatDate(value.EntryDate)));
                    row.append($('<td>').text(formatDate(value.RetirementDate)));
                    table.row.add(row);
                });
                table.draw();
            },
            error: function (error) {
                alert('ERROR');
            }
        });
    };

    loadEmployees();

    $('#searchButton').click(function () {
        var searchTerm = $('#search').val();
        loadEmployees(searchTerm);
    });
});

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('/');
}