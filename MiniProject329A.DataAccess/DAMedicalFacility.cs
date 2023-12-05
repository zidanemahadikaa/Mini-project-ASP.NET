using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.DataAccess
{
    public class DAMedicalFacility
    {

        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();
        public DAMedicalFacility(MiniProject329AContext _db)
        {
            db = _db;
        }
        public VMResponse GetByName(string? name = null)
        {
            try
            {
                List<VMTblMMedicalFacility> data = (
                    from mf in db.TblMMedicalFacilities 
                    join mfc in db.TblMMedicalFacilityCategories 
                    on mf.MedicalFacilityCategoryId equals mfc.Id
                    join l in db.TblMLocations on mf.LocationId equals l.Id
                    where mf.IsDelete == false && mf.Name.Contains(name??"")
                    select new VMTblMMedicalFacility
                    {
                        Id=mf.Id,
                        Name=mf.Name,
                        MedicalFacilityCategoryId = mfc.Id,
                        LocationId = l.Id,
                        FullAddress = mf.FullAddress,
                        Email = mf.Email,
                        PhoneCode = mf.PhoneCode,
                        Phone = mf.Phone,
                        Fax = mf.Fax,
                        CreateBy = mf.CreateBy,
                        CreatedOn = mf.CreatedOn,
                        ModifiedBy = mf.ModifiedBy,
                        ModifiedOn = mf.ModifiedOn,
                        DeletedBy = mf.DeletedBy,
                        DeletedOn = mf.DeletedOn,
                        IsDelete = mf.IsDelete
                    }).ToList();
                response.data = data;
                response.message = (data.Count < 1) ?
                    $"Fasilitas Medis {name} Tidak Ada" :
                    $"Fasilitas Medis {name} Berhasil dicari!";
                response.statusCode = (data.Count < 1) ?
                    HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

    }
}
