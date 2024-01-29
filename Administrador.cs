using System;

namespace Dominio
{
	public class Administrador:Usuario,IValidacion
	{


        public void BloquearUsuario(Usuario u)
        {
            u.Bloquear();
        }

        public void DesBloquearUsuario(Usuario u)
        {
            u.DesBloquear();
        }

        //constructor
        public Administrador(string email,string password,bool bloqueado):base(email,password,bloqueado)
		{
            
		}
        //metoddo para validar administrador
        public void EsValido()
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

