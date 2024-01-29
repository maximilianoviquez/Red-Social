using System;
namespace Dominio
{
	public abstract class Publicacion:IValidacion
	{
		public static int UltimoId { get; set; }
		public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Miembro Autor { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        

        //lista de reacciones
        private List<Reaccion> _reacciones = new List<Reaccion>();


        public int CantidadLikes()
        {
            return _reacciones.Count(r => r.Tipo == TipoReaccion.Like);
        }

        public int CantidadDislikes()
        {
            return _reacciones.Count(r => r.Tipo == TipoReaccion.Dislike);
        }


        public List<Reaccion> GetReacciones()
        {
            return _reacciones;
        }
        public List<Reaccion> ObtenerReaccionesPorTipo(TipoReaccion tipo)
        {
            return _reacciones.Where(r => r.Tipo == tipo).ToList();
        }
        //metoddo para reaccionar
        public void Reaccionar(Miembro m, TipoReaccion tp)
        {
            foreach (var reaccion in _reacciones)
            {
                if (reaccion.MiembroReacciono == m)
                {
                    return;
                }

            }
            _reacciones.Add(new Reaccion(tp, m));

        }


        //metoddos abstractos a usar en post y comentario.
        public abstract double CalcularVA();

		public abstract bool AutorHabilitadoRealizPub();

        public virtual void EsValido()
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
        public Publicacion(DateTime fecha,Miembro autor,string titulo,string contenido)
		{
			Id = UltimoId++;
			Fecha = fecha;
			Autor = autor;
			Titulo = titulo;
			Contenido = contenido;
 

		}


    }
}




