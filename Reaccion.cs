using System;
namespace Dominio
{
	public class Reaccion:IValidacion
	{
		public TipoReaccion Tipo { get; set; }
		public Miembro MiembroReacciono { get; set; }


        public void EsValido()
        {
            if (MiembroReacciono == null)
            {
                throw new Exception("La reaccion debe tener un miembro que la realizo");
            }
            if (Tipo != TipoReaccion.Like && Tipo != TipoReaccion.Dislike)
            {
                throw new Exception("El tipo de reacción es inválido.");
            }
        }

        //constructor de reaccion
        public Reaccion(TipoReaccion tipo,Miembro miembroReacciono)
		{
			Tipo = tipo;
			MiembroReacciono = miembroReacciono;

		}




	}
}

