using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AnotherWebProject.Models;
using AnotherWebProject.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnotherWebProject.Controllers
{
    [Route("api/entries")]
    [ApiController]
    public class DirectoryController : ControllerBase
    {
        private readonly IPersistence persistence;

        public DirectoryController(IPersistence persistence)
        {
            this.persistence = persistence;
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Create(Person person)
        {
            var idOfCreatedPerson = await this.persistence.SaveAsync(person);

            var uriToCreatedRessource = this.BuildResponseUri(new Uri($"/api/entries/{idOfCreatedPerson}", UriKind.Relative));

            return Created(uriToCreatedRessource, person);
        }

        private Uri BuildResponseUri(Uri path)
        {
            int port;

            if (this.Request.IsHttps)
            {
                port = this.Request.Host.Port ?? 443;
            }
            else
            {
                port = this.Request.Host.Port ?? 80;
            }

            return new UriBuilder(this.Request.Scheme, this.Request.Host.Host, port, path.ToString()).Uri;
        }
    }
}

