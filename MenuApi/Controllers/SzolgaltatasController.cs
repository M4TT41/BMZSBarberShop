using BMZSApi.Database;
using BMZSApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BMZSApi.Controllers
{
    public class SzolgaltatasModel
    {
        public string SzolgaltatasNev { get; set; }
        public int Idotartalom { get; set; }
        public int Ar { get; set; }
    }

    public class SzolgaltatasGetModel
    {
        public int Id { get; set; }
        public string SzolgaltatasNev { get; set; }
        public int Idotartalom { get; set; }
        public int Ar { get; set; }
        public SzolgaltatasGetModel(Szolgaltatas szolgaltatas)
        {
            Id = szolgaltatas.Id;
            SzolgaltatasNev = szolgaltatas.SzolgaltatasNev;
            Idotartalom = szolgaltatas.Idotartalom;
            Ar = szolgaltatas.Ar;
        }
    }
    public class SzolgaltatasController : ApiController
    {
        // GET api/<controller>
        BMZSContext ctx = new BMZSContext();
        [HttpGet]
        [Route("api/Szolgaltatas")]
        [ResponseType(typeof(SzolgaltatasGetModel))]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ctx.Szolgaltatasok.ToList().Select(x=>new SzolgaltatasGetModel(x)));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Szolgaltatas/{id}")]
        [ResponseType(typeof(SzolgaltatasGetModel))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var result = ctx.Szolgaltatasok.FirstOrDefault(x => x.Id == id);
                if (result!=null) return Request.CreateResponse(HttpStatusCode.OK, new SzolgaltatasGetModel(result));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {id}");
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/Szolgaltatas")]
        [ResponseType(typeof(SzolgaltatasGetModel))]
        public HttpResponseMessage Post([FromBody] SzolgaltatasModel szolgaltatas)
        {
            try
            {
                var result = ctx.Szolgaltatasok.Add(new Szolgaltatas { SzolgaltatasNev = szolgaltatas.SzolgaltatasNev, Idotartalom = szolgaltatas.Idotartalom, Ar = szolgaltatas.Ar });
                ctx.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new SzolgaltatasGetModel(result));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Szolgaltatas{id}")]
        [ResponseType(typeof(SzolgaltatasGetModel))]
        public HttpResponseMessage Put(int id, [FromBody] SzolgaltatasModel szolgaltatas)
        {
            try
            {
                Szolgaltatas res = ctx.Szolgaltatasok.Where(x => x.Id == id).FirstOrDefault();
                if (res != null)
                {
                    res.SzolgaltatasNev = szolgaltatas.SzolgaltatasNev;
                    res.Idotartalom = szolgaltatas.Idotartalom;
                    res.Ar = szolgaltatas.Ar;
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new SzolgaltatasGetModel(res));
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
        [Route("api/Szolgaltatas{id}")]
        [ResponseType(typeof(SzolgaltatasGetModel))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var res = ctx.Szolgaltatasok.Where(x => x.Id == id).FirstOrDefault();
                if (res != null)
                {
                    ctx.Szolgaltatasok.Remove(res);
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new SzolgaltatasGetModel(res));
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