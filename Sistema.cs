using System;
using System.Net.Http;

namespace Dominio
{
	public class Sistema
	{
		//listas
		private List<Publicacion> _publicaciones = new List<Publicacion>();
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Invitacion> _invitaciones = new List<Invitacion>();
        private List<Reaccion> _reacciones = new List<Reaccion>();
        

        //singleton del sistema:
        #region singleton

        private Sistema()
        {
            Precarga();
        }
        private static Sistema instance = null;
        
        public static Sistema GetInstancia()
        {
            if (instance == null)
            {
                instance = new Sistema();
            }
            return instance;
        }

        #endregion

        //Altas con validacion:
        #region Altas
        
        public void AltaPublicacion(Publicacion pub)
        {
            pub.EsValido();
            _publicaciones.Add(pub);
        }


        public void AltaUsuario(Usuario user)
        {
            if (!ExisteEmail(user.Email))
            {
                user.EsValido();
                _usuarios.Add(user);
            }
            else
            {
                throw new Exception("Ya existe el usuario con este email");
            }
           
        }

        public void AltaInvitacion(Invitacion inv)
        {
            inv.EsValido();
            _invitaciones.Add(inv);
        }
        public void AltaReaccion(Reaccion reac)
        {
            reac.EsValido();
            _reacciones.Add(reac);
        }
        #endregion

        //Methods para obtener las listas:
        #region Gets()

        public List<Publicacion> GetPublicaciones()
        {
            return _publicaciones;
        }
        public List<Usuario> GetUsuarios()
        {
            return _usuarios;
        }
        public List<Invitacion> GetInvitaciones()
        {
            return _invitaciones;
        }
        public List<Reaccion> GetReacciones()
        {
            return _reacciones;
        }
       


        public List<Post> GetPostsOrdenadosEntreFechas(DateTime fechaIn, DateTime fechaFn)
        {
            List<Post> postsEnRango = new List<Post>();
            foreach (Publicacion p in _publicaciones)
            {
                if (p is Post)
                {
                    Post pp = p as Post;
                    if (pp.Fecha >= fechaIn && pp.Fecha <= fechaFn)
                    {
                        postsEnRango.Add(pp);
                    }

                }


            }
            postsEnRango.Sort((post1, post2) => CompareTitulosDescendente(post1.Titulo, post2.Titulo));

            return postsEnRango;
        }

        public List<Post> GetPosts()
        {
            List<Post> posts = new List<Post>();
            foreach (Publicacion p in _publicaciones)
            {
                if (p is Post)
                {
                    Post pp = (Post)p;
                    // Agregar la condición para mostrar solo posts no baneados y visibles
                   
                        posts.Add(pp);
                    
                }
            }
            return posts;
        }

        private int CompareTitulosDescendente(string titulo1, string titulo2)
        {
            // Comparar títulos en orden descendente alfabéticamente
            return string.Compare(titulo2, titulo1, StringComparison.OrdinalIgnoreCase);
        }


        public List<Post> GetPostEnLosQueHallaComentado(string emailMiembro)
        {
            List<Post> postDondeComento = new List<Post>();

            foreach (Publicacion p in _publicaciones)
            {
                if (p is Post)
                {
                    Post pp = p as Post;
                    foreach (Comentario c in pp.GetComentarios())
                    {
                        if (c.Autor.Email == emailMiembro)
                        {
                            postDondeComento.Add(pp);
                        }
                    }
                }
                    
                
            }
            return postDondeComento;
        }


        public List<Publicacion> GetPubQueHallaRealizado(string email)
        {
            List<Publicacion> publicacionesDelUsuario = new List<Publicacion>();
            foreach (Publicacion p in _publicaciones)
            {
                if (p.Autor.Email == email)
                {
                    publicacionesDelUsuario.Add(p);
                }
            }
            return publicacionesDelUsuario;
        }





        public List<Usuario> GetMiembrosConMasPub()
        {
            List<Usuario> miembrosConMasPub = new List<Usuario>();
            int maximoPub = 0;

            foreach (Usuario m in _usuarios)
            {
                int contadorPub = 0;
                foreach (Publicacion p in _publicaciones)
                {
                    if (p.Autor.Email == m.Email)
                    {
                        contadorPub++;
                    }
                }
                if (contadorPub > maximoPub)
                {
                    maximoPub = contadorPub;
                    miembrosConMasPub.Clear();
                    miembrosConMasPub.Add(m);
                }
                else if (contadorPub == maximoPub)
                {
                    miembrosConMasPub.Add(m);
                }
            }
            return miembrosConMasPub;
        }


        #endregion 

        //Precarga de datos:
        #region Precarga
        private void Precarga()
        {
            try
            {
                //usuarios -> miembros
                Usuario m1 = new Miembro("Maximiliano", "Viquez", new DateTime(1999, 02, 09), "viquez@gmail.com", "iammv1999",false);
                AltaUsuario(m1);
                Usuario m2 = new Miembro("Leonardo", "Perez", new DateTime(1969, 10, 02), "mjordan@gmail.com", "5anillos",false);
                AltaUsuario(m2);
                Usuario m3 = new Miembro("Jaime", "Vardy", new DateTime(1978, 02, 06), "vardy@gmail.com", "leicestercity",false);
                AltaUsuario(m3);
                Usuario m4 = new Miembro("Antonella", "Garcia", new DateTime(1992, 07, 12), "anto92@gmail.com", "milan2020",true);
                AltaUsuario(m4);
                Usuario m5 = new Miembro("Nicolas", "Vazquez", new DateTime(1994, 12, 06), "vazquez14@gmail.com", "vazquez3000",false);
                AltaUsuario(m5);
                Usuario m6 = new Miembro("Ana", "Gonzalez", new DateTime(1990, 12, 12), "anagonza90@gmail.com", "valed3200",false);
                AltaUsuario(m6);
                Usuario m7 = new Miembro("Jose", "Alvarez", new DateTime(1988, 05, 05), "josealvrr@gmail.com", "jose123alv",true);
                AltaUsuario(m7);
                Usuario m8 = new Miembro("Pedro", "Garcia", new DateTime(1993, 04, 02), "pablofugarz@gmail.com", "garc2023",false);
                AltaUsuario(m8);
                Usuario m9 = new Miembro("Florencia", "Gomez", new DateTime(1970, 12, 01), "florg70@gmail.com", "8florg70",false);
                AltaUsuario(m9);
                Usuario m10 = new Miembro("Esperanza", "Sanchez", new DateTime(2001, 09, 01), "espesan@gmail.com", "col23esperanza",false);
                AltaUsuario(m10);
                Usuario m11 = new Miembro("Pepe", "Sanz", new DateTime(2002, 09, 01), "sanz@gmail.com", "12345678", false);
                AltaUsuario(m11);
                //usuarios -> admins 
                Usuario a1 = new Administrador("jorge@gmail.com", "vasdf3000",false);
                Usuario a2 = new Administrador("adriana@gmail.com", "adri2023", false);
                AltaUsuario(a1);
                AltaUsuario(a2);

                //Invitaciones
                Invitacion i1 = new Invitacion((Miembro)m1, (Miembro)m2);
                AltaInvitacion(i1);
                AceptarInvitacion(i1);
                Invitacion i2 = new Invitacion((Miembro)m3, (Miembro)m4);
                AltaInvitacion(i2);
                //pendiente
                Invitacion i3 = new Invitacion((Miembro)m5, (Miembro)m6);
                AltaInvitacion(i3);
                AceptarInvitacion(i3);
                Invitacion i4 = new Invitacion((Miembro)m7, (Miembro)m8);
                AltaInvitacion(i4);
                RechazarInvitacion(i4);
                Invitacion i5 = new Invitacion((Miembro)m9, (Miembro)m10);
                RechazarInvitacion(i5);
                AceptarInvitacion(i5);
                //miembro1 tiene invitacion de toddos los demas
                Invitacion i6 = new Invitacion((Miembro)m1, (Miembro)m3);
                AltaInvitacion(i6);
                AceptarInvitacion(i6);
                Invitacion i7 = new Invitacion((Miembro)m1, (Miembro)m4);
                AltaInvitacion(i7);
                Invitacion i8 = new Invitacion((Miembro)m1, (Miembro)m5);
                AltaInvitacion(i8);
                AceptarInvitacion(i8);
                Invitacion i9 = new Invitacion((Miembro)m1, (Miembro)m6);
                AltaInvitacion(i9);
                AceptarInvitacion(i9);
                Invitacion i10 = new Invitacion((Miembro)m1, (Miembro)m7);
                AltaInvitacion(i10);
                AceptarInvitacion(i10);
                Invitacion i11 = new Invitacion((Miembro)m1, (Miembro)m8);
                AltaInvitacion(i11);
                AceptarInvitacion(i11);
                Invitacion i12 = new Invitacion((Miembro)m1, (Miembro)m9);
                AltaInvitacion(i12);
                AceptarInvitacion(i12);
                Invitacion i13 = new Invitacion((Miembro)m1, (Miembro)m10);
                AltaInvitacion(i13);
                AceptarInvitacion(i13);
                //miembro2 tiene invitaciones de toddos los demas tambien
                Invitacion i14 = new Invitacion((Miembro)m2, (Miembro)m3);
                AltaInvitacion(i14);
                Invitacion i15 = new Invitacion((Miembro)m2, (Miembro)m4);
                AltaInvitacion(i15);
                AceptarInvitacion(i15);
                Invitacion i16 = new Invitacion((Miembro)m2, (Miembro)m5);
                AltaInvitacion(i16);
                RechazarInvitacion(i16);
                Invitacion i17 = new Invitacion((Miembro)m2, (Miembro)m6);
                AltaInvitacion(i17);
                AceptarInvitacion(i17);
                Invitacion i18 = new Invitacion((Miembro)m2, (Miembro)m7);
                AltaInvitacion(i18);
                AceptarInvitacion(i18);
                Invitacion i19 = new Invitacion((Miembro)m2, (Miembro)m8);
                AltaInvitacion(i19);
                RechazarInvitacion(i19);
                Invitacion i20 = new Invitacion((Miembro)m2, (Miembro)m9);
                AltaInvitacion(i20);
                AceptarInvitacion(i20);
                Invitacion i21 = new Invitacion((Miembro)m2, (Miembro)m10);
                AltaInvitacion(i21);
                AceptarInvitacion(i21);
                


                //Publicaciones, Post y comentarios
                Publicacion p1 = new Post("lamborgini.png", true, false, new DateTime(2003, 05, 03), (Miembro)m1, "Lamborgini huracan 2020", "es una luz");
                AltaPublicacion(p1);
                Publicacion p2 = new Post("mansion.jpeg", true, false, new DateTime(2010, 04, 03), (Miembro)m1, "Mansion in LA", "la casa de las fiestas exoticas");
                AltaPublicacion(p2);
                Publicacion p3 = new Post("rolex.png", true, false,new DateTime(2011, 05, 10), (Miembro)m7, "rolex models", "the best one");
                AltaPublicacion(p3);
                Publicacion p4 = new Post("yate.jpeg", true, false,  new DateTime(2012, 11, 03), (Miembro)m5, "3 pisos ", "Direccion Guiada");
                AltaPublicacion(p4);
                Publicacion p5 = new Post("mcClaren.png", true, false, new DateTime(2013, 03, 03), (Miembro)m1, "Kilometraje bajo", "Pintura cromada original");
                AltaPublicacion(p5);
                Publicacion p6 = new Post("bugatti.jpeg", true, false, new DateTime(2022, 05, 04), (Miembro)m5, "Compiten por su compra", "ultimos 10 modelos");
                AltaPublicacion(p6);
                Publicacion p7 = new Post("alajas.jpeg", false, false,  new DateTime(2020, 02, 12), (Miembro)m5, "Pulidos naturales", "De los mas codiciados");
                AltaPublicacion(p7);
                Publicacion p8 = new Post("penthouse.jpeg", true, false, new DateTime(2021, 07, 03), (Miembro)m8, "La vista de NY","The Best City");
                AltaPublicacion(p8);
                //comentarios                                                   
                Publicacion com1 = new Comentario(false,new DateTime(2020, 05, 10), (Miembro)m6, "Wooow!", "Increible");
                AgregarComentario((Post)p1, (Comentario)com1);
                Publicacion com2 = new Comentario(false, new DateTime(2023, 05, 09), (Miembro)m4, "Geniaaaal", "esta tremendo");
                AgregarComentario((Post)p1, (Comentario)com2);
                Publicacion com3 = new Comentario(false, new DateTime(2022, 04, 07), (Miembro)m2, "pasame info", "info plz");
                AgregarComentario((Post)p1, (Comentario)com3);
                Publicacion com4 = new Comentario(false, new DateTime(2023, 01, 08), (Miembro)m5, "como se llama?", "Alguien me dice el nombre");
                AgregarComentario((Post)p2, (Comentario)com4);
                Publicacion com5 = new Comentario(false, new DateTime(2023, 09, 06), (Miembro)m9, "en donde", "pasen ubi");
                AgregarComentario((Post)p2, (Comentario)com5);
                Publicacion com6 = new Comentario(false, new DateTime(2022, 08, 11), (Miembro)m7, "para cuando?", " cuando seria");
                AgregarComentario((Post)p2, (Comentario)com6);
                Publicacion com7 = new Comentario(false, new DateTime(2000, 07, 10), (Miembro)m6, "Jajajaja", "que risa");
                AgregarComentario((Post)p3, (Comentario)com7);
                Publicacion com8 = new Comentario(true, new DateTime(2004, 06, 12), (Miembro)m1, "que carajos", "Queeee");
                AgregarComentario((Post)p3, (Comentario)com8);
                Publicacion com9 = new Comentario(false, new DateTime(2020, 04, 11), (Miembro)m1, "yo quiero", "yendo");
                AgregarComentario((Post)p3, (Comentario)com9);
                Publicacion com10 = new Comentario(false, new DateTime(2010, 02, 01), (Miembro)m6, "hay mejores", "tampoco es tanto");
                AgregarComentario((Post)p4, (Comentario)com10);
                Publicacion com11 = new Comentario(false, new DateTime(2011, 03, 10), (Miembro)m5, "quiero tu contacto", "pasame contact");
                AgregarComentario((Post)p4, (Comentario)com11);
                Publicacion com12 = new Comentario(false, new DateTime(2001, 02, 02), (Miembro)m4, "hhahaha", "epic haha");
                AgregarComentario((Post)p4, (Comentario)com12);
                Publicacion com13 = new Comentario(false, new DateTime(2023, 02, 05), (Miembro)m8, "so great", "amazing");
                AgregarComentario((Post)p5, (Comentario)com13);
                Publicacion com14 = new Comentario(false, new DateTime(2022, 04, 07), (Miembro)m8, "negro esta mejor", "en negro");
                AgregarComentario((Post)p5, (Comentario)com14);
                Publicacion com15 = new Comentario(true, new DateTime(2023, 01, 01), (Miembro)m10, "que vista", "increible");
                AgregarComentario((Post)p5, (Comentario)com15);

                //reaccion a 2 post y 2 comentarios
                p4.Reaccionar((Miembro)m1,TipoReaccion.Dislike);
                p4.Reaccionar((Miembro)m7, TipoReaccion.Like);
                p5.Reaccionar((Miembro)m3, TipoReaccion.Like);
                com1.Reaccionar((Miembro)m4, TipoReaccion.Like);
                com6.Reaccionar((Miembro)m7, TipoReaccion.Like);
                com2.Reaccionar((Miembro)m5, TipoReaccion.Like);


            }
            catch(Exception e)
            {
                Console.WriteLine("Error " + e.Message);

            }


        }
        #endregion


        //Invitacion
        public void AceptarInvitacion(Invitacion i)
        {
            i.Aceptar();
        }
        public void RechazarInvitacion(Invitacion i)
        {
            i.Rechazar();
        }

        //Publicaciones ,calculo Cantidad de publicaciones de un usuario:
        public int CantidadPublicacionesDe(int idUsusario)
        {
            int contador = 0;
            foreach (Publicacion p in _publicaciones)
            {
                if (p.Autor.Id == idUsusario)
                {
                    contador++;
                }
            }
            return contador;
        }
        //Comentarios
        public void AgregarComentario(Post p,Comentario c)
        {
            if (p.EsPublico)
            {
                AltaPublicacion(c);
                p.AgregarComentario(c);
            }
            else
            {
                if (SonAmigos(p.Autor, c.Autor))
                {
                    AltaPublicacion(c);
                    p.AgregarComentario(c);
                }
            }
        }
        public Comentario GetComentarioPorId(int idComentario)
        {
            foreach (Publicacion publicacion in GetPublicaciones())
            {
                if (publicacion is Comentario comentario && comentario.Id == idComentario)
                {
                    return comentario;
                }
            }
            return null;
        }

        //Post
        public void RealizarPost(Publicacion p)
        {
            AltaPublicacion(p);
        }
        public bool ExisteEmail(string e)
        {
            foreach (Usuario u in _usuarios)
            {
                if (u.Email.Equals(e))
                {
                    return true;
                }
            }
            return false;
        }

        public Post GetPostPorId(int idPost)
        {
            foreach (Publicacion p in _publicaciones)
            {
                if (p is Post && p.Id == idPost)
                {
                    return (Post)p;
                }
            }
            return null;
        }

        public List<Publicacion> GetPostsPorTextoYNum(string textobuscar, int numbuscar)
        {
            List<Publicacion> resultado = new List<Publicacion>();

            foreach (Publicacion publicacion in _publicaciones)
            {
                double va = publicacion.CalcularVA();

                if (va > numbuscar)
                {
                    if (publicacion is Post)
                    {
                        Post post = (Post)publicacion;
                        if (!post.Censurados && (post.Titulo.Contains(textobuscar, StringComparison.OrdinalIgnoreCase) || post.Contenido.Contains(textobuscar, StringComparison.OrdinalIgnoreCase)))
                        {
                            resultado.Add(post);
                        }
                    }
                    else if (publicacion is Comentario)
                    {
                        Comentario comentario = (Comentario)publicacion;
                        if (!comentario.EstaCensurado && (comentario.Titulo.Contains(textobuscar, StringComparison.OrdinalIgnoreCase) || comentario.Contenido.Contains(textobuscar, StringComparison.OrdinalIgnoreCase)))
                        {
                            resultado.Add(comentario);
                        }
                    }
                }
            }

            return resultado;
        }
        //Login
        public bool IniciarSesion(string email, string password)
        {
            Usuario usuario = null;

            foreach (Usuario u in _usuarios)
            {
                if (u.Email == email)
                {
                    usuario = u;
                    break;
                }
            }

            if (usuario != null)
            {
                if (usuario.Password == password && !usuario.Bloqueado)
                {
                    return true;
                }
            }

            return false;
        }

        public Usuario ExisteUsuario(string email, string pass)
        {
           foreach(Usuario u in _usuarios)
            {
                if (u.Email.Equals(email) && u.Password.Equals(pass))
                {
                    return u;
                }
            }
            return null;
        }
       
        public Miembro MiembroPorId(object idMiembro)
        {
            int id = Convert.ToInt32(idMiembro);

            foreach (Usuario usuario in _usuarios)
            {
                if (usuario is Miembro && usuario.Id == id)
                {
                    return (Miembro)usuario;
                }
            }
            return null;
        }

        public Usuario BuscarUsuario(int? lid)
        {
            foreach (Usuario u in _usuarios)
            {
                if (u.Id.Equals(lid))
                {
                    return u as Usuario;
                }
            }
            return null;
        }


        public bool EstaHabilitadoParaMiembro(Post post, Miembro miembro)
        {
            return !post.Censurados && (post.EsPublico || post.Autor == miembro || SonAmigos(post.Autor, miembro));
        }


        //Valido si son o no amigos
        public bool SonAmigos(Miembro m1, Miembro m2)
        {
            if (m1.GetMiembros().Contains(m2) && m2.GetMiembros().Contains(m1))
            {
                return true;
            }
            else
            {
                return false;

            }
        }



    }
}

