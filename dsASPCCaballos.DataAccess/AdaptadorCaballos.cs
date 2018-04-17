using dsASPCCaballos.Entidades;
using dsCore2.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace dsASPCCaballos.DataAccess
{
    public class AdaptadorCaballos : BaseDataAccess
    {
        private IConfiguration _configuration;
        public AdaptadorCaballos(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int ParticipanteRegistrar()
        {
            var res = 1;
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                //SqlParameter[] param = new SqlParameter[]
                //{
                //    new SqlParameter("@pedido", pedido)
                //};
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Caballos.PruebaConexion", null);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res = AsignaEntero("Conexion");
                }
            }
            return res;
        }
        public List<Participante> ParticipantesLeer()
        {
            var res = new List<Participante>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                //SqlParameter[] param = new SqlParameter[]
                //{
                //    new SqlParameter("@pedido", pedido)
                //};
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Caballos.ParticipantesLeer", null);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var pr = new Participante
                    {
                        IDParticipante = AsignaEntero("IDParticipante"),
                        Nombre = AsignaCadena("Nombre"),
                    };
                    res.Add(pr);
                }
            }
            return res;
        }
        public ResultadoIM ParticipantesModificar(Participante pr)
        {
            var res = new ResultadoIM();
            //var prst = dsCore.Comun.Ayudas.SerializarACadenaXML(pr);
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    //new SqlParameter("@participante", prst)
                    new SqlParameter("@IDParticipante", pr.IDParticipante),
                    new SqlParameter("@Nombre", pr.Nombre),

                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Caballos.ParticipanteModificar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Identidad = AsignaEntero("Identidad");
                    res.Resultado = AsignaCadena("Resultado");
                }
            }
            return res;
        }
         
    }
}
