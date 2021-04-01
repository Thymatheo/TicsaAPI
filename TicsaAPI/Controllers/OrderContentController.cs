using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.Order;
using TicsaAPI.BLL.DTO.OrderContent;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderContentController : ControllerBase
    {
        private readonly IBsOrderContent BsOrderContent;
        private readonly IBsOrder BsOrder;

        public OrderContentController(IBsOrderContent bsOrderContent, IBsOrder bsOrder)
        {
            BsOrderContent = bsOrderContent;
            BsOrder = bsOrder;
        }

        /// <summary>
        /// Recupère tout les Contenus de toutes les Commandes
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Contenus de toute les Commandes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<DtoOrderContent>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<DtoOrderContent>>>> GetAllOrderContent()
        {
            try
            {
                return Ok(new Response<IEnumerable<DtoOrderContent>>() { Error = "", Data = await BsOrderContent.GetAll<DtoOrderContent>(), Succes = true });
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
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idOrderContent}")]
        [ProducesResponseType(typeof(Response<DtoOrderContent>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoOrderContent>>> GetOrderById([FromRoute] int idOrderContent)
        {
            try
            {
                if (idOrderContent == 0)
                    return BadRequest(new Response<string>() { Error = "IdOrderContent can't be equal to 0", Succes = true });
                var result = await BsOrderContent.GetById<DtoOrderContent>(idOrderContent);
                if (result == null)
                    return NotFound(new Response<string>() { Error = "the OrderContent doesn't exist", Succes = true });
                return Ok(new Response<DtoOrderContent>() { Error = "", Data = result, Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère tout les Contenus d'une Commandes     
        /// </summary>
        /// <param name="idOrder"></param>
        /// <response code="200">Succes / Retourne toutes les Contenus de Commandes</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("order/{idOrder}")]
        [ProducesResponseType(typeof(Response<IEnumerable<DtoOrderContent>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<DtoOrderContent>>>> GetOrderContentByIdOrder([FromRoute] int idOrder)
        {
            try
            {
                if (idOrder == 0)
                    return BadRequest(new Response<string>() { Error = "IdOrder can't be equal to 0", Succes = true });
                if ((await BsOrder.GetById<DtoOrder>(idOrder)) == null)
                    return NotFound(new Response<string>() { Error = "the Order doesn't exist", Succes = true });
                return Ok(new Response<IEnumerable<DtoOrderContent>>() { Error = "", Data = await BsOrderContent.GetByIdOrder(idOrder), Succes = true });
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
        /// <param name="idOrderContent"></param>  
        /// <response code="200">Succes / Retourne le Contenu de Commande modifié</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{idOrderContent}")]
        [ProducesResponseType(typeof(Response<DtoOrderContent>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoOrderContent>>> UpdateOrderContent([FromRoute] int idOrderContent, [FromBody] DtoOrderContentUpdate orderContent)
        {
            try
            {
                if (idOrderContent == 0)
                    return BadRequest(new Response<string>() { Error = "IdOrderContent can't be equal to 0", Succes = true });
                if ((await BsOrderContent.GetById<DtoOrderContent>(idOrderContent)) == null)
                    return NotFound(new Response<string>() { Error = "the OrderContent doesn't exist", Succes = true });
                if (orderContent == null)
                    return BadRequest(new Response<string>() { Error = "The OrderContent can't be null", Succes = true });
                return Ok(new Response<DtoOrderContent>() { Error = "", Data = await BsOrderContent.Update<DtoOrderContent, DtoOrderContentUpdate>(idOrderContent, orderContent), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Supprime un Contenu de Commande
        /// </summary>
        /// <param name="idOrderContent"></param>  
        /// <response code="200">Succes / Retourne la Commande supprimé</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove/{idOrderContent}")]
        [ProducesResponseType(typeof(Response<DtoOrderContent>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoOrderContent>>> RemoveOrderContent([FromRoute] int idOrderContent)
        {
            try
            {
                if (idOrderContent == 0)
                    return BadRequest(new Response<string>() { Error = "IdOrderContent can't be equal to 0", Succes = true });
                if ((await BsOrderContent.GetById<DtoOrderContent>(idOrderContent)) == null)
                    return NotFound(new Response<string>() { Error = "the OrderContent doesn't exist", Succes = true });
                return Ok(new Response<DtoOrderContent>() { Error = "", Data = await BsOrderContent.Remove<DtoOrderContent>(idOrderContent), Succes = true });
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
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<DtoOrderContent>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoOrderContent>>> AddOrderContent([FromBody] DtoOrderContentAdd orderContent)
        {
            try
            {
                if (orderContent == null)
                    return BadRequest(new Response<string>() { Error = "The OrderContent can't be null", Succes = true });
                return Ok(new Response<DtoOrderContent>() { Error = "", Data = await BsOrderContent.Add<DtoOrderContent, DtoOrderContentAdd>(orderContent), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}
