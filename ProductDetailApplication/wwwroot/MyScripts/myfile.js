//Load Data in Table when documents is ready  
//var fileData;
$(document).ready(function () {
    $.noConflict();
    loadData();

    $('#Image').on('change', function (e) {
        var files = e.target.files;
        if (files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("ImageFile", files[x]);
                }
                fileData = data;
            } else {
                alert("This browser doesn't support HTML file uploads!");
            }
        }
    });
});

//Load Data function  
function loadData() {
    $.ajax({
        url: "https://localhost:7291/api/ProductDetails/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (obj) {
            var html = '';
            $.each(obj, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.productId + '</td>';
                html += '<td>' + item.productTitle + '</td>';
                html += '<td>' + item.image + '</td>';
                html += '<td>' + item.price + '</td>';
                html += '<td>' + item.stock + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.productId + ')">Edit</a> | <a href="#" onclick="Delete(' + item.productId + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('#demo1').DataTable({
                'order': [[0, 'desc']],
                'aoColumns': [
                    null,
                    {
                        'bSortable': false
                    }
                ] });
            //var dataSet = new Array;
            //if (!('error' in obj)) {
            //    $.each(obj, function (index, value) {
            //        var tempArray = new Array;
            //        for (var o in value) {
            //            tempArray.push(value[o]);
            //        }
            //        dataSet.push(tempArray);
            //    })
            //    var table = $('#example').DataTable();
            //    table.destroy();
            //    $('#example').empty();
            //    $('#example').DataTable({
            //        data: obj
            //    });
            //}
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
};

//Add Data Function
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }

    //var empObj = {
    //    ProductID: $('#ProductId').val(),
    //    ProductTitle: $('#ProductTitle').val(),
    //    Image: fileData,
    //    Price: $('#Price').val(),
    //    Stock: $('#Stock').val()
    //};
    fileData.append("ProductID", 0);
    fileData.append("ProductTitle", $('#ProductTitle').val());
    //fileData.append("Image", fileData);
    fileData.append("Price", $('#Price').val());
    fileData.append("Stock", $('#Stock').val());
    $.ajax({
        url: "https://localhost:7291/api/ProductDetails",
        data: fileData,
        type: "POST",
        async: false,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            loadData();
            $('#close_btn').click();
            fileData = undefined;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon Employee ID  
function getbyID(ProductId) {

    //$('#ProductTitle').css('border-color', 'lightgrey');
    //$('#Image').css('border-color', 'lightgrey');
    //$('#Price').css('border-color', 'lightgrey');
    //$('#Stock').css('border-color', 'lightgrey');
    $.ajax({
        url: "https://localhost:7291/api/ProductDetails/" + ProductId,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#exampleModal').modal('show');
            $('#btnAdd').hide();
            $('#btnUpdate').show();
            $('#imageNameforedit').show();
            $('#imageNameforedit').val(result.image)
            $('#ProductId').val(result.productId);
            $('#ProductTitle').val(result.productTitle);
            $('#Price').val(result.price);
            $('#Stock').val(result.stock);
            /* $('#Image').val(result.image);*/
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating employee's record  
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    if (fileData == undefined) {
        fileData = new FormData();
        fileData.append("ImageFile", $('#imageNameforedit'));
    }
    fileData.append("ProductID", $('#ProductId').val());
    fileData.append("ProductTitle", $('#ProductTitle').val());
    fileData.append("Image", $('#imageNameforedit').val());
    fileData.append("Price", $('#Price').val());
    fileData.append("Stock", $('#Stock').val());
    //ImageFile

    $.ajax({
        url: "https://localhost:7291/api/ProductDetails/" + $('#ProductId').val(),
        data: fileData,
        type: "PUT",
        async: false,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ProductID').val("");
            $('#ProductTitle').val("");
            $('#Image').val("");
            $('#Price').val("");
            $('#Stock').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


//function for deleting employee's record  
function Delete(ProductID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "https://localhost:7291/api/ProductDetails/" + ProductID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Valdidation using jquery
//function validate() {
   // var isValid = true;
    //if ($('#ProductTitle').val().trim() == "") {
    //    $('#ProductTitle').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#ProductTitle').css('border-color', 'lightgrey');
    //}
    //if ($('#Image').val().trim() == "") {
    //    $('#Image').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#Image').css('border-color', 'lightgrey');
    //}
    //if ($('#Price').val().trim() == "") {
    //    $('#Price').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#Price').css('border-color', 'lightgrey');
    //}
    //if ($('#Stock').val().trim() == "") {
    //    $('#Stock').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#Stock').css('border-color', 'lightgrey');
    //}
   // return isValid;
//}


// $('#example').DataTable( {
//    ajax: 'data/objects.txt',
//    columns: [
//        { data: 'ProductID' },
//        { data: 'ProductTitle' },
//        { data: 'Image' },
//        { data: 'Price' },
//        { data: 'Stock' }
//    ]
//});