﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.

showInPopup = (url, title) => { /*url: gideceği action, title: New Transaction or Edit Transaction*/
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) { //res : action'ın döndürdüğü view
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    })
};

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) { //res.Success'de hata veriyor
                    $("#view-all").html(res.html);
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-title").html('');
                    $("#form-modal").modal('hide');
                    $.notify("Access granted", "success"); //$.notify(res.SuccessMessage, "success");
                }
                else {
                    console.log(res)
                    $("#form-modal .modal-body").html(res.html);
                    $.notify("System getting error.Contact your admin", "success"); //$.notify(res.ErrorMessage, "error");

                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    }
    catch (e) {
        console.log(e);
    }
    //to prevent default form submit event
    return false;
};


jQueryAjaxDelete = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {

                $("#view-all").html(res.html);
                $("#form-modal .modal-body").html('');
                $("#form-modal .modal-title").html('');
                $("#form-modal").modal('hide');
                $.notify("Delete granted", "success");
            },
            error: function (err) {
                console.log(err);
            }
        })
    }
    catch (e) {
        console.log(e);
    }
    //to prevent default form submit event
    return false;
};

