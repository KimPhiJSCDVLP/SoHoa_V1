using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamMobile.EntityModels;

namespace XamMobile.Services
{
    public interface IInvoiceService
    {
        Task<List<PhieuThuEntity>> GetInvoices(int hoGiaDinhId);
    }
}
