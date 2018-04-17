using System;
using System.Runtime.Serialization;

namespace dsASPCCaballos.Entidades
{
    [DataContract]
    public class FormularioRegistro
    {
        [DataMember]
        public string tal { get; set; }
        [DataMember]
        public string cual { get; set; }
    }
    [DataContract]
    public class BusquedaPaginada
    {
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string cadena { get; set; }
        [DataMember]
        public int? numPag { get; set; }
        [DataMember]
        public string LastValor { get; set; }
        [DataMember]
        public int LastIndice { get; set; }
        [DataMember]
        public string AccionPagina { get; set; }
        [DataMember]
        public string FirstValor { get; set; }
        [DataMember]
        public int FirstIndice { get; set; }
        [DataMember]
        public int? idSeccion { get; set; }
    }
    [DataContract]
    public class Participante
    {
        [DataMember]
        public int IDParticipante { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public Boolean Presente { get; set; }
    }
    [DataContract]
    public class CadenasBusqueda
    {
        [DataMember]
        public string Vista { get; set; }
        [DataMember]
        public string EntidadFuncion { get; set; }
        [DataMember]
        public string CampoClave { get; set; }
        [DataMember]
        public string Entidad { get; set; }
        [DataMember]
        public string CampoOrdenacion { get; set; }
    }
    public class ResultadoIM
    {
        public int Identidad { get; set; }
        public string Resultado { get; set; }
    }
}
