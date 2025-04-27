using BMZSApi.Database;
using BMZSApi.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;

namespace BMZSApi.Controllers
{
    public class VasarloModel
    {
        public string FelhasznaloNev { get; set; }
        public string Nev { get; set; }
        public string TelefonSzam { get; set; }
        public string EMail { get; set; }
        public string ProfilePic { get; set; }
        public string Jelszo { get; set; }
    }
    public class VasarloGetModel : VasarloModel
    {
        public int Id { get; set; }

        public VasarloGetModel(Vasarlo vasarlo)
        {
            Id = vasarlo.Id;
            FelhasznaloNev = vasarlo.FelhasznaloNev;
            Nev = vasarlo.Nev;
            ProfilePic = vasarlo.ProfilePic;
            TelefonSzam = vasarlo.TelefonSzam;
            EMail = vasarlo.EMail;
        }
    }
    public class VasarloUpdateModel
    {
        public string FelhasznaloNev { get; set; }
        public string Nev { get; set; }
        public string TelefonSzam { get; set; }
        public string EMail { get; set; }
        public string ProfilePic { get; set; }
        public string OldPasswd { get; set; }
        public string NewPasswd { get; set; }
    }
    public class ValidationModel
    {
        public string EMail { get; set; }
        public string Jelszo { get; set; }
    }
    public class VasarloController : ApiController
    {
        BMZSContext ctx = new BMZSContext();
        Account account = new Account("bmzsbarbershop", "186697288787767", "UehYb88mIPgymUu2yJckWsk-K2A");
        // GET api/<controller>
        [HttpGet]
        [Route("api/Vasarlo")]
        [ResponseType(typeof(VasarloGetModel))]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ctx.Vasarlok.ToList().Select(x => new VasarloGetModel(x)));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Vasarlo/{id}")]
        [ResponseType(typeof(VasarloGetModel))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var result = ctx.Vasarlok.FirstOrDefault(x => x.Id == id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new VasarloGetModel(result));
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
        [Route("api/Vasarlo")]
        [ResponseType(typeof(VasarloGetModel))]
        public HttpResponseMessage Post([FromBody] VasarloModel vasarlo)
        {
        
                var email = ctx.Vasarlok.FirstOrDefault(x => x.EMail == vasarlo.EMail);
                if (email != null)
                    return Request.CreateResponse(HttpStatusCode.Conflict, "EMAIL_EXISTS");
                byte[] passwordHash, passwordSalt;
                PasswordHasher.CreatePasswordHash(vasarlo.Jelszo, out passwordHash, out passwordSalt);



            var input = ctx.Vasarlok.Add(new Vasarlo
            {
                FelhasznaloNev = vasarlo.FelhasznaloNev,
                Nev = vasarlo.Nev,
                TelefonSzam = vasarlo.TelefonSzam,
                EMail = vasarlo.EMail,
                ProfilePic = "",
                JelszoHash = passwordHash,
                JelszoSalt = passwordSalt
            }); ;
                ctx.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new VasarloGetModel(input));
        
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Vasarlo{id}")]
        [ResponseType(typeof(VasarloGetModel))]
        public HttpResponseMessage Put(int id, [FromBody] VasarloModel vasarlo)
        {
            try
            {
                var result = ctx.Vasarlok.FirstOrDefault(x => x.Id == id);
                byte[] passwordHash, passwordSalt;
                if (result != null)
                {
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        Folder = "UserProfilePic",
                        File = new FileDescription(vasarlo.ProfilePic)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);

                    PasswordHasher.CreatePasswordHash(vasarlo.Jelszo, out passwordHash, out passwordSalt);
                    result.FelhasznaloNev = vasarlo.FelhasznaloNev;
                    result.Nev = vasarlo.Nev;
                    result.TelefonSzam = vasarlo.TelefonSzam;
                    result.EMail = vasarlo.EMail;
                    result.ProfilePic = uploadResult.SecureUrl.ToString();
                    result.JelszoHash = passwordHash;
                    result.JelszoSalt = passwordSalt;
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new VasarloGetModel(result));
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
        [Route("api/Vasarlo{id}")]
        [ResponseType(typeof(VasarloGetModel))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = ctx.Vasarlok.FirstOrDefault(x => x.Id == id);
                if (result != null)
                {
                    ctx.Vasarlok.Remove(result);
                    ctx.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new VasarloGetModel(result));
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Index nem található! Index: {id}");
        }

        [HttpPost]
        [Route("api/Vasarlo/authenticate")]
        [ResponseType(typeof(VasarloGetModel))]
        public HttpResponseMessage Authenticate([FromBody] ValidationModel value)
        {
            var result = ctx.Vasarlok.FirstOrDefault(x => x.EMail == value.EMail);
            if (result != null)
            {
                var valid = PasswordHasher.VerifyPasswordHash
                    (value.Jelszo, result.JelszoHash, result.JelszoSalt);
                var response = new VasarloGetModel(result);

                if (valid)
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                else
                    return Request.CreateResponse
                        (HttpStatusCode.Unauthorized, response);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("api/Vasarlo/Patch/id")]
        [ResponseType(typeof(VasarloGetModel))]
        public HttpResponseMessage Patch(int id, [FromBody] VasarloUpdateModel value)
        {
            try
            {
                var result = ctx.Vasarlok.FirstOrDefault(x => x.Id == id);
                if (result == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (!string.IsNullOrEmpty(value.NewPasswd))
                {
                    if (string.IsNullOrEmpty(value.OldPasswd))
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Missing current password.");
                    }

                    var valid = PasswordHasher.VerifyPasswordHash(value.OldPasswd, result.JelszoHash, result.JelszoSalt);
                    if (!valid)
                        return Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

                if (value.EMail != null && value.EMail != result.EMail)
                {
                    var email = ctx.Vasarlok.FirstOrDefault(x => x.EMail == value.EMail);
                    if (email != null)
                        return Request.CreateResponse(HttpStatusCode.Conflict, "EMAIL_EXISTS");
                }

                if (value.Nev != null)
                    result.Nev = value.Nev;
                if (value.FelhasznaloNev != null)
                    result.FelhasznaloNev = value.FelhasznaloNev;
                if (value.TelefonSzam != null)
                    result.TelefonSzam = value.TelefonSzam;
                if (value.EMail != null)
                    result.EMail = value.EMail;

                if (!string.IsNullOrEmpty(value.NewPasswd))
                {
                    PasswordHasher.CreatePasswordHash(value.NewPasswd, out byte[] hash, out byte[] salt);
                    result.JelszoHash = hash;
                    result.JelszoSalt = salt;
                }

                if (value.ProfilePic != null)
                {

                    result.ProfilePic = value.ProfilePic;
                }

                var response = new VasarloGetModel(result);

                ctx.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        }
    }
}