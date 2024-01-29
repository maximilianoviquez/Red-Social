using System;
namespace Dominio
{
	public class Comentario:Publicacion,IValidacion
	{

        public bool EstaCensurado { get; private set; }

        //metoddo para censurar comentario
        public bool Censurar()
        {
            if (!EstaCensurado)
            {
                EstaCensurado = true;
            }
            return EstaCensurado;
            
        }

        //metoddo heredado para calcular Valor de Aceptacion
        public override double CalcularVA()
        {
            double retlike = 0;
            double retdis = 0;
            foreach (Reaccion r in GetReacciones())
            {
                if (r.Tipo == TipoReaccion.Like)
                {
                    retlike++;
                }
                if (r.Tipo == TipoReaccion.Dislike)
                {
                    retdis++;
                }
            }
            return (retlike * 5)-(retdis * -2);

        }
        //metoddo heredado para saber si el autor esta habilitado a realizar publicacion
        public override bool AutorHabilitadoRealizPub()
        {
            if (!Autor.Bloqueado)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //metoddo para validar comentario
        public void EsValido()
        {
            
            if (Titulo.Length < 3)
            {
                throw new Exception("El titulo debe tener almenos 3 caracteres");
            }
            if (String.IsNullOrEmpty(Titulo))
            {
                throw new Exception("El titulo no puede ser vacio");
            }
            if (Titulo.Length < 3)
            {
                throw new Exception("El titulo debe tener almenos 3 caracteres");
            }
            if (String.IsNullOrEmpty(Contenido))
            {
                throw new Exception("El contenido no puede ser vacio");
            }
          

        }

     

        //constructor
        public Comentario(bool estaCensurado,DateTime fecha,Miembro autor,string titulo,string contenido):base(fecha, autor, titulo, contenido)
		{
            EstaCensurado = estaCensurado;
		}
	}
}

