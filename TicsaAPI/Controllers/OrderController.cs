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
    public class OrderController : ControllerBase
    {

        private IBsOrder _bsOrder{ get; set; }
        public OrderController(IBsOrder bsOrder)
        {
            _bsOrder = bsOrder;
        }

        /// <summary>
        /// Recupère toute les Commandes
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Commandes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<Orders>>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<IEnumerable<Orders>>>> GetAllOrder()
        {
            try
            {
                return Ok(new Response<IEnumerable<Orders>>() { Error = "", Data = await _bsOrder.GetAll(), Succes = true });
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
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idGamme}")]
        [ProducesResponseType(typeof(Response<Orders>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Orders>>> GetOrderById([FromRoute] int idOrder)
        {
            try
            {
                if (idOrder == 0)
                    throw new Exception("IdGamme can't be equal to 0");
                return Ok(new Response<Orders>() { Error = "", Data = await _bsOrder.GetById(idOrder), Succes = true });
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
        /// <response code="200">Succes / Retourne la Commande modifié</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(Response<Orders>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Orders>>> UpdateOrder([FromBody] Orders order)
        {
            try
            {
                return Ok(new Response<Orders>() { Error = "", Data = await _bsOrder.Update(order), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Supprime une Commande
        /// </summary>
        /// <param name="order"></param>  
        /// <response code="200">Succes / Retourne la Commande supprimé</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove")]
        [ProducesResponseType(typeof(Response<Orders>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Orders>>> RemoveOrder([FromBody] Orders order)
        {
            try
            {
                return Ok(new Response<Orders>() { Error = "", Data = await _bsOrder.Remove(order), Succes = true });
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
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<Orders>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Orders>>> AddOrder([FromBody] Orders order)
        {
            try
            {
                return Ok(new Response<Orders>() { Error = "", Data = await _bsOrder.Add(order), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}
