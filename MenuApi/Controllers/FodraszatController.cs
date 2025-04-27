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
    public class FodraszatValidationModel
    {
        public int Id { get; set; }
        public string FodraszatAzon { get; set; }
    }
    public class FodraszatModel
    {
        public int VarosId { get; set; }
        public string Utca { get; set; }
        public int HazSzam { get; set; }
    }
    public class FodraszatPostModel
    {
        public int VarosId { get; set; }
        public string Utca { get; set; }
        public int HazSzam { get; set; }
        public string FodraszatAzon { get; set; }
    }
    public class FodraszatGetModel : FodraszatModel
    {
        public int Id { get; set; }
        public FodraszatGetModel(Fodraszat fodraszat)
        {
            Id = fodraszat.Id;
            VarosId = fodraszat.Varos.Id;
            Utca = fodraszat.Utca;
            HazSzam = fodraszat.HazSzam;

        }
    }

    public class FodraszatController : ApiController
    {
        BMZSContext ctx = new BMZSContext();
        // GET api/<controller>
        [HttpGet]
        [Route("api/Fodraszat/0")]
        [ResponseType(typeof(FodraszatGetModel))]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ctx.Fodraszatok.Include(x => x.Varos).ToList().Select(x => new FodraszatGetModel(x)));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Fodraszat/{id}")]
        [ResponseType(typeof(FodraszatGetModel))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                FodraszatGetModel result = null;
                var fodraszat = ctx.Fodraszatok
                .Include(x => x.Varos)
                .Where(x => x.Id == id)
                .FirstOrDefault();
                if (fodraszat != null)
                {
                    result = new FodraszatGetModel(fodraszat);
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
        [Route("api/Fodraszat/0")]
        [ResponseType(typeof(FodraszatModel))]
        public HttpResponseMessage Post([FromBody] FodraszatPostModel fodraszat)
        {
            try
            {
                var getvaros = ctx.Varosok.Where(x => x.Id == fodraszat.VarosId).FirstOrDefault();
                PasswordHasher.CreatePasswordHash(fodraszat.FodraszatAzon, out byte[] passwordHash, out byte[] passwordSalt);
                var result = ctx.Fodraszatok.Add(new Fodraszat
                {
                    VarosId = getvaros.Id,
                    Varos = getvaros,
                    Utca = fodraszat.Utca,
                    HazSzam = fodraszat.HazSzam,
                    FodraszatAzonHash = passwordHash,
                    FodraszatAzonSalt = passwordSalt
                });
                ctx.SaveChanges();

                var getResult = new FodraszatGetModel(result);
                return Request.CreateResponse(HttpStatusCode.OK, getResult);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("api/Fodraszat/authenticate")]
        [ResponseType(typeof(FodraszatGetModel))]
        public HttpResponseMessage Authenticate([FromBody] FodraszatValidationModel value)
        {
            var result = ctx.Fodraszatok.FirstOrDefault(x => x.Id == value.Id);
            if (result != null)
            {
                var valid = PasswordHasher.VerifyPasswordHash
                    (value.FodraszatAzon, result.FodraszatAzonHash, result.FodraszatAzonSalt);
                var response = new FodraszatGetModel(result);

                if (valid)
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                else
                    return Request.CreateResponse
                        (HttpStatusCode.Unauthorized, response);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Fodraszat{id}/0")]
        [ResponseType(typeof(FodraszatModel))]
        public HttpResponseMessage Put(int id, [FromBody] FodraszatPostModel fodraszat)
        {
            try
            {
                var result = ctx.Fodraszatok.Where(x => x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    PasswordHasher.CreatePasswordHash(fodraszat.FodraszatAzon, out byte[] passwordHash, out byte[] passwordSalt);
                    result.VarosId = fodraszat.VarosId;
                    result.Utca = fodraszat.Utca;
                    result.HazSzam = fodraszat.HazSzam;
                    result.FodraszatAzonHash = passwordHash;
                    result.FodraszatAzonSalt = passwordSalt;
                    ctx.SaveChanges();

                    var getResult = new FodraszatGetModel(result);
                    return Request.CreateResponse(HttpStatusCode.OK, getResult);
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
        [Route("api/Fodraszat{id}/0")]
        [ResponseType(typeof(FodraszatGetModel))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = ctx.Fodraszatok.Where(x => x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    ctx.Fodraszatok.Remove(result);
                    ctx.SaveChanges();

                    var getResult = new FodraszatGetModel(result);
                    return Request.CreateResponse(HttpStatusCode.OK, getResult);
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