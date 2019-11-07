using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CoreVanillaJs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("permitir")]
    public class PersonaController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            using (Models.CrudVanillaJsContext db = new Models.CrudVanillaJsContext())
            {
                var lst = (from d in db.Persona
                           select d).ToList();
                return Ok(lst);
            }
        }

        [HttpPost]
        public ActionResult Insert([FromBody] Models.Request.PersonaRequest model)
        {
            using (Models.CrudVanillaJsContext db = new Models.CrudVanillaJsContext())
            {
                Models.Persona oPersona = new Models.Persona();
                oPersona.Nombre = model.Nombre;
                oPersona.Edad = model.Edad;
                db.Persona.Add(oPersona);
                db.SaveChanges();
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] Models.Request.PersonaEditRequest model)
        {
            using (Models.CrudVanillaJsContext db = new Models.CrudVanillaJsContext())
            {
                Models.Persona oPersona = db.Persona.Find(model.Id);
                oPersona.Nombre = model.Nombre;
                oPersona.Edad = model.Edad;
                db.Entry(oPersona).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] Models.Request.PersonaEditRequest model)
        {
            using (Models.CrudVanillaJsContext db = new Models.CrudVanillaJsContext())
            {                
                Models.Persona oPersona = db.Persona.Find(model.Id);
                db.Persona.Remove(oPersona);
                db.SaveChanges();
            }
            
            return Ok();
        }
    }
}
