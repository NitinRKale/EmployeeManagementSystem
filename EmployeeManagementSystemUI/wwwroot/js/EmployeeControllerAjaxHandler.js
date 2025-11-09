// Submit employee (form-encoded, includes EmpStatus, keeps antiforgery header)
$(function () {
    $('#btnSubmit').on('click', function (e) {
        e.preventDefault();
        debugger;
        var $btn = $(this);
        var $form = $btn.closest('form');

        // client-side unobtrusive validation
        if (!$form.valid()) return;

        // Convert form to object, ensuring checkbox and date are handled predictably
        function formToObject($form) {
            var obj = {};
            $form.serializeArray().forEach(function (item) {
                // normalize empty strings for date if needed
                if (item.name === 'BirthDate') {
                    obj[item.name] = item.value ? item.value : null;
                } else {
                    // handle repeated keys (arrays) if necessary
                    if (obj.hasOwnProperty(item.name)) {
                        if (!Array.isArray(obj[item.name])) obj[item.name] = [obj[item.name]];
                        obj[item.name].push(item.value);
                    } else {
                        obj[item.name] = item.value;
                    }
                }
            });

            // Ensure EmpStatus exists (checkbox unchecked won't be in serialized array)
            var $status = $form.find('input[name="EmpStatus"]');
            if ($status.length) {
                obj.EmpStatus = $status.is(':checked');
            }

            return obj;
        }

        var payloadObj = formToObject($form);
        var payload = $.param(payloadObj); // url-encode for application/x-www-form-urlencoded

        // antiforgery token from the hidden input rendered by @Html.AntiForgeryToken()
        var token = $form.find('input[name="__RequestVerificationToken"]').val();

        // UI: disable button while request is in-flight
        $btn.prop('disabled', true);

        $.ajax({
            url: '/EmployeeAjax/Create',
            method: 'POST',
            // do NOT set contentType => jQuery will send application/x-www-form-urlencoded; charset=UTF-8
            data: payload,
            headers: token ? { 'RequestVerificationToken': token } : {},
            success: function (resp) {
                if (resp && resp.success) {
                    Swal.fire(
                        {
                            title: 'Saved',
                            icon: 'success',
                            text: resp.message || 'Saved'
                        })
                        .then(function () {
                            if (resp.redirectTo) window.location.href = resp.redirectTo;
                        });
                } else {
                    Swal.fire('Error', (resp && (resp.ErrorMessage || resp.message)) || 'Failed', 'error');
                }
            },
            error: function (xhr) {
                console.error('AJAX error', xhr);
                Swal.fire('Error', 'Server error; see console/Network tab', 'error');
            },
            complete: function () {
                $btn.prop('disabled', false);
            }
        });
    });

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

        // Convert form to object, ensuring checkbox and date are handled predictably




        //function formToObject($form) {
        //    var obj = {};
        //    $form.serializeArray().forEach(function (item) {
        //        // normalize empty strings for date if needed
        //        if (item.name === 'BirthDate') {
        //            obj[item.name] = item.value ? item.value : null;
        //        } else {
        //            // handle repeated keys (arrays) if necessary
        //            if (obj.hasOwnProperty(item.name)) {
        //                if (!Array.isArray(obj[item.name])) obj[item.name] = [obj[item.name]];
        //                obj[item.name].push(item.value);
        //            } else {
        //                obj[item.name] = item.value;
        //            }
        //        }
        //    });

        //    // Ensure EmpStatus exists (checkbox unchecked won't be in serialized array)
        //    var $status = $form.find('input[name="EmpStatus"]');
        //    if ($status.length) {
        //        obj.EmpStatus = $status.is(':checked');
        //    }

        //    return obj;
        //}

        //var payloadObj = formToObject($form);
        //var payload = $.param(payloadObj); // url-encode for application/x-www-form-urlencoded

        //// antiforgery token from the hidden input rendered by @Html.AntiForgeryToken()
        //var token = $form.find('input[name="__RequestVerificationToken"]').val();

        //// UI: disable button while request is in-flight
        //$btn.prop('disabled', true);


        $.ajax({
            url: '/EmployeeAjax/Edit',
            type: 'POST',
           // contentType: 'application/json; charset=utf-8',
            // data: JSON.stringify(obj),
            data: payload,
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


    /* Delete Employee Start */

    /* Delete Employee (Index listing) - delegated so it works for rows loaded by DataTables */
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
            icon: 'warning',
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

    /*Delete Employee End */
});



//$(function () {
//    // Initialize birth datepicker and ensure server-rendered value (dd-MM-yyyy) is parsed
//    (function initBirthDatepicker() {
//        if (!$.fn.datetimepicker) return;

//        var pickerSelector = '#EmpBirthDate';
//        var $input = $('input[name="BirthDate"]');

//        // Use the same format as server DisplayFormat ("dd-MM-yyyy")
//        $(pickerSelector).datetimepicker({
//            format: 'DD-MM-YYYY',
//            // optional: keep useCurrent false to avoid auto-selecting today's date when input empty
//            useCurrent: false
//        });

//        // If server rendered a value like "25-12-1990", ensure the picker shows it
//        var serverVal = $input.val();
//        if (serverVal) {
//            // If moment is available, set via moment to ensure correct parsing
//            if (window.moment) {
//                try {
//                    var m = moment(serverVal, 'DD-MM-YYYY');
//                    if (m.isValid()) {
//                        $(pickerSelector).datetimepicker('date', m);
//                    } else {
//                        // fallback: set raw value
//                        $input.val(serverVal);
//                    }
//                } catch (ex) {
//                    $input.val(serverVal);
//                }
//            } else {
//                // Without moment, many datetimepickers can parse with matching format; set the value directly.
//                $input.val(serverVal);
//            }
//        }
//    })();
//});


