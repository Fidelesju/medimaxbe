using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.RealTimeServices.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace MediMax.Business.RealTimeServices
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly INotificacaoCreateMapper _notificacaoCreateMapper;
        private readonly INotificacaoRepository _notificacaoRepository;
        private readonly ILoggerService _loggerService;
        public NotificacaoService(
            ILoggerService loggerService,
            INotificacaoCreateMapper notificacaoCreateMapper,
            INotificacaoRepository notificacaoRepository)
        {
            _loggerService = loggerService;
            _notificacaoCreateMapper = notificacaoCreateMapper;
            _notificacaoRepository = notificacaoRepository;
        }

        //public async Task<int> SetToRead(int[] notificationIds)
        //{
        //    Dictionary<string, string> errors;
        //    if (notificationIds.Any(notificationId => notificationId.Equals(0)))
        //    {
        //        errors = new Dictionary<string, string>();
        //        errors.Add("Id", "Esse id não existe");
        //        throw new CustomValidationException(errors);
        //    }
        //    foreach (int notificationId in notificationIds)
        //    {
        //        await _notificationRepository.SetToRead(notificationId);
        //    }
        //    return 1;
        //}

        //public async Task<int> SetToReadAll(int ownerId)
        //{
        //    Dictionary<string, string> errors;
        //    if (ownerId.Equals(0)) // TODO: This validation is absolutely unnecessary and must be removed.
        //    {
        //        errors = new Dictionary<string, string>();
        //        errors.Add("Id", "Esse id não existe");
        //        throw new CustomValidationException(errors);
        //    }
        //    try
        //    {
        //        await _notificationRepository.SetToReadAll(ownerId);
        //        return 1;
        //    }
        //    catch (Exception exception)
        //    {
        //        errors = new Dictionary<string, string>();
        //        errors.Add("Id", exception.Message);
        //        throw new CustomValidationException(errors);
        //    }
        //}


        public async Task<int> RegistarNotificacao(NotificacaoCreateRequestModel request)
        {
            NotificacaoCreateValidation validation;
            Notificacao notificacao;
            validation = new NotificacaoCreateValidation();
            if (!validation.IsValid(request))
            {
                Dictionary<string, string> errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }

            try
            {
                _notificacaoCreateMapper.SetBaseMapping(request);
                notificacao = _notificacaoCreateMapper.BuscarNotificacao();
                _notificacaoRepository.Create(notificacao);
                return notificacao.id;
            }
            catch (DbUpdateException ex)
            {
                Dictionary<string, string> errors = validation.GetPersistenceErrors(ex);
                if (errors.Count > 0)
                {
                    throw new CustomValidationException(errors);
                }
                throw;
            }
        }
    }
}