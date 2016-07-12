var mode = 0;

$(document).ready(function (e) {

    

    $("body").append('<div id="dvPopup" style="display:none;position:absolute;background-color:#ddd; z-index:1000;height:200px; overflow:auto" class="borderAll">Popup</div>');
    $("*").click(function () {
        if (!$(this).hasClass("btBrowse")) {
           // $("#dvPopup").hide();
        }
    });
    $("*").keydown(function (e) {
        if (e.keyCode == 27) {
            $("#dvPopup").hide();
        }
    });
    //*********Event click Combo *******

    $(document).click(function (e) {
        if (!($(e.target).is($("#dvPopup")) + $(e.target).is($(".btBrowse")))) {
            $("#dvPopup").hide();
        }
    });

});

function callAjax(ajaxURL, data2Send) {
  
    var re = "";
    $.ajax({
        url: ajaxURL,
        data: data2Send,
        async: false,
        type: "POST",
        beforeSend: function () {
            openSpinner();
        },
        error: function () {
            closeSpinner();
            alert("Error Loading Data");
        },
        success: function (data) {
            closeSpinner();
            if (data.toString() == "nosession") {
                alert("Session Expired!");
                //window.location = "../Default.aspx"
               
            }
          
            re = data;

            afterAction();
        }
    });
    return re;
}

function callgetAjaxOnly(ajaxURL, data2Send) {

    var re = "";
    $.ajax({
        url: ajaxURL,
        data: data2Send,
        async: false,
        type: "GET",
        beforeSend: function () {
            openSpinner();
        },
        error: function () {
            closeSpinner();
            alert("Error Loading Data");
        },
        success: function (data) {
            closeSpinner();
            if (data.toString() == "nosession") {
                alert("Session Expired!");
                //window.location = "../Default.aspx"

            }

            re = data;


        }
    });
    return re;
}

function callAjaxOnly(ajaxURL, data2Send) {
    
    var re = "";
    $.ajax({
        url: ajaxURL,
        data: data2Send,
        async: false,
        type: "POST",
        beforeSend: function () {
            openSpinner();
        },
        error: function () {
            closeSpinner();
            alert("Error Loading Data");
        },
        success: function (data) {
            closeSpinner();
            if (data.toString() == "nosession") {
                alert("Session Expired!");
                //window.location = "../Default.aspx"

            }

            re = data;

           
        }
    });
    return re;
}




function filterData(colID, v) {
    //if (e.keyCode == 13) {;
   
    //alert($(v).css("height"));
    var left = $("#txt" + $(v).attr("id")).offset().left;
    var top = $("#txt" + $(v).attr("id")).offset().top;
    var h = $("#txt" +  $(v).attr("id")).css("height");
    
    $("#dvPopup").css("left",left + "px");
    $("#dvPopup").css("top", (parseFloat(top) + parseFloat(h) + 5) + "px");
    $("#dvPopup").width("500px");
    $("#dvPopup").html(callAjax("../include/ajax.aspx", "&app=filterData2&colID=" + colID +
        "&searchSQL=" + $("#txt" + $(v).attr("id")).val() +
        "&SQLStr=" + $("#txtSQL" + $(v).attr("id")).val()));
    $("#dvPopup").css("display", "block");
  

    //*********navigation event
    navCombo(v);
    

    // Event
    $("#dvPopup").find("tr.popupList").unbind("click");
    $("#dvPopup").find("tr.popupList").click(function (e) {
       
        $("#txt" + $(v).attr("id")).val($(this).attr("data"));
        $("#" + $(v).attr("id").replace('txt', '')).val($(this).attr("rel"));
        
        $(this).find("td").each(function (i, va) {
            var a = ($(va).text());
            $(v).attr("attr" + i, a);
        });
      
        $(v).change();
        $("#dvPopup").hide();
        
        if(typeof afterSSA == "function")
            afterSSA();

    });

    var tmp = $(v).attr("id").replace("txt", "");
    $("#" + tmp).html(callAjax("../include/ajax.aspx", "&app=filterData&colID=" + colID + "&searchSQL=" + $(v).val()));
    //}
}

function saveRecord(ajaxURL, screenid, data2Send, dv) {

    //$("#newEntry").serialize() + "&app=saveEntry&screenid="
    var re = callAjax(ajaxURL, $("#" + dv).find("#" + data2Send).serialize() + "&screenid=" + screenid);
    if (re.toString() == "nosession") {
        alert("Session Expired!");
        window.location = "../Default.aspx"
    }
    if (re.toString().indexOf("ok") > -1) {
        alert("Data Saved !");
        $("#" + dv).append("<input type='text' id='txtNEWID' value='" + re.replace('ok', '') + "'/>");
        afterEvent(dv, "save");
        // going to summary page
        showSummaryRecord(re.replace('ok', ''), screenid, data2Send, dv, ajaxURL);
       

    } else if (re.toString() == "del") {

        loadScreen(screenid, dv, "", ajaxURL);
        adjustScreen(dv);
    } else if (re.toString() == "nopermission") {
        $("#" + dv).find("#dvErrInfo").html("Insufficient Priviledge");
    } else if (re.toString() == "userdefine") {
        afterEvent(dv, "savenew");
    }

    else {
        try {
            re = $.parseJSON(re);
            $.each(re, function (i, v) {
                $("#" + dv).find("#td" + v.ColName).find("label.error").remove();
                $("#" + dv).find("#td" + v.ColName).find("div.error").remove();
                $("#" + dv).find("#td" + v.ColName).find("label.required").remove();
                
                $("#" + dv).find("#td" + v.ColName).find(">div:eq(1)").append(v.Message);
            });
        } catch (e) { alert("Error : " + re); }
    }

}

function saveRecord2(ajaxURL, screenid, data2Send, dv) {
    
    //$("#newEntry").serialize() + "&app=saveEntry&screenid="
    var re = callAjax(ajaxURL, $("#" + dv).find("#" + data2Send).serialize() + "&screenid=" + screenid);
    
    if (re.toString() == "nosession") {
        alert("Session Expired!");
        window.location = "../Default.aspx"
    }
    if (re.toString().indexOf("ok") > -1) {
        afterEvent(dv, "save");

    } else if (re.toString() == "nopermission") {
        $("#" + dv).find("#dvErrInfo").html("Insufficient Priviledge");
    }
    else {
        try {
            
            re = $.parseJSON(re);
            $.each(re, function (i, v) {
                $("#" + dv).find("#td" + v.ColName).find("label.error").remove();
                $("#" + dv).find("#td" + v.ColName).find("div.error").remove();
                $("#" + dv).find("#td" + v.ColName).find("label.required").remove();
                $("#" + dv).find("#td" + v.ColName).find(">div:eq(0)").append(v.Message);
                
            });
        } catch (e) { alert("Error : " + re); }
    }

}

function showSummaryRecord(eid, screenid, data2Send, dv, url) {
    data = {
        "app": "newEntry",
        "screenid": screenid,
        "eid": eid
    };
    $("#" + dv).html(callAjax("../include/ajax.aspx", data));
    // tab setting
    $("#" + dv).find("#dvScreen").tabs({ activate: function (event, ui) { tabClick(event, ui); } });

    $("#" + dv).find("#txtMode").val("1");
    // Edit Button
    $("#" + dv).find("#btEdit").click(function (e) {
        showEditRecord($("#" + dv).find("#txtEID").val(), screenid, data2Send, dv, url);
    });

    // Delete Button
    $("#" + dv).find("#btDelete").click(function (e) {
        // Change to jquery UI
        if (confirm("Delete Current Record ?")) {
            saveRecord(url + "?app=del&eid=" + eid, screenid, data2Send, dv);
            afterEvent(dv, "delete");
            return false;
        }
    });
    
    $("#" + dv).find(".date").datepicker({ dateFormat: "dd/mm/yy" });

    adjustScreen(dv);
    afterEvent(dv, "summary");

}

function showEditRecord(eid, screenid, data2Send, dv, url) {
    data = {
        "app": "newEntry",
        "screenid": screenid,
        "eid": eid,
        "isEdit": "1"
    };
    $("#" + dv).html(callAjax("../include/ajax.aspx", data));

    // tab setting
    $("#" + dv).find("#dvScreen").tabs({ activate: function (event, ui) { tabClick(event, ui); } });

    $("#" + dv).find("#txtMode").val("2");
    //Redefind Save button
    $("#" + dv).find("#btSave").click(function (e) {
        //alert($("#" + dv).find("#newEntry").serialize());
        saveRecord(url.replace("&app=save&eid=" + eid + "&curDiv=" + dv, "") + "&app=save&eid=" + eid + "&curDiv=" + dv, screenid, "newEntry", dv);
    });

    //Redefind Cancel button
    $("#" + dv).find("#btCancel").click(function (e) {
        showSummaryRecord(eid, screenid, data2Send, dv, url);
    });

    $("#" + dv).find(".date").datepicker({ dateFormat: "dd/mm/yy" });
    adjustScreen(dv);
    afterEvent(dv, "summary");
}

function adjustScreen(dv) {
    //$("#" + dv).find("#dvCPanel").css("width", "200px");
    //$("#" + dv).find("#dvScreen").width($("#" + dv).width() - $("#" + dv).find("#dvCPanel").width() - 40);
}

function findRecord(screenid) {
    $("#dvList").html(callAjax("../include/ajax.aspx", $("#newEntry").serialize() + "&app=list&screenid=" + screenid));
}

function showDia(dv) {
    $("#" + dv).dialog({ modal: true, closeOnEscape: false });
}

function loadScreen(screenid, dv, eid, url) {

    var data;
    if (eid == "") {
        data = {
            "app": "newEntry",
            "screenid": screenid
        };
    } else {
        data = {
            "app": "newEntry",
            "screenid": screenid,
            "eid": eid
        };
    }
    $.ajax({
        url: "../include/ajax.aspx",
        data: data,
        async: false,
        type: "POST",
        error: function () { alert("Error Loading Data ..."); },
        success: function (data) {
            if (data.toString() == "nosession") {
                alert("Session Expired!");
                window.location = "../Default.aspx"
            }
            $("#" + dv).html(data);
            // tab setting
            $("#" + dv).find("#dvScreen").tabs({ activate: function (event, ui) { tabClick(event, ui); } });

            // set Mode
            $("#" + dv).find("#txtMode").val("0");
            // Save Button
            $("#" + dv).find("#btSave").click(function (e) {

                saveRecord(url.replace("&app=save&curDiv=" + dv, "") + "&app=save&curDiv=" + dv, screenid, "newEntry", dv);
            });

            // Cancel Button
            $("#" + dv).find("#btCancel").click(function (e) {
                $("#" + dv).find(".frmElement").each(function (i, v) {
                    if ($(v).hasClass("number")) {
                        $(v).val('0.00');
                    } else {
                        $(v).val('');
                    }
                });
                $("#" + dv).find(".frmElement:first").select();
            });

            // Edit Button
            $("#" + dv).find("#btEdit").click(function (e) {
                showEditRecord($("#" + dv).find("#txtEID").val(), screenid, "newEntry", dv, url);
            });

            // Delete Button
            $("#" + dv).find("#btDelete").click(function (e) {
                // Change to jquery UI
                if (confirm("Delete Current Record ?")) {
                    saveRecord(url.replace("&app=save&eid=" + $("#" + dv).find("#txtEID").val(), "") + "&app=del&eid=" + $("#" + dv).find("#txtEID").val(), screenid, "newEntry", dv);
                    afterEvent(dv, "delete");
                    return false;
                }
            });

            // Find Button
            $("#" + dv).find("#btFind").click(function (e) {
                $("#" + dv).find("#dvList").html(callAjax("../include/ajax.aspx", $("#" + dv).find("#newEntry").serialize() + "&app=list&screenid=" + screenid));
            });

            // Date
            $("#" + dv).find(".date").datepicker({ dateFormat: "dd/mm/yy" });

            if (eid != "") {
                afterEvent(dv, "summary");
            } else {
                afterEvent(dv, "new");
            }
        }
    });
}
function navCombo(v) {
    var currentSelection = -1;
    // Register keypress events on the whole document
    $("#txt" + $(v).attr("id")).focus();
    $(document).keypress(function (e) {

        switch (e.keyCode) {
            // User pressed "up" arrow
            case 38:
                navigate('up');
                break;
                // User pressed "down" arrow
            case 40:
                navigate('down');
                break;
                // User pressed "enter"
            case 13:
                
                $("#txt" + $(v).attr("id")).val($("#dvPopup").find("tr.popupList").eq(currentSelection).attr("data"));
                $("#" + $(v).attr("id").replace('txt', '')).val($("#dvPopup").find("tr.popupList").eq(currentSelection).attr("rel"));
                $("#dvPopup").hide();

                if (typeof afterSSA == "function")
                    afterSSA();
                break;
        }


        //// Add data to let the hover know which index they have
        //for (var i = 0; i < $("#dvPopup").find("tr.popupList").size() ; i++) {
        //    $("#dvPopup").find("tr.popupList").eq(i).data("number", i);
        //}

        //    // Simulote the "hover" effect with the mouse
        //    $("#dvPopup").find("tr.popupList").hover(
        //    function () {
        //        currentSelection = $(this).data("number");
        //        setSelected(currentSelection);
        //    }, function () {
        //        $("#dvPopup").find("tr.popupList").removeClass("itemhover");
        //        currentUrl = '';
        //    }
        //);
    });

    function navigate(direction) {

        // Check if any of the menu items is selected
        if ($("#dvPopup").find("tr.popupList").size() == 0) {
            currentSelection = -1;

        }

        if (direction == 'up' && currentSelection != -1) {
            if (currentSelection != 0) {

                currentSelection--;
            }
        } else if (direction == 'down') {
            if (currentSelection != $("#dvPopup").find("tr.popupList").size() - 1) {
                currentSelection++;
            }
        }
        setSelected(currentSelection);
    }

    function setSelected(menuitem) {
        $("#dvPopup").find("tr.popupList").removeClass("itemhover");
        $("#dvPopup").find("tr.popupList").eq(menuitem).addClass("itemhover");
        //currentUrl = $("#dvPopup").find("tr.popupList").eq(menuitem).attr("href");
    }
           
}

function filterDataEnter(colID, v, e) {
    //alert($(v).attr("id").split("txt",""));
    var v = $(v).attr('id').toString().replace('txt', '');

    if (e.keyCode == 113) {
        filterData(colID, $("#" + v));
    }

}

function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
}

function openSpinner() {
    $("#loading").html("<img src='/Images/loading.gif' width='50px'/>");
}

function closeSpinner() {
    $("#loading").find('img').remove();
}