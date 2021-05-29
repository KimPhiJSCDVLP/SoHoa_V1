using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamMobile.EntityModels;
using XamMobile.Services.Models;

namespace XamMobile.Services
{
    public class InvoiceService : BaseService, IInvoiceService
    {
        public async Task<List<PhieuThuEntity>> GetInvoices(int phongId)
        {
            try
            {
                var userResponse = await GetRequestWithHandleErrorAsync<OdataResult<List<PhieuThuEntity>>>($"{AppConstant.AppConstant.APIPhieuThu}?$filter=PhongId eq {phongId}");
                if (userResponse.Message.Code == ResponseCode.SUCCESS)
                {
                    return userResponse.Result.Results;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
