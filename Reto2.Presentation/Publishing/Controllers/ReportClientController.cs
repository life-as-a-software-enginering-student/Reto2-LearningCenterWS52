using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reto2.Domain;
using Reto2.Infrastructure.Entities;
using Reto2.Infrastructure.Repositories;
using Reto2.Presentation.Publishing.Request;
using Reto2.Presentation.Publishing.Response;

namespace Reto2.Presentation.Publishing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportClientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClienteData _clienteData;
        private readonly IClienteDomain _clienteDomain;
        
        public ReportClientController(IMapper mapper, IClienteData clienteData, IClienteDomain clienteDomain)
        {
            _mapper = mapper;
            _clienteData = clienteData;
            _clienteDomain = clienteDomain;
        }
        
        // GET: api/ReportClient
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _clienteData.GetAllAsync();
            var result = _mapper.Map<List<Cliente>, List<ClienteResponse>>(data);
            if (result.Count == 0) return NotFound();
            
            return Ok(result);
        }
        
        // POST: api/ReportClient/Create
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteRequest input)
        {
            //if (!ModelState.IsValid) { // Get the details of the validation errors
            if (!ModelState.IsValid) { 
                
                var errorMessages = ModelState.Values .SelectMany(v => v.Errors) .Select(e => e.ErrorMessage) .ToList();
                
                return BadRequest(new { Errors = errorMessages });
                
            }
            
            var cliente = _mapper.Map<ClienteRequest, Cliente>(input);
            var result = await _clienteDomain.SaveAsync(cliente);

            if (result > 0)
                return StatusCode(StatusCodes.Status201Created, result);

            return BadRequest();

        }

        // GET: api/ReportClient/Search
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetSearchAsync(string name)
        {
            var data = await _clienteData.GetSearchAsync(name);
            var result = _mapper.Map<List<Cliente>, List<ClienteResponse>>(data);
            if (result.Count == 0) return StatusCode(StatusCodes.Status404NotFound);
            
            return Ok(result);
        }
        
        // GET: api/ReportClient/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var data = _clienteData.GetById(id);
            var result = _mapper.Map<Cliente, ClienteResponse>(data);
            if (result != null) return Ok(result);
            
            return StatusCode(StatusCodes.Status404NotFound);
        }

        
        

        // PUT: api/ReportClient/Pedido
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PedidoRequest input)
        {
            if (!ModelState.IsValid)
            {
                var pedido = _mapper.Map<PedidoRequest, Pedido>(input);
                var result = _clienteDomain.UpdatePedidoAsync(pedido);
                if (result != null) return Ok(result);

                return StatusCode(StatusCodes.Status404NotFound);
            }
            return BadRequest();
        }
        

        // DELETE: api/ReportClient/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
