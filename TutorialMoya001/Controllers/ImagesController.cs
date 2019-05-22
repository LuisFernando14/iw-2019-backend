using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutorialMoya001.Repositories;

namespace TutorialMoya001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IImagenesRepository ir;

        public ImagesController(IImagenesRepository ir)
        {
            this.ir = ir;
        }

        // POST: api/Images
        [HttpPost]
        public async Task<ActionResult<string>> Post(IFormFile image)
        {
            var url = "";
            if (image != null && image.Length > 0)
            {
                url = await ir.SaveImage(image.FileName, image.ContentType, image.OpenReadStream());
            }
            // await ir.
            return url;
            //return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }
        // GET: api/Images
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Images/5
        [HttpGet("{id}", Name = "GetImage")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT: api/Images/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /*
        [HttpPost]
        public async Task<ActionResult<string>> UploadImage(IFormFile image)
        {
            var url = "";
            if(image != null && image.Length > 0)
            {
                url = await ir.SaveImage(image.Name, image.OpenReadStream());
            }
            // await ir.
            return url;
            //return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }
        */
    }
}
