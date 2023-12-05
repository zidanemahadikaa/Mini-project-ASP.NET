using Microsoft.IdentityModel.Tokens;
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
    public class DADoctorEducation
    {
        private readonly MiniProject329AContext db;
        private VMResponse response;

        public DADoctorEducation(MiniProject329AContext _db)
        {
            db = _db;
            response = new VMResponse();
        }

        //Get All
        public VMResponse GetAll()
        {
            try
            {
                ////Get All Doctor Education
                //List<VMTblMDoctorEducation> data = (
                //    from de in db.TblMDoctorEducations
                //    join d in db.TblMDoctors on de.DoctorId equals d.Id
                //    join el in db.TblMEducationLevels on de.EducationLevelId equals el.Id
                //    join bio in db.TblMBiodata on d.BiodataId equals bio.Id
                //    where de.IsDelete == false
                //        && de.DoctorId == 2
                //    select new VMTblMDoctorEducation
                //    {
                //        Id = de.Id,

                //        DoctorId = d.Id,
                //        EducationLevelId = el.Id,

                //        InstituteName = de.InstituteName,
                //        Major = de.Major,
                //        StartYear = de.StartYear,
                //        EndYear = de.EndYear,
                //        IsLastEducation = false,

                //        CreateBy = de.CreateBy,
                //        CreatedOn = DateTime.Now,

                //        ModifiedBy = de.ModifiedBy,
                //        ModifiedOn = de.ModifiedOn,

                //        DeletedBy = de.DeletedBy,
                //        DeletedOn = de.DeletedOn,

                //        IsDelete = de.IsDelete,

                //        EducationLevel = (
                //            from el in db.TblMEducationLevels
                //            join de in db.TblMDoctorEducations on el.Id equals de.EducationLevelId
                //            where el.IsDelete == false
                //                && el.Id == de.EducationLevelId
                //            select new VMTblMEducationLevel
                //            {
                //                Id = el.Id,
                //                Name = el.Name
                //            }).FirstOrDefault(),

                //        Doctor = (
                //            from d in db.TblMDoctors
                //            join bio in db.TblMBiodata on d.BiodataId equals bio.Id
                //            where d.IsDelete == false
                //            select new VMTblMDoctor
                //            {
                //                Id = d.Id,
                //                BiodataId = bio.Id,

                //                Str = d.Str,

                //                Biodata = (
                //                from bio in db.TblMBiodata
                //                join d in db.TblMDoctors on bio.Id equals d.BiodataId
                //                where bio.IsDelete == false
                //                 && bio.Id == d.BiodataId
                //                select new VMTblMBiodata
                //                {
                //                    Id = bio.Id,
                //                    Fullname = bio.Fullname,
                //                    MobilePhone = bio.MobilePhone,
                //                    Image = bio.Image,
                //                    ImagePath = bio.ImagePath
                //                }).FirstOrDefault()

                //            }).FirstOrDefault()
                //    }).ToList();

                //response.data = data;
                //response.message = (data.Count < 1) ? "Pendidikan Dokter tidak ditemukan" : "Pendidikan Dokter ditemukan";
                //response.statusCode = (data.Count < 1) ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        //Create Update Data
        public VMResponse CreateUpdate(VMTblMDoctorEducation dataInput)
        {
            string strCreateUpdate = "";
            try
            {
                //Prepare empty Data Education level for Incoming Data
                TblMDoctorEducation data = new TblMDoctorEducation();

                //Data must Input
                data.DoctorId = dataInput.DoctorId;
                data.EducationLevelId = dataInput.EducationLevelId;
                data.InstituteName = dataInput.InstituteName;
                data.Major = dataInput.Major;
                data.StartYear = dataInput.StartYear;
                data.EndYear = dataInput.EndYear;

                data.IsDelete = false;

                if (dataInput.Id == null || dataInput.Id == 0)
                {
                    strCreateUpdate = "Buat";

                    //Assign each Data Model column from each Input Data
                    data.CreateBy = 1;
                    data.CreatedOn = DateTime.Now;

                    //Update New Data
                    db.Add(data);
                    response.statusCode = HttpStatusCode.Created;

                }
                else
                {
                    strCreateUpdate = "Ubah";

                    VMTblMDoctorEducation? existingData = FindById(dataInput.Id);

                    //If Data Exist
                    if(existingData != null)
                    {
                        data.Id = existingData.Id;

                        data.CreateBy = existingData.CreateBy;
                        data.CreatedOn = existingData.CreatedOn;

                        data.ModifiedBy = 1;
                        data.ModifiedOn = DateTime.Now;

                        data.DeletedBy = existingData.DeletedBy;
                        data.DeletedOn = existingData.DeletedOn;

                        db.Update(data);
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        throw new Exception($"Pendidikan dokter gagal di {strCreateUpdate}");
                    }
                }
                //Save Data to Database
                db.SaveChanges();

                //Update API Respon
                response.data = data;
                response.message = $"Pendidikan dokter berhasil di {strCreateUpdate}";
            }
            catch(Exception ex)
            {
                response.message = $"Pendidikan dokter gagal di {strCreateUpdate}" + ex.Message;
            }
            finally
            {
                db.Dispose();
            }
            return response;
        }
        //Read data
        private VMTblMDoctorEducation? FindById(long? Id)
        {
            return
            (
                from de in db.TblMDoctorEducations
                where de.IsDelete == false
                    && de.Id == Id
                select new VMTblMDoctorEducation
                {
                    Id = de.Id,
                    DoctorId = de.DoctorId,
                    EducationLevelId = de.EducationLevelId,


                    InstituteName = de.InstituteName,
                    Major = de.Major,
                    StartYear = de.StartYear,
                    EndYear = de.EndYear,
                    IsLastEducation = de.IsLastEducation,

                    CreateBy = de.CreateBy,
                    CreatedOn = de.CreatedOn,

                    ModifiedBy = de.ModifiedBy,
                    ModifiedOn = de.ModifiedOn,

                    DeletedBy = de.DeletedBy,
                    DeletedOn = de.DeletedOn,
                    IsDelete = de.IsDelete,

                }).FirstOrDefault();
        }
        //Delete Data
        public VMResponse Delete(long? id, int userId)
        {
            VMTblMDoctorEducation? existingData = FindById(id);

            try
            {
                if(existingData != null)
                {

                }
                else
                {

                }
            }
            catch(Exception ex)
            {

            }
            return response;
        }
    }
}
