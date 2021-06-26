using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.Clients;
using TicsaAPI.DAL.Models;
namespace TicsaAPI.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase {

        private IBsClient BsClient { get; set; }
        public ClientController(IBsClient bsClient) {
            BsClient = bsClient;
        }

        /// <summary>
        /// Recupère toute les Clients
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Clients</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<DtoClient>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<DtoClient>>>> GetAllClient() {
            try {
                return Ok(new Response<IEnumerable<DtoClient>>() { Error = "", Data = await BsClient.GetAll(), Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère un Client en fonction de son Id
        /// </summary>
        /// <param name="idClient"></param>  
        /// <response code="200">Succes / Retourne un Client</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / l'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idClient}")]
        [ProducesResponseType(typeof(Response<DtoClient>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoClient>>> GetClientById([FromRoute] int idClient) {
            try {
                if (idClient == 0) {
                    return BadRequest(new Response<string>() { Error = "IdClient can't be equal to 0", Succes = true });
                }

                if ((await BsClient.GetById(idClient)) == null) {
                    return NotFound(new Response<string>() { Error = "The Client doesn't exist", Data = null, Succes = true });
                }

                return Ok(new Response<DtoClient>() { Error = "", Data = await BsClient.GetById(idClient), Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Met a jour un Client
        /// </summary>
        /// <param name="client"></param>  
        /// <param name="idClient"></param>  
        /// <response code="200">Succes / Retourne le Client modifié</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / l'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{idClient}")]
        [ProducesResponseType(typeof(Response<DtoClient>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoClient>>> UpdateClient([FromRoute] int idClient, [FromBody] DtoClientUpdate client) {
            try {
                if (idClient == 0) {
                    return BadRequest(new Response<string>() { Error = "IdClient can't be equal to 0", Succes = true });
                }

                if ((await BsClient.GetById(idClient)) == null) {
                    return NotFound(new Response<string>() { Error = "The Client doesn't exist", Succes = true });
                }

                if (client == null) {
                    return BadRequest(new Response<string>() { Error = "The Clientcan't be null", Succes = true });
                }

                return Ok(new Response<DtoClient>() { Error = "", Data = await BsClient.Update(idClient, client), Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Supprime un Client
        /// </summary>
        /// <param name="idClient"></param>  
        /// <response code="200">Succes / Retourne le Client supprimé</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / l'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove/{idClient}")]
        [ProducesResponseType(typeof(Response<DtoClient>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoClient>>> RemoveClient([FromRoute] int idClient) {
            try {
                if (idClient == 0) {
                    return BadRequest(new Response<string>() { Error = "IdClient can't be equal to 0", Succes = true });
                }

                if ((await BsClient.GetById(idClient)) == null) {
                    return NotFound(new Response<string>() { Error = "The Client doesn't exist", Succes = true });
                }

                return Ok(new Response<DtoClient>() { Error = "", Data = await BsClient.Remove(idClient), Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Ajoute un Client
        /// </summary>
        /// <param name="client"></param>       
        /// <response code="200">Succes / Retourne le Client ajouté</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<DtoClient>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoClient>>> AddClient([FromBody] Client client) {
            try {
                if (client == null) {
                    return BadRequest(new Response<string>() { Error = "The Client can't be null", Succes = true });
                }

                return Ok(new Response<DtoClientAdd>() { Error = "", Data = await BsClient.Add(client), Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}
