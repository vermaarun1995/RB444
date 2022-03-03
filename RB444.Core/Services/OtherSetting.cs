using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Data.Repository;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RB444.Core.Services
{
    public class OtherSetting : IOtherSetting
    {
        private readonly IBaseRepository _baseRepository;

        public OtherSetting(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<CommonReturnResponse> AddUpdateLogoAsync(Logo model)
        {
            Logo _logo = null;
            try
            {
                if (model.Id > 0)
                {
                    _logo = await _baseRepository.GetDataByIdAsync<Logo>(model.Id);
                    if (_logo != null)
                    {
                        int _resultId = await _baseRepository.UpdateAsync(model);
                        if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                        return new CommonReturnResponse { Data = null, Message = _resultId > 0 ? MessageStatus.Update : MessageStatus.Error, IsSuccess = _resultId > 0, Status = _resultId > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                    }
                    else
                    {
                        return new CommonReturnResponse { Data = null, Message = MessageStatus.NoRecord, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
                    }
                }
                else
                {
                    var _result = await _baseRepository.InsertAsync(model);
                    if (_result > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse { Data = _result > 0, Message = _result > 0 ? MessageStatus.Create : MessageStatus.Error, IsSuccess = _result > 0, Status = _result > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                }
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : CurrencyService : AddUpdatedAsync()", ex);
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally
            {
                if (_logo != null) { _logo = null; }
            }
        }

        public async Task<CommonReturnResponse> AddUpdateNewsAsync(News model)
        {
            News _news = null;
            try
            {
                if (model.Id > 0)
                {
                    _news = await _baseRepository.GetDataByIdAsync<News>(model.Id);
                    if (_news != null)
                    {
                        int _resultId = await _baseRepository.UpdateAsync(model);
                        if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                        return new CommonReturnResponse { Data = null, Message = _resultId > 0 ? MessageStatus.Update : MessageStatus.Error, IsSuccess = _resultId > 0, Status = _resultId > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                    }
                    else
                    {
                        return new CommonReturnResponse { Data = null, Message = MessageStatus.NoRecord, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
                    }
                }
                else
                {
                    var _result = await _baseRepository.InsertAsync(model);
                    if (_result > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse { Data = _result > 0, Message = _result > 0 ? MessageStatus.Create : MessageStatus.Error, IsSuccess = _result > 0, Status = _result > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                }
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : CurrencyService : AddUpdatedAsync()", ex);
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally
            {
                if (_news != null) { _news = null; }
            }
        }

        public async Task<CommonReturnResponse> AddUpdateSliderAsync(Slider model)
        {
            Slider _slider = null;
            try
            {
                if (model.Id > 0)
                {
                    _slider = await _baseRepository.GetDataByIdAsync<Slider>(model.Id);
                    if (_slider != null)
                    {
                        int _resultId = await _baseRepository.UpdateAsync(model);
                        if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                        return new CommonReturnResponse { Data = null, Message = _resultId > 0 ? MessageStatus.Update : MessageStatus.Error, IsSuccess = _resultId > 0, Status = _resultId > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                    }
                    else
                    {
                        return new CommonReturnResponse { Data = null, Message = MessageStatus.NoRecord, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
                    }
                }
                else
                {
                    var _result = await _baseRepository.InsertAsync(model);
                    if (_result > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse { Data = _result > 0, Message = _result > 0 ? MessageStatus.Create : MessageStatus.Error, IsSuccess = _result > 0, Status = _result > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                }
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : CurrencyService : AddUpdatedAsync()", ex);
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally
            {
                if (_slider != null) { _slider = null; }
            }
        }
    }
}
