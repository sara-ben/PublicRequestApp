using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicRequestApp.Models;
using PublicRequestApp.Repositories;
using Azure.Core;

namespace PublicRequestApp.Controllers
{

    [ApiController]
    [Route("api/RequestController")]
    public class RequestController : ControllerBase
    {
        private readonly RequestRepository _requestRepository;
        public RequestController(RequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var requests = _requestRepository.GetAllRequests();
            return Ok(requests);
        }
        [HttpGet("{id}")]
        public IActionResult GetRequestById(int id)
        {
            var request = _requestRepository.GetRequestById(id);
            if (request == null)
                return NotFound(); 

            return Ok(request);
        }

        [HttpPost]
        public IActionResult PostRequest([FromBody] PublicRequestApp.Models.Request request)
        {
            if (request == null)
                return BadRequest();
            request.Id = 0;


            var createdRequest = _requestRepository.CreateRequest(request);
            return CreatedAtAction(nameof(GetRequestById), new { id = createdRequest.Id }, createdRequest);
        }
        [HttpGet("summary")]
        public IActionResult GetRequestsComparison()
        {
            var dt = _requestRepository.GetRequestsComparison();
            var data = _requestRepository.ConvertDataTableToList(dt);

            return Ok(data);
        }
    }
    }