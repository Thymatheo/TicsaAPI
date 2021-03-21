using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private IBsClient _bsClient { get; set; }
        public ClientController(IBsClient bsClient)
        {
            _bsClient = bsClient;
        }

        /// <summary>
        /// Recupère toute les Clients
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Clients</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<Clients>>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<IEnumerable<Clients>>>> GetAllClient()
        {
            try
            {
                return Ok(new Response<IEnumerable<Clients>>() { Error = "", Data = await _bsClient.GetAll(), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère un Client en fonction de son Id
        /// </summary>
        /// <param name="idClient"></param>  
        /// <response code="200">Succes / Retourne un Client</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idGamme}")]
        [ProducesResponseType(typeof(Response<Clients>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Clients>>> GetClientById([FromRoute] int idClient)
        {
            try
            {
                if (idClient == 0)
                    throw new Exception("IdGamme can't be equal to 0");
                return Ok(new Response<Clients>() { Error = "", Data = await _bsClient.GetById(idClient), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Met a jour un Client
        /// </summary>
        /// <param name="client"></param>  
        /// <response code="200">Succes / Retourne le Client modifié</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(Response<Clients>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Clients>>> UpdateClient([FromBody] Clients client)
        {
            try
            {
                return Ok(new Response<Clients>() { Error = "", Data = await _bsClient.Update(client), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Supprime un Client
        /// </summary>
        /// <param name="client"></param>  
        /// <response code="200">Succes / Retourne le Client supprimé</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove")]
        [ProducesResponseType(typeof(Response<Clients>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Clients>>> RemoveClient([FromBody] Clients client)
        {
            try
            {
                return Ok(new Response<Clients>() { Error = "", Data = await _bsClient.Remove(client), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Ajoute un Client
        /// </summary>
        /// <param name="client"></param>       
        /// <response code="200">Succes / Retourne le Client ajouté</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<Clients>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Clients>>> AddClient([FromBody] Clients client)
        {
            try
            {
                return Ok(new Response<Clients>() { Error = "", Data = await _bsClient.Add(client), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}
