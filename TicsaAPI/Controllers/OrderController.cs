using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private IBsOrder BsOrder { get; set; }
        public OrderController(IBsOrder bsOrder)
        {
            BsOrder = bsOrder;
        }

        /// <summary>
        /// Recupère toute les Commandes
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Commandes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<Order>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<Order>>>> GetAllOrder()
        {
            try
            {
                return Ok(new Response<IEnumerable<Order>>() { Error = "", Data = await BsOrder.GetAll(), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
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
        [ProducesResponseType(typeof(Response<Order>), 200)]
        [ProducesResponseType(typeof(Response<Order>), 400)]
        [ProducesResponseType(typeof(Response<Order>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Order>>> GetOrderById([FromRoute] int idOrder)
        {
            try
            {
                if (idOrder == 0)
                    return BadRequest(new Response<Order>() { Error = "IdOrder can't be equal to 0", Data = null, Succes = true });
                if ((await BsOrder.GetById(idOrder)) == null)
                    return NotFound(new Response<Order>() { Error = "The Order doesn't exist", Data = null, Succes = true });
                return Ok(new Response<Order>() { Error = "", Data = await BsOrder.GetById(idOrder), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
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
        [ProducesResponseType(typeof(Response<Order>), 200)]
        [ProducesResponseType(typeof(Response<Order>), 400)]
        [ProducesResponseType(typeof(Response<Order>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Order>>> UpdateOrder([FromRoute] int idOrder, [FromBody] Order order)
        {
            try
            {
                if (idOrder == 0)
                    return BadRequest(new Response<Order>() { Error = "IdOrder can't be equal to 0", Data = null, Succes = true });
                if ((await BsOrder.GetById(idOrder)) == null)
                    return NotFound(new Response<Order>() { Error = "The Order doesn't exist", Data = null, Succes = true });
                if (order == null)
                    return BadRequest(new Response<Order>() { Error = "The Order can't be null", Data = null, Succes = true });
                return Ok(new Response<Order>() { Error = "", Data = await BsOrder.Update(idOrder, order), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
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
        [ProducesResponseType(typeof(Response<Order>), 200)]
        [ProducesResponseType(typeof(Response<Order>), 400)]
        [ProducesResponseType(typeof(Response<Order>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Order>>> RemoveOrder([FromRoute] int idOrder)
        {
            try
            {
                if (idOrder == 0)
                    return BadRequest(new Response<Order>() { Error = "IdOrder can't be equal to 0", Data = null, Succes = true });
                if ((await BsOrder.GetById(idOrder)) == null)
                    return NotFound(new Response<Order>() { Error = "The Order doesn't exist", Data = null, Succes = true });
                return Ok(new Response<Order>() { Error = "", Data = await BsOrder.Remove(idOrder), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
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
        [ProducesResponseType(typeof(Response<Order>), 200)]
        [ProducesResponseType(typeof(Response<Order>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Order>>> AddOrder([FromBody] Order order)
        {
            try
            {
                if (order == null)
                    return BadRequest(new Response<Order>() { Error = "The Order can't be null", Data = null, Succes = true });
                return Ok(new Response<Order>() { Error = "", Data = await BsOrder.Add(order), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}
