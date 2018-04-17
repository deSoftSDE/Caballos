appadmin.controller('consulta', function ($scope, Llamada, $timeout) {
    alert("Holi")
    $scope.cambiarPagina = function (sender, val) {
        cambiarBotonesPaginacion("");
        switch (val) {
            case "F":
                cambiarBotonesPaginacionIniciales("disabled");
                break;
            case "L":
                cambiarBotonesPaginacionFinales("disabled");
                break;
        }
        $scope.vm.cm.accionPagina = val;
        LeerRegistros($scope.vm.cm);
    };
    $scope.dataGridOptions = {
        dataSource: [],
        keyExpr: "id",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false,
            texts: {
                deleteRow: "eliminar",
            }
        },
        width: "auto",
        columnAutoWidth: true,
        selection: {
            mode: "single"
        },
        columns: [
            {
                dataField: "idParticipante",
                caption: "ID",
                width: "30%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                alignment: "center"
            }, {
                dataField: "nombre",
                width: "50%",
                caption: "Nombre"
            }, {
                caption: "",
                alignment: "center",
                width: "10%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "editTemplate"
            },
            {
                caption: "",
                width: "10%",
                alignment: "center",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
            }
        ],
        onInitialized: function (e) {
            console.log(e);
            $scope.datagrid = e.component;
        }
    };
    LeerRegistros = function (obj, objmodificado) {
        $scope.lastConsulta = JSON.parse("" + JSON.stringify(obj));
        Llamada.post("LecturasGenericasPaginadas", obj)
            .then(function (respuesta) {
                if (respuesta.data.articulos.length < 1) {
                    switch (obj.accionPagina) {
                        case "N":
                            cambiarBotonesPaginacionFinales("disabled");
                            break;
                        case "P":
                            cambiarBotonesPaginacionIniciales("disabled");
                            break;
                    }

                } else {
                    $scope.vm = respuesta.data;
                    $scope.orders = respuesta.data.articulos;
                    if (NotNullNotUndefinedNotEmpty(objmodificado)) {
                        $scope.orders.splice(0, 0, objmodificado);
                    }
                    console.log($scope.orders);
                    console.log("Arriba las orders");
                    $scope.datagrid.option("dataSource", $scope.orders);
                }
                //$scope.datagrid.repaint();
            });
    };
    
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: 660,
        height: 540,
        showTitle: true,
        title: "Participante",
        dragEnabled: false,
        bindingOptions: {
            visible: 'popupVisible'
        },
        closeOnOutsideClick: true
    };
    
    $scope.eliminarRegistro = function (a) {
        console.log(a);
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar este tipo de vidrio?");
        result.then(function (val) {
            if (val) {
                alert("OK");
            }
        });

    };
    
    var obj = {
        tipo: "Participante",
        cadena: ""
    };
    LeerRegistros(obj);
    $scope.cambiarPagina = function (sender, val) {
        cambiarBotonesPaginacion("");
        switch (val) {
            case "F":
                cambiarBotonesPaginacionIniciales("disabled");
                break;
            case "L":
                cambiarBotonesPaginacionFinales("disabled");
                break;
        }
        $scope.vm.cm.accionPagina = val;
        LeerRegistros($scope.vm.cm);
    };
    var buscaChangePromise;
    $scope.cambioBuscador = function () {
        if (buscaChangePromise) {
            $timeout.cancel(buscaChangePromise);
        }
        buscaChangePromise = $timeout($scope.activarBusqueda, 1000);
    }
    $scope.activarBusqueda = function () {
        console.log($scope.buscador);
        var obj = {
            tipo: "Participante",
            cadena: $scope.buscador,
        };
        LeerRegistros(obj);
    }
    $scope.anularBusqueda = function () {
        $scope.buscador = "";
        var obj = {
            tipo: "Participante",
            cadena: "",

        };
        LeerRegistros(obj);
    }
});
