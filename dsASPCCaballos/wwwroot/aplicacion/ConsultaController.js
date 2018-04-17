appadmin.controller('consulta', function ($scope, Llamada, $timeout) {
    LeerParticipantes = function () {
        Llamada.get("ParticipantesLeer")
            .then(function (respuesta) {
                console.log(respuesta.data);
                $scope.participantes = respuesta.data;
                $scope.datagrid.option("dataSource", $scope.participantes);
            })
    }
    $scope.dataGridOptions = {
        dataSource: [],
        keyExpr: "id",
        filterRow: { visible: true },
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
                allowEditing: false,
                alignment: "center"
            }, {
                dataField: "nombre",
                caption: "Nombre",
                width: "50%",
                allowEditing: false,
            }, {
                dataField: "presente",
                caption: "Presente",
                width: "10%",
                allowEditing: false,
            }, {
                caption: "Modificar",
                alignment: "center",
                width: "10%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "editTemplate"
            },
            {
                caption: "Eliminar",
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
            LeerParticipantes();
        }
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
    $scope.modificarParticipante = function (participante) {
        $scope.popupVisible = true;
        $scope.currentParticipante = participante;
        console.log($scope.currentParticipante);
    }
    $scope.eliminarParticipante = function (participante) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar este participante?");
        result.then(function (val) {
            if (val) {
                Llamada.get("ParticipanteEliminar?idParticipante=" + participante.idParticipante)
                    .then(function (respuesta) {
                        console.log(respuesta);
                        LeerParticipantes();
                    });
            }
        });
    }
    $scope.cancelarCambios = function () {
        $scope.popupVisible = false;
    }
    $scope.guardarCambiosPopup = function () {
        console.log($scope.currentParticipante);
        Llamada.post("ParticipanteModificar", $scope.currentParticipante)
            .then(function (respuesta) {
                console.log(respuesta.data);
                $scope.popupVisible = false;
            })
    }
    
});
