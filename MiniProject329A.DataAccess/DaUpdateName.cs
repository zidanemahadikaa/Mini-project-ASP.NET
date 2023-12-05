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
    public class DaUpdateName
    {


        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();

        public DaUpdateName(MiniProject329AContext _db)
        {
            db = _db;
            VMResponse response = new VMResponse();
        }


        public VMTblMCustomerMember? FindByIdcustomermember(long? id)
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
                data = FindByIdcustomermember(id);

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



        public VMTblMCustomer? FindByIdcustomer(long? id)
        {
            return (
                from cm in db.TblMCustomerMembers
                join cus in db.TblMCustomers on
                cm.CustomerId equals cus.Id
                where cus.Id == id
                && cus.IsDelete==false
                //cm.ParentBiodataId equals bio.Id
                //join cus in db.TblMCustomers on
                //cm.CustomerId equals cus.Id
                //join cr in db.TblMCustomerRelations on
                //cm.CustomerRelationId equals cr.Id
                //where cm.IsDelete == false
                //&& cm.Id == id
                select new VMTblMCustomer
                {
                    Id = cus.Id,
                    BiodataId=cus.BiodataId,
                    Dob=cus.Dob, 
                    Gender=cus.Gender,
                    BloodGroupId=cus.BloodGroupId,
                    RhesusType=cus.RhesusType,
                    //ParentBiodataId = cm.ParentBiodataId,
                    //ParentName = bio.Fullname,
                    //CustomerId = cm.CustomerId,

                    //CustomerRelationId = cm.CustomerRelationId,
                    //CustomerRelationName = cr.Name,

                    IsDelete = cus.IsDelete,

                    CreateBy = cus.CreateBy,
                    CreateOn = cus.CreateOn,
                    ModifiedBy = cus.ModifiedBy,
                    ModifiedOn = cus.ModifiedOn,
                    DeletedBy = cus.DeletedBy,
                    DeletedOn = cus.DeletedOn,
                }
                ).FirstOrDefault();
        }
        public VMResponse GetByIdcus(long? id = null)
        {
            VMTblMCustomer? data = null;
            try
            {
                data = FindByIdcustomer(id);

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



        public VMTblMBiodata? FindByIdBioid(long? id)
        {
            return (
                from cus in db.TblMCustomers
                join bio in db.TblMBiodata on
                cus.BiodataId equals bio.Id
                where bio.Id == id
                && bio.IsDelete == false
                //cm.ParentBiodataId equals bio.Id
                //join cus in db.TblMCustomers on
                //cm.CustomerId equals cus.Id
                //join cr in db.TblMCustomerRelations on
                //cm.CustomerRelationId equals cr.Id
                //where cm.IsDelete == false
                //&& cm.Id == id
                select new VMTblMBiodata
                {
                    Id = bio.Id,
                    Fullname= bio.Fullname,
                    //ParentBiodataId = cm.ParentBiodataId,
                    //ParentName = bio.Fullname,
                    //CustomerId = cm.CustomerId,

                    //CustomerRelationId = cm.CustomerRelationId,
                    //CustomerRelationName = cr.Name,

                    IsDelete = cus.IsDelete,

                    CreateBy = cus.CreateBy,
                    CreateOn = cus.CreateOn,
                    ModifiedBy = cus.ModifiedBy,
                    ModifiedOn = cus.ModifiedOn,
                    DeletedBy = cus.DeletedBy,
                    DeletedOn = cus.DeletedOn,
                }
                ).FirstOrDefault();
        }
        public VMResponse GetByIdbio(long? id = null)
        {
            VMTblMBiodata? data = null;
            try
            {
                data = FindByIdBioid(id);

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

        public VMResponse UpdateBiodata(VMTblMBiodata? formData)
        {
            try
            {
            //    TblMCustomerRelation data = new TblMCustomerRelation();
            //    VMTblMCustomerRelation? existingData = FindById(formData.Id);
            //    VMTblMCustomerRelation? checkName = CheckName(formData.Name);
                VMTblMBiodata? exsistingData=FindByIdBioid(formData.Id);

                TblMBiodata data = new TblMBiodata();
                data.Id= exsistingData.Id;
                data.Fullname=formData.Fullname;
                data.MobilePhone = exsistingData.MobilePhone;
                data.Image = exsistingData.Image;
                data.ImagePath = exsistingData.ImagePath;

                data.CreateBy = exsistingData.CreateBy;
                data.CreateOn= exsistingData.CreateOn;

                data.ModifiedBy = exsistingData.Id;
                data.ModifiedOn=DateTime.Now;
                data.DeletedBy = exsistingData.DeletedBy;
                data.IsDelete = exsistingData.IsDelete;
                //if (existingData != null && checkName == null)
                //{
                //    if (existingData.Name.ToUpper() == formData.Name.ToUpper())
                //    {
                //        response.message = $"Hubungan Relasi {data.Name} sudah ada";
                //        response.statusCode = HttpStatusCode.NoContent;
                //    }
                //    else
                //    {
                //        data.Id = existingData.Id;
                //        data.Name = formData.Name;

                //        data.CreateBy = existingData.CreateBy;
                //        data.CreateOn = existingData.CreateOn;

                //        data.ModifiedBy = formData.ModifiedBy;
                //        data.ModifiedOn = DateTime.Now;

                //        db.Update(data);
                //        response.statusCode = HttpStatusCode.OK;
                //    }
                //}
                //else
                //{
                //    data.Id = existingData.Id;
                //    data.Name = existingData.Name;

                //    data.CreateBy = existingData.CreateBy;
                //    data.CreateOn = existingData.CreateOn;

                //    data.ModifiedBy = formData.ModifiedBy;
                //    data.ModifiedOn = DateTime.Now;

                //    db.Update(data);
                //    response.message = $"Hubungan Relasi Sudah ada";
                //    response.statusCode = HttpStatusCode.OK;
                //}
                db.Update(data);

                db.SaveChanges();

                response.data = data;
            }
            catch (Exception ex)
            {
                response.message = "Data Pasien gagal diubah" + ex.Message;
            }
            finally { db.Dispose(); }
            return response;
        }
        public VMResponse UpdateCustomer(VMTblMCustomer? formData)
        {
            try
            {
                //    TblMCustomerRelation data = new TblMCustomerRelation();
                //    VMTblMCustomerRelation? existingData = FindById(formData.Id);
                //    VMTblMCustomerRelation? checkName = CheckName(formData.Name);
                VMTblMCustomer? exsistingData = FindByIdcustomer(formData.Id);

                TblMCustomer data = new TblMCustomer();
                data.Id = exsistingData.Id;
                data.BiodataId = exsistingData.BiodataId;
                data.Dob = formData.Dob;
                data.Gender = formData.Gender;
                data.BloodGroupId = formData.BloodGroupId;
                data.RhesusType = formData.RhesusType;
                data.Height = formData.Height;
                data.Weight = formData.Weight;

                data.CreateBy = exsistingData.CreateBy;
                data.CreateOn = exsistingData.CreateOn;

                data.ModifiedBy = exsistingData.Id;
                data.ModifiedOn = DateTime.Now;
                data.DeletedBy = exsistingData.DeletedBy;
                data.IsDelete = exsistingData.IsDelete;
                //if (existingData != null && checkName == null)
                //{
                //    if (existingData.Name.ToUpper() == formData.Name.ToUpper())
                //    {
                //        response.message = $"Hubungan Relasi {data.Name} sudah ada";
                //        response.statusCode = HttpStatusCode.NoContent;
                //    }
                //    else
                //    {
                //        data.Id = existingData.Id;
                //        data.Name = formData.Name;

                //        data.CreateBy = existingData.CreateBy;
                //        data.CreateOn = existingData.CreateOn;

                //        data.ModifiedBy = formData.ModifiedBy;
                //        data.ModifiedOn = DateTime.Now;

                //        db.Update(data);
                //        response.statusCode = HttpStatusCode.OK;
                //    }
                //}
                //else
                //{
                //    data.Id = existingData.Id;
                //    data.Name = existingData.Name;

                //    data.CreateBy = existingData.CreateBy;
                //    data.CreateOn = existingData.CreateOn;

                //    data.ModifiedBy = formData.ModifiedBy;
                //    data.ModifiedOn = DateTime.Now;

                //    db.Update(data);
                //    response.message = $"Hubungan Relasi Sudah ada";
                //    response.statusCode = HttpStatusCode.OK;
                //}
                db.Update(data);

                db.SaveChanges();
                response.statusCode = HttpStatusCode.OK;
                response.data = data;
            }
            catch (Exception ex)
            {
                response.message = "Data Golongan darah baru gagal dibuat" + ex.Message;
            }
            finally { db.Dispose(); }
            return response;
        }
        public VMResponse UpdateCustomerMember(VMTblMCustomerMember? formData)
        {
            try
            {
                VMTblMCustomerMember? exsistingData = FindByIdcustomermember(formData.Id);

                TblMCustomerMember data = new TblMCustomerMember();
                data.Id = exsistingData.Id;
                data.ParentBiodataId = exsistingData.ParentBiodataId;
                data.CustomerId = exsistingData.CustomerId;
                data.CustomerRelationId = formData.CustomerRelationId;

                data.CreateBy = exsistingData.CreateBy;
                data.CreateOn = exsistingData.CreateOn;

                data.ModifiedBy = exsistingData.Id;
                data.ModifiedOn = DateTime.Now;
                data.DeletedBy = exsistingData.DeletedBy;
                data.IsDelete = exsistingData.IsDelete;

                db.Update(data);

                db.SaveChanges();
                response.statusCode = HttpStatusCode.OK;
                response.data = data;
            }
            catch (Exception ex)
            {
                response.message = "Data Pasien gagal diubah" + ex.Message;
            }
            finally { db.Dispose(); }
            return response;
        }
    }
}
