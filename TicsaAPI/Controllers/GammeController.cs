using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.Gamme;
using TicsaAPI.BLL.DTO.GammeType;
using TicsaAPI.BLL.DTO.Producer;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GammeController : ControllerBase {
        private IBsGamme BsGamme { get; set; }
        private IBsGammeType BsGammeType { get; set; }
        private IBsProducer BsProducer { get; set; }

        public GammeController(IBsGamme bsGamme, IBsGammeType bsGammeType, IBsProducer bsProducer) {
            BsGamme = bsGamme;
            BsGammeType = bsGammeType;
            BsProducer = bsProducer;
        }

        /// <summary>
        /// Recupère toute les Gammes
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<DtoGamme>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<DtoGamme>>>> GetAllGamme() {
            try {
                return Ok(new Response<IEnumerable<DtoGamme>>() { Error = "", Data = await BsGamme.GetAll(), Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère une Gamme en fonction de son Id
        /// </summary>
        /// <param name="idGamme"></param>  
        /// <response code="200">Succes / Retourne une Gamme</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idGamme}")]
        [ProducesResponseType(typeof(Response<DtoGamme>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoGamme>>> GetGammeById([FromRoute] int idGamme) {
            try {
                if (idGamme == 0) {
                    return BadRequest(new Response<string>() { Error = "IdGamme can't be equal to 0", Succes = true });
                }

                DtoGamme? result = await BsGamme.GetById(idGamme);
                if (result == null) {
                    return NotFound(new Response<string>() { Error = "The Gamme doesn't exist", Succes = true });
                }

                return Ok(new Response<DtoGamme>() { Error = "", Data = result, Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère les Gammes ayant le type souhaité
        /// </summary>
        /// <param name="idType"></param> 
        /// <response code="200">Succes / Retourne une Gamme</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("type/{idType}")]
        [ProducesResponseType(typeof(Response<IEnumerable<DtoGamme>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<DtoGamme>>>> GetGammeByType([FromRoute] int idType) {
            try {
                if (idType == 0) {
                    return BadRequest(new Response<string>() { Error = "IdGammeType can't be equal to 0", Succes = true });
                }

                if ((await BsGammeType.GetById(idType)) == null) {
                    return NotFound(new Response<string>() { Error = "The GammeType doesn't exist", Succes = true });
                }

                return Ok(new Response<IEnumerable<DtoGamme>>() { Error = "", Data = await BsGamme.GetGammesByIdType(idType), Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère les Gammes en fonction du producteur souhaité
        /// </summary>
        /// <param name="idProducer"></param> 
        /// <response code="200">Succes / Retourne une Gamme</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("producer/{idProducer}")]
        [ProducesResponseType(typeof(Response<IEnumerable<DtoGamme>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<DtoGamme>>>> GetGammeByProducer([FromRoute] int idProducer) {
            try {
                if (idProducer == 0) {
                    return BadRequest(new Response<string>() { Error = "IdProducer can't be equal to 0", Succes = true });
                }

                if ((await BsProducer.GetById(idProducer)) == null) {
                    return NotFound(new Response<string>() { Error = "The Producer doesn't exist", Succes = true });
                }

                return Ok(new Response<IEnumerable<DtoGamme>>() { Error = "", Data = await BsGamme.GetGammesByIdProducer(idProducer), Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Met a jour une Gamme
        /// </summary>
        /// <param name="gamme"></param>  
        /// <param name="idGamme"></param>  
        /// <response code="200">Succes / Retourne la Gamme modifié</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{idGamme}")]
        [ProducesResponseType(typeof(Response<DtoGamme>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoGamme>>> UpdateGamme([FromRoute] int idGamme, [FromBody] DtoGammeUpdate gamme) {
            try {
                if (idGamme == 0) {
                    return BadRequest(new Response<string>() { Error = "IdGamme can't be equal to 0", Succes = true });
                }

                if ((await BsGamme.GetById(idGamme)) == null) {
                    return NotFound(new Response<string>() { Error = "The Gamme doesn't exist", Succes = true });
                }

                if (gamme == null) {
                    return BadRequest(new Response<string>() { Error = "The Gamme can't be null", Succes = true });
                }

                return Ok(new Response<DtoGamme>() { Error = "", Data = await BsGamme.Update(idGamme, gamme), Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Supprime une Gamme
        /// </summary>
        /// <param name="idGamme"></param>  
        /// <response code="200">Succes / Retourne la Gamme supprimé</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove/{idGamme}")]
        [ProducesResponseType(typeof(Response<DtoGamme>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoGamme>>> RemoveGamme([FromRoute] int idGamme) {
            try {
                if (idGamme == 0) {
                    return BadRequest(new Response<string>() { Error = "IdGamme can't be equal to 0", Succes = true });
                }

                if ((await BsGamme.GetById(idGamme)) == null) {
                    return NotFound(new Response<string>() { Error = "The Gamme doesn't exist", Succes = true });
                }

                return Ok(new Response<DtoGamme>() { Error = "", Data = await BsGamme.Remove(idGamme), Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Ajoute une Gamme
        /// </summary>
        /// <param name="gamme"></param>       
        /// <response code="200">Succes / Retourne la Gamme ajouté</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<DtoGamme>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoGammeAdd>>> AddGamme([FromBody] Gamme gamme) {
            try {
                if (gamme == null) {
                    return BadRequest(new Response<string>() { Error = "The Gamme can't be null", Succes = true });
                }

                return Ok(new Response<DtoGammeAdd>() { Error = "", Data = await BsGamme.Add(gamme), Succes = true });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}