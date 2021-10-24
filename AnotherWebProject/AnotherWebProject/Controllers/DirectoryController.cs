using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            var list = persistence.GetEntries();
            Console.WriteLine($"list: {list.ToArray().Length}");
            foreach (var person in list.ToArray())
            {
                Console.WriteLine("Firstname: " + person.Firstname);
            }

            //var uriToGetRessource = BuildResponseUri(new Uri($"/api/entries", UriKind.Relative));
            //Created(uriToGetRessource, list.ToArray());
            return persistence.GetEntries();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<int> Delete(int id)
        {
            return await persistence.DeleteAsync(id);
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

