using BMZSApi.Database;
using BMZSApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BMZSApi.Controllers
{
    public class FodraszModel
    {
        public string Nev { get; set; }
        public string TelefonSzam { get; set; }
        public string Leiras { get; set; }
        public string PfpName { get; set; }
        public int FodraszatId { get; set; }
    }
    public class FodraszGetModel : FodraszModel
    {
        [Required]
        public int Id { get; set; }
        public FodraszGetModel(Fodrasz fodrasz)
        {
            Id = fodrasz.Id;
            Nev = fodrasz.Nev;
            TelefonSzam = fodrasz.TelefonSzam;
            PfpName = fodrasz.PfpName;
            Leiras = fodrasz.Leiras;
            FodraszatId = fodrasz.FodraszatId;
        }
    }

    public class FodraszController : ApiController
    {
        Account account = new Account("bmzsbarbershop", "186697288787767", "UehYb88mIPgymUu2yJckWsk-K2A");
        BMZSContext ctx = new BMZSContext();
        // GET api/<controller>
        [HttpGet]
        [Route("api/Fodrasz/1")]
        [ResponseType(typeof(FodraszGetModel))]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ctx.Fodraszok.Include(x => x.Fodraszat).ToList().Select(x => new FodraszGetModel(x)));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Fodrasz/{id}/1")]
        [ResponseType(typeof(FodraszGetModel))]
        public HttpResponseMessage GetFodrasz(int id)
        {
            try
            {
                FodraszGetModel result = null;
                var fodrasz = ctx.Fodraszok.Include(x => x.Fodraszat).Where(x => x.Id == id).FirstOrDefault();
                if (fodrasz != null)
                {
                    result = new FodraszGetModel(fodrasz);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
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
        [Route("api/Fodrasz/GetFodraszByFodraszat/{id}/1")]
        [ResponseType(typeof(FodraszGetModel))]
        public HttpResponseMessage GetFodraszByFodraszat(int id)
        {
            try
            {

                var fodrasz = ctx.Fodraszok.Include(x => x.Fodraszat).Where(x => x.FodraszatId == id).ToList();
                if (fodrasz != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, fodrasz);
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
        [Route("api/Fodrasz/1")]
        [ResponseType(typeof(FodraszModel))]
        public HttpResponseMessage Post([FromBody] FodraszModel fodrasz)
        {
            try
            {
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    Folder = "FodraszPfp",
                    File = new FileDescription(fodrasz.PfpName)
                };
                var uploadResult = cloudinary.Upload(uploadParams);

                var res = ctx.Fodraszok.Add(new Fodrasz
                {
                    Nev = fodrasz.Nev,
                    TelefonSzam = fodrasz.TelefonSzam,
                    Leiras = fodrasz.Leiras,
                    PfpName= uploadResult.SecureUrl.ToString(),
                    FodraszatId = fodrasz.FodraszatId
                });
                ctx.SaveChanges();

                var getResult = new FodraszGetModel(res);
                return Request.CreateResponse(HttpStatusCode.OK, getResult);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Fodrasz{id}/1")]
        [ResponseType(typeof(FodraszModel))]
        public HttpResponseMessage Put(int id, [FromBody] FodraszModel fodrasz)
        {
            try
            {
                var result = ctx.Fodraszok.Where(x => x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        Folder = "FodraszPfp",
                        File = new FileDescription(fodrasz.PfpName)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);

                    result.Nev = fodrasz.Nev;
                    result.TelefonSzam = fodrasz.TelefonSzam;
                    result.Leiras = fodrasz.Leiras;
                    result.PfpName = uploadResult.SecureUrl.ToString();
                    result.FodraszatId = fodrasz.FodraszatId;
                    ctx.SaveChanges();

                    var getResult = new FodraszGetModel(result);
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
        [Route("api/Fodrasz{id}/1")]
        [ResponseType(typeof(FodraszGetModel))]
        public HttpResponseMessage Delete(int id)
        {
            var result = ctx.Fodraszok.Where(x => x.Id == id).FirstOrDefault();
            try
            {
                if (result != null)
                {
                    ctx.Fodraszok.Remove(result);
                    ctx.SaveChanges();

                    var getResult = new FodraszGetModel(result);
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
