using System;
namespace Dominio
{
	public class Miembro:Usuario,IValidacion
	{
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime FechaNac { get; set; }

        //lista de amigos
        private List<Miembro> _amigos = new List<Miembro>();


        public List<Miembro> GetMiembros()
        {
            return _amigos;
        }

        //metoddo para agregar un amigo
        public void AgregarAmigo(Miembro m)
		{
			m.EsValido();
			_amigos.Add(m);
		}
		//metoddo para validar Miembro
		public void EsValido()
		{
            if (String.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre no puede ser vacio");
            }
            if (String.IsNullOrEmpty(Apellido))
            {
                throw new Exception("El apellido no puede ser vacio");
            }
            if (String.IsNullOrEmpty(Email))
            {
                throw new Exception("El email no puede ser vacio");
            }
            if (!Email.Contains("@"))
            {
                throw new Exception("El email debe tener @");
            }
            if (String.IsNullOrEmpty(Password))
            {
                throw new Exception("El Password no puede ser vacio");
            }
            if (Password.Length <= 7)
            {
                throw new Exception("El Password debe tener al menos 8 caracteres");
            }
            if (Bloqueado)
			{
                throw new Exception("El miembro esta bloquedo");
            }
            
        }

        public bool EstaBloqueado()
        {
            if (Bloqueado)
            {
                return true;
            }
            return false;
        }

      

        //constructor miembro, y la base que hereda de usuario
        public Miembro(string nombre,string apellido,DateTime fechaNac,string email,string password,bool bloqueado):base(email,password,bloqueado)
		{
			Nombre = nombre;
			Apellido = apellido;
            FechaNac = fechaNac;
		}

        public Miembro() : base("", "", false)
        {
            
        }

    }
}

