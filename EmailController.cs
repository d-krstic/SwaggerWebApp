using Microsoft.AspNetCore.Mvc;
using SwaggerWebApp.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerWebApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        IEmailService _emailService;

        public EmailController(IEmailService emailService) {
            _emailService = emailService;
        }

        // GET: api/Email
        [HttpGet]
        public List<Email> Get()
        {
            return _emailService.Gets();
        }

        // GET: api/Email/[mail]
        [HttpGet("{mail}", Name = "Get")]
        public Email Get(string m)
        {
            return _emailService.Get(m);
        }

        // POST: api/Email
        [HttpPost]
        [Produces("application/json")]
        public List<Email> Post([FromBody] Email e)
        {
            return _emailService.Save(e);
        }

        // DELETE: api/Email/5
        [HttpDelete("{id}")]
        public List<Email> Delete(int id)
        {
            return _emailService.Delete(id);
        }

        // GET: api/Email
        [HttpGet("save")]
        public List<Email> Update()
        {
            return _emailService.Update();
        }
    }
}
