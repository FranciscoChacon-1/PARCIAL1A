using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly Parcial1AContext _parcial1AContext;
        public PostsController(Parcial1AContext PostsContext)
        {
            _parcial1AContext = PostsContext;

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

        [HttpGet]
        [Route("GetPostsporAutor")]
        public IActionResult GetPosrsporAutor(string Autornombre)
        {
            var posts = (from p in _parcial1AContext.Posts
                         join a in _parcial1AContext.Autores on p.AutorId equals a.Id
                         where a.Nombre == Autornombre
                         orderby p.FechaPublicacion descending
                         select p).Take(20).ToList();

            if (posts.Count == 0)
            {
                return NotFound();
            }

            return Ok(posts);
        }

        [HttpGet]
        [Route("GetPostsporLibro")]
        public IActionResult GetPostsporLibro()
        {
            var PostsporLibro = (from p in _parcial1AContext.Posts
                                 join l in _parcial1AContext.Libros on p.Id equals l.Id
                                 orderby l.Titulo
                                 group p by new { l.Id, l.Titulo } into pass
                                 select new
                                 {
                                     LibroId = pass.Key.Id,
                                     Titulo = pass.Key.Titulo,
                                     Posts = pass.ToList()
                                 }).ToList();

            if (PostsporLibro.Count == 0)
            {
                return NotFound();
            }

            return Ok(PostsporLibro);
        }
    }
}
   
