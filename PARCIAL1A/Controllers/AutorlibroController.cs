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
    public class AutorlibroController : ControllerBase
    {
        private readonly Parcial1AContext _parcial1AContext;
        public AutorlibroController(Parcial1AContext parcial1AContext)
        {
            _parcial1AContext = parcial1AContext;
        }

        /// <sumary>
        /// EndPointRetorna listado de los Autorlibro exisentes
        /// </sumary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllAutorlibro")]
        public IActionResult GetAutorlibro()
        {
            List<Autorlibro> listadoAutorlibro = (from au in _parcial1AContext.Autorlibro
                                                  select au).ToList();

            if (listadoAutorlibro.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoAutorlibro);

        }



        [HttpPost]
        [Route("AddAutorlibro")]
        public IActionResult GuardarEquipo([FromBody] Autorlibro autorlibro)
        {
            try
            {
                _parcial1AContext.Autorlibro.Add(autorlibro);
                _parcial1AContext.SaveChanges();
                return Ok(autorlibro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar autorlibro/{id}")]
        public IActionResult Actualizarautorlibro(int id, [FromBody] Autorlibro AutorlibroModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual alteraremos alguna propiedad
            Autorlibro? AutorlibroActual = (from au in _parcial1AContext.Autorlibro
                                           where au.AutorId == id
                                           select au).FirstOrDefault();
            //Verificamos que exista el registro segun su ID
            if (AutorlibroActual == null)
            { return NotFound(); }
            //Si se encuentra el registro, se alteran los campos modificables
            AutorlibroActual.Orden = AutorlibroModificar.Orden;


            //Se marca el registro como modificado en el contexto //y se envia la modificacion a la base de datos
            _parcial1AContext.Entry(AutorlibroActual).State = EntityState.Modified;
            _parcial1AContext.SaveChanges();
            return Ok(AutorlibroModificar);

        }

        [HttpDelete]
        [Route("eliminaraurolibro/{id}")]
        public IActionResult Eliminarautorlibro(int id)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual eliminaremos
            Autorlibro? Autorlibro = (from e in _parcial1AContext.Autorlibro
                                where e.AutorId == id
                                select e).FirstOrDefault();
            //Verificamos que exista el registro segun su ID
            if (Autorlibro == null)
                return NotFound();
            //Ejecutamos la accion de elminar el registro _equiposContexto.equipos.Attach(equipo);
            _parcial1AContext.Autorlibro.Remove(Autorlibro);
            _parcial1AContext.SaveChanges();
            return Ok(Autorlibro);
        }
    }
}
