using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.Clients;
using TicsaAPI.BLL.DTO.Order;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private IBsOrder BsOrder { get; set; }
        private IBsClient BsClient { get; set; }

        public OrderController(IBsOrder bsOrder, IBsClient bsClient)
        {
            BsOrder = bsOrder;
            BsClient = bsClient;
        }

        /// <summary>
        /// Recupère toute les Commandes
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Commandes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<DtoOrder>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<DtoOrder>>>> GetAllOrder()
        {
            try
            {
                return Ok(new Response<IEnumerable<DtoOrder>>() { Error = "", Data = await BsOrder.GetAll<DtoOrder>(), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
            }
        }

        /// <summary>
        /// Recupère une Commande en fonction de son Id
        /// </summary>
        /// <param name="idOrder"></param>  
        /// <response code="200">Succes / Retourne une Commande</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idOrder}")]
        [ProducesResponseType(typeof(Response<IEnumerable<DtoOrder>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<DtoOrder>>>> GetOrderById([FromRoute] int idOrder)
        {
            try
            {
                if (idOrder == 0)
                    return BadRequest(new Response<string>() { Error = "IdOrder can't be equal to 0", Succes = true });
                var result = await BsOrder.GetById<DtoOrder>(idOrder);
                if (result == null)
                    return NotFound(new Response<string>() { Error = "The Order doesn't exist", Succes = true });
                return Ok(new Response<DtoOrder>() { Error = "", Data = result, Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
            }
        }

        /// <summary>
        /// Recupère Toutes les Commande d'un utilisateur
        /// </summary>
        /// <param name="idClient"></param>  
        /// <response code="200">Succes / Retourne une Commande</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("client/{idClient}")]
        [ProducesResponseType(typeof(Response<IEnumerable<DtoOrder>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<DtoOrder>>>> GetOrderByIdClient([FromRoute] int idClient)
        {
            try
            {
                if (idClient == 0)
                    return BadRequest(new Response<string>() { Error = "IdClient can't be equal to 0", Succes = true });
                if ((await BsClient.GetById<DtoClient>(idClient)) == null)
                    return NotFound(new Response<string>() { Error = "The Client doesn't exist", Succes = true });
                return Ok(new Response<IEnumerable<DtoOrder>>() { Error = "", Data = await BsOrder.GetByIdClient(idClient), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
            }
        }

        /// <summary>
        /// Met a jour une Commande
        /// </summary>
        /// <param name="order"></param>  
        /// <param name="idOrder"></param>  
        /// <response code="200">Succes / Retourne la Commande modifié</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{idOrder}")]
        [ProducesResponseType(typeof(Response<DtoOrder>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoOrder>>> UpdateOrder([FromRoute] int idOrder, [FromBody] DtoOrderUpdate order)
        {
            try
            {
                if (idOrder == 0)
                    return BadRequest(new Response<string>() { Error = "IdOrder can't be equal to 0", Succes = true });
                if ((await BsOrder.GetById<DtoOrder>(idOrder)) == null)
                    return NotFound(new Response<string>() { Error = "The Order doesn't exist", Succes = true });
                if (order == null)
                    return BadRequest(new Response<string>() { Error = "The Order can't be null", Succes = true });
                return Ok(new Response<DtoOrder>() { Error = "", Data = await BsOrder.Update<DtoOrder, DtoOrderUpdate>(idOrder, order), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
            }
        }

        /// <summary>
        /// Supprime une Commande
        /// </summary>
        /// <param name="idOrder"></param>  
        /// <response code="200">Succes / Retourne la Commande supprimé</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove/{idOrder}")]
        [ProducesResponseType(typeof(Response<DtoOrder>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoOrder>>> RemoveOrder([FromRoute] int idOrder)
        {
            try
            {
                if (idOrder == 0)
                    return BadRequest(new Response<string>() { Error = "IdOrder can't be equal to 0", Succes = true });
                if ((await BsOrder.GetById<DtoOrder>(idOrder)) == null)
                    return NotFound(new Response<string>() { Error = "The Order doesn't exist", Succes = true });
                return Ok(new Response<DtoOrder>() { Error = "", Data = await BsOrder.Remove<DtoOrder>(idOrder), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
            }
        }

        /// <summary>
        /// Ajoute une Commande
        /// </summary>
        /// <param name="order"></param>       
        /// <response code="200">Succes / Retourne la Commande ajouté</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<DtoOrder>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoOrder>>> AddOrder([FromBody] DtoOrderAdd order)
        {
            try
            {
                if (order == null)
                    return BadRequest(new Response<string>() { Error = "The Order can't be null", Succes = true });
                return Ok(new Response<DtoOrder>() { Error = "", Data = await BsOrder.Add<DtoOrder, DtoOrderAdd>(order), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
            }
        }
    }
}
