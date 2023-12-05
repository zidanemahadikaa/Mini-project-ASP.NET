using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using System.Net;

namespace MiniProject329A.DataAccess
{
    public class DABloodGroup
    {
        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();
        public DABloodGroup(MiniProject329AContext _db)
        {
            db = _db;
            VMResponse response = new VMResponse();
        }
        private VMTblMBloodGroup? FindById(long? id)
        {
            return (
                from bg in db.TblMBloodGroups
                where bg.IsDelete == false
                && bg.Id == id
                select new VMTblMBloodGroup
                {
                    Id = bg.Id,
                    Code = bg.Code,
                    Description = bg.Description,

                    IsDelete = bg.IsDelete,

                    CreateBy = bg.CreateBy,
                    CreateOn = bg.CreateOn,
                    ModifiedBy = bg.ModifiedBy,
                    ModifiedOn = bg.ModifiedOn,
                    DeletedBy = bg.DeletedBy,
                    DeletedOn = bg.DeletedOn,
                }
                ).FirstOrDefault();
        }
        public VMResponse GetByCode(string? code = null)
        {
            try
            {
                List<VMTblMBloodGroup> data = (
                    from bg in db.TblMBloodGroups
                    where bg.IsDelete == false
                    && bg.Code.Contains(code ?? "")
                    select new VMTblMBloodGroup
                    {
                        Id = bg.Id,
                        Code = bg.Code,
                        Description = bg.Description,

                        IsDelete = bg.IsDelete,

                        CreateBy = bg.CreateBy,
                        CreateOn = bg.CreateOn,
                        ModifiedBy = bg.ModifiedBy,
                        ModifiedOn = bg.ModifiedOn,
                        DeletedBy = bg.DeletedBy,
                        DeletedOn = bg.DeletedOn,
                    }
                    ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                    ? $"Golongan darah dengan code {code} ditemukan"
                    : $"Golongan darah dengan code {code} tidak ada";
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
        public VMResponse Get(long? id = null)
        {
            try
            {
                VMTblMBloodGroup? data = FindById(id);

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
        public VMTblMBloodGroup? CheckCode(string code)
        {
            return (
                from bg in db.TblMBloodGroups
                where bg.Code == code && bg.IsDelete == false
                select new VMTblMBloodGroup
                {
                    Id = bg.Id,
                    Code = bg.Code,
                    Description = bg.Description,

                    IsDelete = bg.IsDelete,

                    CreateBy = bg.CreateBy,
                    CreateOn = bg.CreateOn,
                    ModifiedBy = bg.ModifiedBy,
                    ModifiedOn = bg.ModifiedOn,
                    DeletedBy = bg.DeletedBy,
                    DeletedOn = bg.DeletedOn,
                }
                ).FirstOrDefault();
        }
        public VMResponse Create(VMTblMBloodGroup formData)
        {
            try
            {
                TblMBloodGroup data = new TblMBloodGroup();


                data.Code = formData.Code.ToUpper();
                formData.Code = formData.Code.Replace(" ", "");
                data.Description = formData.Description;

                data.CreateOn = DateTime.Now;

                data.IsDelete = false;

                if (formData.Id == null || formData.Id == 0)
                {
                    VMTblMBloodGroup? checkData = CheckCode(formData.Code);
                    //string[] arrBG = { "A", "B", "AB", "O" };

                    response.data = checkData;

                    if (checkData != null)
                    {
                        if (formData.Code.ToUpper() == checkData.Code.ToUpper())
                        {
                            response.message = $"Golongan darah {data.Code} sudah ada";
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
                        //if(Array.Exists(arrBG, g => g == formData.Code.ToUpper()))
                        //{

                        //}
                        //else
                        //{
                        //    response.message = $"{data.Code} Tidak termasuk dalam golongan darah";
                        //    response.statusCode = HttpStatusCode.NoContent;
                        //}
                    }
                }
            }
            catch(Exception ex)
            {
                response.message=ex.Message;
            }
            finally { db.Dispose(); }
            return response;
        }
        public VMResponse Update(VMTblMBloodGroup formData)
        {
            try
            {
                TblMBloodGroup data = new TblMBloodGroup();
                VMTblMBloodGroup? existingData = FindById(formData.Id);
                VMTblMBloodGroup? checkData = CheckCode(formData.Code);
                string[] arrBG = { "A", "B", "AB", "O" };


                if (existingData != null && checkData == null)
                {
                    if (existingData.Code.ToUpper() == formData.Code.ToUpper())
                    {
                        response.message = $"Golongan darah {data.Code} sudah ada";
                        response.statusCode=HttpStatusCode.NoContent;
                    }
                    else
                    {
                        //if(Array.Exists(arrBG, g => g == formData.Code.ToUpper()))
                        //{
                        //    data.Id = existingData.Id;
                        //    data.Code = formData.Code.ToUpper();
                        //    data.Description = formData.Description;

                        //    data.CreateBy = existingData.CreateBy;
                        //    data.CreateOn = existingData.CreateOn;

                        //    data.ModifiedBy = formData.ModifiedBy;
                        //    data.ModifiedOn = DateTime.Now;

                        //    db.Update(data);
                        //    response.statusCode = HttpStatusCode.OK;
                        //}
                        //else
                        //{
                        //    response.message = $"{formData.Code} Tidak termasuk dalam golongan darah";
                        //    response.statusCode = HttpStatusCode.NoContent;
                        //}
                    }
                }
                else
                {
                    if (checkData.Code.ToUpper() == formData.Code.ToUpper())
                    {
                        response.message = $"Golongan darah {data.Code} sudah ada";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                    else
                    {
                        data.Id = existingData.Id;
                        data.Code = existingData.Code;
                        data.Description = formData.Description;

                        data.CreateBy = existingData.CreateBy;
                        data.CreateOn = existingData.CreateOn;

                        data.ModifiedBy = formData.ModifiedBy;
                        data.ModifiedOn = DateTime.Now;

                        db.Update(data);
                        response.statusCode = HttpStatusCode.OK;
                    }
                }

                db.SaveChanges();

                response.data = data;
            }
            catch (Exception ex)
            {
                response.message = "Data Golongan darah baru gagal diubah" + ex.Message;
            }
            finally { db.Dispose(); }
            return response;
        }
        public VMResponse Delete(long? id, long userId)
        {
            try
            {
                VMTblMBloodGroup? existingData = FindById(id);

                if(existingData != null)
                {
                    TblMBloodGroup data = new TblMBloodGroup();

                    data.Id = existingData.Id;
                    data.Code = existingData.Code;
                    data.Description = existingData.Description;

                    data.CreateBy= existingData.CreateBy;
                    data.CreateOn= existingData.CreateOn;

                    data.ModifiedBy= existingData.ModifiedBy;
                    data.ModifiedOn = existingData.ModifiedOn;

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
            catch(Exception ex)
            {
                response.message += $"{ex.Message}";
            }
            return response;
        }        
    }
}