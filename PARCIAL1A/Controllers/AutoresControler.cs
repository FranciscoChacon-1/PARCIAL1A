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
        public AutoresControler(Parcial1AContext AutoresContext)
        {
            _parcial1AContext = AutoresContext;

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
        public IActionResult Getautores(int id)
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
        public IActionResult Guardarautor([FromBody] Autores autores)
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
        public IActionResult Actualizarautor(int id, [FromBody] Autores autoresModificar)
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

        public IActionResult Eliminarautor(int id)
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
