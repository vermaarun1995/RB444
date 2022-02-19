using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Data.Repository;
using RB444.Models.Model;
using System;
using System.Threading.Tasks;

namespace RB444.Core.Services
{
    public class CommonService : ICommonService
    {
        private readonly IBaseRepository _baseRepository;
        public CommonService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<CommonReturnResponse> GetAllRolesAsync()
        {            
            try
            {
                var roles = await _baseRepository.GetListAsync<UserRoles>();
                return new CommonReturnResponse
                {
                    Data = roles,
                    Message = roles.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = roles.Count > 0,
                    Status = roles.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }
    }
}
