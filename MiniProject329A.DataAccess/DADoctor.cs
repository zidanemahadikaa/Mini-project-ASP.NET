
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
    public class DADoctor
    {
        private readonly MiniProject329AContext db;

        public DADoctor(MiniProject329AContext _db)
        {
            db = _db;
        }
        private VMResponse response = new VMResponse();

        public VMResponse GetAll()
        {
            try
            {
                //Get All Doctor Data
                VMTblMDoctor? data = (
                    from d in db.TblMDoctors
                    join bio in db.TblMBiodata on d.BiodataId equals bio.Id
                    where d.IsDelete == false
                    select new VMTblMDoctor
                    {
                        Id = d.Id,
                        BiodataId = bio.Id,
                        Image_Path = bio.ImagePath,

                        Str = d.Str,

                        CreateBy = 1,
                        CreateOn = d.CreateOn,

                        ModifiedBy = d.ModifiedBy,
                        ModifiedOn = d.ModifiedOn,

                        DeletedBy = d.DeletedBy,
                        DeletedOn = d.DeletedOn,
                        IsDelete = false,

                        DoctorEducation = (
                        from de in db.TblMDoctorEducations
                        join d in db.TblMDoctors on de.DoctorId equals d.Id
                        join el in db.TblMEducationLevels on de.EducationLevelId equals el.Id
                        where de.IsDelete == false
                        select new VMTblMDoctorEducation
                        {
                            Id = de.Id,
                            DoctorId = d.Id,
                            EducationLevelId = el.Id,
                            EducationLevelName = el.Name,

                            InstituteName = de.InstituteName,
                            Major = de.Major,
                            StartYear = de.StartYear,
                            EndYear = de.EndYear,

                            CreateBy = de.CreateBy,
                            CreatedOn = de.CreatedOn,

                            ModifiedBy = de.ModifiedBy,
                            ModifiedOn = de.ModifiedOn,

                            DeletedBy = de.DeletedBy,
                            DeletedOn = de.DeletedOn,

                            IsDelete = false

                        }).ToList(),

                        CurrentDoctorSpecialization = (
                        from cds in db.TblTCurrentDoctorSpecializations
                        join d in db.TblMDoctors on cds.DoctorId equals d.Id
                        join s in db.TblMSpecializations on cds.SpecializationId equals s.Id
                        where cds.IsDelete == false
                        select new VMTblTCurrentDoctorSpecialization
                        {
                            Id = cds.Id,
                            DoctorId = d.Id,
                            SpecializationId = s.Id,

                            CreatedBy = cds.CreatedBy,
                            CreatedOn = cds.CreatedOn,

                            ModifiedBy = cds.ModifiedBy,
                            ModifiedOn = cds.ModifiedOn,

                            DeletedBy = cds.DeletedBy,
                            DeletedOn = cds.DeletedOn,

                            IsDelete = false

                        }).ToList(),

                        Biodata = (
                        from b in db.TblMBiodata
                        where b.IsDelete == false
                        select new VMTblMBiodata
                        {
                            Id = b.Id,
                            Fullname = b.Fullname,
                            MobilePhone = b.MobilePhone,
                            Image = b.Image,
                            ImagePath = b.ImagePath,

                            CreateBy = b.CreateBy,
                            CreateOn = b.CreateOn,

                            ModifiedBy = b.ModifiedBy,
                            ModifiedOn = b.ModifiedOn,

                            IsDelete = false
                        }).FirstOrDefault(),

                        DoctorOfficeSchedule = (
                        from dos in db.TblTDoctorOfficeSchedules
                        join d in db.TblMDoctors on dos.DoctorId equals d.Id
                        join mfs in db.TblMMedicalFacilitySchedules on dos.MedicalFacilityScheduleId equals mfs.Id
                        where dos.IsDelete == false
                        select new VMTblTDoctorOfficeSchedule
                        {
                            Id = dos.Id,
                            DoctorId = d.Id,
                            MedicalFacilityScheduleId = mfs.Id,
                            Slot = dos.Slot,

                            CreatedBy = dos.CreatedBy,
                            CreatedOn = dos.CreatedOn,

                            ModifiedBy = dos.ModifiedBy,
                            ModifiedOn = dos.ModifiedOn,

                            DeletedBy = dos.DeletedBy,
                            DeletedOn = dos.DeletedOn,

                            IsDelete = false,

                            Appointment = (
                            from a in db.TblTAppointments
                            join c in db.TblMCustomers on a.CustomerId equals c.Id
                            join dof in db.TblTDoctorOffices on a.DoctorOfficeId equals dof.Id
                            join dos in db.TblTDoctorOfficeSchedules on a.DoctorOfficeScheduleId equals dos.Id
                            join dot in db.TblTDoctorOfficeTreatments on a.DoctorOfficeTreatmentId equals dot.Id
                            where a.IsDelete == false
                            select new VMTblTAppointment
                            {
                                Id = a.Id,
                                CustomerId = c.Id,
                                DoctorOfficeId = dof.Id,
                                DoctorOfficeScheduleId = dos.Id,
                                DoctorOfficeTreatmentId = dot.Id,
                                AppointmentDate = a.AppointmentDate,

                                CreateBy = a.CreateBy,
                                CreatedOn = a.CreatedOn,

                                ModifiedBy = a.ModifiedBy,
                                ModifiedOn = a.ModifiedOn,

                                DeletedBy = a.DeletedBy,
                                DeletedOn = a.DeletedOn,

                                IsDelete = false,

                                AppointmentCancellation = (
                                from ac in db.TblTAppointmentCancellations
                                join a in db.TblTAppointments on ac.AppointmentId equals a.Id
                                where ac.IsDelete == false
                                select new VMTblTAppointmentCancellation
                                {
                                    Id = ac.Id,
                                    AppointmentId = a.Id,

                                    CreateBy = ac.CreateBy,
                                    CreatedOn = ac.CreatedOn,

                                    ModifiedBy = ac.ModifiedBy,
                                    ModifiedOn = ac.ModifiedOn,

                                    DeletedBy = ac.DeletedBy,
                                    DeletedOn = ac.DeletedOn,

                                    IsDelete = false
                                }).ToList(),

                                AppointmentDone = (
                                from ad in db.TblTAppointmentDones
                                join a in db.TblTAppointments on ad.AppointmentId equals a.Id
                                where ad.IsDelete == false
                                select new VMTblTAppointmentDone
                                {
                                    Id = ad.Id,
                                    AppointmentId = a.Id,
                                    Diagnosis = ad.Diagnosis,

                                    CreatedBy = ad.CreatedBy,
                                    CreatedOn = ad.CreatedOn,

                                    ModifiedBy = ad.ModifiedBy,
                                    ModifiedOn = ad.ModifiedOn,

                                    DeletedBy = ad.DeletedBy,
                                    DeletedOn = ad.DeletedOn,

                                    IsDelete = false
                                }).ToList()
                            }).ToList(),

                            MedicalFacilitySchedule = (
                            from mfs in db.TblMMedicalFacilitySchedules
                            join mf in db.TblMMedicalFacilities on mfs.MedicalFacilityId equals mf.Id
                            where mfs.IsDelete == false
                            select new VMTblMMedicalFacilitySchedule
                            {
                                Id = mfs.Id,
                                MedicalFacilityId = mf.Id,
                                Day = mfs.Day,
                                TimeScheduleStart = mfs.TimeScheduleStart,
                                TimeScheduleEnd = mfs.TimeScheduleEnd,

                                CreateBy = mfs.CreateBy,
                                CreatedOn = mfs.CreatedOn,

                                ModifiedBy = mfs.ModifiedBy,
                                ModifiedOn = mfs.ModifiedOn,

                                DeletedBy = mfs.DeletedBy,
                                DeletedOn = mfs.DeletedOn,

                                IsDelete = false

                            }).ToList()

                        }).ToList(),

                        DoctorOffice = (
                        from dof in db.TblTDoctorOffices
                        join d in db.TblMDoctors on dof.DoctorId equals d.Id
                        join mf in db.TblMMedicalFacilities on dof.MedicalFacilityId equals mf.Id
                        where dof.IsDelete == false
                        select new VMTblTDoctorOffice
                        {
                            Id = dof.Id,
                            DoctorId = d.Id,
                            MedicalFacilityId = mf.Id,
                            Specialization = dof.Specialization,
                            StartDate = dof.StartDate,
                            EndDate = dof.EndDate,

                            CreatedBy = dof.CreatedBy,
                            CreatedOn = dof.CreatedOn,

                            ModifiedBy = dof.ModifiedBy,
                            ModifiedOn = dof.ModifiedOn,

                            DeletedBy = dof.DeletedBy,
                            DeletedOn = dof.DeletedOn,

                            IsDelete = false,

                            MedicalFacility = (
                            from mf in db.TblMMedicalFacilities
                            join mfc in db.TblMMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                            join l in db.TblMLocations on mf.LocationId equals l.Id
                            where mf.IsDelete == false
                            select new VMTblMMedicalFacility
                            {
                                Id = mf.Id,
                                Name = mf.Name,
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

                                IsDelete = false,

                                Location = (
                                from l in db.TblMLocations
                                join ll in db.TblMLocationLevels on l.LocationLevelId equals ll.Id
                                where l.IsDelete == false
                                select new VMTblMLocation
                                {
                                    Id = l.Id,
                                    Name = l.Name,
                                    ParentId = l.ParentId,
                                    LocationLevelId = ll.Id,

                                    CreateBy = l.CreateBy,
                                    CreatedOn = l.CreatedOn,

                                    ModifiedBy = l.ModifiedBy,
                                    ModifiedOn = l.ModifiedOn,

                                    DeletedBy = l.DeletedBy,
                                    DeletedOn = l.DeletedOn,

                                    IsDelete = false

                                    //LocationLevel = (
                                    //from ll in db.TblMLocationLevels
                                    //where ll.IsDelete == false
                                    //select new VMTblMLocationLevel
                                    //{
                                    //    Id = ll.Id,
                                    //    Name = ll.Name,
                                    //    Abbreviation = ll.Abbreviation,

                                    //    CreateBy = ll.CreateBy,
                                    //    CreatedOn = ll.CreatedOn,

                                    //    ModifiedBy = ll.ModifiedBy,
                                    //    ModifiedOn = ll.ModifiedOn,

                                    //    DeletedBy = ll.DeletedBy,
                                    //    DeletedOn = ll.DeletedOn,

                                    //    IsDelete = false
                                    //}).ToList()
                                }).ToList()

                            }).ToList()
                        }).ToList()

                    }).FirstOrDefault();

                response.data = data;
                response.message = (data == null) 
                    ? "Data Dokter tidak Ada" 
                    : "Data Dokter berhasil ditemukan";
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

        public VMTblMDoctor? FindById(long id)
        {
            return (
                from d in db.TblMDoctors
                join bio in db.TblMBiodata on d.BiodataId equals bio.Id
                where d.IsDelete == false
                    && d.Id == id
                select new VMTblMDoctor
                {
                    Id = d.Id,
                    BiodataId = bio.Id,
                    Image_Path = bio.ImagePath,

                    Str = d.Str,

                    CreateBy = 1,
                    CreateOn = d.CreateOn,

                    ModifiedBy = d.ModifiedBy,
                    ModifiedOn = d.ModifiedOn,

                    DeletedBy = d.DeletedBy,
                    DeletedOn = d.DeletedOn,
                    IsDelete = false,

                    DoctorEducation = (
                    from de in db.TblMDoctorEducations
                    join d in db.TblMDoctors on de.DoctorId equals d.Id
                    join el in db.TblMEducationLevels on de.EducationLevelId equals el.Id
                    where de.IsDelete == false
                        && d.Id == id
                    select new VMTblMDoctorEducation
                    {
                        Id = de.Id,
                        DoctorId = d.Id,
                        EducationLevelId = el.Id,
                        EducationLevelName = el.Name,

                        InstituteName = de.InstituteName,
                        Major = de.Major,
                        StartYear = de.StartYear,
                        EndYear = de.EndYear,

                        CreateBy = de.CreateBy,
                        CreatedOn = de.CreatedOn,

                        ModifiedBy = de.ModifiedBy,
                        ModifiedOn = de.ModifiedOn,

                        DeletedBy = de.DeletedBy,
                        DeletedOn = de.DeletedOn,

                        IsDelete = false,

                        //EducationLevel = (
                        //from el in db.TblMEducationLevels
                        //where el.IsDelete == false
                        //select new VMTblMEducationLevel
                        //{
                        //    Id = el.Id,
                        //    Name = el.Name,

                        //    CreateBy = el.CreateBy,
                        //    CreatedOn = el.CreatedOn,

                        //    ModifiedBy = el.ModifiedBy,
                        //    ModifiedOn = el.ModifiedOn,

                        //    DeletedBy = el.DeletedBy,
                        //    DeletedOn = el.DeletedOn,

                        //    IsDelete = false
                        //}).FirstOrDefault()

                    }).ToList(),

                    CurrentDoctorSpecialization = (
                    from cds in db.TblTCurrentDoctorSpecializations
                    join d in db.TblMDoctors on cds.DoctorId equals d.Id
                    join s in db.TblMSpecializations on cds.SpecializationId equals s.Id
                    where cds.IsDelete == false && d.Id == id
                    select new VMTblTCurrentDoctorSpecialization
                    {
                        Id = cds.Id,
                        DoctorId = d.Id,
                        SpecializationId = s.Id,
                        SpecializationName = s.Name,

                        CreatedBy = cds.CreatedBy,
                        CreatedOn = cds.CreatedOn,

                        ModifiedBy = cds.ModifiedBy,
                        ModifiedOn = cds.ModifiedOn,

                        DeletedBy = cds.DeletedBy,
                        DeletedOn = cds.DeletedOn,

                        IsDelete = false

                    }).ToList(),

                    Biodata = (
                    from b in db.TblMBiodata
                    where b.IsDelete == false
                    select new VMTblMBiodata
                    {
                        Id = b.Id,
                        Fullname = b.Fullname,
                        MobilePhone = b.MobilePhone,
                        Image = b.Image,
                        ImagePath = b.ImagePath,

                        CreateBy = b.CreateBy,
                        CreateOn = b.CreateOn,

                        ModifiedBy = b.ModifiedBy,
                        ModifiedOn = b.ModifiedOn,

                        IsDelete = false
                    }).FirstOrDefault(),

                    DoctorOfficeSchedule = (
                    from dos in db.TblTDoctorOfficeSchedules
                    join d in db.TblMDoctors on dos.DoctorId equals d.Id
                    join mfs in db.TblMMedicalFacilitySchedules on dos.MedicalFacilityScheduleId equals mfs.Id
                    where dos.IsDelete == false && d.Id == id
                    select new VMTblTDoctorOfficeSchedule
                    {
                        Id = dos.Id,
                        DoctorId = d.Id,
                        MedicalFacilityScheduleId = mfs.Id,
                        Slot = dos.Slot,

                        CreatedBy = dos.CreatedBy,
                        CreatedOn = dos.CreatedOn,

                        ModifiedBy = dos.ModifiedBy,
                        ModifiedOn = dos.ModifiedOn,

                        DeletedBy = dos.DeletedBy,
                        DeletedOn = dos.DeletedOn,

                        IsDelete = false,

                        Appointment = (
                        from a in db.TblTAppointments
                        join c in db.TblMCustomers on a.CustomerId equals c.Id
                        join dof in db.TblTDoctorOffices on a.DoctorOfficeId equals dof.Id
                        join dos in db.TblTDoctorOfficeSchedules on a.DoctorOfficeScheduleId equals dos.Id
                        join dot in db.TblTDoctorOfficeTreatments on a.DoctorOfficeTreatmentId equals dot.Id
                        where a.IsDelete == false
                        select new VMTblTAppointment
                        {
                            Id = a.Id,
                            CustomerId = c.Id,
                            DoctorOfficeId = dof.Id,
                            DoctorOfficeScheduleId = dos.Id,
                            DoctorOfficeTreatmentId = dot.Id,
                            AppointmentDate = a.AppointmentDate,

                            CreateBy = a.CreateBy,
                            CreatedOn = a.CreatedOn,

                            ModifiedBy = a.ModifiedBy,
                            ModifiedOn = a.ModifiedOn,

                            DeletedBy = a.DeletedBy,
                            DeletedOn = a.DeletedOn,

                            IsDelete = false,

                            AppointmentCancellation = (
                            from ac in db.TblTAppointmentCancellations
                            join a in db.TblTAppointments on ac.AppointmentId equals a.Id
                            where ac.IsDelete == false
                            select new VMTblTAppointmentCancellation
                            {
                                Id = ac.Id,
                                AppointmentId = a.Id,

                                CreateBy = ac.CreateBy,
                                CreatedOn = ac.CreatedOn,

                                ModifiedBy = ac.ModifiedBy,
                                ModifiedOn = ac.ModifiedOn,

                                DeletedBy = ac.DeletedBy,
                                DeletedOn = ac.DeletedOn,

                                IsDelete = false
                            }).ToList(),

                            AppointmentDone = (
                            from ad in db.TblTAppointmentDones
                            join a in db.TblTAppointments on ad.AppointmentId equals a.Id
                            where ad.IsDelete == false
                            select new VMTblTAppointmentDone
                            {
                                Id = ad.Id,
                                AppointmentId = a.Id,
                                Diagnosis = ad.Diagnosis,

                                CreatedBy = ad.CreatedBy,
                                CreatedOn = ad.CreatedOn,

                                ModifiedBy = ad.ModifiedBy,
                                ModifiedOn = ad.ModifiedOn,

                                DeletedBy = ad.DeletedBy,
                                DeletedOn = ad.DeletedOn,

                                IsDelete = false
                            }).ToList()
                        }).ToList(),

                        MedicalFacilitySchedule = (
                        from mfs in db.TblMMedicalFacilitySchedules
                        join mf in db.TblMMedicalFacilities on mfs.MedicalFacilityId equals mf.Id
                        where mfs.IsDelete == false
                        select new VMTblMMedicalFacilitySchedule
                        {
                            Id = mfs.Id,
                            MedicalFacilityId = mf.Id,
                            Day = mfs.Day,
                            TimeScheduleStart = mfs.TimeScheduleStart,
                            TimeScheduleEnd = mfs.TimeScheduleEnd,

                            CreateBy = mfs.CreateBy,
                            CreatedOn = mfs.CreatedOn,

                            ModifiedBy = mfs.ModifiedBy,
                            ModifiedOn = mfs.ModifiedOn,

                            DeletedBy = mfs.DeletedBy,
                            DeletedOn = mfs.DeletedOn,

                            IsDelete = false

                        }).ToList()

                    }).ToList(),

                    DoctorOffice = (
                    from dof in db.TblTDoctorOffices
                    join d in db.TblMDoctors on dof.DoctorId equals d.Id
                    join mf in db.TblMMedicalFacilities on dof.MedicalFacilityId equals mf.Id
                    where dof.IsDelete == false && d.Id == 5
                    select new VMTblTDoctorOffice
                    {
                        Id = dof.Id,
                        DoctorId = d.Id,
                        MedicalFacilityId = mf.Id,
                        Specialization = dof.Specialization,
                        StartDate = dof.StartDate,
                        EndDate = dof.EndDate,

                        CreatedBy = dof.CreatedBy,
                        CreatedOn = dof.CreatedOn,

                        ModifiedBy = dof.ModifiedBy,
                        ModifiedOn = dof.ModifiedOn,

                        DeletedBy = dof.DeletedBy,
                        DeletedOn = dof.DeletedOn,

                        IsDelete = false,

                        MedicalFacility = (
                        from mf in db.TblMMedicalFacilities
                        join mfc in db.TblMMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                        join l in db.TblMLocations on mf.LocationId equals l.Id
                        where mf.IsDelete == false
                        select new VMTblMMedicalFacility
                        {
                            Id = mf.Id,
                            Name = mf.Name,
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

                            IsDelete = false,

                            Location = (
                            from l in db.TblMLocations
                            join ll in db.TblMLocationLevels on l.LocationLevelId equals ll.Id
                            where l.IsDelete == false
                            select new VMTblMLocation
                            {
                                Id = l.Id,
                                Name = l.Name,
                                ParentId = l.ParentId,
                                LocationLevelId = ll.Id,

                                CreateBy = l.CreateBy,
                                CreatedOn = l.CreatedOn,

                                ModifiedBy = l.ModifiedBy,
                                ModifiedOn = l.ModifiedOn,

                                DeletedBy = l.DeletedBy,
                                DeletedOn = l.DeletedOn,

                                IsDelete = false

                                //LocationLevel = (
                                //from ll in db.TblMLocationLevels
                                //where ll.IsDelete == false
                                //select new VMTblMLocationLevel
                                //{
                                //    Id = ll.Id,
                                //    Name = ll.Name,
                                //    Abbreviation = ll.Abbreviation,

                                //    CreateBy = ll.CreateBy,
                                //    CreatedOn = ll.CreatedOn,

                                //    ModifiedBy = ll.ModifiedBy,
                                //    ModifiedOn = ll.ModifiedOn,

                                //    DeletedBy = ll.DeletedBy,
                                //    DeletedOn = ll.DeletedOn,

                                //    IsDelete = false
                                //}).ToList()
                            }).ToList()

                        }).ToList()
                    }).ToList()

                }).FirstOrDefault();
        }

        public VMResponse GetById(long id)
        {
            try
            {
                VMTblMDoctor? data = FindById(id);
                response.data = data;
                response.message = (data == null)
                    ? $"Dokter dengan : {id} tidak ditemukan"
                    : $"Dokter dengan : {id} berhasil ditemukan";
                response.statusCode = (data == null)
                    ? HttpStatusCode.NoContent
                    : HttpStatusCode.OK;
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public VMResponse GetBiodataId(long id)
        {
            try
            {
                VMTblMDoctor? data = (
                    from d in db.TblMDoctors
                    join bio in db.TblMBiodata on d.BiodataId equals bio.Id
                    where d.IsDelete == false
                    select new VMTblMDoctor
                    {
                        Id = d.Id,
                        BiodataId = bio.Id,
                        Str = d.Str,

                        CreateBy = d.CreateBy,
                        CreateOn = d.CreateOn,

                        ModifiedBy = d.ModifiedBy,
                        ModifiedOn = d.ModifiedOn,

                        DeletedBy = d.DeletedBy,
                        DeletedOn = d.DeletedOn,
                        IsDelete = false,

                        Biodata = (
                        from bio in db.TblMBiodata
                        join user in db.TblMUsers on bio.Id equals user.BiodataId
                        where bio.Id == user.BiodataId
                            && bio.IsDelete == false
                        select new VMTblMBiodata
                        {
                            Id = bio.Id,
                            Fullname = bio.Fullname,
                            MobilePhone = bio.MobilePhone,
                            Image = bio.Image,
                            ImagePath = bio.ImagePath,

                            CreateBy = bio.CreateBy,
                            CreateOn = bio.CreateOn,

                            ModifiedBy = bio.ModifiedBy,
                            ModifiedOn = bio.ModifiedOn,

                            DeletedBy = bio.DeletedBy,
                            DeletedOn = bio.DeletedOn,

                            IsDelete = false

                        }).FirstOrDefault()

                    }).FirstOrDefault();

                response.data = data;
                response.message = (data == null)
                    ? $"Dokter dengan : {id} tidak ditemukan"
                    : $"Dokter dengan : {id} berhasil ditemukan";
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
    }
}
