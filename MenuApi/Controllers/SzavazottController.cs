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
    public class SzavazottModel
    {
        public int Csillag { get; set; }
        public int KepekId { get; set; }
        public int VasarloId { get; set; }
    }
    public class Szavazas
    {
        public int Csillag { get; set; }
    }
    public class SzavazottGetModel
    {
        public int Csillag { get; set; }
   
        public int KepekId { get; set; }
        public int VasarloId { get; set; }
        public SzavazottGetModel() { }
        public SzavazottGetModel(Szavazott szavazott)
        {
            Csillag = szavazott.Csillag;
            KepekId = szavazott.Kepek.Id;
            VasarloId = szavazott.Vasarlo.Id;
        }
    }
    public class SzavazottController : ApiController
    {
        BMZSContext ctx = new BMZSContext();
        // GET api/<controller>

        [HttpGet]
        [Route("api/Szavazott")]
        [ResponseType(typeof(SzavazottModel))]

        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ctx.Szavazott.Include(x => x.Kepek).Include(x => x.Vasarlo).ToList().Select(x => new SzavazottGetModel(x)));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Szavazott{kepid}/{vasarloid}")]
        [ResponseType(typeof(SzavazottModel))]
        public HttpResponseMessage Get(int kepid, int vasarloid)
        {
            try
            {
                var result = ctx.Szavazott.Include(x => x.Kepek).Include(x => x.Vasarlo).FirstOrDefault(x => x.KepekId == kepid && x.VasarloId == vasarloid);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new SzavazottGetModel(result));
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {kepid}, {vasarloid}");
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/Szavazott")]
        [ResponseType(typeof(SzavazottModel))]
        public HttpResponseMessage Post([FromBody] SzavazottModel szavaz)
        {
            try
            {
                var result = ctx.Szavazott.Add(new Szavazott
                {
                    Csillag = szavaz.Csillag,
                    KepekId = szavaz.KepekId,
                    VasarloId = szavaz.VasarloId
                });
                ctx.SaveChanges();

                var mentett = ctx.Szavazott
                    .Include(x => x.Kepek)
                    .Include(x => x.Vasarlo)
                    .FirstOrDefault(x => x.KepekId == szavaz.KepekId && x.VasarloId == szavaz.VasarloId);

                return Request.CreateResponse(HttpStatusCode.OK, new SzavazottGetModel(mentett));
            }
            catch (Exception e)
            {
                Exception inner = e;
                while (inner.InnerException != null)
                {
                    inner = inner.InnerException; // Végigmegyünk az összes belső kivételen
                }
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, inner.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Szavazott")]
        [ResponseType(typeof(SzavazottModel))]
        public HttpResponseMessage Put(int kepid, int vasarloid, [FromBody] Szavazas szavaz)
        {
            try
            {
                var result = ctx.Szavazott.Include(x => x.Kepek).Include(x => x.Vasarlo).FirstOrDefault(x => x.KepekId == kepid && x.VasarloId == vasarloid);
                if (result != null)
                {
                    result.Csillag = szavaz.Csillag;
                    result.KepekId = kepid;
                    result.VasarloId = vasarloid;

                    ctx.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new SzavazottGetModel(result));
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {kepid}, {vasarloid}");
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/Szavazott")]
        [ResponseType(typeof(SzavazottModel))]
        public HttpResponseMessage Delete(int vasarloid, int kepid)
        {
            try
            {
                var result = ctx.Szavazott.Include(x => x.Kepek).Include(x => x.Vasarlo).FirstOrDefault(x => x.KepekId == kepid && x.VasarloId == vasarloid);
                if (result != null)
                {
                    ctx.Szavazott.Remove(result);
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new SzavazottGetModel(result));
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {kepid}, {vasarloid}");
        }

        [HttpPut]
        [Route("api/Szavazott/Szavazas/{kepid}/{vasarloid}")]
        [ResponseType(typeof(SzavazottModel))]
        public HttpResponseMessage Szavazas(int kepid, int vasarloid, [FromBody] Szavazas szavaz)
        {
            try
            {
                var result = ctx.Szavazott.Include(x => x.Kepek).Include(x => x.Vasarlo).FirstOrDefault(x => x.KepekId == kepid && x.VasarloId == vasarloid);
                if (result != null)
                {
                    result.Csillag = szavaz.Csillag;
                    result.KepekId = result.KepekId;
                    result.VasarloId = result.VasarloId;

                    ctx.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, result.Csillag);
                }
                else
                {
                    result = ctx.Szavazott.Add(new Szavazott
                    {
                        Csillag = szavaz.Csillag,
                        KepekId = kepid,
                        VasarloId = vasarloid
                    });
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.Created, result.Csillag);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}