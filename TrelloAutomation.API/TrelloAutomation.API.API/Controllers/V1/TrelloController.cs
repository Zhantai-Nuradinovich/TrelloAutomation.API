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

namespace TrelloAutomation.API.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/trello")]//required for default versioning
    [Route("api/v{version:apiVersion}/trello")]
    [ApiController]
    public class TrelloController : Controller
    {
        private readonly ITrelloService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<TrelloController> _logger;

#pragma warning disable CS1591
        public TrelloController(ITrelloService service, IMapper mapper, ILogger<TrelloController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }
#pragma warning restore CS1591

        #region GET
        /// <returns>
        /// Returns true according to the state of trello.
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(bool))]
        [HttpGet("daily/check")]
        public async Task<S.BaseResponse<string[]>> CheckDailyStart() // todo: return messages with Data, etc
        {
            _logger.LogDebug($"TrelloControllers::CheckingDailyStart");

            Request.Headers.TryGetValue("Token", out Microsoft.Extensions.Primitives.StringValues token);
            Request.Headers.TryGetValue("Key", out Microsoft.Extensions.Primitives.StringValues key);
            if(Microsoft.Extensions.Primitives.StringValues.IsNullOrEmpty(token) 
                || Microsoft.Extensions.Primitives.StringValues.IsNullOrEmpty(key))
            {
                return new S.BaseResponse<string[]>()
                {
                    Message = "Token or Key are null or empty"
                }; ;
            }

            _service.SetAuthorization(token, key);
            return await _service.CheckDailyStartAsync();
        }
        
        /// <returns>
        /// Returns true according to the state of trello.
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(bool))]
        [HttpGet("weekly/check")]
        public async Task<S.BaseResponse<string[]>> CheckWeeklyStart()
        {
            _logger.LogDebug($"TrelloControllers::CheckingDailyStart");

            Request.Headers.TryGetValue("Token", out Microsoft.Extensions.Primitives.StringValues token);
            Request.Headers.TryGetValue("Key", out Microsoft.Extensions.Primitives.StringValues key);
            if (Microsoft.Extensions.Primitives.StringValues.IsNullOrEmpty(token)
                || Microsoft.Extensions.Primitives.StringValues.IsNullOrEmpty(key))
            {
                return new S.BaseResponse<string[]>()
                {
                    Message = "Token or Key are null or empty"
                };
            }

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
        //#endregion

        //#region DELETE
        //#endregion

        #region Excepions
        [HttpGet("exception/{message}")]
        [ProducesErrorResponseType(typeof(Exception))]
        public async Task RaiseException(string message)
        {
            _logger.LogDebug($"TrelloControllers::RaiseException::{message}");

            throw new Exception(message);
        }
        #endregion
    }
}
