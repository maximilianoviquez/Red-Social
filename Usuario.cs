using System;
namespace Dominio
{
	public abstract class Usuario:IValidacion
	{
		public static int UltimoId { get; set; } = 1;
		public int Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public bool Bloqueado { get; set; }

		//constructor
		public Usuario(string email,string password,bool bloqueado)
		{
			Id = UltimoId++;
			Email = email;
			Password = password;
            Bloqueado = false;
		}



        //metoddos para bloquear y desbloquear usuarios
        public void Bloquear()
        {
            if (!Bloqueado)
            {
                Bloqueado = true;
            }
        }

        public void DesBloquear()
        {
            if (Bloqueado)
            {
                Bloqueado = false;
            }
        }

        public virtual void EsValido()
        {
            if (String.IsNullOrEmpty(Email))
            {
                throw new Exception("El email no puede ser vacio");
            }
            if (String.IsNullOrEmpty(Password))
            {
                throw new Exception("El Password no puede ser vacio");
            }

        }



    }
}

