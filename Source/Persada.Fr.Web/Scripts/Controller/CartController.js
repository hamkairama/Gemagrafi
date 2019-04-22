//public param
var objectHeader = {};

function Calculate() {
    if (IsValidCart()) {
        //Header Booking
        var ttl = $("#total")[0].innerHTML;
        var ttlFinal = $("#totalFinal")[0].innerHTML;
        var total = EraseComma(ttl);
        var totalFinal = EraseComma(ttlFinal);
        var paymentType = $("#paymentType :selected").text();
        var discount = 0;
        objectHeader['total'] = total;
        objectHeader['totalFinal'] = totalFinal;
        objectHeader['paymentType'] = paymentType;
        objectHeader['discount'] = discount;

        //Detail Booking
        var objectDetails = [];
        for (var i = 1; i <= 2; i++) {
            var cartHeader = ".cart-header" + i;
            var x = $(cartHeader).length;
            if (x > 0) {
                var dt = $("#dateTime" + i)[0].value;
                if (dt != "") {
                    var objectDetail = {};
                    var priceListId = $("#priceListId" + i)[0].innerHTML;
                    objectDetail['priceListId'] = priceListId;
                    objectDetail['dateTimeStart'] = dt;
                    objectDetails.push(objectDetail);
                }
                else {
                    alert("Please input datetime you want book");
                    return false;
                }
            }
        }
        objectHeader["listHeaderDetail"] = objectDetails;

        ActionCreateBooking(objectHeader);
    }   
}

function IsValidCart() {
    var fields = $(".cartHeaderTop");
    var valid = true;

    if (fields.length == 0) {
        valid = false;
    }

    return valid;
}

function ActionCreateBooking(data) {
    var jsonBookingView = data;

    rs = $.xResponseJson(fullOrigin + '/Booking/ActionCreate/',
          JSON.stringify({ bookingView: jsonBookingView }));

    //if (IsSuccess()) {
    //    CleanInputText();
    //    CleanImage();
    //    ModalCreate('Service/Index/', '#modalDialog');
    //} else {
    //    ModalCreate('Service/Edit/', '#modalDialog');
    //}
}

function ServiceActionEdit() {
    var txtId = $("#txtId").val();
    var txtServiceName = $("#txtServiceName").val();
    var txtDescription = $("#txtDescription").val();
    var txtClassProp = $("#txtClassProp").val();
    var txtImage = EraseAttImg($('#txtImage').attr('src'));
    var txtCreatedBy = $("#txtCreatedBy").val();
    var txtCreatedDt = $("#txtCreatedDt").val();

    var jsonService = {
        id: txtId,
        service_nm: txtServiceName,
        service_desc: txtDescription,
        class_prop: txtClassProp,
        img: txtImage,
        created_by: txtCreatedBy,
        created_dt: txtCreatedDt
    };

    rs = $.xResponseJson(fullOrigin + 'Service/ActionEdit/',
          JSON.stringify({ service: jsonService }));

    if (IsSuccess()) {
        CleanInputText();
        CleanImage();
        ModalCreate('Service/Index/', '#modalDialog');
    } else {
        ModalCreate('Service/Edit/', '#modalDialog');
    }
}