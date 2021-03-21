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
    public class OrderContentController : ControllerBase
    {
        private IBsOrderContent _bsOrderContent { get; set; }
        public OrderContentController(IBsOrderContent bsOrderContent)
        {
            _bsOrderContent = bsOrderContent;
        }

        /// <summary>
        /// Recupère tout les Contenus de toutes les Commandes
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Contenus de toute les Commandes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<OrderContent>>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<IEnumerable<OrderContent>>>> GetAllOrderContent()
        {
            try
            {
                return Ok(new Response<IEnumerable<OrderContent>>() { Error = "", Data = await _bsOrderContent.GetAll(), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère tout les Contenus d'une Commandes     
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Contenus de Commandes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("order/{idOrder}")]
        [ProducesResponseType(typeof(Response<IEnumerable<OrderContent>>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<IEnumerable<OrderContent>>>> GetOrderContentByIdOrder([FromRoute] int idOrder)
        {
            try
            {
                if (idOrder == 0)
                    throw new Exception("idOrder can't be equal to 0");
                return Ok(new Response<IEnumerable<OrderContent>>() { Error = "", Data = await _bsOrderContent.GetByIdOrder(idOrder), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère un Contenu de Commande en fonction de son Id
        /// </summary>
        /// <param name="idOrderContent"></param>  
        /// <response code="200">Succes / Retourne un Contenu de Commande</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idOrderContent}")]
        [ProducesResponseType(typeof(Response<OrderContent>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<OrderContent>>> GetOrderById([FromRoute] int idOrderContent)
        {
            try
            {
                if (idOrderContent == 0)
                    throw new Exception("idOrderContent can't be equal to 0");
                return Ok(new Response<OrderContent>() { Error = "", Data = await _bsOrderContent.GetById(idOrderContent), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Met a jour un Contenu de Commande
        /// </summary>
        /// <param name="orderContent"></param>  
        /// <response code="200">Succes / Retourne le Contenu de Commande modifié</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(Response<OrderContent>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<OrderContent>>> UpdateOrderContent([FromBody] OrderContent orderContent)
        {
            try
            {
                return Ok(new Response<OrderContent>() { Error = "", Data = await _bsOrderContent.Update(orderContent), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Supprime un Contenu de Commande
        /// </summary>
        /// <param name="orderContent"></param>  
        /// <response code="200">Succes / Retourne la Commande supprimé</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove")]
        [ProducesResponseType(typeof(Response<OrderContent>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<OrderContent>>> RemoveOrderContent([FromBody] OrderContent orderContent)
        {
            try
            {
                return Ok(new Response<OrderContent>() { Error = "", Data = await _bsOrderContent.Remove(orderContent), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Ajoute un Contenu de Commande
        /// </summary>
        /// <param name="orderContent"></param>       
        /// <response code="200">Succes / Retourne la Commande ajouté</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<OrderContent>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<OrderContent>>> AddOrderContent([FromBody] OrderContent orderContent)
        {
            try
            {
                return Ok(new Response<OrderContent>() { Error = "", Data = await _bsOrderContent.Add(orderContent), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}
