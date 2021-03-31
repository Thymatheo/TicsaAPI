using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.GammeType;
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
        [ProducesResponseType(typeof(Response<IEnumerable<DtoGammeType>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<DtoGammeType>>>> GetAllGammeType()
        {
            try
            {
                return Ok(new Response<IEnumerable<DtoGammeType>>() { Error = "", Data = await BsGammeType.GetAll<DtoGammeType>(), Succes = true });
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
        [ProducesResponseType(typeof(Response<DtoGammeType>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoGammeType>>> GetGammeTypeById([FromRoute] int idType)
        {
            try
            {
                if (idType == 0)
                    return BadRequest(new Response<string>() { Error = "IdGammeType can't be equal to 0", Succes = true });
                var result = await BsGammeType.GetById<DtoGammeType>(idType);
                if (result == null)
                    return NotFound(new Response<string>() { Error = "the GammeType doesn't exist", Succes = true });
                return Ok(new Response<DtoGammeType>() { Error = "", Data = result, Succes = true });
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
        [ProducesResponseType(typeof(Response<DtoGammeType>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoGammeType>>> UpdateGammeType([FromRoute] int idType, [FromBody] DtoGammeTypeUpdate type)
        {
            try
            {
                if (idType == 0)
                    return BadRequest(new Response<string>() { Error = "IdGammeType can't be equal to 0", Succes = true });
                if ((await BsGammeType.GetById<DtoGammeType>(idType)) == null)
                    return NotFound(new Response<string>() { Error = "the GammeType doesn't exist", Succes = true });
                if (type == null)
                    return BadRequest(new Response<string>() { Error = "The GammeType can't be null", Succes = true });
                return Ok(new Response<DtoGammeType>() { Error = "", Data = await BsGammeType.Update<DtoGammeType,DtoGammeTypeUpdate>(idType, type), Succes = true });
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
        [ProducesResponseType(typeof(Response<DtoGammeType>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoGammeType>>> RemoveGammeType([FromRoute] int idType)
        {
            try
            {
                if (idType == 0)
                    return BadRequest(new Response<string>() { Error = "IdGammeType can't be equal to 0", Data = null, Succes = true });
                if ((await BsGammeType.GetById<DtoGammeType>(idType)) == null)
                    return NotFound(new Response<string>() { Error = "the GammeType doesn't exist", Data = null, Succes = true });
                return Ok(new Response<DtoGammeType>() { Error = "", Data = await BsGammeType.Remove<DtoGammeType>(idType), Succes = true });
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
        [ProducesResponseType(typeof(Response<DtoGammeType>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoGammeType>>> AddGammeType([FromBody] DtoGammeTypeAdd type)
        {
            try
            {
                if (type == null)
                    return BadRequest(new Response<string>() { Error = "The GammeType can't be null", Succes = true });
                return Ok(new Response<DtoGammeType>() { Error = "", Data = await BsGammeType.Add<DtoGammeType,DtoGammeTypeAdd>(type), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}
