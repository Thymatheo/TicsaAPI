using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.Model;

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
        [ProducesResponseType(typeof(IEnumerable<Gamme>), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<IEnumerable<Gamme>>> GetAllGamme()
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
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("type/{id}")]
        [ProducesResponseType(typeof(IEnumerable<Gamme>), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<IEnumerable<Gamme>>> GetGammeByType([FromRoute] int idType)
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
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Gamme), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<Gamme>> GetGammeById([FromRoute] int idGamme)
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
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(Gamme), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<Gamme>> UpdateGamme([FromBody] Gamme gamme)
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
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("update")]
        [ProducesResponseType(typeof(Gamme), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<Gamme>> removeGamme([FromBody] Gamme gamme)
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
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(Gamme), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<ActionResult<Gamme>> AddGamme([FromBody] Gamme gamme)
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