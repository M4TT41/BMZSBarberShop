using BMZSApi.Database;
using BMZSApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMZSApi.Controllers
{
    public class BeosztasPostPut
    {
        public int FodraszId { get; set; }
        public DateTime Datum { get; set; }
        public string NapNeve { get; set; }
        public TimeSpan Kezdes { get; set; }
        public TimeSpan Vege { get; set; }
    }
    public class BeosztasGetModel
    {
        public int Id { get; set; }
        public int FodraszId { get; set; }
        public DateTime Datum { get; set; }
        public string NapNeve { get; set; }
        public TimeSpan Kezdes { get; set; }
        public TimeSpan Vege { get; set; }

        public BeosztasGetModel(Beosztas value)
        {
            Id = value.Id;
            FodraszId = value.FodraszId;
            Datum = value.Datum;
            NapNeve = value.NapNeve;
            Kezdes = value.Kezdes;
            Vege = value.Vege;
        }
    }
    public class BeosztasController : ApiController
    {
        // GET api/<controller>
        BMZSContext ctx = new BMZSContext();
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ctx.Beosztasok.Include(x => x.Fodrasz).ToList().Select(x => new BeosztasGetModel(x)));
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/beosztas/fodrasz/{id}")]
        public HttpResponseMessage GetByFodraszId(int id)
        {
            try
            {
                var result = ctx.Beosztasok.Where(x => x.FodraszId == id).Include(x => x.Fodrasz).ToList().Select(x => new BeosztasGetModel(x));
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] BeosztasPostPut value)
        {
            try
            {
                if (value != null)
                {
                    var result = ctx.Beosztasok.Add(new Beosztas
                    {
                        FodraszId = value.FodraszId,
                        NapNeve = (Enum.Parse(typeof(HetNapja), value.NapNeve)).ToString(),
                        Datum = value.Datum,
                        Kezdes = value.Kezdes,
                        Vege = value.Vege
                    });
                    ctx.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, new BeosztasGetModel(result));
                }
                else return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }



        // DELETE api/<controller>/5

        [HttpDelete]
        [Route("api/Beosztas{id}/")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var value = ctx.Beosztasok.Where(x => x.Id == id).FirstOrDefault();
                if (value != null)
                {
                    var result = ctx.Beosztasok.Remove(value);
                    ctx.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new BeosztasGetModel(result));
                }
                else return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Beosztas{id}/")]
        public HttpResponseMessage Put(int id, [FromBody] BeosztasPostPut beosztas)
        {
            try
            {
                var result = ctx.Beosztasok.Where(x => x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    result.FodraszId = beosztas.FodraszId;
                    result.Datum = beosztas.Datum;
                    result.NapNeve = (Enum.Parse(typeof(HetNapja), beosztas.NapNeve)).ToString();
                    result.Kezdes = beosztas.Kezdes;
                    result.Vege = beosztas.Vege;
                    ctx.SaveChanges();

                    var getResult = new BeosztasGetModel(result);
                    return Request.CreateResponse(HttpStatusCode.OK, getResult);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {id}");
        }
    }
}