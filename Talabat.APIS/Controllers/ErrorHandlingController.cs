using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;

namespace Talabat.APIS.Controllers
{
    [Route("Error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] // Prevents this controller from appearing in Swagger documentation
    public class ErrorHandlingController : ControllerBase
    {
        public ActionResult HandleError(int code)
        {
            var response = new ApiResponse(code);
            return StatusCode(code, response);
        }

        /// <summary>
        /// Simulates a 400 Bad Request error.
        /// </summary>
        [HttpGet("bad-request")]
        public ActionResult SimulateBadRequest()
        {
            var response = new ApiResponse(StatusCodes.Status400BadRequest, "This is a simulated bad request error.");
            return BadRequest(response);
        }

        /// <summary>
        /// Simulates a 401 Unauthorized error.
        /// </summary>
        [HttpGet("unauthorized")]
        public ActionResult SimulateUnauthorized()
        {
            var response = new ApiResponse(StatusCodes.Status401Unauthorized, "This is a simulated unauthorized error.");
            return Unauthorized(response);
        }

        /// <summary>
        /// Simulates a 404 Not Found error.
        /// </summary>
        [HttpGet("not-found")]
        public ActionResult SimulateNotFound()
        {
            var response = new ApiResponse(StatusCodes.Status404NotFound, "This is a simulated not found error.");
            return NotFound(response);
        }

        /// <summary>
        /// Simulates a 500 Internal Server Error.
        /// </summary>
        [HttpGet("server-error")]
        public ActionResult SimulateServerError()
        {
            try
            {
                string input = null; // Intentionally set to null to throw a NullReferenceException
                int len = input.Length; // This line will cause an exception
                return Ok(len);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(StatusCodes.Status500InternalServerError,
                    "An internal server error occurred.");
                return StatusCode(500, response);
            }
        }
    }
}
