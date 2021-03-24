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
    public class GammeTypeController : ControllerBase
    {
        private IBsGammeType BsGammeType { get; set; }
        public GammeTypeController(IBsGammeType bsGammeType)
        {
            BsGammeType = bsGammeType;
        }

        /// <summary>
        /// Recupère toute les Types de Gamme
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Types de Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<GammeType>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<GammeType>>>> GetAllGammeType()
        {
            try
            {
                return Ok(new Response<IEnumerable<GammeType>>() { Error = "", Data = await BsGammeType.GetAll(), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère un Type de Gamme en fonction de son Id
        /// </summary>
        /// <param name="idType"></param>  
        /// <response code="200">Succes / Retourne toutes un Type de Gamme</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idType}")]
        [ProducesResponseType(typeof(Response<GammeType>), 200)]
        [ProducesResponseType(typeof(Response<GammeType>), 400)]
        [ProducesResponseType(typeof(Response<GammeType>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<GammeType>>> GetGammeTypeById([FromRoute] int idType)
        {
            try
            {
                if (idType == 0)
                    return BadRequest(new Response<GammeType>() { Error = "IdGammeType can't be equal to 0", Data = null, Succes = true });
                if ((await BsGammeType.GetById(idType)) == null)
                    return NotFound(new Response<GammeType>() { Error = "the GammeType doesn't exist", Data = null, Succes = true });
                return Ok(new Response<GammeType>() { Error = "", Data = await BsGammeType.GetById(idType), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Met a jour un Type de Gamme
        /// </summary>
        /// <param name="type"></param>
        /// <param name="idType"></param>  
        /// <response code="200">Succes / Retourne le Type de Gamme modifié</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{idType}")]
        [ProducesResponseType(typeof(Response<GammeType>), 200)]
        [ProducesResponseType(typeof(Response<GammeType>), 400)]
        [ProducesResponseType(typeof(Response<GammeType>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<GammeType>>> UpdateGammeType([FromRoute] int idType, [FromBody] GammeType type)
        {
            try
            {
                if (idType == 0)
                    return BadRequest(new Response<GammeType>() { Error = "IdGammeType can't be equal to 0", Data = null, Succes = true });
                if ((await BsGammeType.GetById(idType)) == null)
                    return NotFound(new Response<GammeType>() { Error = "the GammeType doesn't exist", Data = null, Succes = true });
                if (type == null)
                    return BadRequest(new Response<GammeType>() { Error = "The GammeType can't be null", Data = null, Succes = true });
                return Ok(new Response<GammeType>() { Error = "", Data = await BsGammeType.Update(idType, type), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Supprime un Type de Gamme
        /// </summary>
        /// <param name="idType"></param>  
        /// <response code="200">Succes / Retourne le Type de Gamme supprimé</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove/{idType}")]
        [ProducesResponseType(typeof(Response<GammeType>), 200)]
        [ProducesResponseType(typeof(Response<GammeType>), 400)]
        [ProducesResponseType(typeof(Response<GammeType>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<GammeType>>> RemoveGammeType([FromRoute] int idType)
        {
            try
            {
                if (idType == 0)
                    return BadRequest(new Response<GammeType>() { Error = "IdGammeType can't be equal to 0", Data = null, Succes = true });
                if ((await BsGammeType.GetById(idType)) == null)
                    return NotFound(new Response<GammeType>() { Error = "the GammeType doesn't exist", Data = null, Succes = true });
                return Ok(new Response<GammeType>() { Error = "", Data = await BsGammeType.Remove(idType), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Ajoute un Type de Gamme
        /// </summary>
        /// <param name="type"></param>       
        /// <response code="200">Succes / Retourne le Type de Gamme ajouté</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<GammeType>), 200)]
        [ProducesResponseType(typeof(Response<GammeType>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<GammeType>>> AddGammeType([FromBody] GammeType type)
        {
            try
            {
                if (type == null)
                    return BadRequest(new Response<GammeType>() { Error = "The GammeType can't be null", Data = null, Succes = true });
                return Ok(new Response<GammeType>() { Error = "", Data = await BsGammeType.Add(type), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}
