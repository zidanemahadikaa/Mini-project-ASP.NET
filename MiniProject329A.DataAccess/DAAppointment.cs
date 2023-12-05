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
    public class DAAppointment
    {
        private readonly MiniProject329AContext db;
        private readonly DAUser user;
        public DAAppointment(MiniProject329AContext _db)
        {
            db = _db;
            user = new DAUser(db);
        }
        public VMResponse response = new VMResponse();
        public VMTblTAppointment? FindById(long? id = null)
        {
            return (
                from app in db.TblTAppointments
                where app.Id == id && app.IsDelete == false
                select new VMTblTAppointment
                {
                    Id = app.Id,
                    CustomerId = app.CustomerId,
                    DoctorOfficeId = app.DoctorOfficeId,
                    DoctorOfficeScheduleId = app.DoctorOfficeScheduleId,
                    DoctorOfficeTreatmentId = app.DoctorOfficeTreatmentId,
                    AppointmentDate = app.AppointmentDate,

                    CreateBy = app.CreateBy,
                    CreatedOn = app.CreatedOn,

                    ModifiedBy = app.ModifiedBy,
                    ModifiedOn = app.ModifiedOn,

                    DeletedBy = app.DeletedBy,
                    DeletedOn = app.DeletedOn,
                }
                ).FirstOrDefault();
        }
        public VMResponse GetById(long? id = null)
        {
            try
            {
                VMTblTAppointment? data = FindById(id);

                response.data = data;
                response.message = (data == null)
                    ? $"Golongan darah dengan id = {id} tidak ada"
                    : $"Golongan darah dengan id = {id} ditemukan";
                response.statusCode = (data == null)
                    ? HttpStatusCode.NoContent
                    : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse GetAll()
        {
            try
            {
                List<VMTblTAppointment> data = (
                    from app in db.TblTAppointments
                    where app.IsDelete == false
                    select new VMTblTAppointment
                    {
                        Id = app.Id,
                        CustomerId = app.CustomerId,
                        DoctorOfficeId = app.DoctorOfficeId,
                        DoctorOfficeScheduleId = app.DoctorOfficeScheduleId,
                        DoctorOfficeTreatmentId = app.DoctorOfficeTreatmentId,
                        AppointmentDate = app.AppointmentDate,

                        CreateBy = app.CreateBy,
                        CreatedOn = app.CreatedOn,

                        ModifiedBy = app.ModifiedBy,
                        ModifiedOn = app.ModifiedOn,

                        DeletedBy = app.DeletedBy,
                        DeletedOn = app.DeletedOn,
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                    ? $"Data  ditemukan"
                    : $"Data Customer tidak ada";
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
        public List<VMTblTAppointment?> FindByParentCustomer(long? id)
        {
            return (
                from app in db.TblTAppointments
                join cus in db.TblMCustomers on
                app.CustomerId equals cus.Id
                join bio in db.TblMBiodata on
                cus.BiodataId equals bio.Id
                join cm in db.TblMCustomerMembers on
                cus.Id equals cm.CustomerId
                join ad in db.TblTAppointmentDones on
                app.Id equals ad.AppointmentId
                join dof in db.TblTDoctorOffices on
                app.DoctorOfficeId equals dof.Id
                join doc in db.TblMDoctors on
                dof.DoctorId equals doc.Id
                join biodoc in db.TblMBiodata on
                doc.BiodataId equals biodoc.Id
                where app.IsDelete == false && cm.ParentBiodataId == id && ad.Diagnosis != null
                select new VMTblTAppointment
                {
                    Id = app.Id,
                    CustomerId = app.CustomerId,
                    CustomerName = bio.Fullname,
                    Parent = cm.ParentBiodataId,
                    DoctorOfficeId = app.DoctorOfficeId,
                    DoctorName = biodoc.Fullname,
                    DoctorOfficeScheduleId = app.DoctorOfficeScheduleId,
                    DoctorOfficeTreatmentId = app.DoctorOfficeTreatmentId,
                    AppointmentDate = app.AppointmentDate,
                    Diagnose = ad.Diagnosis,

                    CreateBy = app.CreateBy,
                    CreatedOn = app.CreatedOn,

                    ModifiedBy = app.ModifiedBy,
                    ModifiedOn = app.ModifiedOn,

                    DeletedBy = app.DeletedBy,
                    DeletedOn = app.DeletedOn,
                }
                ).ToList();
        }
        public VMResponse GetCustomerAppointment(long? id = null)
        {
            List<VMTblTAppointment> data = null;
            try
            {
                data = FindByParentCustomer(id);

                response.data = data;
                response.message = (data == null)
                    ? $"Data tidak ditemukan"
                    : $"Data ada";
                response.statusCode = (data == null)
                    ? HttpStatusCode.NoContent
                    : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse GetByCusDocrName(long parId, string? name)
        {
            List<VMTblTAppointment> ParentId = FindByParentCustomer(parId);
            try
            {
                List<VMTblTAppointment> data = (
                from app in db.TblTAppointments
                join cus in db.TblMCustomers on
                app.CustomerId equals cus.Id
                join bio in db.TblMBiodata on
                cus.BiodataId equals bio.Id
                join cm in db.TblMCustomerMembers on
                cus.Id equals cm.CustomerId
                join ad in db.TblTAppointmentDones on
                app.Id equals ad.AppointmentId
                join dof in db.TblTDoctorOffices on
                app.DoctorOfficeId equals dof.Id
                join doc in db.TblMDoctors on
                dof.DoctorId equals doc.Id
                join biodoc in db.TblMBiodata on
                doc.BiodataId equals biodoc.Id
                where app.IsDelete == false && cm.ParentBiodataId == parId && ad.Diagnosis != null
                && bio.Fullname.Contains(name ?? "") /*&& biodoc.Fullname.Contains(name ?? "")*/
                select new VMTblTAppointment
                {
                    Id = app.Id,
                    CustomerId = app.CustomerId,
                    CustomerName = bio.Fullname,
                    Parent = cm.ParentBiodataId,
                    DoctorOfficeId = app.DoctorOfficeId,
                    DoctorName = biodoc.Fullname,
                    DoctorOfficeScheduleId = app.DoctorOfficeScheduleId,
                    DoctorOfficeTreatmentId = app.DoctorOfficeTreatmentId,
                    AppointmentDate = app.AppointmentDate,
                    Diagnose = ad.Diagnosis,

                    CreateBy = app.CreateBy,
                    CreatedOn = app.CreatedOn,

                    ModifiedBy = app.ModifiedBy,
                    ModifiedOn = app.ModifiedOn,

                    DeletedBy = app.DeletedBy,
                    DeletedOn = app.DeletedOn,
                }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                    ? $"Nama pasien {name} ditemukan"
                    : $"Nama pasien {name} tidak ada";
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
