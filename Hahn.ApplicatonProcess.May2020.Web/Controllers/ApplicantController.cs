using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Data;
using Hahn.ApplicatonProcess.May2020.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicatonProcess.May2020.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(Startup.ApiVersion.ToString())]
    public class ApplicantController : ControllerBase
    {
        private readonly ILogger<ApplicantController> _logger;
        private readonly IApplicantService service;

        public ApplicantController(ILogger<ApplicantController> logger, IApplicantService service)
        {
            _logger = logger;
            this.service = service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                var obj = service.Get(id);
                return Ok(obj);
            }
            catch (ApplicantMissingExcpetion ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //If the parsing of applicant fails, the method is not called
        public IActionResult Post([FromBody] Applicant applicant)
        {
            try
            {
                service.Create(applicant);
                var applicantID = applicant.ID;
                string url = ComposeUrl(applicantID);
                return Created(url, applicant);
            }
            catch (ValidationFailedException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (ApplicantAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Applicant applicant)
        {
            try
            {
                service.Update(applicant);
                return StatusCode(StatusCodes.Status200OK, applicant);
            }

            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicantMissingExcpetion ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                service.Delete(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (ApplicantMissingExcpetion ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        private string ComposeUrl(int id)
        {
            //TODO:
            //The Url.RouteUrl is not working and spent some time firguring it
            //out and left it. https://github.com/dotnet/aspnetcore/issues/14877
            //This fails in both Integration testing and production. For now
            //composing the URL from hand. Possible issues might be versioning
            //localization if added. 
            if (this.Request == null)
                return
                    $"https://{Environment.MachineName}/{nameof(Applicant)}/{id}";
            else
                return
                    $"https://{Request.Host.Value}{Request.Path}/{id}";
        }
    }
}
