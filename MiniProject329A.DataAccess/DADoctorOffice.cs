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
    public class DADoctorOffice
    {
        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();
        public DADoctorOffice(MiniProject329AContext _db)
        {
            db = _db;
            VMResponse response = new VMResponse();
        }
        public VMResponse GetAll()
        {
            try
            {
                List<VMTblTDoctorOffice> data = (
                    from dof in db.TblTDoctorOffices
                    join doc in db.TblMDoctors on
                    dof.DoctorId equals doc.Id
                    join bio in db.TblMBiodata on
                    doc.BiodataId equals bio.Id
                    join mf in db.TblMMedicalFacilities on
                    dof.MedicalFacilityId equals mf.Id
                    where dof.IsDelete == false
                    select new VMTblTDoctorOffice
                    {
                        Id = dof.Id,
                        DoctorId = dof.DoctorId,
                        DoctorName = bio.Fullname,
                        MedicalFacilityId = dof.MedicalFacilityId,
                        MedicalFacilityName = mf.Name,
                        Specialization = dof.Specialization,

                        IsDelete = dof.IsDelete,

                        CreatedBy = dof.CreatedBy,
                        CreatedOn = dof.CreatedOn,
                        ModifiedBy = dof.ModifiedBy,
                        ModifiedOn = dof.ModifiedOn,
                        DeletedBy = dof.DeletedBy,
                        DeletedOn = dof.DeletedOn,
                    }
                    ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                    ? $"Data Dokter ditemukan"
                    : $"Data Dokter tidak ada";
                response.statusCode = (data.Count > 0)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
    }
}
