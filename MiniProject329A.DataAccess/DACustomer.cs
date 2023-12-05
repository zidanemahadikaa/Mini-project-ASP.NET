using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.DataAccess
{
    public class DACustomer
    {
        private readonly MiniProject329AContext db;
        private readonly DAUser user;
        public DACustomer(MiniProject329AContext _db)
        {
            db = _db;
            user = new DAUser(db);
        }
        private VMResponse response = new VMResponse();
        public VMTblMCustomer? FindById(long? id = null)
        {
            return (
                    from cus in db.TblMCustomers
                    join bio in db.TblMBiodata on
                    cus.BiodataId equals bio.Id
                    join bg in db.TblMBloodGroups on
                    cus.BloodGroupId equals bg.Id
                    join cm in db.TblMCustomerMembers on
                    cus.Id equals cm.CustomerId
                    join cr in db.TblMCustomerRelations on
                    cm.CustomerRelationId equals cr.Id  
                    where cus.IsDelete == false && cus.Id == id
                    select new VMTblMCustomer
                    {
                        Id = cus.Id,

                        BiodataId = cus.BiodataId,
                        Fullname = bio.Fullname,

                        Dob = cus.Dob,
                        Gender = cus.Gender,

                        BloodGroupId = cus.BloodGroupId,
                        BloodGroupCode = bg.Code,
                        RhesusType = cus.RhesusType,

                        Height = cus.Height,
                        Weight = cus.Weight,

                        //RelationId = cm.CustomerRelationId,
                        //RelationName = cr.Name
                    }
                    ).FirstOrDefault();
        }
        public VMResponse GetByName(string? name = null)
        {
            try
            {
               List<VMTblMCustomer> data = (
                    from cus in db.TblMCustomers
                    join bio in db.TblMBiodata on
                    cus.BiodataId equals bio.Id
                    join bg in db.TblMBloodGroups on
                    cus.BloodGroupId equals bg.Id
                    where cus.IsDelete == false &&
                    bio.Fullname.Contains(name ?? "")
                    select new VMTblMCustomer
                    {
                        Id = cus.Id,

                        BiodataId = cus.BiodataId,
                        Fullname = bio.Fullname,

                        Dob = cus.Dob,
                        Gender = cus.Gender,

                        BloodGroupId = cus.BloodGroupId,
                        BloodGroupCode = bg.Code,
                        RhesusType = cus.RhesusType,

                        Height = cus.Height,
                        Weight = cus.Weight
                    }
                    ).ToList();

                response.data = data;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse GetById(long? id = null)
        {
            try
            {
                VMTblMCustomer? data = FindById(id);

                response.data = data;
                response.message = (data == null)
                    ? $"Customer dengan id = {id} tidak ada"
                    : $"Customer dengan id = {id} ditemukan";
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
                List<VMTblMCustomer> data = (
                    from cus in db.TblMCustomers
                    join bio in db.TblMBiodata on
                    cus.BiodataId equals bio.Id
                    join bg in db.TblMBloodGroups on
                    cus.BloodGroupId equals bg.Id
                    where cus.IsDelete == false
                    select new VMTblMCustomer
                    {
                        Id = cus.Id,

                        BiodataId = cus.BiodataId,
                        Fullname = bio.Fullname,

                        Dob = cus.Dob,
                        Gender = cus.Gender,

                        BloodGroupId = cus.BloodGroupId,
                        BloodGroupCode = bg.Code,
                        RhesusType = cus.RhesusType,

                        Height = cus.Height,
                        Weight = cus.Weight,

                        IsDelete = cus.IsDelete,
                        CreateBy = cus.CreateBy,
                        CreateOn = cus.CreateOn,
                        ModifiedBy = cus.ModifiedBy,
                        ModifiedOn = cus.ModifiedOn,
                        DeletedBy = cus.DeletedBy,
                        DeletedOn = cus.DeletedOn,
                    }
                    ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                    ? $"Data Customer ditemukan"
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
        public VMResponse CreateUpdate(VMTblMCustomer formData)
        {
            string? strInfo = "";
            try
            {
                TblMCustomer data = new TblMCustomer();

                if (formData.Id == null || formData.Id == 0)
                {
                    strInfo = "Create";

                    
                    data.BiodataId = formData.BiodataId;
                    
                    data.Dob = formData.Dob;
                    data.Gender = formData.Gender;
                    data.BloodGroupId = formData.BloodGroupId;
                    data.RhesusType = formData.RhesusType;
                    data.Height = formData.Height;
                    data.Weight = formData.Weight;
                    data.IsDelete = false;

                    db.Add(data);
                    db.SaveChanges();
                    response.statusCode = HttpStatusCode.Created;
                }
                else
                {
                    strInfo = "Update";

                    VMTblMCustomer? existingData = FindById(formData.Id);

                    if (existingData != null)
                    {
                        data.Id = existingData.Id;
                        data.BiodataId = existingData.BiodataId;
                        
                        data.Dob = formData.Dob;
                        data.Gender = formData.Gender;
                        data.BloodGroupId = formData.BloodGroupId;
                        data.RhesusType = formData.RhesusType;
                        data.Height = formData.Height;
                        data.Weight = formData.Weight;

                        data.CreateBy = existingData.CreateBy;
                        data.CreateOn = existingData.CreateOn;

                        data.ModifiedBy = formData?.ModifiedBy;
                        data.ModifiedOn = DateTime.Now;

                        db.Update(data);
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        throw new Exception($"Data Customer baru gagal {strInfo}");
                    }
                    response.statusCode = HttpStatusCode.OK;
                }
                db.SaveChanges();
                response.data = data;
                response.message = $"Data Pasien berhasil {strInfo}";
            }
            catch (Exception ex)
            {
                response.message = $"Data pasien Gagal {strInfo}! " + ex.Message;
            }
            return response;
        }
        public VMTblMCustomer? FindByBioId(long? bioId = null)
        {
            return (
                    from cus in db.TblMCustomers
                    join bio in db.TblMBiodata on
                    cus.BiodataId equals bio.Id
                    join bg in db.TblMBloodGroups on
                    cus.BloodGroupId equals bg.Id
                    where cus.IsDelete == false && cus.BiodataId == bioId
                    select new VMTblMCustomer
                    {
                        Id = cus.Id,

                        BiodataId = bio.Id,
                        Fullname = bio.Fullname,

                        Dob = cus.Dob,
                        Gender = cus.Gender,

                        BloodGroupId = cus.BloodGroupId,
                        BloodGroupCode = bg.Code,
                        RhesusType = cus.RhesusType,

                        Height = cus.Height,
                        Weight = cus.Weight,

                        CreateBy = cus.CreateBy,
                        CreateOn = cus.CreateOn,

                        //RelationId = cm.CustomerRelationId,
                        //RelationName = cr.Name
                    }
                    ).FirstOrDefault();
        }
        public VMResponse GetByBioId(long? id = null)
        {
            VMTblMCustomer? data = null;
            try
            {
                data = FindByBioId(id);

                response.data = data;
                response.message = (data == null)
                    ? $"Customer tidak ditemukan"
                    : $"Customer ditemukan";
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
