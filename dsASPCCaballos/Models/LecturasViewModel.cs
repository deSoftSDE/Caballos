using dsASPCCaballos.DataAccess;
using dsASPCCaballos.Entidades;
using dsCore.Buscador;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCCaballos.Models
{
    public class LecturasViewModel
    {
        public BusquedaPaginada cm;
        public List<object> Articulos;
        public int NumReg;
        public int NumPags;
        public Boolean SinResultados;
        public int? idSeccion;
        public LecturasViewModel(IConfiguration configuration, BusquedaPaginada ba)
        {
            var cadenas = ObtenerCadenasSegúnTipo(ba.tipo);
            cm = new BusquedaPaginada();
            cm.cadena = ba.cadena;
            cm.tipo = ba.tipo;
            idSeccion = ba.idSeccion;
            var v = "";
            var vc = "";
            if (ba.AccionPagina == null)
            {
                ba.AccionPagina = "F";
            }
            switch (ba.AccionPagina)
            {
                case "P":
                    v = ba.FirstValor;
                    vc = ba.FirstIndice.ToString();
                    break;
                case "N":
                    v = ba.LastValor;
                    vc = ba.LastIndice.ToString();
                    break;
            }
            string lcb = null;
            if (ba.idSeccion.HasValue)
            {
                lcb = "IDSeccion=" + ba.idSeccion;
                if (ba.cadena != null && ba.cadena.Length > 0)
                {
                    lcb = lcb + " AND " + cadenas.CampoOrdenacion + " LIKE '%" + ba.cadena + "%' ";
                }
            }
            else if (ba.cadena != null && ba.cadena.Length > 0)
            {
                lcb = cadenas.CampoOrdenacion + " LIKE '%" + ba.cadena.Replace(" ", "%") + "%' ";
                if (cadenas.EntidadFuncion == "WebArticulos")
                {
                    lcb = lcb + " OR Codigo LIKE '%" + ba.cadena.Replace(" ", "%") + "%' ";
                }
            }
            var criterioAuxiliares = new CriterioBusqueda
            {
                IdISOLang = null,
                SqlWhere = lcb,
                CampoOrdenacion = cadenas.CampoOrdenacion, //"Descripcion",
                TipoOrden = "ASC",
                NumPagina = 1,
                TamanoPagina = 6,
                CamposBusqueda = null,
                Entidad = cadenas.Entidad, //"BuscaArticulo",
                Paginacion = false,
                Operacion = ba.AccionPagina,
                Valor = v,
                ValorClave = vc,
                CampoClave = cadenas.CampoClave, //"IdArticulo",
                EntidadFuncion = cadenas.EntidadFuncion, //"BuscaArticulo",
                ValorFuncion = "'" + ba.cadena.Replace(" ", "%") + "'",
                EntidadVista = cadenas.Vista, // "VBuscaArticulo",
                idAlmacen = 1,
                idDelegacion = 0
            };
            var ls = new LecturasDA(configuration);
            var res = ls.LeerLista(criterioAuxiliares);
            Articulos = res.ListaResultados;
            NumReg = res.ContadorResultados;
            NumPags = res.NumeroPaginas;
            ColocarIndicesSegunTipo(ba.tipo, ba.AccionPagina);
        }
        private CadenasBusqueda ObtenerCadenasSegúnTipo(string tipo)
        {
            var res = new CadenasBusqueda();
            res.CampoOrdenacion = "Descripcion";
            switch (tipo)
            {
                case "Participante":
                case "Participantes":
                    res.Vista = "VParticipantes";
                    res.EntidadFuncion = "VParticipantes";
                    res.CampoClave = "IdParticipante";
                    res.Entidad = "Participante";
                    res.CampoOrdenacion = "Nombre";
                    break;
                
            }
            return res;
        }
        private void ColocarIndicesSegunTipo(string tipo, string action)
        {
            try
            {
                switch (tipo)
                {
                    case "Participante":
                    case "Participantes":
                        RellenoIndiceParticipante(action);
                        break;
                }
            }
            catch
            {
                SinResultados = true;
            }

        }
        private void RellenoIndiceParticipante(string action)
        {
            var c = (Participante)Articulos[Articulos.Count - 1];
            var d = (Participante)Articulos[0];
            cm.LastValor = c.Nombre;
            cm.LastIndice = c.IDParticipante;
            cm.FirstValor = d.Nombre;
            cm.FirstIndice = d.IDParticipante;
            cm.AccionPagina = action;
        }
        
    }
}
