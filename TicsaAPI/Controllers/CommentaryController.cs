using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.Clients;
using TicsaAPI.BLL.DTO.Commentary;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentaryController : ControllerBase
    {


        private IBsCommentary BsCommentary { get; set; }
        private IBsClient BsClient { get; set; }

        public CommentaryController(IBsCommentary bsCommentary, IBsClient bsClient)
        {
            BsCommentary = bsCommentary;
            BsClient = bsClient;
        }

        /// <summary>
        /// Recupère toute les Commentaires
        /// </summary>
        /// <response code="200">Succes / Retourne toutes les Commentaires</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<DtoCommentary>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<IEnumerable<DtoCommentary>>>> GetAllCommentary()
        {
            try
            {
                return Ok(new Response<IEnumerable<DtoCommentary>>() { Error = "", Data = await BsCommentary.GetAll<DtoCommentary>(), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
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
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoCommentary>>> GetCommentaryById([FromRoute] int idCommentary)
        {
            try
            {
                if (idCommentary == 0)
                    return BadRequest(new Response<string>() { Error = "IdCommentary can't be equal to 0", Succes = true });
                var result = await BsCommentary.GetById<DtoCommentary>(idCommentary);
                if (result == null)
                    return NotFound(new Response<string>() { Error = "The Commentary doesn't exist", Succes = true });
                return Ok(new Response<DtoCommentary>() { Error = "", Data = result, Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
            }
        }

        /// <summary>
        /// Recupère Tout les commentaire d'un client
        /// </summary>
        /// <param name="idClient"></param>  
        /// <response code="200">Succes / Retourne un Commentaire</response>
        /// <response code="400">BadRequest / Un des params est vide</response>
        /// <response code="404">NotFound / L'objet recherché n'existe pas</response>
        /// <response code="500">InternalError / Erreur interne au serveur</response>
        /// <returns></returns>
        [HttpGet]
        [Route("client/{idClient}")]
        [ProducesResponseType(typeof(Response<IEnumerable<Commentary>>), 200)]
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoCommentary>>> GetCommentaryByIdClient([FromRoute] int idClient)
        {
            try
            {
                if (idClient == 0)
                    return BadRequest(new Response<IEnumerable<string>>() { Error = "IdClient can't be equal to 0", Succes = true });
                if ((await BsClient.GetById<DtoClient>(idClient)) == null)
                    return NotFound(new Response<IEnumerable<string>>() { Error = "The Client doesn't exist", Succes = true });
                return Ok(new Response<IEnumerable<DtoCommentary>>() { Error = "", Data = await BsCommentary.GetByIdClient(idClient), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
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
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoCommentary>>> UpdateCommentary([FromRoute] int idCommentary, [FromBody] DtoCommentaryUpdate commentary)
        {
            try
            {
                if (idCommentary == 0)
                    return BadRequest(new Response<string>() { Error = "IdCommentary can't be equal to 0", Succes = true });
                if ((await BsCommentary.GetById<DtoCommentary>(idCommentary)) == null)
                    return NotFound(new Response<string>() { Error = "The Commentary doesn't exist", Succes = true });
                if (commentary == null)
                    return BadRequest(new Response<string>() { Error = "The Commentary can't be null", Succes = true });
                return Ok(new Response<DtoCommentary>() { Error = "", Data = await BsCommentary.Update<DtoCommentary, DtoCommentaryUpdate>(idCommentary, commentary), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
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
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 404)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoCommentary>>> RemoveCommentary([FromRoute] int idCommentary)
        {
            try
            {
                if (idCommentary == 0)
                    return BadRequest(new Response<string>() { Error = "IdCommentary can't be equal to 0", Data = null, Succes = true });
                if ((await BsCommentary.GetById<DtoCommentary>(idCommentary)) == null)
                    return NotFound(new Response<string>() { Error = "The Commentary doesn't exist", Data = null, Succes = true });
                return Ok(new Response<DtoCommentary>() { Error = "", Data = await BsCommentary.Remove<DtoCommentary>(idCommentary), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
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
        [ProducesResponseType(typeof(Response<string>), 400)]
        [ProducesResponseType(typeof(Response<string>), 500)]
        public async Task<ActionResult<Response<DtoCommentary>>> AddCommentary([FromBody] DtoCommentaryAdd commentary)
        {
            try
            {
                if (commentary == null)
                    return BadRequest(new Response<string>() { Error = "The Commentary can't be null", Succes = true });
                return Ok(new Response<DtoCommentary>() { Error = "", Data = await BsCommentary.Add<DtoCommentary, DtoCommentaryAdd>(commentary), Succes = true });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string[]>() { Error = e.Message, Data = e.StackTrace.Split("\r\n"), Succes = false });
            }
        }
    }
}
