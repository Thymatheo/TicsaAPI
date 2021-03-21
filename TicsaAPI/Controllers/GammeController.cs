using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GammeController : ControllerBase
    {
        private IBsGamme _bsGamme { get; set; }
        public GammeController(IBsGamme bsGamme)
        {
            _bsGamme = bsGamme;
        }

        /// <summary>
        /// Recupère toute les Gammes
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<Gammes>>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<IEnumerable<Gammes>>>> GetAllGamme()
        {
            try
            {
                return Ok(new Response<IEnumerable<Gammes>>() { Error = "", Data = await _bsGamme.GetAll(), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère les Gammes ayant le type souhaité
        /// </summary>
        /// <param name="idType"></param> 
        /// <response code="200">Succes / Retourne une Gamme</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("type/{idType}")]
        [ProducesResponseType(typeof(Response<IEnumerable<Gammes>>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<IEnumerable<Gammes>>>> GetGammeByType([FromRoute] int idType)
        {
            try
            {
                if (idType == 0)
                    throw new Exception("IdType can't be equal to 0");
                return Ok(new Response<IEnumerable<Gammes>>() { Error = "", Data = await _bsGamme.GetGammesByIdType(idType), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère une Gamme en fonction de son Id
        /// </summary>
        /// <param name="idGamme"></param>  
        /// <response code="200">Succes / Retourne une Gamme</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idGamme}")]
        [ProducesResponseType(typeof(Response<Gammes>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Gammes>>> GetGammeById([FromRoute] int idGamme)
        {
            try
            {
                if (idGamme == 0)
                    throw new Exception("IdGamme can't be equal to 0");
                return Ok(new Response<Gammes>() { Error = "", Data = await _bsGamme.GetById(idGamme), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Met a jour une Gamme
        /// </summary>
        /// <param name="gamme"></param>  
        /// <response code="200">Succes / Retourne la Gamme modifié</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(Response<Gammes>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Gammes>>> UpdateGamme([FromBody] Gammes gamme)
        {
            try
            {
                return Ok(new Response<Gammes>() { Error = "", Data = await _bsGamme.Update(gamme), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Supprime une Gamme
        /// </summary>
        /// <param name="gamme"></param>  
        /// <response code="200">Succes / Retourne la Gamme supprimé</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove")]
        [ProducesResponseType(typeof(Response<Gammes>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Gammes>>> RemoveGamme([FromBody] Gammes gamme)
        {
            try
            {
                return Ok(new Response<Gammes>() { Error = "", Data = await _bsGamme.Remove(gamme), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Ajoute une Gamme
        /// </summary>
        /// <param name="gamme"></param>       
        /// <response code="200">Succes / Retourne la Gamme ajouté</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<Gammes>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Gammes>>> AddGamme([FromBody] Gammes gamme)
        {
            try
            {
                return Ok(new Response<Gammes>() { Error = "", Data = await _bsGamme.Add(gamme), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}