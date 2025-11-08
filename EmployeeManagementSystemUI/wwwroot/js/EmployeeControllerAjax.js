$(document).ready(function () {

    /*Create New Employee Start */

    // send form-encoded data so default model binder can bind to EmployeeMaster
    $(function () {
        $('#btnSubmit').on('click', function (e) {
            debugger;
            e.preventDefault();

            //var $empform = $(this).closest('form');
            var $form = $(this).closest('form');

            // Trigger jQuery Unobtrusive Validation
            if (!$form.valid()) {
                // If invalid, do not proceed with AJAX
                return;
            }
            
            // log serialized data for debugging
            var serialized = $form.serialize();
            console.log('Serialized form payload:', serialized);

            // optional: show each name/value pair
            $form.serializeArray().forEach(function (f) { console.log(f.name + '=' + f.value); });

            // ensure antiforgery token input is present in the form
            var token = $form.find('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                url: '/EmployeeAjax/Create',
                method: 'POST',
                // do NOT set contentType here (let jQuery use application/x-www-form-urlencoded)
                data: serialized,
                headers: token ? { 'RequestVerificationToken': token } : {},
                success: function (resp) {
                    console.log('Server response:', resp);
                    if (resp && resp.success) {
                        Swal.fire({
                            title: 'Saved',
                            icon: 'success',
                            text: resp.message || 'Saved'
                        }).then(function ()
                        {
                            if (resp.redirectTo) window.location.href = resp.redirectTo;
                        });
                    } else {
                        Swal.fire('Error', (resp && (resp.ErrorMessage || resp.message)) || 'Failed', 'error');
                    }
                },
                error: function (xhr) {
                    console.error('AJAX error', xhr);
                    Swal.fire('Error', 'Server error; see console/Network tab', 'error');
                }
            });
        });
    });    

    //$('#btnSubmit').click(function (e) {
    //    e.preventDefault();
    //    debugger;
    //    var $empform = $(this).closest('form');
    //    debugger;

    //    // Trigger jQuery Unobtrusive Validation
    //    if (!$empform.valid()) {
    //        // If invalid, do not proceed with AJAX
    //        return;
    //    }

    //    // Gather form data
    //    var form = $(this).closest('form')[0];
    //    var formData = new FormData(form);
    //    var data = {};

    //    formData.forEach(function (value, key) {
    //        if (key === "EmpStatus") {
    //            data[key] = $('input[name="EmpStatus"]').is(':checked');
    //        } else if (key === "BirthDate") {
    //            // Ensure date is in yyyy-MM-dd format
    //            data[key] = value ? value : null;
    //        } else {
    //            data[key] = value;
    //        }
    //    });

    //    // Get anti-forgery token
    //    var token = $('input[name="__RequestVerificationToken"]').val();

    //    $.ajax({
    //        url: '/EmployeeAjax/Create',
    //        type: 'POST',
    //        contentType: 'application/json',
    //        headers: { 'RequestVerificationToken': token },
    //        data: JSON.stringify(data),
    //        success: function (result) {
    //            if (result.success) {
    //                Swal.fire({
    //                    title: 'Saved',
    //                    icon: 'success',
    //                    text: result.message,
    //                    button: 'Close'
    //                }).then(function () {
    //                    window.location.href = result.redirectTo;
    //                });
    //            } else {
    //                Swal.fire('Error', 'Failed to create employee.', 'error');
    //            }
    //        },
    //        error: function () {
    //            Swal.fire('Error', 'Failed to create employee. Please check your input.', 'error');
    //        }
    //    });
    //});

    /*Create New Employee End */

    /*Edit Employee Start */

    $('#btnUpdate').click(function (e) {
        e.preventDefault();
        debugger;

        // var updatedEmployeeData = {
        //     EmpId: $('#EmpId').val(),
        //     EmpFirstName: $('#EmpFirstName').val(),
        //     EmpLastName: $('#EmpLastName').val(),
        //     EmailId: $('#EmailId').val(),
        //     BirthDate:$('#BirthDate').val(),
        //     EmpPhoneNumber: $('#EmpPhoneNumber').val(),
        //     Salary: $('#Salary').val(),
        //     EmpStatus: $('input[name="EmpStatus"]').is(':checked'),
        //     DeptId:$('#DeptId').val()
        // };

        // Gather form data
        var form = $(this).closest('form')[0];
        var formData = new FormData(form);
        var data = {};
        formData.forEach(function (value, key) {
            debugger;
            if (key === "EmpStatus") {
                // data[key] = form.EmpStatus.checked;
                data[key] = $('input[name="EmpStatus"]').is(':checked');
            } else {
                data[key] = value;
            }
        });
        debugger;
        // Get anti-forgery token
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            //url: '/Employee/Edit', // Use your AJAX endpoint
            url: '@Url.Action("DeleteConfirm", "Employee")',
            type: 'POST',
            contentType: 'application/json',
            headers: { 'RequestVerificationToken': token },
            data: JSON.stringify(data),
            success: function (result) {
                debugger;
                if (result.success) {
                    Swal.fire({
                        title: 'Updated',
                        type: 'success',
                        icon: 'success',
                        text: result.message,
                        button: 'Close'
                    }).then(function () {
                        window.location.href = result.redirectTo;
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops..',
                        text: 'Failed to Update employee. Please check your input.'
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops..',
                    text: 'Failed to Update employee. Please check your input.'
                });
            }
        });
    });

    /*Edit Employee End */


    /* Delete Employee Start */
    $('#btnDelete').click(function (e) {
        e.preventDefault();

        Swal.fire({
            title: 'Are you sure?',
            text: "Do you want to delete this employee?",
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                //this.submit(); // Submit the form if confirmed
                debugger;
                var employeeId = $('#EmpId').val();
                // Get anti-forgery token
                var token = $('input[name="__RequestVerificationToken"]').val();
                $.ajax({
                    //url: '/Employee/DeleteConfirm', // Use your AJAX endpoint
                    url: '@Url.Action("DeleteConfirm", "Employee")', // Use your AJAX endpoint
                    type: 'POST',
                    headers: { 'RequestVerificationToken': token },
                    data: { empId: employeeId },
                    success: function (result) {
                        debugger;
                        if (result.success == "True") {
                            Swal.fire({
                                title: 'Deleted',
                                type: 'success',
                                icon: 'success',
                                text: result.message,
                                button: 'Close'
                            }).then(function () {
                                window.location.href = result.redirectTo;
                            });
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops..',
                                text: 'Failed to Update employee. Please check your input.'
                            });
                        }
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops..',
                            text: 'Failed to Update employee. Please check your input.'
                        });
                    }
                });
            }
        });
    });

    /*Delete Employee End */





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