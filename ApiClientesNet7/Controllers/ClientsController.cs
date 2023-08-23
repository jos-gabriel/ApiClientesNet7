using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiClientesNet7.Data;
using ApiClientesNet7.Models;
using ApiClientesNet7.Repository;
using ApiClientesNet7.Models.Dto;

namespace ApiClientesNet7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private readonly IClientRepository _clientRepository;
        protected ResponseDto _response;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _response = new ResponseDto();
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Getclients()
        {
            try
            {
                var lista = await _clientRepository.GetClients();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de Clientes";
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var cliente = await _clientRepository.GetClientById(id);
            if (cliente == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Cliente No Existe";
                return NotFound(_response);
            }
            _response.Result = cliente;
            _response.DisplayMessage = "Información del cliente";
            return Ok(_response);
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, ClientDto clientDto)
        {
            try
            {
                ClientDto model = await _clientRepository.CreateUpdate(clientDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Actualizar el Registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(ClientDto clientDto)
        {
            try
            {
                ClientDto model = await _clientRepository.CreateUpdate(clientDto);
                _response.Result = model;
                return CreatedAtAction("GetClient", new { id = model.Id }, _response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al grabar el Registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                bool estaEliminado = await _clientRepository.DeleteClient(id);
                if (estaEliminado)
                {
                    _response.Result = estaEliminado;
                    _response.DisplayMessage = "Cliente Eliminado con Éxito";
                    return Ok(_response);
                }
                else
                {

                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al elminar cliente";
                    return BadRequest(_response);

                }
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
