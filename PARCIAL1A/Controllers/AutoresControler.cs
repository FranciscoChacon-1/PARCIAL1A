using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.Models;
using Microsoft.EntityFrameworkCore;

using static PARCIAL1A.Models.Parcial1AContext;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresControler : ControllerBase
    {
        private readonly Parcial1AContext _parcial1AContext;
        public AutoresControler(Parcial1AContext equiposContext)
        {
            _parcial1AContext = equiposContext;

        }

        /// <sumary>
        /// EndPointRetorna listado de los Autorlibro exisentes
        /// </sumary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllAutorlibro")]
        public IActionResult GetAutorlibro()
        {
            List<Autorlibro> listadoAutorlibro = (from a in _parcial1AContext.Autorlibro
                                            select a).ToList();
            
            if (listadoAutorlibro.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoAutorlibro);
        }


        /// <sumary>
        /// EndPointRetorna listado de los Autorlibro exisentes
        /// </sumary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllPosts")]
        public IActionResult GetPosts()
        {
            List<Posts> listadoPosts = (from a in _parcial1AContext.Posts
                                                  select a).ToList();

            if (listadoPosts.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPosts);
        }

        [HttpPost]
        [Route("AddPosts")]
        public IActionResult GuardarPosts([FromBody] Posts Posts)
        {
            try
            {
                _parcial1AContext.Posts.Add(Posts);
                _parcial1AContext.SaveChanges();
                return Ok(Posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizarPosts/{id}")]
        public IActionResult ActualizarPosts(int id, [FromBody] Posts PostsModificar)
        {

            Posts? PostsActual = (from e in _parcial1AContext.Posts
                                    where e.Id == id
                                    select e).FirstOrDefault();
            //Verificamos que exista el registro segun su ID
            if (PostsActual == null)
            { return NotFound(); }
            //Si se encuentra el registro, se alteran los campos modificables
            PostsActual.Id = PostsModificar.Id;
            PostsActual.Titulo = PostsModificar.Titulo;
            PostsActual.Contenido = PostsModificar.Contenido;
            PostsActual.FechaPublicacion = PostsModificar.FechaPublicacion;
            //Se marca el registro como modificado en el contexto //y se envia la modificacion a la base de datos
            _parcial1AContext.Entry(PostsActual).State = EntityState.Modified;
            _parcial1AContext.SaveChanges();
            return Ok(PostsModificar);

        }

        [HttpDelete]
        [Route("eliminarPosts/{id}")]

        public IActionResult EliminarPosts(int id)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual eliminaremos
            Posts? Posts = (from e in _parcial1AContext.Posts
                              where e.Id == id
                              select e).FirstOrDefault();
            //Verificamos que exista el registro segun su ID
            if (Posts == null)
                return NotFound();
            //Ejecutamos la accion de elminar el registro _equiposContexto.equipos.Attach(equipo);
            _parcial1AContext.Posts.Remove(Posts);
            _parcial1AContext.SaveChanges();
            return Ok(Posts);
        }

        /// <sumary>
        /// EndPointRetorna listado de los Autorlibro exisentes
        /// </sumary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllLibros")]
        public IActionResult GetLibros()
        {
            List<Libros> listadoLibros = (from a in _parcial1AContext.Libros
                                            select a).ToList();

            if (listadoLibros.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibros);
        }

        /// <sumary>
        /// EndPoint que retorna el listado de todos los equipos existentes
        /// </sumary>
        /// <param name="id"></param>>
        /// <returns></returns>
        [HttpPost]
        [Route("AddLibros")]
        public IActionResult GuardarLibros([FromBody] Libros Libros)
        {
            try
            {
                _parcial1AContext.Libros.Add(Libros);
                _parcial1AContext.SaveChanges();
                return Ok(Libros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizarLibros/{id}")]
        public IActionResult ActualizarLibros(int id, [FromBody] Libros LibrosModificar)
        {

            Libros? LibrosActual = (from e in _parcial1AContext.Libros
                                     where e.Id == id
                                     select e).FirstOrDefault();
            //Verificamos que exista el registro segun su ID
            if (LibrosActual == null)
            { return NotFound(); }
            //Si se encuentra el registro, se alteran los campos modificables
            LibrosActual.Id = LibrosModificar.Id;
            LibrosActual.Titulo = LibrosModificar.Titulo;
            //Se marca el registro como modificado en el contexto //y se envia la modificacion a la base de datos
            _parcial1AContext.Entry(LibrosActual).State = EntityState.Modified;
            _parcial1AContext.SaveChanges();
            return Ok(LibrosModificar);

        }

        [HttpDelete]
        [Route("eliminarLibro/{id}")]

        public IActionResult EliminarLibro(int id)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual eliminaremos
            Libros? Libros = (from e in _parcial1AContext.Libros
                               where e.Id == id
                               select e).FirstOrDefault();
            //Verificamos que exista el registro segun su ID
            if (Libros == null)
                return NotFound();
            //Ejecutamos la accion de elminar el registro _equiposContexto.equipos.Attach(equipo);
            _parcial1AContext.Libros.Remove(Libros);
            _parcial1AContext.SaveChanges();
            return Ok(Libros);
        }

        /// <sumary>
        /// EndPointRetorna listado de los Autorlibro exisentes
        /// </sumary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllAutores")]
        public IActionResult GetAutores()
        {
            List<Autores> listadoAutores = (from a in _parcial1AContext.Autores
                                            select a).ToList();

            if (listadoAutores.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoAutores);
        }



        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            Autores? autores = (from a in _parcial1AContext.Autores
                               where a.Id == id
                               select a).FirstOrDefault();

            if (autores== null)
            {
                return NotFound();
            }
            return Ok(autores);
        }


        [HttpGet]
        [Route("Find/{nombreAutor}")]
        public IActionResult FindByAuthor(string nombreAutor)
        {
            var posts = (from p in _parcial1AContext.Posts
                         join a in _parcial1AContext.Autores on p.AutorId equals a.Id
                         where a.Nombre == nombreAutor
                         orderby p.FechaPublicacion descending
                         select new
                         {
                             p.Titulo,
                             p.Contenido,
                             p.FechaPublicacion,
                             Autor = a.Nombre
                         }
                        ).Take(20).ToList();

            if (posts.Count == 0)
            {
                return NotFound();
            }

            return Ok(posts);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] Autores autores)
        {
            try
            {
                _parcial1AContext.Autores.Add(autores);
                _parcial1AContext.SaveChanges();
                return Ok(autores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody] Autores autoresModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual alteraremos alguna propiedad
            Autores? AutoresActual = (from e in _parcial1AContext.Autores
                                     where e.Id == id
                                     select e).FirstOrDefault();
            //Verificamos que exista el registro segun su ID
            if (AutoresActual == null)
            { return NotFound(); }
            //Si se encuentra el registro, se alteran los campos modificables
            AutoresActual.Nombre = autoresModificar.Nombre;


            //Se marca el registro como modificado en el contexto //y se envia la modificacion a la base de datos
            _parcial1AContext.Entry(AutoresActual).State = EntityState.Modified;
            _parcial1AContext.SaveChanges();
            return Ok(autoresModificar);

        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual eliminaremos
            Autores? autores = (from e in _parcial1AContext.Autores
                               where e.Id == id
                               select e).FirstOrDefault();
            //Verificamos que exista el registro segun su ID
            if (autores == null)
                return NotFound();
            //Ejecutamos la accion de elminar el registro _equiposContexto.equipos.Attach(equipo);
            _parcial1AContext.Autores.Remove(autores);
            _parcial1AContext.SaveChanges();
            return Ok(autores);
        }
    }
}
