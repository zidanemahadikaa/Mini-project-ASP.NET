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
    public class DASpecialization
    {
        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();
        public DASpecialization(MiniProject329AContext _db)
        {
            db = _db;
        }
        private VMTblMSpecialization? FindById(long id)
        {
            return (from s in db.TblMSpecializations
                    where s.IsDelete == false
                    && s.Id==id
                    select new VMTblMSpecialization
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CreateBy = s.CreateBy,
                        CreatedOn = s.CreatedOn,
                        ModifiedBy = s.ModifiedBy,
                        ModifiedOn = s.ModifiedOn,
                        DeletedBy = s.DeletedBy,
                        DeletedOn = s.DeletedOn,
                        IsDelete = s.IsDelete
                    }).FirstOrDefault();
        }
        private VMTblMSpecialization? FindByName(string name)
        {
            return (from s in db.TblMSpecializations
                    where s.IsDelete == false
                    && s.Name == name
                    select new VMTblMSpecialization
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CreateBy = s.CreateBy,
                        CreatedOn = s.CreatedOn,
                        ModifiedBy = s.ModifiedBy,
                        ModifiedOn = s.ModifiedOn,
                        DeletedBy = s.DeletedBy,
                        DeletedOn = s.DeletedOn,
                        IsDelete = s.IsDelete
                    }).FirstOrDefault();
        }
        public VMResponse GetByName(string? name = null)
        {
            try
            {
                List<VMTblMSpecialization> data = (
                    from s in db.TblMSpecializations
                    where s.IsDelete == false
                    && s.Name.Contains(name ?? "")
                    select new VMTblMSpecialization
                    {
                        Id = s.Id,
                        Name = s.Name,

                        CreateBy = s.CreateBy,
                        CreatedOn = s.CreatedOn,
                        ModifiedBy = s.ModifiedBy,
                        ModifiedOn = s.ModifiedOn,
                        DeletedBy = s.DeletedBy,
                        DeletedOn = s.DeletedOn,
                        IsDelete = s.IsDelete
                    }).ToList();
                response.data = data;
                response.message = (data.Count < 1) ? $"Spesialis {name} Tidak Ada" :
                    $"Spesialis {name} Didapatkan!";
                response.statusCode = (data.Count < 1) ?
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
                VMTblMSpecialization? data =
                    (
                    from s in db.TblMSpecializations
                    where s.IsDelete == false
                    && s.Id == id
                    select new VMTblMSpecialization
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CreateBy = s.CreateBy,
                        CreatedOn = s.CreatedOn,
                        ModifiedBy = s.ModifiedBy,
                        ModifiedOn = s.ModifiedOn,
                        DeletedBy = s.DeletedBy,
                        DeletedOn = s.DeletedOn,
                        IsDelete = s.IsDelete
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
        public VMResponse Delete(long id, long userId)
        {
            try
            {
                VMTblMSpecialization? existingData = FindById(id);
                if (existingData != null)
                {
                    TblMSpecialization data = new TblMSpecialization();

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

                    response.message = ($"Spesialis berhasil dihapus");
                    response.statusCode = HttpStatusCode.OK;
                }
            }
            catch
            {
                response.message = $"Spesialis Gagal Dihapus";
            }

            return response;
        }
        public VMResponse Add(VMTblMSpecialization dataInput)
        {
            try
            {
                TblMSpecialization data = new TblMSpecialization();
                VMTblMSpecialization? cekNama = FindByName(dataInput.Name);
                response.data = cekNama;
                if (response.data != null)
                {
                    if (cekNama.Name.ToLower() == dataInput.Name.ToLower())
                    {
                        response.message = $"Spesialis {data.Name} sudah ada";
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
                    response.message = "Spesialis Berhasil Ditambahkan";
                    response.statusCode = HttpStatusCode.Created;
                }
            }
            catch
            {
                response.message = "Gagal Menambahkan Spesialis";
            }
            return response;
        }

        public VMResponse Edit(VMTblMSpecialization dataInput)
        {
            try
            {
                TblMSpecialization data = new TblMSpecialization();
                VMTblMSpecialization? existingData = FindById(dataInput.Id);
                VMTblMSpecialization? cekNama = FindByName(dataInput.Name);

                response.data = cekNama;
                if (response.data != null)
                {
                    if (cekNama.Name.ToLower() == dataInput.Name.ToLower())
                    {
                        response.message = $"Spesialis {data.Name} sudah ada";
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
                    response.message = ($"Spesialis {dataInput}");
                    response.statusCode = HttpStatusCode.OK;

                }
            }
            catch
            {
                response.message = "Gagal Edit Spesialis ";
            }
            return response;
        }
    }
}
