using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
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
    public class DALocation
    {
        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();
        public DALocation(MiniProject329AContext _db)
        {
            db = _db;
        }
        private VMTblMLocation? FindById(long id)
        {
            return (
                from l in db.TblMLocations
                join ll in db.TblMLocationLevels on l.LocationLevelId equals ll.Id
                join cr in db.TblMCustomerRelations on l.ParentId equals cr.Id
                where l.IsDelete == false && l.Id == id
                select new VMTblMLocation
                {
                    Id = l.Id,
                    Name = l.Name,
                    ParentId = cr.Id,
                    LocationLevelId = ll.Id,
                    CreateBy = l.CreateBy,
                    CreatedOn = l.CreatedOn,
                    ModifiedBy = l.ModifiedBy,
                    ModifiedOn = l.ModifiedOn,
                    DeletedBy = l.DeletedBy,
                    DeletedOn = l.DeletedOn,
                    IsDelete = l.IsDelete
                }).FirstOrDefault();
        }

        private VMTblMLocation? FindByName(string? name)
        {
            return (
                from l in db.TblMLocations
                join ll in db.TblMLocationLevels on l.LocationLevelId equals ll.Id
                join cr in db.TblMCustomerRelations on l.ParentId equals cr.Id
                where l.IsDelete == false && l.Name == name
                select new VMTblMLocation
                {
                    Id = l.Id,
                    Name = l.Name,
                    ParentId = cr.Id,
                    LocationLevelId = ll.Id,
                    CreateBy = l.CreateBy,
                    CreatedOn = l.CreatedOn,
                    ModifiedBy = l.ModifiedBy,
                    ModifiedOn = l.ModifiedOn,
                    DeletedBy = l.DeletedBy,
                    DeletedOn = l.DeletedOn,
                    IsDelete = l.IsDelete
                }).FirstOrDefault();
        }
        private VMTblMLocation? FindByLevelId(long? id)
        {
            return (
                from l in db.TblMLocations
                join ll in db.TblMLocationLevels on l.LocationLevelId equals ll.Id
                where l.IsDelete == false && l.LocationLevelId== id 
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
                    IsDelete = l.IsDelete
                }).FirstOrDefault();
        }
        private VMTblMLocation? FindByParentId(long? id)
        {
            return (
                from l in db.TblMLocations
                join ll in db.TblMLocationLevels on l.LocationLevelId equals ll.Id
                where l.IsDelete == false && l.ParentId == id
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
                    IsDelete = l.IsDelete
                }).FirstOrDefault();
        }

        public VMResponse GetByParent(long? id)
        {
            try
            {
                VMTblMLocation? data = (
                   from l in db.TblMLocations
                   join ll in db.TblMLocationLevels on l.LocationLevelId equals ll.Id
                   where l.IsDelete == false
                   && l.ParentId == id
                   select new VMTblMLocation
                   {
                       Id = l.Id,
                       Name = l.Name,
                       //ParentId = cr.Id,
                       LocationLevelId = ll.Id,
                       CreateBy = l.CreateBy,
                       CreatedOn = l.CreatedOn,
                       ModifiedBy = l.ModifiedBy,
                       ModifiedOn = l.ModifiedOn,
                       DeletedBy = l.DeletedBy,
                       DeletedOn = l.DeletedOn,
                       IsDelete = l.IsDelete
                   }).FirstOrDefault();
                response.data = data;
                response.message = (data == null) ?
                    $"Lokasi Tidak Ada" :
                    $"Lokasi Berhasil didapatkan!";
                response.statusCode = (data == null) ?
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
                VMTblMLocation? data = (
                   from l in db.TblMLocations
                   join ll in db.TblMLocationLevels on l.LocationLevelId equals ll.Id
                   where l.IsDelete == false
                   && l.Id == id
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
                       IsDelete = l.IsDelete
                   }).FirstOrDefault();
                response.data = data;
                response.message = (data == null) ?
                    $"Lokasi Tidak Ada" :
                    $"Lokasi Berhasil didapatkan!";
                response.statusCode = (data == null) ?
                    HttpStatusCode.NoContent : HttpStatusCode.OK;
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
                List<VMTblMLocation> data = (
                    from l in db.TblMLocations
                    join ll in db.TblMLocationLevels on l.LocationLevelId equals ll.Id
                    where l.IsDelete == false
                    && l.Name.Contains(name ?? "")
                    select new VMTblMLocation
                    {
                        Id = l.Id,
                        Name = l.Name,
                        LocationLevelId = ll.Id,
                        ParentId = l.ParentId,
                        CreateBy = l.CreateBy,
                        CreatedOn = l.CreatedOn,
                        ModifiedBy = l.ModifiedBy,
                        ModifiedOn = l.ModifiedOn,
                        DeletedBy = l.DeletedBy,
                        DeletedOn = l.DeletedOn,
                        IsDelete = l.IsDelete
                    }).ToList();
                response.data = data;
                response.message = (data.Count < 1) ?
                    $"Nama Lokasi {name} Tidak Ada" :
                    $"Nama Lokasi {name} Berhasil dicari!";
                response.statusCode = (data.Count < 1) ?
                    HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse GetByNameLevel(string? name = null)
        {
            try
            {
                List<VMTblMLocation> data = (
                    from l in db.TblMLocations
                    join ll in db.TblMLocationLevels on l.LocationLevelId equals ll.Id
                    where l.IsDelete == false
                    && ll.Name.Contains(name ?? "")
                    select new VMTblMLocation
                    {
                        Id = l.Id,
                        Name = l.Name,
                        LocationLevelId = ll.Id,
                        ParentId = l.ParentId,
                        CreateBy = l.CreateBy,
                        CreatedOn = l.CreatedOn,
                        ModifiedBy = l.ModifiedBy,
                        ModifiedOn = l.ModifiedOn,
                        DeletedBy = l.DeletedBy,
                        DeletedOn = l.DeletedOn,
                        IsDelete = l.IsDelete
                    }).ToList();
                response.data = data;
                response.message = (data.Count < 1) ?
                    $"Nama Lokasi {name} Tidak Ada" :
                    $"Nama Lokasi {name} Berhasil dicari!";
                response.statusCode = (data.Count < 1) ?
                    HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }


        public VMResponse Add(VMTblMLocation dataInput)
        {
            try
            {
                VMTblMLocation? cekParent = FindByParentId(dataInput.ParentId);
                VMTblMLocation? cekNama = FindByName(dataInput.Name);
                VMTblMLocation? cekLevel = FindByLevelId(dataInput.LocationLevelId);
                TblMLocation data = new TblMLocation();

                var responseData = new
                {
                    ExistingLocation = cekNama,
                    ExistingLevel = cekLevel,
                    ExistingParent = cekParent
                };

                response.data = responseData;

                if (cekNama != null && cekLevel != null && cekParent != null)
                {
                    if (cekNama.Name.ToLower() == dataInput.Name.ToLower() && cekLevel.LocationLevelId == dataInput.LocationLevelId && cekParent.ParentId == dataInput.ParentId)
                    {
                        response.message = $"Lokasi {data.Name} sudah ada, ";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    data.Name = dataInput.Name;
                    data.LocationLevelId = dataInput.LocationLevelId;
                    data.CreateBy = dataInput.CreateBy;
                    data.CreatedOn = DateTime.Now;
                    data.ModifiedBy = dataInput.ModifiedBy;
                    data.ModifiedOn = dataInput.ModifiedOn;
                    data.DeletedBy = dataInput.DeletedBy;
                    data.DeletedOn = dataInput.DeletedOn;
                    data.IsDelete = false;
                    data.ParentId = dataInput.ParentId;

                    db.Update(data);
                    db.SaveChanges();
                    response.statusCode = HttpStatusCode.OK;
                    response.data = data;
                    response.message = "Lokasi Berhasil ditambahkan!";
                }
            }
            catch
            {
                response.message = "Gagal Menambahkan Lokasi";
            }
            return response;
        }


        public VMResponse Edit(VMTblMLocation dataInput)
        {

            try
            {
                VMTblMLocation? cekParent = FindByParentId(dataInput.ParentId);
                VMTblMLocation? cekNama = FindByName(dataInput.Name);
                VMTblMLocation? cekLevel = FindByLevelId(dataInput.LocationLevelId);
                TblMLocation data = new TblMLocation();

                var responseData = new
                {
                    ExistingLocation = cekNama,
                    ExistingLevel = cekLevel,
                    ExistingParent = cekParent
                };

                response.data = responseData;

                if (cekNama != null && cekLevel != null && cekParent != null)
                {
                    if (cekNama.Name.ToLower() == dataInput.Name.ToLower() && cekLevel.LocationLevelId == dataInput.LocationLevelId && cekParent.ParentId == dataInput.ParentId)
                    {
                        response.message = $"Lokasi {data.Name} sudah ada ";
                        response.statusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    VMTblMLocation? existingData = FindById(dataInput.Id);
                    data.Id = existingData.Id;
                    data.Name = dataInput.Name;
                    data.LocationLevelId = dataInput.LocationLevelId;
                    data.CreateBy = existingData.CreateBy;
                    data.CreatedOn = existingData.CreatedOn;
                    data.ModifiedBy = dataInput.ModifiedBy;
                    data.ModifiedOn = DateTime.Now;
                    data.DeletedBy = existingData.DeletedBy;
                    data.DeletedOn = existingData.DeletedOn;
                    data.IsDelete = false;
                    data.ParentId = dataInput.ParentId;
                    db.Update(data);
                    db.SaveChanges();
                    response.statusCode = HttpStatusCode.OK;
                    response.data = data;
                    response.message = "Lokasi Berhasil diedit!";
                }
            }
            catch
            {
                response.message = "Gagal Menambahkan Lokasi";
            }
            return response;
        }

        public VMResponse Delete(long id, long userId)
        {
            try
            {
                VMTblMLocation? existingData = FindById(id);
                if (existingData != null)
                {
                    TblMLocation data = new TblMLocation();
                    data.Id = existingData.Id;
                    data.Name = existingData.Name;
                    data.LocationLevelId = existingData.LocationLevelId;
                    data.CreateBy = existingData.CreateBy;
                    data.CreatedOn = existingData.CreatedOn;
                    data.ModifiedBy = existingData.ModifiedBy;
                    data.ModifiedOn = existingData.ModifiedOn;
                    data.DeletedOn = DateTime.Now;
                    data.IsDelete = true;
                    data.ParentId = existingData.ParentId;
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
    }
}
