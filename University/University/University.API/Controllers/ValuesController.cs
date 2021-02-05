using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace University.API.Controllers
{
    public class ValuesController : ApiController
    {
        public class Input
        {

        }
        [HttpGet]//get para consultas = parametros por url
        public IHttpActionResult Get() 
        {
            //return StatusCode(HttpStatusCode.OK);
            return Ok();
        }
        [HttpPost]//post para insetar = enviar datos
        public IHttpActionResult Post(Input input) 
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok();//json
        }
        [HttpPut]//put para Modificar
        public IHttpActionResult Put(Input input) 
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok();//json
        }
        [HttpDelete]//delete para eliminar
        public IHttpActionResult Delete(int? id)
        {
            if (id != null)
                return NotFound();

            return Ok();
        }
    }
}
