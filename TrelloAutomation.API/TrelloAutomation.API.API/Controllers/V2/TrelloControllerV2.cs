using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TrelloAutomation.API.API.DataContracts;
using TrelloAutomation.API.API.DataContracts.Requests;
using TrelloAutomation.API.Services.Contracts;
using S = TrelloAutomation.API.Services.Model;

namespace TrelloAutomation.API.API.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/trello")]
    [ApiController]
    public class TrelloControllerV2 : Controller
    {
        private readonly ITrelloService _service;
        private readonly IMapper _mapper;

#pragma warning disable CS1591
        public TrelloControllerV2(ITrelloService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
#pragma warning restore CS1591

        #region GET
        /// <summary>
        /// Returns a user entity according to the provided Id.
        /// </summary>
        /// <remarks>
        /// XML comments included in controllers will be extracted and injected in Swagger/OpenAPI file.
        /// </remarks>
        /// <returns>
        /// Returns true according to the state of trello.
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(bool))]
        [HttpGet("daily/check")]
        public async Task<bool> CheckDailyStart()
        {
            Request.Headers.TryGetValue("Token", out Microsoft.Extensions.Primitives.StringValues token);
            Request.Headers.TryGetValue("Key", out Microsoft.Extensions.Primitives.StringValues key);

            _service.SetAuthorization(token, key);
            return await _service.CheckDailyStartAsync();
        }
        
        /// <summary>
        /// Returns a user entity according to the provided Id.
        /// </summary>
        /// <remarks>
        /// XML comments included in controllers will be extracted and injected in Swagger/OpenAPI file.
        /// </remarks>
        /// <returns>
        /// Returns true according to the state of trello.
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(bool))]
        [HttpGet("weekly/check")]
        public async Task<bool> CheckWeeklyStart()
        {
            Request.Headers.TryGetValue("Token", out Microsoft.Extensions.Primitives.StringValues token);
            Request.Headers.TryGetValue("Key", out Microsoft.Extensions.Primitives.StringValues key);

            _service.SetAuthorization(token, key);
            return await _service.CheckWeeklyReportAsync();
        }
        #endregion

        #region POST
        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created user.</returns>
        /// <response code="201">Returns the newly created item.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
        public async Task<bool> SetTrelloCredentials()//Кренделя будут браться прямиком из хедера
        {
            return false;
            //_logger.LogDebug($"TrelloControllers::Auth Requets::");

            //if (value == null)
            //    throw new ArgumentNullException("value");
            //if (value.Token == null)
            //    throw new ArgumentNullException("value.Token");
            //if (value.ApiKey == null)
            //    throw new ArgumentNullException("value.ApiKey");

            //return await _service.SetTrelloCredentialsAsync(value.Token, value.ApiKey);
        }
        #endregion

        //#region PUT
        ///// <summary>
        ///// Updates an user entity.
        ///// </summary>
        ///// <remarks>
        ///// No remarks.
        ///// </remarks>
        ///// <param name="parameter"></param>
        ///// <returns>
        ///// Returns a boolean notifying if the user has been updated properly.
        ///// </returns>
        ///// <response code="200">Returns a boolean notifying if the user has been updated properly.</response>
        //[HttpPut()]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        //public async Task<bool> UpdateUser(User parameter)
        //{
        //    if (parameter == null)
        //        throw new ArgumentNullException("parameter");

        //    return await _service.UpdateAsync(_mapper.Map<S.User>(parameter));
        //}
        //#endregion

        //#region DELETE
        ///// <summary>
        ///// Deletes an user entity.
        ///// </summary>
        ///// <remarks>
        ///// No remarks.
        ///// </remarks>
        ///// <param name="id">User Id</param>
        ///// <returns>
        ///// Boolean notifying if the user has been deleted properly.
        ///// </returns>
        ///// <response code="200">Boolean notifying if the user has been deleted properly.</response>
        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        //public async Task<bool> DeleteDevice(string id)
        //{
        //    return await _service.DeleteAsync(id);
        //}
        //#endregion

        #region Excepions
        [HttpGet("exception/{message}")]
        [ProducesErrorResponseType(typeof(Exception))]
        public async Task RaiseException(string message)
        {
            throw new Exception(message);
        }
        #endregion
    }
}
