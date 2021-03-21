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
    public class GammeTypeController : ControllerBase
    {
        private IBsGammeType _bsGammeType { get; set; }
        public GammeTypeController(IBsGammeType bsGammeType)
        {
            _bsGammeType = bsGammeType;
        }

        /// <summary>
        /// Recupère toute les Gammes
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Types de Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<GammeTypes>>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<IEnumerable<GammeTypes>>>> GetAllGammeType()
        {
            try
            {
                return Ok(new Response<IEnumerable<GammeTypes>>() { Error = "", Data = await _bsGammeType.GetAll(), Succes = true });
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
        /// <response code="200">Succes / Retourne toutes le Type de Gamme/response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idGamme}")]
        [ProducesResponseType(typeof(Response<GammeTypes>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<GammeTypes>>> GetGammeTypeById([FromRoute] int idType)
        {
            try
            {
                if (idType == 0)
                    throw new Exception("IdType can't be equal to 0");
                return Ok(new Response<GammeTypes>() { Error = "", Data = await _bsGammeType.GetById(idType), Succes = true });
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
        /// <response code="200">Succes / Retourne le type de gamme modifié</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(Response<GammeTypes>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<GammeTypes>>> UpdateGammeType([FromBody] GammeTypes type)
        {
            try
            {
                return Ok(new Response<GammeTypes>() { Error = "", Data = await _bsGammeType.Update(type), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Supprime un Type de Gamme
        /// </summary>
        /// <param name="type"></param>  
        /// <response code="200">Succes / Retourne le type de gamme supprimé</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove")]
        [ProducesResponseType(typeof(Response<GammeTypes>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<GammeTypes>>> RemoveGammeType([FromBody] GammeTypes type)
        {
            try
            {
                return Ok(new Response<GammeTypes>() { Error = "", Data = await _bsGammeType.Remove(type), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Ajoute un Type de Gamme
        /// </summary>
        /// <param name="gamme"></param>       
        /// <response code="200">Succes / Retourne le type de gamme ajouté</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<GammeTypes>), 200)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<GammeTypes>>> AddGammeType([FromBody] GammeTypes type)
        {
            try
            {
                return Ok(new Response<GammeTypes>() { Error = "", Data = await _bsGammeType.Add(type), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}
