using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly Parcial1AContext _parcial1AContext;
        public LibrosController(Parcial1AContext parcial1AContext)
        {
            _parcial1AContext = parcial1AContext;
        }
        /// <sumary>
        /// EndPointRetorna listado de los Autorlibro exisentes
        /// </sumary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllLibros")]
        public IActionResult GetLibros()
        {
            List<Libros> listadoLibros = (from l in _parcial1AContext.Libros
                                          select l).ToList();

            if (listadoLibros.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibros);
        }

        [HttpGet]
        [Route("GetByIdlibros/{id}")]
        public IActionResult Getlibros(int id)
        {
            Libros? Libros = (from l in _parcial1AContext.Libros
                                where l.Id == id
                                select l).FirstOrDefault();

            if (Libros == null)
            {
                return NotFound();
            }
            return Ok(Libros);
        }
        /// proba este meto que e sel de busacar un libro mediante el nombre del auotr mira si te funciona
        [HttpGet]
        [Route("Find/{nombreAutor}")]
        public IActionResult FindByAuthor(string nombreAutor)
        {
            var libros = (from l in _parcial1AContext.Libros
                          join al in _parcial1AContext.Autorlibro on l.Id equals al.LibroId
                          join a in _parcial1AContext.Autores on al.AutorId equals a.Id
                          where a.Nombre == nombreAutor
                          select l).ToList();

            if (libros.Count == 0)
            {
                return NotFound();
            }

            return Ok(libros);
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


    }
}
