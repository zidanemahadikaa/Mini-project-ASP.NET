using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MiniProject329A.DataAccess
{
    public class DABiodata
    {
        private readonly MiniProject329AContext db;
        public DABiodata(MiniProject329AContext _db)
        {
            db = _db;
        }
        private VMResponse response = new VMResponse();

        public VMResponse GetAll()
        {
            try
            {
                //Get All Category Data
                List<VMTblMBiodata> data = (
                    from bio in db.TblMBiodata
                    where bio.IsDelete == false
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

                        IsDelete = bio.IsDelete,
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count < 1) ? "Biodata cannot be fatched" : "Biodata Successfully fetched";
                response.statusCode = (data.Count < 1) ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public long GetAllData()
        {
            List<VMTblMBiodata> data = new List<VMTblMBiodata>();
            try
            {
                //Get All Category Data
                    data = (
                    from bio in db.TblMBiodata
                    where bio.IsDelete == false
                    select new VMTblMBiodata
                    {
                        Id = bio.Id,
                        Fullname = bio.Fullname,
                        MobilePhone = bio.MobilePhone,
                        Image = bio.Image,
                        ImagePath = bio.ImagePath,

                        CreateBy = bio.CreateBy,
                        CreateOn = DateTime.Now,

                        ModifiedBy = bio.ModifiedBy,
                        ModifiedOn = bio.ModifiedOn,

                        DeletedBy = bio.DeletedBy,
                        DeletedOn = bio.DeletedOn,

                        IsDelete = bio.IsDelete,
                    }
                ).ToList();

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return data.Count;
        }
        public VMTblMBiodata? FindById(int id)
        {
            return (
                from bio in db.TblMBiodata
                where bio.IsDelete == false && bio.Id == id
                select new VMTblMBiodata
                {
                    Id = bio.Id,
                    Fullname = bio.Fullname,
                    MobilePhone = bio.MobilePhone,
                    Image = bio.Image,
                    ImagePath = bio.ImagePath,

                    CreateBy = bio.CreateBy,
                    CreateOn = DateTime.Now,

                    ModifiedBy = bio.ModifiedBy,
                    ModifiedOn = bio.ModifiedOn,

                    DeletedBy = bio.DeletedBy,
                    DeletedOn = bio.DeletedOn,

                    IsDelete = bio.IsDelete,
                }
            ).FirstOrDefault();
        }
        public VMResponse GetById(int id)
        {
            try
            {
                VMTblMBiodata? data = FindById(id);

                response.data = data;
                response.message = (data == null)
                    ? $"Biodata with id = {id} is not exist"
                    : $"Biodata with id = {id} successfully fatched";
                response.statusCode = (data == null)
                    ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            finally
            {
                db.Dispose();
            }
            return response;
        }
        public VMResponse? GetByName(string? name = null)
        {
            string? strName = !string.IsNullOrEmpty(name) ? $" with Name = {name}" : "";
            try
            {
                List<VMTblMBiodata> data = (
                    from bio in db.TblMBiodata
                    where bio.IsDelete == false &&
                    bio.Fullname.Contains(name ?? "")
                    select new VMTblMBiodata
                    {
                        Id = bio.Id,
                        Fullname = bio.Fullname,
                        MobilePhone = bio.MobilePhone,
                        Image = bio.Image,
                        ImagePath = bio.ImagePath,

                        CreateBy = bio.CreateBy,
                        CreateOn = DateTime.Now,

                        ModifiedBy = bio.ModifiedBy,
                        ModifiedOn = bio.ModifiedOn,

                        DeletedBy = bio.DeletedBy,
                        DeletedOn = bio.DeletedOn,

                        IsDelete = bio.IsDelete,                    
                    }
                    ).ToList();
                response.data = data;
                response.message = "....";
                response.statusCode = (data.Count < 1)
                    ? HttpStatusCode.NoContent
                    : HttpStatusCode.OK;
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        private long GetIdGenerator(long totalData)
        {
            totalData = GetAllData();
            long currentId = (totalData > 0) ? (totalData + 1) : 1;

            return currentId;
        }
        public VMResponse CreateBiodata(VMTblMBiodata formData)
        {
            try
            {
                long IdBiodata = db.TblMBiodata.OrderByDescending(
                    bio => bio.Id).Select(bio => bio.Id).FirstOrDefault();

                TblMBiodata data = new TblMBiodata();

                //assign each datamodel column from each data input
                data.Fullname = formData.Fullname;
                data.MobilePhone = formData.MobilePhone;
                data.Image = formData.Image;
                data.ImagePath = formData.ImagePath;

                data.CreateBy = IdBiodata + 1;
                data.CreateOn = DateTime.Now;

                data.IsDelete = false;


                //prosess create new data in table from data model
                db.Add(data);
                db.SaveChanges();

                //update api response
                response.data = data;
                response.message = "New biodata has been succesfully created";
                response.statusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            finally { db.Dispose(); }
            return response;
        }
        public VMTblMBiodata? FindByIdLong(long id)
        {
            return (
                from bio in db.TblMBiodata
                where bio.IsDelete == false && bio.Id == id
                select new VMTblMBiodata
                {
                    Id = bio.Id,
                    Fullname = bio.Fullname,
                    MobilePhone = bio.MobilePhone,
                    Image = bio.Image,
                    ImagePath = bio.ImagePath,

                    CreateBy = bio.CreateBy,
                    CreateOn = DateTime.Now,

                    ModifiedBy = bio.ModifiedBy,
                    ModifiedOn = bio.ModifiedOn,

                    DeletedBy = bio.DeletedBy,
                    DeletedOn = bio.DeletedOn,

                    IsDelete = bio.IsDelete,
                }
            ).FirstOrDefault();
        }
        public VMResponse UpdateBiodata(VMTblMBiodata formData)
        {
            try
            {
                TblMBiodata data = new TblMBiodata();

                data.ImagePath= formData.ImagePath;
                data.IsDelete= false;

                VMTblMBiodata? dataExisting = FindByIdLong(formData.Id);

                if (dataExisting != null)
                {
                    data.Id = dataExisting.Id;
                    data.MobilePhone = formData.MobilePhone;
                    data.Image = formData.Image;
                    data.ImagePath = formData.ImagePath;

           
                    data.CreateBy = dataExisting.CreateBy;
                    data.CreateBy = dataExisting.CreateBy;

                    data.ModifiedBy = GetIdGenerator(data.Id);
                    data.ModifiedOn = DateTime.Now;

                    db.Update(data);
                    response.statusCode = HttpStatusCode.OK;

                }

                db.SaveChanges();

                //update api response
                response.data = data;
                response.message = "New biodata has been succesfully created";
                response.statusCode = HttpStatusCode.Created;

                data=null;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse GetByLongId(long id)
        {
            try
            {
                VMTblMBiodata? data = FindByIdLong(id);

                response.data = data;
                response.message = (data == null)
                    ? $"Biodata with id = {id} is not exist"
                    : $"Biodata with id = {id} successfully fatched";
                response.statusCode = (data == null)
                    ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
    }
}
