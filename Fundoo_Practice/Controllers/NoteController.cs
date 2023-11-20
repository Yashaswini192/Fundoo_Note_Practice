using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Fundoo_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness noteBusiness;


        public NoteController(INoteBusiness noteBusiness)
        {
            this.noteBusiness = noteBusiness;
        }

        [HttpPost]
        [Route("Create")]

        public IActionResult CreateNote(NoteModel model)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = noteBusiness.CreateNote(model, userId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "SuccessFully Created Note", data = result });
                }
                else
                {
                    return BadRequest(new { success = true, message = "UnsuccessFull" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult UpdateNote(NoteModel model, int NoteId, int userId)
        {
            try
            {
                //int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);

                var result = noteBusiness.UpdateNote(model, NoteId, userId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Updated Note SuccessFully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to update Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("Retreive")]

        public IActionResult RetreiveNote(int NoteId)
        {
            try
            {
                var result = noteBusiness.RetreiveNote(NoteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Retreived Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Couldnot find noteid" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
