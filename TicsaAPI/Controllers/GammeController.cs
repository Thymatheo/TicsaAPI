﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GammeController : ControllerBase
    {
        private IBsGamme _bsGamme { get; set; }

        private IBsGammeType _bsGammeType { get; set; }

        public GammeController(IBsGamme bsGamme, IBsGammeType bsGammeType)
        {
            _bsGamme = bsGamme;
            _bsGammeType = bsGammeType;
        }

        /// <summary>
        /// Recupère toute les Gammes
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Gammes</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<Gamme>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<Gamme>>>> GetAllGamme()
        {
            try
            {
                return Ok(new Response<IEnumerable<Gamme>>() { Error = "", Data = await _bsGamme.GetAll(), Succes = true });
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
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("type/{idType}")]
        [ProducesResponseType(typeof(Response<IEnumerable<Gamme>>), 200)]
        [ProducesResponseType(typeof(Response<IEnumerable<Gamme>>), 400)]
        [ProducesResponseType(typeof(Response<IEnumerable<Gamme>>), 404)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<IEnumerable<Gamme>>>> GetGammeByType([FromRoute] int idType)
        {
            try
            {
                if (idType == 0)
                    return BadRequest(new Response<IEnumerable<Gamme>>() { Error = "IdGammeType can't be equal to 0", Data = null, Succes = true });
                if ((await _bsGammeType.GetById(idType)) == null)
                    return NotFound(new Response<IEnumerable<Gamme>>() { Error = "the GammeType doesn't exist", Data = null, Succes = true });
                return Ok(new Response<IEnumerable<Gamme>>() { Error = "", Data = await _bsGamme.GetGammesByIdType(idType), Succes = true });
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
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idGamme}")]
        [ProducesResponseType(typeof(Response<Gamme>), 200)]
        [ProducesResponseType(typeof(Response<Gamme>), 400)]
        [ProducesResponseType(typeof(Response<Gamme>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Gamme>>> GetGammeById([FromRoute] int idGamme)
        {
            try
            {
                if (idGamme == 0)
                    return BadRequest(new Response<Gamme>() { Error = "IdGamme can't be equal to 0", Data = null, Succes = true });
                if ((await _bsGamme.GetById(idGamme)) == null)
                    return NotFound(new Response<Gamme>() { Error = "The Gamme doesn't exist", Data = null, Succes = true });
                return Ok(new Response<Gamme>() { Error = "", Data = await _bsGamme.GetById(idGamme), Succes = true });
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
        /// <param name="idGamme"></param>  
        /// <response code="200">Succes / Retourne la Gamme modifié</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{idGamme}")]
        [ProducesResponseType(typeof(Response<Gamme>), 200)]
        [ProducesResponseType(typeof(Response<Gamme>), 400)]
        [ProducesResponseType(typeof(Response<Gamme>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Gamme>>> UpdateGamme([FromRoute] int idGamme, [FromBody] Gamme gamme)
        {
            try
            {
                if (idGamme == 0)
                    return BadRequest(new Response<Gamme>() { Error = "IdGamme can't be equal to 0", Data = null, Succes = true });
                if ((await _bsGamme.GetById(idGamme)) == null)
                    return NotFound(new Response<Gamme>() { Error = "The Gamme doesn't exist", Data = null, Succes = true });
                if (gamme == null)
                    return BadRequest(new Response<Gamme>() { Error = "The Gamme can't be null", Data = null, Succes = true });
                return Ok(new Response<Gamme>() { Error = "", Data = await _bsGamme.Update(idGamme, gamme), Succes = true });
            }
            catch (Exception e)
            {
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
        [ProducesResponseType(typeof(Response<Gamme>), 200)]
        [ProducesResponseType(typeof(Response<Gamme>), 400)]
        [ProducesResponseType(typeof(Response<Gamme>), 404)]
        [ProducesResponseType(typeof(Response<Exception>), 500)]
        public async Task<ActionResult<Response<Gamme>>> RemoveGamme([FromBody] int idGamme)
        {
            try
            {
                if (idGamme == 0)
                    return BadRequest(new Response<Gamme>() { Error = "IdGamme can't be equal to 0", Data = null, Succes = true });
                if ((await _bsGamme.GetById(idGamme)) == null)
                    return NotFound(new Response<Gamme>() { Error = "The Gamme doesn't exist", Data = null, Succes = true });
                return Ok(new Response<Gamme>() { Error = "", Data = await _bsGamme.Remove(idGamme), Succes = true });
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
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<Gamme>), 200)]
        [ProducesResponseType(typeof(Response<Gamme>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Gamme>>> AddGamme([FromBody] Gamme gamme)
        {
            try
            {
                if (gamme == null)
                    return BadRequest(new Response<Gamme>() { Error = "The Gamme can't be null", Data = null, Succes = true });
                return Ok(new Response<Gamme>() { Error = "", Data = await _bsGamme.Add(gamme), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}