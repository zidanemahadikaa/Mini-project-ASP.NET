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
    public class DACurrentDoctorSpecialization
    {
        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();
        public DACurrentDoctorSpecialization(MiniProject329AContext _db)
        {
            db = _db;
        }

        private VMTblTCurrentDoctorSpecialization? FindById(long id)
        {
            return (
                from cds in db.TblTCurrentDoctorSpecializations
                join spec in db.TblMSpecializations on cds.SpecializationId equals spec.Id
                join doc in db.TblMDoctors on cds.DoctorId equals doc.Id
                where cds.IsDelete == false && cds.Id == id
                select new VMTblTCurrentDoctorSpecialization
                {
                    Id=cds.Id,
                    DoctorId=doc.Id,
                    SpecializationId=spec.Id,
                    CreatedBy=cds.CreatedBy,
                    CreatedOn=cds.CreatedOn,
                    ModifiedBy=cds.ModifiedBy,
                    ModifiedOn=cds.ModifiedOn,
                    DeletedBy=cds.DeletedBy,
                    DeletedOn=cds.DeletedOn,
                    IsDelete=cds.IsDelete,
                }).FirstOrDefault();
        }
        public VMResponse GetDoctorId(long? id)
        {
            try
            {
                VMTblTCurrentDoctorSpecialization? data = (
                 from cds in db.TblTCurrentDoctorSpecializations
                 join spec in db.TblMSpecializations on cds.SpecializationId equals spec.Id
                 join doc in db.TblMDoctors on cds.DoctorId equals doc.Id
                 where cds.IsDelete == false && doc.Id == id
                 select new VMTblTCurrentDoctorSpecialization
                 {
                     Id = cds.Id,
                     DoctorId = doc.Id,
                     SpecializationId = spec.Id,
                     CreatedBy = cds.CreatedBy,
                     CreatedOn = cds.CreatedOn,
                     ModifiedBy = cds.ModifiedBy,
                     ModifiedOn = cds.ModifiedOn,
                     DeletedBy = cds.DeletedBy,
                     DeletedOn = cds.DeletedOn,
                     IsDelete = cds.IsDelete,
                 }).FirstOrDefault();
                response.data = data;
                response.message = (data == null) ?
                    $"Id {id} Tidak Ada dalam Current Doctor Specialization" :
                    $"Current Doctor Specialization didapatkan!";
                response.statusCode = (data == null) ?
                    HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse GetByNameSpecialization(string? name = null)
        {
            try
            {
                List<VMTblTCurrentDoctorSpecialization> data = (
                    from cds in db.TblTCurrentDoctorSpecializations
                    join spec in db.TblMSpecializations on cds.SpecializationId equals spec.Id
                    join doc in db.TblMDoctors on cds.DoctorId equals doc.Id
                    where cds.IsDelete == false && spec.Name.Contains(name??"")
                    select new VMTblTCurrentDoctorSpecialization
                    {
                        Id = cds.Id,
                        DoctorId = doc.Id,
                        SpecializationId = spec.Id,
                        CreatedBy = cds.CreatedBy,
                        CreatedOn = cds.CreatedOn,
                        ModifiedBy = cds.ModifiedBy,
                        ModifiedOn = cds.ModifiedOn,
                        DeletedBy = cds.DeletedBy,
                        DeletedOn = cds.DeletedOn,
                        IsDelete = cds.IsDelete,
                    }).ToList();
                response.data = data;
                response.message = (data == null) ?
                    $"Spesialis {name} Tidak Ada" :
                    $"Spesialis {name} Didapatkan!";
                response.statusCode = (data == null) ?
                    HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse GetSpecializationId(long? id)
        {
            try
            {
                VMTblTCurrentDoctorSpecialization? data = (
                 from cds in db.TblTCurrentDoctorSpecializations
                 join spec in db.TblMSpecializations on cds.SpecializationId equals spec.Id
                 join doc in db.TblMDoctors on cds.DoctorId equals doc.Id
                 where cds.IsDelete == false && spec.Id == id
                 select new VMTblTCurrentDoctorSpecialization
                 {
                     Id = cds.Id,
                     DoctorId = doc.Id,
                     SpecializationId = spec.Id,
                     CreatedBy = cds.CreatedBy,
                     CreatedOn = cds.CreatedOn,
                     ModifiedBy = cds.ModifiedBy,
                     ModifiedOn = cds.ModifiedOn,
                     DeletedBy = cds.DeletedBy,
                     DeletedOn = cds.DeletedOn,
                     IsDelete = cds.IsDelete,
                 }).FirstOrDefault();
                response.data = data;
                response.message = (data == null) ?
                    $"Id {id} Tidak Ada dalam Current Doctor Specialization" :
                    $"Current Doctor Specialization didapatkan!";
                response.statusCode = (data == null) ?
                    HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse GetById(long? id)
        {
            try
            {
                VMTblTCurrentDoctorSpecialization? data = (
                 from cds in db.TblTCurrentDoctorSpecializations
                 join spec in db.TblMSpecializations on cds.SpecializationId equals spec.Id
                 join doc in db.TblMDoctors on cds.DoctorId equals doc.Id
                 where cds.IsDelete == false && cds.Id == id
                 select new VMTblTCurrentDoctorSpecialization
                 {
                     Id = cds.Id,
                     DoctorId = doc.Id,
                     SpecializationId = spec.Id,
                     CreatedBy = cds.CreatedBy,
                     CreatedOn = cds.CreatedOn,
                     ModifiedBy = cds.ModifiedBy,
                     ModifiedOn = cds.ModifiedOn,
                     DeletedBy = cds.DeletedBy,
                     DeletedOn = cds.DeletedOn,
                     IsDelete = cds.IsDelete,
                 }).FirstOrDefault();
                response.data = data;
                response.message = (data == null) ?
                    $"Id {id} Tidak Ada dalam Current Doctor Specialization" :
                    $"Current Doctor Specialization didapatkan!";
                response.statusCode = (data == null) ?
                    HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse Add(VMTblTCurrentDoctorSpecialization dataInput)
        {
            try
            {
            TblTCurrentDoctorSpecialization data = new TblTCurrentDoctorSpecialization();
                data.DoctorId = dataInput.DoctorId;
                data.SpecializationId = dataInput.SpecializationId;
                data.CreatedBy = dataInput.CreatedBy;
                data.CreatedOn = DateTime.Now;
                data.ModifiedBy = dataInput.ModifiedBy;
                data.ModifiedOn = dataInput.ModifiedOn;
                data.DeletedBy = dataInput.DeletedBy;
                data.DeletedOn = dataInput.DeletedOn;
                data.IsDelete = false;

                db.Update(data);
                db.SaveChanges();
                response.statusCode = HttpStatusCode.OK;
                response.data = data;
                response.message = "Current Doctor Specialization Berhasil ditambahkan!";
            }
            catch
            {
                response.message = "Gagal Menambahkan Current Doctor Specialization";
            }
            return response;
        }
         public VMResponse Edit(VMTblTCurrentDoctorSpecialization dataInput)
        {
            try
            {   
                VMTblTCurrentDoctorSpecialization? existingData = FindById(dataInput.Id);
                TblTCurrentDoctorSpecialization data = new TblTCurrentDoctorSpecialization();
                
                data.Id = existingData.Id;
                data.SpecializationId = existingData.SpecializationId;
                data.CreatedBy = existingData.CreatedBy;
                data.CreatedOn = existingData.CreatedOn;
                data.ModifiedBy = dataInput.ModifiedBy;
                data.ModifiedOn = DateTime.Now;
                data.DeletedBy = existingData.DeletedBy;
                data.DeletedOn = existingData.DeletedOn;
                data.IsDelete = false;

                db.Update(data);
                db.SaveChanges();

                response.statusCode = HttpStatusCode.OK;
                response.data = data;
                response.message = "Current Specialization Berhasil diedit!";
            }
            catch
            {
                response.message = "Gagal Edit Current Doctor Specialization";
            }
            return response;
         }
        public VMResponse Delete(long id, long userId)
        {
            try
            {
                VMTblTCurrentDoctorSpecialization? existingData = FindById(id);
                if (existingData != null)
                {
                    TblTCurrentDoctorSpecialization data = new TblTCurrentDoctorSpecialization();

                    data.Id = existingData.Id;
                    data.SpecializationId = existingData.SpecializationId;
                    data.CreatedBy = existingData.CreatedBy;
                    data.CreatedOn = existingData.CreatedOn;
                    data.ModifiedBy = existingData.ModifiedBy;
                    data.ModifiedOn = existingData.ModifiedOn;
                    data.DeletedBy = userId;
                    data.DeletedOn = DateTime.Now;
                    data.IsDelete = true;
                    db.Update(data);
                    db.SaveChanges();
                    response.data = data;

                    response.message = ($"Current Specialization berhasil dihapus");
                    response.statusCode = HttpStatusCode.OK;
                }
            }
            catch
            {
                response.message = $"Current Specialization Gagal Dihapus";
            }

            return response;
        }


    }
}
