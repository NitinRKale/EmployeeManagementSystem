$(document).ready(function () {

    /*Create New Employee Start */
    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        var $empform = $(this).closest('form');

        if (!$empform.valid()) return;


        // Normalize BirthDate to ISO (yyyy-MM-dd) so ASP.NET model binder can parse reliably
        var rawBirth = $('#BirthDate').val();
        var birthIso = '';
        if (rawBirth) {
            if (window.moment) {
                var m = moment(rawBirth, ['DD-MM-YYYY', 'DD/MM/YYYY', 'YYYY-MM-DD', moment.ISO_8601], true);
                if (!m.isValid()) m = moment(rawBirth); // permissive fallback
                if (m.isValid()) birthIso = m.format('YYYY-MM-DD');
                else birthIso = rawBirth; // let server try to parse if unknown format
            } else {
                // simple dd-MM-yyyy or dd/MM/yyyy -> yyyy-MM-dd
                var match = rawBirth.match(/^\s*(\d{1,2})[\/-](\d{1,2})[\/-](\d{4})\s*$/);
                if (match) {
                    birthIso = match[3] + '-' + match[2].padStart(2, '0') + '-' + match[1].padStart(2, '0');
                } else {
                    birthIso = rawBirth;
                }
            }
        }

        var employee = {
            empId: 0,
            EmpFirstName: $('#EmpFirstName').val(),
            EmpMiddleName: $('#EmpMiddleName').val(),
            EmpLastName: $('#EmpLastName').val(),
            EmailId: $('#EmailId').val(),
            BirthDate: birthIso,
            EmpGender: $('input[name="EmpGender"]:checked').val() || null,
            PhoneNumber: $('#PhoneNumber').val(),
            EmployeeAddress: $('#EmployeeAddress').val(),
            Salary: parseFloat($('#Salary').val()) || 0,
            EmpStatus: $('#EmpStatus').is(':checked'),
            DepartmentId: parseInt($('#DepartmentId').val(), 10) || 0,
            DesignationId: parseInt($('#DesignationId').val(), 10) || 0
        };

        var token = $empform.find('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: '/EmployeeAjax/Create',
            type: 'POST',
            //contentType: 'application/json; charset=utf-8', // required for [FromBody] JSON binding
           // data: JSON.stringify(employee),
           data: employee, // use standard form data for [FromForm] binding
            headers: token ? { 'RequestVerificationToken': token } : {},
            success: function (result) {
                if (result && result.success) {
                    Swal.fire('Saved', result.message || 'Saved', 'success').then(function () {
                        if (result.redirectTo) window.location.href = result.redirectTo;
                    });
                } else {
                    Swal.fire('Error', (result && (result.message || result.ErrorMessage)) || 'Failed to create employee.', 'error');
                }
            },
            error: function (xhr) {
                var msg = 'Server error.';
                try { msg = (xhr.responseJSON || JSON.parse(xhr.responseText)).message || msg; } catch { }
                Swal.fire('Error', msg, 'error');
            }
        });
    });
    /*Create New Employee End */


    // Edit employee — mirrors #btnSubmit behavior but sends EmpId and targets Edit endpoint
    $('#btnUpdate').click(function (e) {
        e.preventDefault();
        var $empform = $(this).closest('form');
        if (!$empform.valid()) return;

        var $btn = $(this);

        // Normalize BirthDate to ISO (yyyy-MM-dd) so ASP.NET model binder can parse reliably
        var rawBirth = $('#BirthDate').val();
        var birthIso = '';
        if (rawBirth) {
            if (window.moment) {
                var m = moment(rawBirth, ['DD-MM-YYYY', 'DD/MM/YYYY', 'YYYY-MM-DD', moment.ISO_8601], true);
                if (!m.isValid()) m = moment(rawBirth); // permissive fallback
                if (m.isValid()) birthIso = m.format('YYYY-MM-DD');
                else birthIso = rawBirth;
            } else {
                var match = rawBirth.match(/^\s*(\d{1,2})[\/-](\d{1,2})[\/-](\d{4})\s*$/);
                if (match) {
                    birthIso = match[3] + '-' + match[2].padStart(2, '0') + '-' + match[1].padStart(2, '0');
                } else {
                    birthIso = rawBirth;
                }
            }
        }

        var employee = {
            EmpId: parseInt($('#EmpId').val(), 10) || 0,
            EmpFirstName: $('#EmpFirstName').val(),
            EmpMiddleName: $('#EmpMiddleName').val(),
            EmpLastName: $('#EmpLastName').val(),
            EmailId: $('#EmailId').val(),
            BirthDate: birthIso,
            EmpGender: $('input[name="EmpGender"]:checked').val() || null,
            PhoneNumber: $('#PhoneNumber').val(),
            EmployeeAddress: $('#EmployeeAddress').val(),
            Salary: parseFloat($('#Salary').val()) || 0,
            EmpStatus: $('#EmpStatus').is(':checked'),
            DepartmentId: parseInt($('#DepartmentId').val(), 10) || 0,
            DesignationId: parseInt($('#DesignationId').val(), 10) || 0
        };

        var token = $empform.find('input[name="__RequestVerificationToken"]').val();

        $btn.prop('disabled', true);

        $.ajax({
            url: '/EmployeeAjax/Edit',
            type: 'POST',
            //contentType: 'application/json; charset=utf-8',
            // data: JSON.stringify(employee), // JSON data for [FromBody] binding
            data: employee, // form-encoded data for model binding
            headers: token ? { 'RequestVerificationToken': token } : {},
            success: function (result) {
                if (result && result.success) {
                    Swal.fire('Updated', result.message || 'Updated', 'success').then(function () {
                        if (result.redirectTo) window.location.href = result.redirectTo;
                        else window.location.reload();
                    });
                } else {
                    Swal.fire('Error', (result && (result.message || result.ErrorMessage)) || 'Failed to update employee.', 'error');
                }
            },
            error: function (xhr) {
                var msg = 'Server error.';
                try { msg = (xhr.responseJSON || JSON.parse(xhr.responseText)).message || msg; } catch { }
                Swal.fire('Error', msg, 'error');
            },
            complete: function () {
                $btn.prop('disabled', false);
            }
        });
    });


    ///Delete Employee Start ///

    $('#btnDelete').on('click', function (e) {
        e.preventDefault();
        var $btn = $(this);
        var empId = $('#EmpId').val();

        if (!empId) {
            console.warn('No empId found on delete button.');
            return;
        }

        Swal.fire({
            title: 'Are you sure?',
            text: 'This action will permanently delete the employee.',
            icon: 'alert',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            cancelButtonText: 'Cancel'
        }).then(function (result) {
            if (!result.isConfirmed) return;
            debugger;
            // Anti-forgery token   
            var token = $('input[name="__RequestVerificationToken"]').val();

            $btn.prop('disabled', true);

            $.ajax({
                url: '/EmployeeAjax/Delete',
                type: 'POST',
                headers: token ? { 'RequestVerificationToken': token } : {},
                data: { empId: empId },
                success: function (resp) {
                    if (resp && resp.success) {
                        Swal.fire('Deleted', resp.message || 'Employee deleted.', 'success')
                            .then(function () {
                                if ($.fn.DataTable && $('#jqtblEmployee').length) {
                                    $('#jqtblEmployee').DataTable().ajax.reload(null, false);
                                } else {
                                    $btn.closest('tr').fadeOut(200, function () { $(this).remove(); });
                                }
                                if (resp.redirectTo) {
                                    window.location.href = resp.redirectTo;
                                }
                            });
                    } else {
                        Swal.fire('Error', (resp && (resp.ErrorMessage || resp.message)) || 'Failed to delete', 'error');
                    }
                },
                error: function (xhr) {
                    var err = 'An error occurred while deleting the employee.';
                    try {
                        var j = xhr.responseJSON || JSON.parse(xhr.responseText);
                        if (j && (j.message || j.ErrorMessage)) err = j.message || j.ErrorMessage;
                    } catch (ex) { }
                    Swal.fire('Error', err, 'error');
                },
                complete: function () {
                    $btn.prop('disabled', false);
                }
            });
        });
    });

    ///Delete Employee End ///

    /*
    
      $('#btnUpdate').on('click', function (e) {
        e.preventDefault();
        debugger;

        var $btn = $(this);
        var $form = $btn.closest('form');

        // client-side validation
        if (!$form.valid()) return;

        // Build JS object from form fields
        var obj = {};
        $form.serializeArray().forEach(function (item) {
            obj[item.name] = item.value;
        });

        // Ensure EmpStatus boolean is accurate
        obj.EmpStatus = $form.find('input[name="EmpStatus"]').is(':checked');

        // Normalize BirthDate to ISO (yyyy-MM-dd) so JSON model binder can parse it reliably.
        var bd = (obj.BirthDate || '').toString().trim();
        var isoDate = null;
        debugger;
        if (bd) {
            if (window.moment) {
                // try strict formats first, then a non-strict fallback
                var formats = [
                    'YYYY-MM-DD',
                    'YYYY/MM/DD',
                    'DD-MM-YYYY',
                    'DD/MM/YYYY',
                    'MM-DD-YYYY',
                    'MM/DD/YYYY',
                    moment.ISO_8601
                ];
                var m = moment(bd, formats, true);
                if (!m.isValid()) {
                    // non-strict fallback (more permissive)
                    m = moment(bd);
                }
                if (m.isValid()) {
                    isoDate = m.format('YYYY-MM-DD');
                } else {
                    // parsing failed: send the raw value rather than null to avoid losing the entered value
                    isoDate = bd;
                }
            } else {
                // No moment: attempt simple regex-based conversions
                var ymd = /^\s*(\d{4})[\/-](\d{1,2})[\/-](\d{1,2})\s*$/; // yyyy-mm-dd or yyyy/mm/dd
                var dmy = /^\s*(\d{1,2})[\/-](\d{1,2})[\/-](\d{4})\s*$/; // dd-mm-yyyy or dd/mm/yyyy
                var match;
                if (ymd.test(bd)) {
                    match = bd.match(ymd);
                    isoDate = match[1] + '-' + match[2].padStart(2, '0') + '-' + match[3].padStart(2, '0');
                } else if (dmy.test(bd)) {
                    match = bd.match(dmy);
                    isoDate = match[3] + '-' + match[2].padStart(2, '0') + '-' + match[1].padStart(2, '0');
                } else {
                    // last-resort: keep original string so the server can attempt parsing
                    isoDate = bd;
                }
            }
        } else {
            // empty input -> explicit null so server knows it's cleared
            isoDate = null;
        }

        obj.BirthDate = isoDate; // now either "YYYY-MM-DD", original string, or null (if empty)

        // Ensure numeric fields are numbers
        if (obj.EmpId) obj.EmpId = parseInt(obj.EmpId, 10) || 0;
        if (obj.DepartmentId) obj.DepartmentId = parseInt(obj.DepartmentId, 10) || 0;
        if (obj.DesignationId) obj.DesignationId = parseInt(obj.DesignationId, 10) || 0;
        if (obj.Salary) obj.Salary = parseFloat(obj.Salary) || 0;

        // Anti-forgery token
        var token = $form.find('input[name="__RequestVerificationToken"]').val();

        $btn.prop('disabled', true);

        $.ajax({
            url: '/EmployeeAjax/Edit',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(obj),
            headers: token ? { 'RequestVerificationToken': token } : {},
            success: function (result) {
                debugger;
                if (result && result.success) {
                    Swal.fire({
                        title: 'Updated',
                        icon: 'success',
                        text: result.message || 'Employee updated successfully.'
                    }).then(function () {
                        if (result.redirectTo) {
                            window.location.href = result.redirectTo;
                        } else {
                            // default fallback
                            window.location.reload();
                        }
                    });
                } else {
                    var msg = (result && (result.ErrorMessage || result.message)) || 'Failed to update employee.';
                    Swal.fire({ icon: 'error', title: 'Error', text: msg });
                }
            },
            error: function (xhr) {
                console.error('Update error', xhr);
                Swal.fire({ icon: 'error', title: 'Error', text: 'Server error; see console/Network tab' });
            },
            complete: function () {
                $btn.prop('disabled', false);
            }
        });
    });
    
    */
});