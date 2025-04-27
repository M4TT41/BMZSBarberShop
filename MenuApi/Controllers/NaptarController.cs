using BMZSApi.Database;
using BMZSApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BMZSApi.Controllers
{
    public class NaptarModel
    {
        public DateTime Datum { get; set; }
        public int FodraszId { get; set; }
    }
    public class NaptarGet
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public Fodrasz Fodrasz { get; set; }
        public int FodraszId { get; set; }
        public NaptarGet(Naptar Naptar)
        {
            Id = Naptar.Id;
            Datum = Naptar.Datum;
            Fodrasz = Naptar.Fodrasz;
            FodraszId = Naptar.FodraszId;
        }
    }
    public class NaptarController : ApiController
    {
        BMZSContext ctx = new BMZSContext();
        // GET api/<controller>
               
        [HttpGet]
        [Route("api/Naptar")]
        [ResponseType(typeof(NaptarGet))]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ctx.Naptar.Include(x => x.Fodrasz).ToList().Select(x => new NaptarGet(x)));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Naptar{id}")]
        [ResponseType(typeof(NaptarGet))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Naptar result = null;
                var naptar = ctx.Naptar
                .Include(x => x.Fodrasz)
                .Where(x => x.Id == id)
                .FirstOrDefault();
                if (naptar != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new NaptarGet(naptar));
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {id}");

        }

        [HttpGet]
        [Route("api/Naptar/GetDates/{id}")]
        [ResponseType(typeof(NaptarGet))]
        public HttpResponseMessage GetDates(int id)
        {
            try
            {
                Naptar result = null;
                var naptar = ctx.Naptar
                .Include(x => x.Fodrasz)
                .Where(x => x.FodraszId == id)
                .ToList();
                if (naptar != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, naptar.Select(x => new NaptarGet(x)));
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {id}");

        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Naptar/{name}/3")]
        [ResponseType(typeof(NaptarGet))]
        public HttpResponseMessage GetNaptarByFodrasz(string name)
        {                                       
            try
            {
                var naptar = ctx.Naptar
                .Include(x => x.Fodrasz)
                .Where(x => x.Fodrasz.Nev == name)
                .ToList();
                if (naptar != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, naptar.Select(x=> new NaptarGet(x)));
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {name}");

        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/Naptar")]
        [ResponseType(typeof(NaptarGet))]
        public HttpResponseMessage Post([FromBody] NaptarModel naptar)
        {
            try
            {
                var result = ctx.Naptar.Add(new Naptar { Datum=naptar.Datum, FodraszId=naptar.FodraszId});
                ctx.SaveChanges();

                var mentett = ctx.Naptar.Include(x => x.Fodrasz).FirstOrDefault(x => x.Id == result.Id);
                return Request.CreateResponse(HttpStatusCode.OK, new NaptarGet(mentett));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Naptar{id}")]
        [ResponseType(typeof(NaptarGet))]
        public HttpResponseMessage Put(int id, [FromBody] NaptarModel naptar)
        {
            try
            {
                var result = ctx.Naptar.Where(x => x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    result.Datum = naptar.Datum;
                    result.FodraszId = naptar.FodraszId;

                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new NaptarGet(result));
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
        [Route("api/Naptar{id}")]
        [ResponseType(typeof(Naptar))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = ctx.Naptar.Include(x=>x.Fodrasz).FirstOrDefault(x => x.Id == id);
                if (result != null)
                {
                    ctx.Naptar.Remove(result);
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, result);
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