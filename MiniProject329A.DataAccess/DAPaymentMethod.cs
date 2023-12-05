using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MiniProject329A.DataAccess
{

    public class DAPaymentMethod
    {
        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();
        public DAPaymentMethod(MiniProject329AContext _db)
        {
            db = _db;
        }

        private VMTblMPaymentMethod? FindById(long id)
        {
            return (
                from pm in db.TblMPaymentMethods
                where pm.IsDelete == false
                && pm.Id == id
                select new VMTblMPaymentMethod
                {
                    Id = pm.Id,
                    Name = pm.Name,
                    CreateBy = pm.CreateBy,
                    CreatedOn = pm.CreatedOn,
                    ModifiedBy = pm.ModifiedBy,
                    ModifiedOn = pm.ModifiedOn,
                    DeletedBy = pm.DeletedBy,
                    DeletedOn = pm.DeletedOn,
                    IsDelete = pm.IsDelete
                }).FirstOrDefault();
        }
        private VMTblMPaymentMethod? FindByName(string name)
        {
            return(from pm in db.TblMPaymentMethods
                   where pm.IsDelete == false
                   && pm.Name == name
                   select new VMTblMPaymentMethod
                   {
                       Id = pm.Id,
                       Name = pm.Name,
                       CreateBy = pm.CreateBy,
                       CreatedOn = pm.CreatedOn,
                       ModifiedBy = pm.ModifiedBy,
                       ModifiedOn = pm.ModifiedOn,
                       DeletedBy = pm.DeletedBy,
                       DeletedOn = pm.DeletedOn,
                       IsDelete = pm.IsDelete
                   }).FirstOrDefault();
        }
       

        public VMResponse GetById(long? id)
        {
            try
            {
                VMTblMPaymentMethod? data =
                    (
                    from pm in db.TblMPaymentMethods
                    where pm.IsDelete == false
                    && pm.Id == id
                    select new VMTblMPaymentMethod
                    {
                        Id = pm.Id,
                        Name = pm.Name,
                        CreateBy = pm.CreateBy,
                        CreatedOn = pm.CreatedOn,
                        ModifiedBy = pm.ModifiedBy,
                        ModifiedOn = pm.ModifiedOn,
                        DeletedBy = pm.DeletedBy,
                        DeletedOn = pm.DeletedOn,
                        IsDelete = pm.IsDelete
                    }).FirstOrDefault();
                response.data = data;
                response.message = (data == null) ? "Data Metode Pembayaran Tidak Ada" : "";
                response.statusCode = (data == null) ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
   
        public VMResponse GetByName(string? name = null)
        {
            try
            {
                List<VMTblMPaymentMethod> data = (
                    from pm in db.TblMPaymentMethods
                    where pm.IsDelete == false
                    && pm.Name.Contains(name ?? "")
                    select new VMTblMPaymentMethod
                    {
                        Id = pm.Id,
                        Name = pm.Name,

                        CreateBy = pm.CreateBy,
                        CreatedOn = pm.CreatedOn,
                        ModifiedBy = pm.ModifiedBy,
                        ModifiedOn = pm.ModifiedOn,
                        DeletedBy = pm.DeletedBy,
                        DeletedOn = pm.DeletedOn,

                        IsDelete = pm.IsDelete
                    }).ToList();
                response.data = data;
                response.message = (data.Count < 1) ? $"Metode Pembayaran {name} Tidak Ada" :
                    $"Metode Pembayaran {name} Didapatkan!";
                response.statusCode = (data.Count < 1) ?
                    HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public VMResponse Delete(long id, long userId)
        {
            try
            {
                VMTblMPaymentMethod? existingData = FindById(id);
                if (existingData != null)
                {
                    TblMPaymentMethod data = new TblMPaymentMethod();

                    data.Id = existingData.Id;
                    data.Name = existingData.Name;

                    data.CreateBy = existingData.CreateBy;

                    data.CreatedOn = existingData.CreatedOn;
                    data.ModifiedBy = existingData.ModifiedBy;
                    data.ModifiedOn = existingData.ModifiedOn;

                    data.DeletedOn = DateTime.Now;
                    data.DeletedBy = userId;
                    data.IsDelete = true;

                    db.Update(data);
                    db.SaveChanges();
                    response.data = data;

                    response.message = ($"Metode Pembayaran berhasil dihapus");
                    response.statusCode = HttpStatusCode.OK;
                }
            }
            catch
            {
                response.message = $"Metode Pembayaran Gagal Dihapus";
            }

            return response;
        }

        public VMResponse Add(VMTblMPaymentMethod dataInput)
        {
            try
            {
                TblMPaymentMethod data = new TblMPaymentMethod();
                VMTblMPaymentMethod? cekNama = FindByName(dataInput.Name);
                response.data = cekNama;
                if(response.data != null)
                {
                    if(cekNama.Name.ToLower() == dataInput.Name.ToLower())
                    {
                        response.message = $"Metode Pembayaran {data.Name} sudah ada";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    data.Name = dataInput.Name;
                    data.CreateBy = dataInput.CreateBy;
                    data.CreatedOn = DateTime.Now;
                    data.ModifiedBy = dataInput.ModifiedBy;
                    data.ModifiedOn = dataInput.ModifiedOn;
                    data.DeletedOn = dataInput.DeletedOn;
                    data.DeletedBy = dataInput.DeletedBy;

                    data.IsDelete = false;
                    db.Add(data);
                    db.SaveChanges();

                    response.data = data;
                    response.message = "Metode Pembayaran Berhasil Ditambahkan";
                    response.statusCode = HttpStatusCode.Created;
                }
            }
            catch
            {
                response.message = "Gagal Menambahkan Metode Pembayaran";
            }
            return response;
        }

        public VMResponse Edit(VMTblMPaymentMethod dataInput)
        {
            try
            {
                TblMPaymentMethod data = new TblMPaymentMethod();
                VMTblMPaymentMethod? existingData = FindById(dataInput.Id);
                VMTblMPaymentMethod? cekNama = FindByName(dataInput.Name);

                response.data = cekNama;
                if (response.data != null)
                {
                    if (cekNama.Name.ToLower() == dataInput.Name.ToLower())
                    {
                        response.message = $"Metode Pembayaran {data.Name} sudah ada";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    data.Id = existingData.Id;
                    data.Name = dataInput.Name;
                    data.CreateBy = existingData.CreateBy;
                    data.CreatedOn = existingData.CreatedOn;
                    data.ModifiedBy = dataInput.ModifiedBy;
                    data.ModifiedOn = DateTime.Now;
                    data.DeletedBy = existingData.DeletedBy;
                    data.DeletedOn = existingData.DeletedOn;
                    data.IsDelete = false;

                    db.Update(data);
                    db.SaveChanges();

                    response.data = data;
                    response.message = ($"Metode Pembayaran {dataInput} Tidak Ada");
                    response.statusCode = HttpStatusCode.OK;

                }
            }
            catch
            {
                response.message = "Gagal Mengedit Metode Pembayaran ";
            }
            return response;
        }
    }
}
