using System;
namespace Dominio
{
    public class Post : Publicacion, IValidacion, IComparable<Post>
	{
		public string Imagen { get; set; }
        public bool EsPublico { get; set; }
		public bool Censurados { get; set; }
        public int IdAdministradorQueBaneo { get; set; }


        //lista de comentarios
        private List<Comentario> _comentarios = new List<Comentario>();

        public List<Comentario> GetComentarios()
        {
            return _comentarios;
        }

        public void AgregarComentario(Comentario c)
        {
            _comentarios.Add(c);
        }

        //metoddo para saber si un autor esta habilitado a realizar una publicacion
        public override bool AutorHabilitadoRealizPub()
        {
            if (Autor.Bloqueado)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //metoddo para calcular el valor de aceptcion de un post
        public override double CalcularVA()
        {
            double retlike = 0;
            double retdis = 0;
            double retTotal = 0;
            foreach(Reaccion r in GetReacciones())
            {
                if(r.Tipo == TipoReaccion.Like)
                {
                    retlike++;
                }
                if (r.Tipo == TipoReaccion.Dislike)
                {
                    retdis++;
                }
                if (EsPublico)
                {
                    retTotal = (retlike * 5) - (retdis * -2) + 10;
                }
                retTotal = (retlike * 5) - (retdis * -2);
            }
            return retTotal;
            
        }


        public bool DesCensurar(Usuario admin)
        {
            if (Censurados && admin is Administrador)
            {
                Censurados = false;
                return true;
            }
            return false;
        }

        public bool Censurar(Usuario admin)
        {
            if (!Censurados && admin is Administrador)
            {
                Censurados = true;
                IdAdministradorQueBaneo = admin.Id;
                return true;
            }
            return false;
        }
        //metoddo para validar publicacion
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
            if (String.IsNullOrEmpty(Imagen))
            {
                throw new Exception("El texto de la imagen no puede ser vacio");
            }
            if (!Imagen.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) && !Imagen.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
				throw new Exception("El nombre de la imagen debe terminar en .jpg o .png");
			}


        }

        public int CompareTo(Post? other)
        {
            if (other == null)
            {
                return -1;
            }
            int fechaComparar = Fecha.CompareTo(other.Fecha);
            if (fechaComparar != 0)
            {
                return fechaComparar;
            }
            return CompareTitulosDescendente(other.Titulo, Titulo);
        }

        private int CompareTitulosDescendente(string titulo1, string titulo2)
        {
            // Comparar títulos en orden descendente alfabéticamente
            return string.Compare(titulo2, titulo1, StringComparison.OrdinalIgnoreCase);
        }

        public bool PuedeComentar(Miembro miembroComentador)
        {
            //Si el post es público cualquiera puede comentar
            if (EsPublico)
            {
                return true;
            }
            //Si el post es privado  verifica si el miembro es amigo del autor
            return Autor.GetMiembros().Contains(miembroComentador);
        }

        //constructor
        public Post(string imagen,bool estadoPost,bool censurados,DateTime fecha,Miembro autor,string titulo,string contenido):base(fecha,autor,titulo,contenido)
		{
			Imagen = imagen;
			EsPublico = estadoPost;
			Censurados = censurados;
  

		}

        public Post(Miembro autor, string titulo, string contenido) : base(DateTime.Now, autor, titulo, contenido)
        {
            

        }



        

    }
}

