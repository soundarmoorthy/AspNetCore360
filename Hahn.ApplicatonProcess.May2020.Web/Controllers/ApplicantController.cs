using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Data;
using Hahn.ApplicatonProcess.May2020.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicatonProcess.May2020.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly ILogger<ApplicantController> _logger;
        private readonly IApplicantService service;

        public ApplicantController(ILogger<ApplicantController> logger
            , IApplicantService service)
        {
            _logger = logger;
            this.service = service;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Gets the applicant",
            typeof(Applicant))]
        [SwaggerResponse(StatusCodes.Status404NotFound,
            "The applicant for the specified ID is invalid (or) does not exist")]
        [SwaggerResponseExample(StatusCodes.Status200OK
            , typeof(ApplicantExample))]
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
        [SwaggerResponse(StatusCodes.Status201Created,
            "The resource is succesfully created. The Location attribute in" +
            "the response header will have the URL to the resource")]

        [SwaggerResponse(StatusCodes.Status400BadRequest, "The values in the " +
           "input applicant are not valid. Please refer the applicant details" +
            " for more details")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "The resource" +
            "already exists.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Returned" +
           " when there is an unusual error")]
        [SwaggerRequestExample(typeof(Applicant), typeof(ApplicantExample))]
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
        [SwaggerResponse(StatusCodes.Status200OK, "Returned when the resource" +
            "is succesfully updated")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the " +
           "resource that is attempted to update doesn't exist. The existance" +
           " of the resouce is based on the ID attribute of the Applicant")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The values in the " +
           "input applicant are not valid. Please refer the applicant details" +
            " for more details")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Returned" +
           " when there is an unusual error")]
        [SwaggerRequestExample(typeof(Applicant), typeof(ApplicantExample))]
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
        [SwaggerResponse(StatusCodes.Status200OK, "The deletion is succesful")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Returned when the " +
           "resource that is attempted to delete does not exist. The existance" +
           " of the resouce is based on the ID attribute of the Applicant")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Returned" +
           " when there is an unusual error")]
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
