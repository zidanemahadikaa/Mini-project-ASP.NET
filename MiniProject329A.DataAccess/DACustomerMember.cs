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
    public class DACustomerMember
    {
        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();
        public DACustomerMember(MiniProject329AContext _db)
        {
            db = _db;
            VMResponse response = new VMResponse();
        }
        public VMTblMCustomerMember? FindById(long? id)
        {
            return (
                from cm in db.TblMCustomerMembers
                join bio in db.TblMBiodata on
                cm.ParentBiodataId equals bio.Id
                join cus in db.TblMCustomers on
                cm.CustomerId equals cus.Id
                join cr in db.TblMCustomerRelations on
                cm.CustomerRelationId equals cr.Id
                where cm.IsDelete == false
                && cm.Id == id
                select new VMTblMCustomerMember
                {
                    Id = cm.Id,

                    ParentBiodataId = cm.ParentBiodataId,
                    ParentName = bio.Fullname,
                    CustomerId = cm.CustomerId,

                    CustomerRelationId = cm.CustomerRelationId,
                    CustomerRelationName = cr.Name,

                    IsDelete = cm.IsDelete,

                    CreateBy = cm.CreateBy,
                    CreateOn = cm.CreateOn,
                    ModifiedBy = cm.ModifiedBy,
                    ModifiedOn = cm.ModifiedOn,
                    DeletedBy = cm.DeletedBy,
                    DeletedOn = cm.DeletedOn,
                }
                ).FirstOrDefault();
        }
        public VMResponse GetById(long? id = null)
        {
            VMTblMCustomerMember? data = null;
            try
            {
                data = FindById(id);

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
        //public VMResponse GetByName(string? name = null)
        public VMResponse GetAll()
        {
            try
            {
                List<VMTblMCustomerMember> data = (
                    from cm in db.TblMCustomerMembers
                    join bio in db.TblMBiodata on
                    cm.ParentBiodataId equals bio.Id
                    join cus in db.TblMCustomers on
                    cm.CustomerId equals cus.Id
                    join cr in db.TblMCustomerRelations on
                    cm.CustomerRelationId equals cr.Id
                    where cm.IsDelete == false
                    select new VMTblMCustomerMember
                    {
                        Id = cm.Id,

                        ParentBiodataId = cm.ParentBiodataId,
                        ParentName = bio.Fullname,  
                        CustomerId = cm.CustomerId,                      

                        CustomerRelationId = cm.CustomerRelationId,
                        CustomerRelationName = cr.Name,

                        IsDelete = cm.IsDelete,

                        CreateBy = cm.CreateBy,
                        CreateOn = cm.CreateOn,
                        ModifiedBy = cm.ModifiedBy,
                        ModifiedOn = cm.ModifiedOn,
                        DeletedBy = cm.DeletedBy,
                        DeletedOn = cm.DeletedOn,
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
        public VMResponse CreateUpdate(VMTblMCustomerMember formData)
        {
            string? strInfo = "";
            try
            {
                TblMCustomerMember data = new TblMCustomerMember();

                if (formData.Id == null || formData.Id == 0)
                {
                    strInfo = "Create";

                    data.ParentBiodataId = formData.ParentBiodataId;
                    data.CustomerId = formData.CustomerId;
                    data.CustomerRelationId = formData.CustomerRelationId;
                    data.CreateBy = formData.CreateBy;
                    data.IsDelete = false;

                    db.Add(data);

                    response.statusCode = HttpStatusCode.Created;
                }
                else
                {
                    strInfo = "Update";

                    VMTblMCustomerMember? existingData = FindById(formData.Id);
                    
                    if(existingData != null)
                    {
                        data.Id = existingData.Id;
                        data.ParentBiodataId = existingData.ParentBiodataId;
                        data.CustomerId = formData.CustomerId;
                        data.CustomerRelationId = formData.CustomerRelationId;

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
        public VMResponse Delete(long? id, long userId)
        {
            try
            {
                VMTblMCustomerMember? existingData = FindById(id);

                if (existingData != null)
                {
                    TblMCustomerMember data = new TblMCustomerMember();

                    data.Id = existingData.Id;
                    data.ParentBiodataId = existingData.ParentBiodataId;
                    data.CustomerId = existingData.CustomerId;
                    data.CustomerRelationId = existingData.CustomerRelationId;

                    data.CreateBy = existingData.CreateBy;
                    data.CreateOn = existingData.CreateOn;

                    data.ModifiedBy = existingData.ModifiedBy;
                    data.ModifiedOn = existingData?.ModifiedOn;

                    data.DeletedBy = userId;
                    data.DeletedOn = DateTime.Now;
                    data.IsDelete = true;

                    db.Update(data);
                    db.SaveChanges();

                    response.data = data;
                    response.message = "Golongan Darah Berhasil dihapus";
                    response.statusCode = HttpStatusCode.OK;
                }
                else
                {
                    throw new Exception("Golongan darah tidak ada");
                }
            }
            catch (Exception ex)
            {
                response.message += $"{ex.Message}";
            }
            return response;
        }
        public List<VMTblMCustomerMember?> FindByCusId(long? cusId)
        {
            return (
                from cm in db.TblMCustomerMembers
                join bio in db.TblMBiodata on
                cm.ParentBiodataId equals bio.Id
                join cus in db.TblMCustomers on
                cm.CustomerId equals cus.Id
                join cr in db.TblMCustomerRelations on
                cm.CustomerRelationId equals cr.Id
                where cm.IsDelete == false && cm.CustomerId == cusId
                select new VMTblMCustomerMember
                {
                    Id = cm.Id,
                    ParentBiodataId = cm.ParentBiodataId,
                    ParentName = bio.Fullname,
                    CustomerId = cm.CustomerId,

                    CustomerRelationId = cm.CustomerRelationId,
                    CustomerRelationName = cr.Name,

                    IsDelete = cm.IsDelete,

                    CreateBy = cm.CreateBy,
                    CreateOn = cm.CreateOn,
                    ModifiedBy = cm.ModifiedBy,
                    ModifiedOn = cm.ModifiedOn,
                    DeletedBy = cm.DeletedBy,
                    DeletedOn = cm.DeletedOn,
                }
                ).ToList();
        }
        public VMResponse GetByCusId(long? cusId = null)
        {
            List<VMTblMCustomerMember?> data = null;
            try
            {
                data = FindByCusId(cusId);

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
        public List<VMTblMCustomerMember?> FindByParentId(long? parId)
        {
            return (
                from cm in db.TblMCustomerMembers
                join bio in db.TblMBiodata on
                cm.ParentBiodataId equals bio.Id
                join cus in db.TblMCustomers on
                cm.CustomerId equals cus.Id
                join cr in db.TblMCustomerRelations on
                cm.CustomerRelationId equals cr.Id
                where cm.IsDelete == false && cm.ParentBiodataId == parId
                select new VMTblMCustomerMember
                {
                    Id = cm.Id,
                    ParentBiodataId = cm.ParentBiodataId,
                    ParentName = bio.Fullname,
                    CustomerId = cm.CustomerId,

                    CustomerRelationId = cm.CustomerRelationId,
                    CustomerRelationName = cr.Name,

                    IsDelete = cm.IsDelete,

                    CreateBy = cm.CreateBy,
                    CreateOn = cm.CreateOn,
                    ModifiedBy = cm.ModifiedBy,
                    ModifiedOn = cm.ModifiedOn,
                    DeletedBy = cm.DeletedBy,
                    DeletedOn = cm.DeletedOn,
                }
                ).ToList();
        }
        public VMResponse GetByParentId(long? parId = null)
        {
            List<VMTblMCustomerMember?> data = null;
            try
            {
                data = FindByParentId(parId);

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
        public VMResponse GetByCustomerName(long parId, string? name)
        {
            List<VMTblMCustomerMember> ParentId = FindByParentId(parId);
            try
            {
                List<VMTblMCustomerMember> data = (
                    from cm in db.TblMCustomerMembers
                    join cus in db.TblMCustomers on
                    cm.CustomerId equals cus.Id
                    join bio in db.TblMBiodata on
                    cus.BiodataId equals bio.Id
                    join cr in db.TblMCustomerRelations on
                    cm.CustomerRelationId equals cr.Id
                    where cm.IsDelete == false && cm.ParentBiodataId == parId
                    && bio.Fullname.Contains(name ?? "")
                    select new VMTblMCustomerMember
                    {
                        Id = cm.Id,
                        CustomerId = cm.CustomerId,
                        ParentBiodataId = cm.ParentBiodataId,
                        CustomerName = bio.Fullname,
                        DobCustomer = cus.Dob,

                        IsDelete = cm.IsDelete,

                        CustomerRelationId = cm.CustomerRelationId,
                        CustomerRelationName = cr.Name,

                        CreateBy = cm.CreateBy,
                        CreateOn = cm.CreateOn,
                        ModifiedBy = cm.ModifiedBy,
                        ModifiedOn = cm.ModifiedOn,
                        DeletedBy = cm.DeletedBy,
                        DeletedOn = cm.DeletedOn,
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
