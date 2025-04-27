using BMZSApi.Database;
using BMZSApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BMZSApi.Controllers
{
    public class KepekModel
    {
        public int FodraszId { get; set; }
        public string EleresiUt { get; set; }
        public string KepNev { get; set; }
        public string Leiras { get; set; }
    }
    public class KepekGet
    {
        public int FodraszId { get; set; }
        public string EleresiUt { get; set; }
        public string KepNev { get; set; }
        public string Leiras { get; set; }
    }
    public class KepekGetModel : KepekGet
    {
        public int Id { get; set; }
        public KepekGetModel(Kepek kepek)
        {
            Id = kepek.Id;
            FodraszId = kepek.FodraszId;
            EleresiUt = kepek.EleresiUt;
            KepNev = kepek.KepNev;
            Leiras = kepek.Leiras;
        }
    }
    public class KepekController : ApiController
    {
        // GET api/<controller>
        Account account = new Account("bmzsbarbershop", "186697288787767", "UehYb88mIPgymUu2yJckWsk-K2A");
        BMZSContext ctx = new BMZSContext();
        [HttpGet]
        [Route("api/Kepek")]
        [ResponseType(typeof(KepekGetModel))]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ctx.Kepek.ToList().Select(x => new KepekGetModel(x)));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Kepek{id}/1")]
        [ResponseType(typeof(KepekGetModel))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                KepekGetModel result = null;
                var kepek = ctx.Kepek.Where(x => x.FodraszId == id).FirstOrDefault();
                if (kepek != null)
                {
                    result = new KepekGetModel(kepek);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {id}");
        }

        [HttpGet]
        [Route("api/Kepek{id}/2")]
        [ResponseType(typeof(KepekGetModel))]
        public HttpResponseMessage GetKepek(int id)
        {
            try
            {
       
                var kepek = ctx.Kepek.Where(x => x.FodraszId == id).ToList();
                if (kepek != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, kepek);
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
        [Route("api/Kepek")]
        [ResponseType(typeof(KepekModel))]
        public HttpResponseMessage Post([FromBody] KepekModel kepek)
        {
            try
            {
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    Folder = "Kepek",
                    File = new FileDescription(kepek.EleresiUt),
                    DisplayName=kepek.KepNev
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                var result = ctx.Kepek.Add(new Kepek
                {
                    FodraszId = kepek.FodraszId,
                    EleresiUt = uploadResult.SecureUrl.ToString(),
                    KepNev = uploadParams.DisplayName,
                    Leiras = kepek.Leiras
                });
                ctx.SaveChanges();

                var getResult = new KepekGetModel(result);
                return Request.CreateResponse(HttpStatusCode.OK, getResult);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Kepek{id}")]
        [ResponseType(typeof(KepekModel))]
        public HttpResponseMessage Put(int id, [FromBody] KepekModel kepek)
        {
            try
            {
                var result = ctx.Kepek.Where(x => x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        Folder = "Kepek",
                        File = new FileDescription(kepek.EleresiUt)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);

                    result.FodraszId = kepek.FodraszId;
                    result.EleresiUt = uploadResult.SecureUrl.ToString();
                    result.KepNev = kepek.KepNev;
                    result.Leiras = kepek.Leiras;
                    ctx.SaveChanges();

                    var getResult = new KepekGetModel(result);
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
        [Route("api/Kepek{id}")]
        [ResponseType(typeof(KepekModel))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = ctx.Kepek.Where(x => x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    ctx.Kepek.Remove(result);
                    ctx.SaveChanges();

                    var getResult = new KepekGetModel(result);
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