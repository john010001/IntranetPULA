// v = 1.0.2

// Launch Mobile NAV
var sidebarjs = new SidebarJS();

// Menu link - Set page number
function addAttMenuLink () {
    var sideNav = document.querySelectorAll('.sidebar-nav');
    
    sideNav.forEach (function(sideNavItem) {
        var ulList = sideNavItem.querySelector('.nav-left');
        var ulItem = ulList.querySelectorAll('.nav-left-item');
        for (var i = 0; i < ulItem.length; i++) {                    
            ulItem[i].querySelector("a").setAttribute('pagenumber', i + 1);
            if (ulItem[i].querySelector(".nav-left-submenu-dropdown")) {
                var linkA = ulItem[i].querySelector(".nav-left-submenu-dropdown").querySelectorAll("a");
                for (var n = 0; n < linkA.length; n++) {
                    var readPage = ulItem[i].querySelector("a").getAttribute("pagenumber");
                    linkA[n].setAttribute('pagenumber', readPage + '.' + (n + 1));
                    //console.log(linkA[n]);
                }
            }
        }
    });
}
addAttMenuLink()

//Sidenav link active 
function sidenavLink (ele) {
    "use strict";
    var box = document.querySelector('.sidebar-nav-box');
    var alink = box.querySelector('a[pagenumber="' + ele + '"]')

    if (alink) {
        if (ele.includes(".")) {
            alink.classList.add("active");
            var cutEle = ele.slice(0, ele.lastIndexOf("."));
            var droplink = box.querySelector('a[pagenumber="' + cutEle + '"]');
            droplink.click()
        }
        else {
            alink.classList.add("active");
        }
    }
    else {
        console.log("The 'pagenumber' dont exist")
    }
};

//Document ready
document.addEventListener('DOMContentLoaded', function() {

    //

});


// Name of file in InputFile
var customFileInput = document.querySelector('.custom-file-input')
if (customFileInput) {
    customFileInput.addEventListener('change',function(e){
        var fileName = this.files[0].name;
        var nextSibling = e.target.nextElementSibling
        nextSibling.innerText = fileName
    })
}

////////////////////////

function downloadCanvas(id, obj) {
    var canvas = document.getElementById(id);
    var image = canvas.toDataURL("image/jpg");
    obj.href = image;
}

$.each($('.tb-datatable'), function (index, value) {
    $(this).DataTable({
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": '<i class="far fa-angle-right"></i>',
                "previous": '<i class="far fa-angle-left"></i>'
            }
        }
    });
});

$.each($('.tb-datatable-buttons'), function (index, value) {
    $(this).DataTable(
        {
            language: {
                "decimal": "",
                "emptyTable": "No hay información",
                "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": "Mostrar _MENU_ Entradas",
                "loadingRecords": "Cargando...",
                "processing": "Procesando...",
                "search": "Buscar:",
                "zeroRecords": "Sin resultados encontrados",
                "paginate": {
                    "first": "Primero",
                    "last": "Ultimo",
                    "next": '<i class="far fa-angle-right"></i>',
                    "previous": '<i class="far fa-angle-left"></i>'
                }
            },
        dom: 'Bfrtip',
        buttons: [
            //'copy', 'csv', 'excel', 'pdf', 'print'
            'excel', 'pdf', 'print'
        ]
    });
});

$.each($('.tb-datatable-only-buttons'), function (index, value) {
    $(this).DataTable(
        {
            language: {
                "decimal": "",
                "emptyTable": "No hay información",
                "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": "Mostrar _MENU_ Entradas",
                "loadingRecords": "Cargando...",
                "processing": "Procesando...",
                "search": "Buscar:",
                "zeroRecords": "Sin resultados encontrados",
                "paginate": {
                    "first": "Primero",
                    "last": "Ultimo",
                    "next": '<i class="far fa-angle-right"></i>',
                    "previous": '<i class="far fa-angle-left"></i>'
                }
            },
        dom: 'Bfrtip',
        buttons: {
            buttons: [
                //'copy', 'csv', 'excel', 'pdf', 'print'
                // {
                //     extend: 'copy',
                //     text: '<b>Copiar</b>'
                // },
                {
                    extend: 'excel',
                    // text: '<i class="fas fa-file-excel"></i>'
                    text: 'Excel'
                },
                {
                    extend: 'pdf',
                    // text: '<i class="fas fa-file-pdf"></i>'
                    text: 'Pdf'
                },
                {
                    extend: 'print',
                    // text: '<i class="far fa-print"></i>'
                    text: 'Imprimir'
                }
            ],
            dom: {
                button: {
                    className: 'gn-btn gn-btn-line-orange gn-btng-btn'
                }
            }
        },
        paging: false,
        searching: false,
        info: false,
        ordering: false
    });
});