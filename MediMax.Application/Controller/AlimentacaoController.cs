using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using MediMax.Business.Services.Interfaces;

namespace MediMax.Application.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class AlimentacaoController : BaseController<AlimentacaoController>
    {
        private readonly IAlimentacaoService _alimentacaoService;

        public AlimentacaoController(
            ILogger<AlimentacaoController> logger,
            ILoggerService loggerService,
            IAlimentacaoService alimentacaoService) : base(logger, loggerService)
        {
            _alimentacaoService = alimentacaoService ?? throw new ArgumentNullException(nameof(alimentacaoService));
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BaseResponse<int>>> CriarRefeições(AlimentacaoCreateRequestModel request)
        {
            try
            {
                int id = await _alimentacaoService.CriarRefeições(request);
                if (id == 0)
                {
                    return BadRequest(new BaseResponse<int>
                    {
                        Message = "Falha ao criar uma nova refeição.",
                        Data = 0
                    });
                }

                return Ok(new BaseResponse<int>
                {
                    Message = "Refeição criada com sucesso.",
                    Data = id
                });
            }
            catch (CustomValidationException ex)
            {
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }

        [HttpGet("GetMealsByType/TypeMeals/{typeMeals}")]
        public async Task<ActionResult<BaseResponse<List<AlimentacaoResponseModel>>>> BuscarRefeiçõesPorTipo(string typeMeals)
        {
            try
            {
                List<AlimentacaoResponseModel> alimentacao = await _alimentacaoService.BuscarAlimentacaoPorTipo(typeMeals);
                return Ok(new BaseResponse<List<AlimentacaoResponseModel>>
                {
                    Message = "Alimentos encontrados com sucesso.",
                    Data = alimentacao
                });
            }
            catch (RecordNotFoundException ex)
            {
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }
    }
}
