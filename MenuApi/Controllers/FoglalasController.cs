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
    public class FoglalasPostPutModel
    {
        public int SzolgaltatasId { get; set; }
        public int VasarloId { get; set; }
        public int NaptarId { get; set; }
    }
    public class FoglalasModel
    {
        public int SzolgaltatasId { get; set; }
        public int VasarloId { get; set; }
        public int NaptarId { get; set; }
    }
    public class FoglalasGetModel : FoglalasModel
    {
        public int Id { get; set; }
        public FoglalasGetModel(Foglalas foglalas)
        {
            Id = foglalas.Id;
            SzolgaltatasId = foglalas.SzolgaltatasId;
            VasarloId = foglalas.VasarloId;
            NaptarId = foglalas.NaptarId;
        }
    }
    public class FoglalasController : ApiController
    {
        // GET api/<controller>
        BMZSContext ctx = new BMZSContext();
        [HttpGet]
        [Route("api/Foglalas")]
        [ResponseType(typeof(FoglalasGetModel))]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ctx.Foglalasok.Include(x => x.Naptar).Include(x => x.Szolgaltatas).Include(x => x.Vasarlo)
                    .ToList()
                    .Select(x => new FoglalasGetModel(x)));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Foglalas{id}")]
        [ResponseType(typeof(FoglalasGetModel))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var foglalas = ctx.Foglalasok.Include(x => x.Naptar).Include(x => x.Szolgaltatas).Include(x => x.Vasarlo)
                    .FirstOrDefault(x => x.Id == id);
                if (foglalas != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new FoglalasGetModel(foglalas));
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
        [Route("api/Foglalas/{VasarloId}")]
        [ResponseType(typeof(FoglalasGetModel))]
        public HttpResponseMessage GetByVasarloId(int vasarloId)
        {
            try
            {
                var foglalas = ctx.Foglalasok.Include(x => x.Naptar).Include(x => x.Szolgaltatas).Include(x => x.Vasarlo).Where(x => x.VasarloId == vasarloId).ToList();
                if (foglalas != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, foglalas.Select(x => new FoglalasGetModel(x)));
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {vasarloId}");
        }


        // POST api/<controller>
        [HttpPost]
        [Route("api/Foglalas")]
        [ResponseType(typeof(FoglalasGetModel))]
        public HttpResponseMessage Post([FromBody] FoglalasPostPutModel foglalas)
        {
            try
            {
                var result = ctx.Foglalasok.Add(new Foglalas
                {
                    SzolgaltatasId = foglalas.SzolgaltatasId,
                    NaptarId=foglalas.NaptarId,
                    VasarloId=foglalas.VasarloId
                });
                ctx.SaveChanges();

                var getResult = ctx.Foglalasok.Include(x => x.Naptar).Include(x => x.Szolgaltatas).Include(x => x.Vasarlo)
                    .FirstOrDefault(x => x.Id == result.Id); ;
                return Request.CreateResponse(HttpStatusCode.OK, new FoglalasGetModel(result));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Foglalas{id}")]
        [ResponseType(typeof(FoglalasGetModel))]
        public HttpResponseMessage Put(int id, [FromBody] FoglalasPostPutModel foglalas)
        {
            try
            {
                var result = ctx.Foglalasok.FirstOrDefault(x => x.Id == id);

                if (result != null)
                {
                    var szid = ctx.Szolgaltatasok.Where(x => x.Id == foglalas.SzolgaltatasId).FirstOrDefault();

                    result.SzolgaltatasId = foglalas.SzolgaltatasId;
                    result.VasarloId = foglalas.VasarloId;
                    result.NaptarId = foglalas.NaptarId;
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new FoglalasGetModel(result)); ;
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
        [Route("api/Foglalas{id}")]
        [ResponseType(typeof(FoglalasGetModel))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = ctx.Foglalasok.Include(x => x.Naptar).Include(x => x.Szolgaltatas).Include(x => x.Vasarlo)
                    .FirstOrDefault(x => x.Id == id);
                if (result != null)
                {
                    ctx.Foglalasok.Remove(result);
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new FoglalasGetModel(result));
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