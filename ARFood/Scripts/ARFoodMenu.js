$(function () {
    $.ajax(
        {
            url: "/Home/MostrarFamilias"
                        }).done(function (data) {
                $('#todos').html(data);
            }).fail(function () {
                alert("Error 1");
            });
                });
$('#todos').on('click', 'a[data-url]', function () {
    var dataUrl = $(this).data('url');
    $.ajax(
        {
            url: dataUrl
        }).done(function (data) {
            $('#familia').html(data);
        }).fail(function () {
            alert('Error 2');
        });
    $('#todos a').removeClass('active');
    $(this).addClass('active');
});
$('#familia').on('click', 'a[data-url]', function () {
    var dataUrl = $(this).data('url');
    $.ajax(
        {
            url: dataUrl
        }).done(function (data) {
            $('#Platillo').html(data);
        }).fail(function () {
            alert('Error 2');
        });
    $('#todos a').removeClass('active');
    $(this).addClass('active');
});
$('#Platillo').on('click', 'a[data-url]', function () {
    var dataUrl = $(this).data('url');

    var idProdCustom;
    if (dataUrl.indexOf("MostrarPlatillos") > -1) {
        $.ajax(
            {
                url: dataUrl
            }).done(function (data) {
                $('#Platillo').html(data);
            }).fail(function () {
                alert('Error platillo');
            });
        $('#todos a').removeClass('active');
        $(this).addClass('active');
    }
});

function btnSaveOrder(comp) {
    var StayOnPage = "";
    if (comp != null) {
        if (comp == "StayOnPage") {
            StayOnPage = "StayOnPage";
        }
    }
    var conDate = document.getElementById('ProgramarFecha');
    var xParametros = "";
    if (conDate != null) {
        if (conDate.value.length > 0) {
            xParametros = '{ HasDate:' + conDate.value + '}';
        }
        else {
            xParametros = '{ HasDate: ""}';
        }
    }
//    alert(xParametros);
    $.ajax({
        type: "POST",
        traditional: true,
        async: false,
        cache: false,
        url: '/home/GuardarPedido',
        data: xParametros,
        dataType: "json",
        contentType: "application/json",
        success: function (algo) {
            if (algo == "StayOnPage" || StayOnPage == "StayOnPage")  {

            }
            else {
                $.ajax(
                    {
                        url: "/Home/OrdenCreada"
                    }).done(function (data) {
                        $('#AllMenu').html(data);
                    }).fail(function () {
                        alert('Ocurrió un error al momento de guardar la información 2');
                    });
                $('#todos a').removeClass('active');
                $(this).addClass('active');
                }
            },
        failure: function () {
            alert('Ocurrió un error al momento de guardar la información');
        }
    });

}




function fnProcesaPedido(comp) {
    var Obs = document.getElementById('txtPersonaliza');
    var id = document.getElementById('IDPersonalizando');
    Obs.value = document.getElementById("Obs_" + comp.id.substring(comp.id.indexOf("btnstart_") + 9)).value;
    id.value = comp.id.substring(comp.id.indexOf("btnstart_") + 9);
    idDelProd = id.value;
    var i = 0;
    $.ajax({
        type: "POST",
        traditional: true,
        async: false,
        cache: false,
        url: "/home/CargaPersonalizar",
        data: '{ idProd:' + idDelProd + '}',
        dataType: "json",
        contentType: "application/json",
        success: function (algo) {
            algo.forEach(function (elemento, indice, array) {

                if (indice > 0) {

                    var insertAt = document.getElementById("ListaPersonaliza");


                    var inserting = document.createElement("tr");
                    var xID = document.createAttribute("ID");
                    xID.value = "TR_" + indice;
                    inserting.setAttributeNode(xID);
                    var xName = document.createAttribute("name");
                    xName.value = "TR_" + indice;
                    inserting.setAttributeNode(xName);

                    insertAt.appendChild(inserting);
                    //insertAt.parentNode.insertBefore(inserting, insertAt);

                    insertAt = document.getElementById("TR_" + indice);

                    inserting = document.createElement("td");
                    xID = document.createAttribute("ID");
                    xID.value = "TD_" + indice;
                    inserting.setAttributeNode(xID);
                    xName = document.createAttribute("name");
                    xName.value = "TD_" + indice;
                    inserting.setAttributeNode(xName);
                    insertAt.appendChild(inserting);

                    insertAt = document.getElementById("TD_" + indice);

                    MainDiv = document.createElement("div");
                    xclass = document.createAttribute("class");
                    xclass.value = "switch-button";
                    MainDiv.setAttributeNode(xclass);
                    var xID = document.createAttribute("ID");
                    xID.value = "Newswitch-button_" + indice;
                    MainDiv.setAttributeNode(xID);
                    xName = document.createAttribute("name");
                    xName.value = "Newswitch-button_" + indice;
                    MainDiv.setAttributeNode(xName);
                    insertAt.appendChild(MainDiv);

                    insertAt = document.getElementById("Newswitch-button_" + indice);

                    newDiv = document.createElement("input");
                    xclass = document.createAttribute("class");
                    xclass.value = "switch-button__checkbox";
                    newDiv.setAttributeNode(xclass);
                    xID = document.createAttribute("ID");
                    xID.value = "NewSB_" + indice;
                    newDiv.setAttributeNode(xID);
                    xName = document.createAttribute("name");
                    xName.value = "NewSB_" + indice;
                    inserting.setAttributeNode(xName);
                    var xCHK = document.createAttribute("type");
                    xCHK.value = "checkbox";
                    newDiv.setAttributeNode(xCHK);
                    var xON = document.createAttribute("checked");
                    xON.value = "checked";
                    newDiv.setAttributeNode(xON);

                    insertAt.appendChild(newDiv);

                    newDiv = document.createElement("label");
                    xclass = document.createAttribute("class");
                    xclass.value = "switch-button__label";
                    newDiv.setAttributeNode(xclass);
                    xID = document.createAttribute("ID");
                    xID.value = "NewSL_" + indice;
                    newDiv.setAttributeNode(xID);
                    xName = document.createAttribute("name");
                    xName.value = "NewSL_" + indice;
                    inserting.setAttributeNode(xName);
                    var xlbl = document.createAttribute("for");
                    xlbl.value = "NewSB_" + indice;
                    newDiv.setAttributeNode(xlbl);

                    insertAt.appendChild(newDiv);

                    elemento.forEach(function (elemento2, indice2, array) {

                        insertAt = document.getElementById("TR_" + indice);

                        inserting = document.createElement("td");
                        xID = document.createAttribute("ID");
                        xID.value = "TDL_" + indice + "_" + indice2;
                        inserting.setAttributeNode(xID);
                        xName = document.createAttribute("name");
                        xName.value = "TDL_" + indice + "_" + indice2;
                        inserting.setAttributeNode(xName);
                        insertAt.appendChild(inserting);

                        insertAt = document.getElementById("TDL_" + indice + "_" + indice2);

                        newDiv = document.createElement("label");
                        xID = document.createAttribute("ID");
                        xID.value = "lblText_" + indice + "_" + indice2;
                        newDiv.setAttributeNode(xID);
                        xName = document.createAttribute("name");
                        xName.value = "lblText_" + indice + "_" + indice2;
                        newDiv.setAttributeNode(xName);
                        if (indice2 == 0 || indice2 == 1 || indice2 == 3) {
                            xName = document.createAttribute("style");
                            xName.value = "visibility:hidden";
                            newDiv.setAttributeNode(xName);
                        }
                        if (indice2 == 3) {
                            xtitle = document.createAttribute("title");
                            xtitle.value = elemento2;
                            newDiv.setAttributeNode(xtitle);
                        }

                        insertAt.appendChild(newDiv);

                        document.getElementById("lblText_" + indice + "_" + indice2).innerHTML = elemento2;

                    });
                    i++;
                }
            })

        },
        failure: function () {
            alert('Falló');
        }
    });

    
    var iComplemento = document.getElementById('TotalComplementos_' + id.value);
    for (var e = 1; e <= iComplemento.value; e++) {
        var esIngrediente = document.getElementById('lblText_' + e + '_3');

        let ourCheckBox = null;
        this.ourCheckBox = document.querySelector('#NewSB_' + e);

        let checkBox = this.ourCheckBox;
        if (esIngrediente.title == "True") {
            checkBox.checked = true;
        }
        else {
            checkBox.checked = false;
        }
    }
    
    $('.modal-child').on('show.bs.modal', function () {
        var modalParent = $(this).attr('data-modal-parent');
        $(modalParent).css('opacity', 0);
    });

    $('.modal-child').on('hidden.bs.modal', function () {
        var modalParent = $(this).attr('data-modal-parent');
        $(modalParent).css('opacity', 1);
    });

    PersonalizarDialog.show();
    alert('Show');
    PersonalizarDialog.style.zIndex = 10000;
}

function fnCancelPedido(comp) {
    PersonalizarDialog.close();

    var elmtTable = document.getElementById('ListaPersonaliza');
    var tableRows = elmtTable.getElementsByTagName('tr');
    var rowCount = tableRows.length;
    for (var x = rowCount - 1; x >= 0; x--) {
        elmtTable.removeChild(tableRows[x]);
    }
}

function fnGuardaPedido(comp) {
    var Obs = document.getElementById('txtPersonaliza');
    var idProd = document.getElementById('IDPersonalizando');
    var listadiv = document.getElementById('ListaPersonaliza');
    var iComplemento = document.getElementById('TotalComplementos_' + idProd.value);
    var chkarray = new Array();;
    chkarray[0] = 0;
    for (var e = 1; e <= iComplemento.value; e++) {


        let ourCheckBox = null;
        this.ourCheckBox = document.querySelector('#NewSB_' + e);

        let checkBox = this.ourCheckBox;

        if (checkBox.checked) {
            chkarray[e] = "1";
        }
        else {
            chkarray[e] = "0";
        }
    }

    var xParametros = '{ RidProdCustom: ' + idProd.value + ', Comentarios: "' + Obs.value + '", Accion: "Agregar", chkValues: [' + chkarray + '] }';

    //
    $.ajax({
        type: "POST",
        traditional: true,
        async: false,
        cache: false,
        url: '/home/PersonalizarPlatillos',
        data: xParametros,
        dataType: "json",
        contentType: "application/json",
        success: function (algo) {
            //alert('Exito');
        },
        failure: function () {
            alert('Ocurrió un error al momento de guardar la información');
        }
    });

    //$.ajax({
    //        type: "POST",
    //        traditional: true,
    //        async: false,
    //        cache: false,
    //        url: '/home/MostrarPlatillos/',
    //    data: { RidProdCustom: id.value, Comentarios: Obs.value, Accion: "Agregar" }
    //        }).done(function (data) {
    //            $('#Platillo').html(data);
    //        }).fail(function () {
    //            alert('La función de Guardar aún no está Activa');
    //        });


    PersonalizarDialog.close();

    var elmtTable = document.getElementById('ListaPersonaliza');
    var tableRows = elmtTable.getElementsByTagName('tr');
    var rowCount = tableRows.length;
    for (var x = rowCount - 1; x >= 0; x--) {
        elmtTable.removeChild(tableRows[x]);
    }
}

function btnSelectSalon() {
    MesasDialog = document.getElementById('MesasDialog');
    MesasDialog.showModal();

    //@*var id = document.getElementById('IDPersonalizando');
    //dataUrl = "/Salon/SalonDesign?IDPedido=" + -25;

    //window.location = dataUrl;
                }

function btnCierraMesa() {
    MesasDialog = document.getElementById('MesasDialog');
    MesasDialog.close();

}

function AlgoAlo() {
    alert('hola  aale');
}