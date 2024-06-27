using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Application.Controller
{
    [Route("[controller]"), ApiController]
    public class OwnerController : BaseController<OwnerController>
    {
        private readonly IOwnerService _ownerService;
        private readonly ILogger<OwnerController> _logger;

        public OwnerController(
            ILogger<OwnerController> logger,
            ILoggerService loggerService,
            IOwnerService ownerService) : base(logger, loggerService)
        {
            _ownerService = ownerService;
        }

        /// <summary>
        /// Creating owners
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> CreateOwner(OwnerCreateRequestModel request)
        {
            BaseResponse<int> response;
            int id;
            try
            {
                id = await _ownerService.CreateOwner(request);
                if (id == 0)
                {
                    response = BaseResponse<int>
                        .Builder()
                        .SetMessage("Falha ao cadastrar um proprietário.")
                        .SetData(0)
                    ;
                    return BadRequest(response);
                }

                response = BaseResponse<int>
                        .Builder()
                        .SetMessage("Proprietário criado com sucesso.")
                        .SetData(id)
                    ;
                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "CreateOwner: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateOwner: Controller");
                return await UntreatedException(ex);
            }
        }

        [HttpPost("Update")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> UpateOwner ( OwnerUpdateRequestModel request )
        {
            BaseResponse<int> response;
            int id;
            try
            {
                id = await _ownerService.UpateOwner(request);
                if (id == 0)
                {
                    response = BaseResponse<int>
                        .Builder()
                        .SetMessage("Falha ao cadastrar um proprietário.")
                        .SetData(0)
                    ;
                    return BadRequest(response);
                }

                response = BaseResponse<int>
                        .Builder()
                        .SetMessage("Proprietário criado com sucesso.")
                        .SetData(id)
                    ;
                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "CreateOwner: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateOwner: Controller");
                return await UntreatedException(ex);
            }
        }


        [/*Authorize(Roles = "owner,admin"),*/ HttpGet("{ownerId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<OwnerResponseModel>>> GetOwnerById(int ownerId)
        {
            OwnerResponseModel owner;
            BaseResponse<OwnerResponseModel> response;
            try
            {
                owner = await _ownerService.GetOwnerById(ownerId);
                response = BaseResponse<OwnerResponseModel>
                        .Builder()
                        .SetMessage("Owner encontrado com sucesso.")
                        .SetData(owner)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "GetOwnerById: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetOwnerById: Controller");
                return await UntreatedException(ex);
            }
        }


        [HttpPost("Desactive/{ownerId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> DesactiveOwner(int ownerId)
        {
            BaseResponse<bool> response;
            try
            {
                bool success = await _ownerService.DesactiveOwner(ownerId);
                response = BaseResponse<bool>
                        .Builder()
                        .SetMessage("Owner encontrado com sucesso.")
                        .SetData(success)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "DesactiveOwner: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DesactiveOwner: Controller");
                return await UntreatedException(ex);
            }
        }

        [HttpPost("Reactive/{ownerId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> ReactiveOwner ( int ownerId)
        {
            BaseResponse<bool> response;
            try
            {
                bool success = await _ownerService.ReactiveOwner(ownerId);
                response = BaseResponse<bool>
                        .Builder()
                        .SetMessage("Owner encontrado com sucesso.")
                        .SetData(success)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "DesactiveOwner: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DesactiveOwner: Controller");
                return await UntreatedException(ex);
            }
        }
    }
}
