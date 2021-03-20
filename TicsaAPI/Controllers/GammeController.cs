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
        [ProducesResponseType(typeof(IEnumerable<Gammes>), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<IEnumerable<Gammes>>> GetAllGamme()
        {
            try
            {
                return Ok(await _bsGamme.GetAllGamme());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Recupère les Gammes ayant le type souhaité
        /// </summary>
        /// <param name="idType"></param> 
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("type/{id}")]
        [ProducesResponseType(typeof(IEnumerable<Gammes>), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<IEnumerable<Gammes>>> GetGammeByType([FromRoute] int idType)
        {
            try
            {
                return Ok(await _bsGamme.GetGammesByIdType(idType));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Recupère une Gamme en fonction de son Id
        /// </summary>
        /// <param name="idGamme"></param>  
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Gammes), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<Gammes>> GetGammeById([FromRoute] int idGamme)
        {
            try
            {
                return Ok(await _bsGamme.GetGammeById(idGamme));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Met a jour une Gamme
        /// </summary>
        /// <param name="gamme"></param>  
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(Gammes), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<Gammes>> UpdateGamme([FromBody] Gammes gamme)
        {
            try
            {
                return Ok(await _bsGamme.UpdateGamme(gamme));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Supprime une Gamme
        /// </summary>
        /// <param name="gamme"></param>  
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("update")]
        [ProducesResponseType(typeof(Gammes), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<Gammes>> removeGamme([FromBody] Gammes gamme)
        {
            try
            {
                return Ok(await _bsGamme.RemoveGamme(gamme));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Ajoute une Gamme
        /// </summary>
        /// <param name="gamme"></param>       
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(Gammes), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<Gammes>> AddGamme([FromBody] Gammes gamme)
        {
            try
            {
                return Ok(await _bsGamme.AddGamme(gamme));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}