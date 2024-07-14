using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MediMax.Application.Controller
{
    [Route("[controller]"), ApiController]
    public class MedicineController : BaseController<MedicineController>
    {
        private readonly IMedicineService _medicineService;
        public MedicineController(
            ILogger<MedicineController> logger,
            ILoggerService loggerService,
            IMedicineService medicineService) : base(logger, loggerService)
        {
            _medicineService = medicineService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BaseResponse<int>>> CreateMedicineAndTreatment(MedicamentoETreatmentCreateRequestModel request)
        {
            BaseResponse<int> response;
            int id;
            try
            {
                id = await _medicineService.CreateMedicineAndTreatment(request);
                if (id == 0)
                {
                    response = BaseResponse<int>
                        .Builder()
                        .SetMessage("Falha ao cadastrar medicamento.")
                        .SetData(0)
                    ;
                    return BadRequest(response);
                }

                response = BaseResponse<int>
                        .Builder()
                        .SetMessage("Medicamento cadastrado com sucesso.")
                        .SetData(id)
                    ;
                return Ok(response);
            }
            catch (CustomValidationException exception)
            {
                return ValidationErrorsBadRequest(exception);
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }


        [/*Authorize(Roles = "owner,admin"),*/ HttpGet("GetAllMedicine")]
        public async Task<ActionResult<BaseResponse<List<MedicineResponseModel>>>> GetAllMedicine()
        {
            List<MedicineResponseModel> medicine;
            BaseResponse<List<MedicineResponseModel>> response;
            try
            {
                medicine = await _medicineService.GetAllMedicine();
                response = BaseResponse<List<MedicineResponseModel>>
                        .Builder()
                        .SetMessage("Medicamentos encontrados com sucesso.")
                        .SetData(medicine)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException exception)
            {
                return await NotFoundResponse(exception);
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }

        //[/*Authorize(Roles = "owner,admin"),*/ HttpGet("Name/{name}")]
        //public async Task<ActionResult<BaseResponse<UserResponseModel>>> GetUserByName(string name)
        //{
        //    UserResponseModel user;
        //    BaseResponse<UserResponseModel> response;
        //    try
        //    {
        //        user = await _medicineService.GetUserByName(name);
        //        response = BaseResponse<UserResponseModel>
        //                .Builder()
        //                .SetMessage("Usuário encontrado com sucesso.")
        //                .SetData(user)
        //            ;
        //        return Ok(response);
        //    }
        //    catch (RecordNotFoundException exception)
        //    {
        //        return await NotFoundResponse(exception);
        //    }
        //    catch (Exception exception)
        //    {
        //        return await UntreatedException(exception);
        //    }
        //}
    }
}
