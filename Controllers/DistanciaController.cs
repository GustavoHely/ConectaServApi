using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectaServApi.Data;
using ConectaServApi.Services;
using ConectaServApi.DTOs;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistanciaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly GoogleMapsService _mapsService;

        public DistanciaController(AppDbContext context, GoogleMapsService mapsService)
        {
            _context = context;
            _mapsService = mapsService;
        }

        /// <summary>
        /// Calcula a distância em km entre o endereço de um cliente e de uma empresa
        /// </summary>
        /// <param name="idCliente">ID do cliente</param>
        /// <param name="idEmpresa">ID da empresa</param>
        /// <returns>Distância em km</returns>
        [HttpGet("cliente-empresa/{idCliente}/{idEmpresa}")]
        [ProducesResponseType(typeof(DistanciaDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CalcularDistancia(int idCliente, int idEmpresa)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == idCliente);

            var empresa = await _context.Empresas
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(e => e.Id == idEmpresa);

            if (cliente == null || cliente.Endereco == null)
                return NotFound("Cliente ou endereço do cliente não encontrado.");

            if (empresa == null || empresa.Endereco == null)
                return NotFound("Empresa ou endereço da empresa não encontrado.");

            if (cliente.Endereco.Latitude == null || cliente.Endereco.Longitude == null ||
                empresa.Endereco.Latitude == null || empresa.Endereco.Longitude == null)
            {
                return BadRequest("Endereço sem coordenadas geográficas. Verifique se o CEP foi preenchido corretamente.");
            }

            var distancia = await _mapsService.CalcularDistanciaKmAsync(
                cliente.Endereco.Latitude.Value,
                cliente.Endereco.Longitude.Value,
                empresa.Endereco.Latitude.Value,
                empresa.Endereco.Longitude.Value
            );

            if (distancia == null)
                return BadRequest("Erro ao calcular distância com a API do Google.");

            var resultado = new DistanciaDTO
            {
                EmpresaId = empresa.Id,
                EmpresaNome = empresa.Nome,
                DistanciaKm = Math.Round(distancia.Value, 2)
            };

            return Ok(resultado);
        }
    }
}
