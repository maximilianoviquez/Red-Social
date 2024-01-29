using System;
namespace Dominio
{
	public class Invitacion:IValidacion
	{
		public static int UltimoId { get; set; } = 1;
		public int Id { get; set; }
		public Miembro MiembroSolicitante { get; set; }
		public Miembro MiembroSolicitado { get; set; }
		public DateTime FechaSolicitud { get; set; }
		public TipoEstado Estado { get; set; }


		//metoddo para aceptar una invitacion
		public void Aceptar()
		{
			if (MiembroSolicitado.Bloqueado || MiembroSolicitante.Bloqueado)
			{
				throw new Exception("Usuario Bloqueado");
			}
            Estado = TipoEstado.Aceptada;
			MiembroSolicitado.GetMiembros().Add(MiembroSolicitante);
			MiembroSolicitante.GetMiembros().Add(MiembroSolicitado);

		}
		//metoddo para rechazar una invitacion
        public void Rechazar()
        {
            if (MiembroSolicitado.Bloqueado || MiembroSolicitante.Bloqueado)
            {
                throw new Exception("Usuario Bloqueado");
            }
            Estado = TipoEstado.Rechazada;

        }
		//metoddo para validar una invitacion
		public void EsValido()
		{
			if(MiembroSolicitante.Id == MiembroSolicitado.Id)
			{
				throw new Exception("El solicitante es el mismo usuario que el solicitado");
			}
            if (MiembroSolicitante.GetMiembros().Contains(MiembroSolicitado) && MiembroSolicitado.GetMiembros().Contains(MiembroSolicitante))
            {
                throw new Exception("Ya son ");
            }

        }
		//constructor 
        public Invitacion(Miembro miembrosolicitante, Miembro miembrosolicitado)
        {
            MiembroSolicitado = miembrosolicitado;
            MiembroSolicitante = miembrosolicitante;
            FechaSolicitud = DateTime.Now;
            Estado = TipoEstado.Pendiente;
        }
    }
}

