﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {

        private IBsProducer BsProducer { get; set; }
        public ProducerController(IBsProducer bsProducer)
        {
            BsProducer = bsProducer;
        }

        /// <summary>
        /// Recupère toute les Producteurs
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Producteurs</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<Producer>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<Producer>>>> GetAllProducer()
        {
            try
            {
                return Ok(new Response<IEnumerable<Producer>>() { Error = "", Data = await BsProducer.GetAll(), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère un Producteur en fonction de son Id
        /// </summary>
        /// <param name="idProducer"></param>  
        /// <response code="200">Succes / Retourne un Producteur</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idProducer}")]
        [ProducesResponseType(typeof(Response<Producer>), 200)]
        [ProducesResponseType(typeof(Response<Producer>), 400)]
        [ProducesResponseType(typeof(Response<Producer>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Producer>>> GetProducerById([FromRoute] int idProducer)
        {
            try
            {
                if (idProducer == 0)
                    return BadRequest(new Response<Producer>() { Error = "IdProducteur can't be equal to 0", Data = null, Succes = true });
                if ((await BsProducer.GetById(idProducer)) == null)
                    return NotFound(new Response<Producer>() { Error = "The Producteur doesn't exist", Data = null, Succes = true });
                return Ok(new Response<Producer>() { Error = "", Data = await BsProducer.GetById(idProducer), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Met a jour un Producteur
        /// </summary>
        /// <param name="producer"></param>  
        /// <param name="idProducer"></param>  
        /// <response code="200">Succes / Retourne le Producteur modifié</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{idProducer}")]
        [ProducesResponseType(typeof(Response<Producer>), 200)]
        [ProducesResponseType(typeof(Response<Producer>), 400)]
        [ProducesResponseType(typeof(Response<Producer>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Producer>>> UpdateProducer([FromRoute] int idProducer, [FromBody] Producer producer)
        {
            try
            {
                if (idProducer == 0)
                    return BadRequest(new Response<Producer>() { Error = "IdProducteur can't be equal to 0", Data = null, Succes = true });
                if ((await BsProducer.GetById(idProducer)) == null)
                    return NotFound(new Response<Producer>() { Error = "The Producteur doesn't exist", Data = null, Succes = true });
                if (producer == null)
                    return BadRequest(new Response<Producer>() { Error = "The Producteur can't be null", Data = null, Succes = true });
                return Ok(new Response<Producer>() { Error = "", Data = await BsProducer.Update(idProducer, producer), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Supprime un Producteur
        /// </summary>
        /// <param name="idProducer"></param>  
        /// <response code="200">Succes / Retourne le Producteur supprimé</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove/{idProducer}")]
        [ProducesResponseType(typeof(Response<Producer>), 200)]
        [ProducesResponseType(typeof(Response<Producer>), 400)]
        [ProducesResponseType(typeof(Response<Producer>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Producer>>> RemoveProducer([FromRoute] int idProducer)
        {
            try
            {
                if (idProducer == 0)
                    return BadRequest(new Response<Producer>() { Error = "IdProducteur can't be equal to 0", Data = null, Succes = true });
                if ((await BsProducer.GetById(idProducer)) == null)
                    return NotFound(new Response<Producer>() { Error = "The Producteur doesn't exist", Data = null, Succes = true });
                return Ok(new Response<Producer>() { Error = "", Data = await BsProducer.Remove(idProducer), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Ajoute un Producteur
        /// </summary>
        /// <param name="producer"></param>       
        /// <response code="200">Succes / Retourne le Producteur ajouté</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<Producer>), 200)]
        [ProducesResponseType(typeof(Response<Producer>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Producer>>> AddProducer([FromBody] Producer producer)
        {
            try
            {
                if (producer == null)
                    return BadRequest(new Response<Producer>() { Error = "The Producteur can't be null", Data = null, Succes = true });
                return Ok(new Response<Producer>() { Error = "", Data = await BsProducer.Add(producer), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}