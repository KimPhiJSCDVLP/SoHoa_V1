using System;
using System.Data;
using System.Linq;
using System.Web.Http.OData;
using G02Apis.Models;

namespace G02Apis.Controllers
{
    public class LogController : ODataController
    {
        private SoHoaEntities db = new SoHoaEntities();
        // GET: odata/Log
        [EnableQuery]
        public IQueryable<NhatKyDTO> GetLogs()
        {
            var user = db.S_Users;
            return db.NhatKies.Select(x => new NhatKyDTO
            {
                NhatKyId = x.NhatKyId,
                VanBanId = x.VanBanId,
                SoVanBan = x.S_VanBan.SoVanBan,
                HanhDong = x.Action,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                CreatedBy = user.Find(x.CreatedBy).UserName,
                UpdatedBy = user.Find(x.CreatedBy).UserName,
            });
        }
    }
}
