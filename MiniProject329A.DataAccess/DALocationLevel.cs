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

    public class DALocationLevel
    {
        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();
        public DALocationLevel(MiniProject329AContext _db)
        {
            db = _db;
        }

        private VMTblMLocationLevel? FindById(long id)
        {
            return (
                from ll in db.TblMLocationLevels
                where ll.Id == id && ll.IsDelete == false
                select new VMTblMLocationLevel
                {
                    Id = ll.Id,
                    Name = ll.Name,
                    Abbreviation = ll.Abbreviation,
                    CreateBy = ll.CreateBy,
                    CreatedOn = ll.CreatedOn,
                    ModifiedBy = ll.ModifiedBy,
                    ModifiedOn = ll.ModifiedOn,
                    DeletedBy = ll.DeletedBy,
                    DeletedOn = ll.DeletedOn,
                    IsDelete = ll.IsDelete
                }).FirstOrDefault();
        }
        private VMTblMLocationLevel? FindByName(string name)
        {
            return (
                from ll in db.TblMLocationLevels
                where ll.Name == name && ll.IsDelete == false
                select new VMTblMLocationLevel
                {
                    Id = ll.Id,
                    Name = ll.Name,
                    Abbreviation = ll.Abbreviation,
                    CreateBy = ll.CreateBy,
                    CreatedOn = ll.CreatedOn,
                    ModifiedBy = ll.ModifiedBy,
                    ModifiedOn = ll.ModifiedOn,
                    DeletedBy = ll.DeletedBy,
                    DeletedOn = ll.DeletedOn,
                    IsDelete = ll.IsDelete
                }).FirstOrDefault();
        }

        public VMResponse GetById(long? id)
        {
            try
            {
                VMTblMLocationLevel? data = (
                    from ll in db.TblMLocationLevels
                    where ll.IsDelete == false
                    && ll.Id == id
                    select new VMTblMLocationLevel
                    {
                        Id = ll.Id,
                        Name = ll.Name,
                        Abbreviation = ll.Abbreviation,
                        CreateBy = ll.CreateBy,
                        CreatedOn = ll.CreatedOn,
                        ModifiedBy = ll.ModifiedBy,
                        ModifiedOn = ll.ModifiedOn,
                        DeletedBy = ll.DeletedBy,
                        DeletedOn = ll.DeletedOn,
                        IsDelete = ll.IsDelete
                    }).FirstOrDefault();
                response.data = data;
                response.message = (data == null) ? "Data Level Lokasi Tidak Ada" : "";
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
                List<VMTblMLocationLevel> data = (
                    from ll in db.TblMLocationLevels
                    where ll.IsDelete == false
                    && ll.Name.Contains(name ?? "")
                    select new VMTblMLocationLevel
                    {
                        Id = ll.Id,
                        Name = ll.Name,
                        Abbreviation = ll.Abbreviation,
                        CreateBy = ll.CreateBy,
                        CreatedOn = ll.CreatedOn,
                        ModifiedBy = ll.ModifiedBy,
                        ModifiedOn = ll.ModifiedOn,
                        DeletedBy = ll.DeletedBy,
                        DeletedOn = ll.DeletedOn,
                        IsDelete = ll.IsDelete
                    }).ToList();
                response.data = data;
                response.message = (data.Count < 1) ? $"Level Lokasi {name} Tidak Ada" :
                    $"Level Lokasi {name} Didapatkan!";
                response.statusCode = (data.Count < 1) ?
                    HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public VMResponse Add(VMTblMLocationLevel dataInput)
        {
            try
            {

                TblMLocationLevel data = new TblMLocationLevel();
                VMTblMLocationLevel? cekNama = FindByName(dataInput.Name);
                response.data = cekNama;
                if (response.data != null)
                {
                    if (cekNama.Name.ToLower() == dataInput.Name.ToLower())
                    {
                        response.message = $"Level Lokasi {data.Name} sudah ada";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                }
                else 
                {
                    data.Name = dataInput.Name;
                    data.Abbreviation = dataInput.Abbreviation;
                    data.CreateBy = dataInput.CreateBy;
                    data.CreatedOn = DateTime.Now;
                    data.ModifiedBy = dataInput.ModifiedBy;
                    data.ModifiedOn = dataInput.ModifiedOn;
                    data.DeletedBy = dataInput.DeletedBy;
                    data.DeletedOn = dataInput.DeletedOn;
                    data.IsDelete = false;
                    db.Add(data);
                    db.SaveChanges();

                    response.data = data;
                    response.message = "Level Lokasi Berhasil Ditambahkan";
                    response.statusCode = HttpStatusCode.Created;
                }
            }
            catch
            {
                response.message = "Gagal Menambahkan Level Lokasi";
            }
            return response;
        }

        public VMResponse Delete(long id,long userId)
        {
            try
            {
                VMTblMLocationLevel? existingData = FindById(id);
                if (existingData != null)
                {
                    TblMLocationLevel data = new TblMLocationLevel();

                    data.Id = existingData.Id;
                    data.Name = existingData.Name;
                    data.Abbreviation = existingData.Abbreviation;
                    data.CreateBy = existingData.CreateBy;
                    data.CreatedOn = existingData.CreatedOn;
                    data.ModifiedBy = existingData.ModifiedBy;
                    data.ModifiedOn = existingData.ModifiedOn;

                    data.DeletedBy = userId;
                    data.DeletedOn = DateTime.Now;
                    data.IsDelete = true;

                    db.Update(data);
                    db.SaveChanges();
                    response.data = data;

                    response.message = ($"Level Lokasi berhasil dihapus");
                    response.statusCode = HttpStatusCode.OK;
                }
            }
            catch
            {
                response.message = $"Level Lokasi Gagal Dihapus";
            }
            return response;
        }

        public VMResponse Edit(VMTblMLocationLevel dataInput)
        {
            try
            {
                VMTblMLocationLevel? existingData = FindById(dataInput.Id);
                if (existingData != null)
                {
                    TblMLocationLevel data = new TblMLocationLevel();

                    data.Id = existingData.Id;
                    data.Name = dataInput.Name;
                    data.Abbreviation = dataInput.Abbreviation;
                    data.CreateBy = existingData.CreateBy;
                    data.CreatedOn = existingData.CreatedOn;
                    data.ModifiedBy = dataInput.ModifiedBy;
                    data.ModifiedOn = DateTime.Now;

                    data.DeletedBy = dataInput.DeletedBy;
                    data.DeletedOn = existingData.DeletedOn;
                    data.IsDelete = false;

                    db.Update(data);
                    db.SaveChanges();
                    response.data = data;
                    response.message = ($"Level Lokasi {dataInput} Tidak Ada");
                    response.statusCode = HttpStatusCode.OK;
                }
                else
                {
                    throw new Exception($"Level Lokasi {dataInput.Name} Tidak Ada ");
                }
            }
            catch
            {
                response.message = "Level Lokasi Metode Pembayaran Tidak Ada";
            }
            return response;

        }
    }
}
