using BMZSApi.Database;
using BMZSApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BMZSApi.Controllers
{
    public class VarosModel
    {
        public int Iranyitoszam { get; set; }
        public string TelepulesNev { get; set; }
    }

    public class VarosGetModel
    {
        public int Id { get; set; }
        public int Iranyitoszam { get; set; }
        public string TelepulesNev { get; set; }
    }
    public class VarosController : ApiController
    {
        BMZSContext ctx = new BMZSContext();
        // GET api/<controller>
        [HttpGet]
        [Route("api/Varos")]
        [ResponseType(typeof(VarosGetModel))]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ctx.Varosok.ToList());
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Varos/{id}")]
        [ResponseType(typeof(VarosGetModel))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var result = ctx.Varosok.Where(x => x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {id}");
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/Varos")]
        [ResponseType(typeof(VarosModel))]
        public HttpResponseMessage Post([FromBody] VarosModel varos)
        {
            try
            {
                var result = ctx.Varosok.Add(new Varos { IranyitoSzam = varos.Iranyitoszam, TelepulesNev = varos.TelepulesNev });
                ctx.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Varos{id}")]
        [ResponseType(typeof(VarosModel))]
        public HttpResponseMessage Put(int id, [FromBody] VarosModel varos)
        {
            try
            {
                Varos res = ctx.Varosok.Where(x => x.Id == id).FirstOrDefault();

                if (res != null)
                {
                    res.IranyitoSzam = varos.Iranyitoszam;
                    res.TelepulesNev = varos.TelepulesNev;
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {id}");
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/Varos{id}")]
        [ResponseType(typeof(VarosGetModel))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Varos res = ctx.Varosok.Where(x => x.Id == id).FirstOrDefault();

                if (res != null)
                {
                    ctx.Varosok.Remove(res);
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {id}");
        }
    }
}