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
    public class DACustomerRelation
    {
        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();
        public DACustomerRelation(MiniProject329AContext _db)
        {
            db = _db;
            VMResponse response = new VMResponse();
        }
        public VMTblMCustomerRelation? FindById(long? id)
        {
            return (
                from cr in db.TblMCustomerRelations
                where cr.IsDelete == false
                && cr.Id == id
                select new VMTblMCustomerRelation
                {
                    Id = cr.Id,
                    Name = cr.Name,

                    IsDelete = cr.IsDelete,

                    CreateBy = cr.CreateBy,
                    CreateOn = cr.CreateOn,
                    ModifiedBy = cr.ModifiedBy,
                    ModifiedOn = cr.ModifiedOn,
                    DeletedBy = cr.DeletedBy,
                    DeletedOn = cr.DeletedOn,
                }
                ).FirstOrDefault();
        }
        public VMResponse GetByName(string? name = null)
        {
            try
            {
                List<VMTblMCustomerRelation> data = (
                    from cr in db.TblMCustomerRelations
                    where cr.IsDelete == false
                    && cr.Name.Contains(name ?? "")
                    select new VMTblMCustomerRelation
                    {
                        Id = cr.Id,
                        Name = cr.Name,

                        IsDelete = cr.IsDelete,

                        CreateBy = cr.CreateBy,
                        CreateOn = cr.CreateOn,
                        ModifiedBy = cr.ModifiedBy,
                        ModifiedOn = cr.ModifiedOn,
                        DeletedBy = cr.DeletedBy,
                        DeletedOn = cr.DeletedOn,
                    }
                    ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                    ? $"Hubungan relasi dengan {name} ditemukan"
                    : $"Hubungan relasi dengan {name} tidak ada";
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
        public VMResponse GetById(long? id = null)
        {
            try
            {
                VMTblMCustomerRelation? data = FindById(id);

                response.data = data;
                response.message = (data == null)
                    ? $"Hubungan relasi dengan id = {id} tidak ada"
                    : $"Hubungan relasi dengan id = {id} ditemukan";
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
        public VMTblMCustomerRelation? CheckName(string name)
        {
            return (
                from cr in db.TblMCustomerRelations
                where cr.Name == name && cr.IsDelete == false
                select new VMTblMCustomerRelation
                {
                    Id = cr.Id,
                    Name = cr.Name,

                    IsDelete = cr.IsDelete,

                    CreateBy = cr.CreateBy,
                    CreateOn = cr.CreateOn,
                    ModifiedBy = cr.ModifiedBy,
                    ModifiedOn = cr.ModifiedOn,
                    DeletedBy = cr.DeletedBy,
                    DeletedOn = cr.DeletedOn,
                }
                ).FirstOrDefault();
        }
        public VMResponse Create(VMTblMCustomerRelation formData)
        {
            try
            {
                TblMCustomerRelation data = new TblMCustomerRelation();

                data.Name = formData.Name;
                formData.Name = formData.Name.Replace(" ", "");

                data.CreateOn = DateTime.Now;

                data.IsDelete = false;

                if (formData.Id == null || formData.Id == 0)
                {
                    VMTblMCustomerRelation? checkData = CheckName(formData.Name);

                    response.data = checkData;

                    if (checkData != null)
                    {
                        if (formData.Name.ToUpper() == checkData.Name.ToUpper())
                        {
                            response.message = $"Hubungan Relasi {data.Name} sudah ada";
                            response.statusCode = HttpStatusCode.NoContent;
                        }
                        else
                        {
                            throw new Exception($"Gagal Dibuat");
                        }
                    }
                    else
                    {
                        data.CreateBy = formData.CreateBy;
                        db.Add(data);
                        db.SaveChanges();
                        response.statusCode = HttpStatusCode.Created;
                    }
                }
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
            }
            finally { db.Dispose(); }
            return response;
        }
        public VMResponse Update(VMTblMCustomerRelation formData)
        {
            try
            {
                TblMCustomerRelation data = new TblMCustomerRelation();
                VMTblMCustomerRelation? existingData = FindById(formData.Id);
                VMTblMCustomerRelation? checkName = CheckName(formData.Name);

                if (existingData != null && checkName == null)
                {
                    if (existingData.Name.ToUpper() == formData.Name.ToUpper())
                    {
                        response.message = $"Hubungan Relasi {data.Name} sudah ada";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                    else
                    {
                        data.Id = existingData.Id;
                        data.Name = formData.Name;

                        data.CreateBy = existingData.CreateBy;
                        data.CreateOn = existingData.CreateOn;

                        data.ModifiedBy = formData.ModifiedBy;
                        data.ModifiedOn = DateTime.Now;

                        db.Update(data);
                        response.statusCode = HttpStatusCode.OK;
                    }
                }
                else
                {
                    response.message = $"Hubungan Relasi {data.Name} sudah ada";
                    response.statusCode = HttpStatusCode.NoContent;
                }

                db.SaveChanges();

                response.data = data;
            }
            catch (Exception ex)
            {
                response.message = "Data Golongan darah baru gagal dibuat" + ex.Message;
            }
            finally { db.Dispose(); }
            return response;
        }
        public VMResponse Delete(long? id, long userId)
        {
            try
            {
                VMTblMCustomerRelation? existingData = FindById(id);

                if (existingData != null)
                {
                    TblMCustomerRelation data = new TblMCustomerRelation();

                    data.Id = existingData.Id;
                    data.Name = existingData.Name;

                    data.CreateBy = existingData.CreateBy;
                    data.CreateOn = existingData.CreateOn;

                    data.ModifiedBy = existingData.ModifiedBy;
                    data.ModifiedOn = existingData.ModifiedOn;

                    data.DeletedBy = userId;
                    data.DeletedOn = DateTime.Now;

                    data.IsDelete = true;

                    db.Update(data);
                    db.SaveChanges();

                    response.data = data;
                    response.message = "Hubungan Relasi Berhasil dihapus";
                    response.statusCode = HttpStatusCode.OK;
                }
                else
                {
                    throw new Exception("Hubungan Relasi tidak ada");
                }
            }
            catch (Exception ex)
            {
                response.message += $"{ex.Message}";
            }
            return response;
        }
    }
}
