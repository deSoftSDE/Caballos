appadmin.controller('navegacion', function ($scope) {
    cambiarBotonesPaginacion = function (val) {
        console.log("Desactivando");
        document.getElementById("primera").className = val;
        document.getElementById("anterior").className = val;
        document.getElementById("siguiente").className = val;
        document.getElementById("ultima").className = val;
    };
    cambiarBotonesPaginacionIniciales = function (val) {
        console.log("Desactivando");
        document.getElementById("primera").className = val;
        document.getElementById("anterior").className = val;
    };
    cambiarBotonesPaginacionFinales = function (val) {
        console.log("Desactivando");
        document.getElementById("siguiente").className = val;
        document.getElementById("ultima").className = val;
    };
});

