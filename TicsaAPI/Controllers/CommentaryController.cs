using Microsoft.AspNetCore.Authorization;
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
    public class CommentaryController : ControllerBase
    {


        private IBsCommentary BsCommentary { get; set; }
        public CommentaryController(IBsCommentary bsCommentary)
        {
            BsCommentary = bsCommentary;
        }

        /// <summary>
        /// Recupère toute les Commentaires
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Commentaires</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<Commentary>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<Commentary>>>> GetAllClient()
        {
            try
            {
                return Ok(new Response<IEnumerable<Commentary>>() { Error = "", Data = await BsCommentary.GetAll(), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Recupère un Commentaire en fonction de son Id
        /// </summary>
        /// <param name="idCommentary"></param>  
        /// <response code="200">Succes / Retourne un Commentaire</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{idCommentary}")]
        [ProducesResponseType(typeof(Response<Commentary>), 200)]
        [ProducesResponseType(typeof(Response<Commentary>), 400)]
        [ProducesResponseType(typeof(Response<Commentary>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Commentary>>> GetClientById([FromRoute] int idCommentary)
        {
            try
            {
                if (idCommentary == 0)
                    return BadRequest(new Response<Commentary>() { Error = "IdCommentary can't be equal to 0", Data = null, Succes = true });
                if ((await BsCommentary.GetById(idCommentary)) == null)
                    return NotFound(new Response<Commentary>() { Error = "The Commentary doesn't exist", Data = null, Succes = true });
                return Ok(new Response<Commentary>() { Error = "", Data = await BsCommentary.GetById(idCommentary), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Met a jour un Commentaire
        /// </summary>
        /// <param name="commentary"></param>  
        /// <param name="idCommentary"></param>  
        /// <response code="200">Succes / Retourne le Commentaire modifié</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{idCommentary}")]
        [ProducesResponseType(typeof(Response<Commentary>), 200)]
        [ProducesResponseType(typeof(Response<Commentary>), 400)]
        [ProducesResponseType(typeof(Response<Commentary>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Commentary>>> UpdateClient([FromRoute] int idCommentary, [FromBody] Commentary commentary)
        {
            try
            {
                if (idCommentary == 0)
                    return BadRequest(new Response<Commentary>() { Error = "IdCommentary can't be equal to 0", Data = null, Succes = true });
                if ((await BsCommentary.GetById(idCommentary)) == null)
                    return NotFound(new Response<Commentary>() { Error = "The Commentary doesn't exist", Data = null, Succes = true });
                if (commentary == null)
                    return BadRequest(new Response<Commentary>() { Error = "The Commentary can't be null", Data = null, Succes = true });
                return Ok(new Response<Commentary>() { Error = "", Data = await BsCommentary.Update(idCommentary, commentary), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Supprime un Commentaire
        /// </summary>
        /// <param name="idCommentary"></param>  
        /// <response code="200">Succes / Retourne le Commentaire supprimé</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove/{idCommentary}")]
        [ProducesResponseType(typeof(Response<Commentary>), 200)]
        [ProducesResponseType(typeof(Response<Commentary>), 400)]
        [ProducesResponseType(typeof(Response<Commentary>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Commentary>>> RemoveCommentary([FromRoute] int idCommentary)
        {
            try
            {
                if (idCommentary == 0)
                    return BadRequest(new Response<Commentary>() { Error = "IdCommentary can't be equal to 0", Data = null, Succes = true });
                if ((await BsCommentary.GetById(idCommentary)) == null)
                    return NotFound(new Response<Commentary>() { Error = "The Commentary doesn't exist", Data = null, Succes = true });
                return Ok(new Response<Commentary>() { Error = "", Data = await BsCommentary.Remove(idCommentary), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }

        /// <summary>
        /// Ajoute un Commentaire
        /// </summary>
        /// <param name="commentary"></param>       
        /// <response code="200">Succes / Retourne le Commentaire ajouté</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Response<Commentary>), 200)]
        [ProducesResponseType(typeof(Response<Commentary>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<Commentary>>> AddCommentary([FromBody] Commentary commentary)
        {
            try
            {
                if (commentary == null)
                    return BadRequest(new Response<Commentary>() { Error = "The Commentary can't be null", Data = null, Succes = true });
                return Ok(new Response<Commentary>() { Error = "", Data = await BsCommentary.Add(commentary), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>() { Error = e.Message, Data = e.StackTrace, Succes = false });
            }
        }
    }
}
