using dsASPCCaballos.Entidades;
using dsCore2.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace dsASPCCaballos.DataAccess
{
    public class LecturasDA : DataList
    {
        public LecturasDA(IConfiguration configuration) : base(configuration)
        {
            var a = _configuration.GetConnectionString("DefaultConnection");
        }
        protected override void AsignarMetodoRelleno()
        {
            switch (Criterio.Entidad)
            {
                case "Participante":
                    MetodoRellenarLista = metodoRellenoParticipante;
                    TipoDato = typeof(Participante);
                    break;


            }
        }



        private void metodoRellenoParticipante(object entidadLista)
        {
            ((Participante)entidadLista).IDParticipante = AsignaEntero("IDParticipante");
        }

    }
}
