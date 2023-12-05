using MiniProject329A.DataAccess;
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
    public class DAEducationLevel
    {
        private readonly MiniProject329AContext db; 

        private VMResponse response = new VMResponse();

        public DAEducationLevel(MiniProject329AContext _db)
        {
            db = _db;
            response = new VMResponse();
        }

        //Check nama
        private VMTblMEducationLevel? CheckName(string name)
        {
            return
            (
                from el in db.TblMEducationLevels
                where el.Name == name
                    && el.IsDelete == false
                select new VMTblMEducationLevel
                {
                    Id = el.Id,
                    Name = el.Name,

                    CreatedOn = el.CreatedOn,
                    CreateBy = el.CreateBy,

                    ModifiedOn = el.ModifiedOn,
                    ModifiedBy = el.ModifiedBy,

                    DeletedOn = el.DeletedOn,
                    DeletedBy = el.DeletedBy,

                    IsDelete = el.IsDelete
                }
            ).FirstOrDefault();
        }

        //Create Update Data
        public VMResponse CreateUpdate(VMTblMEducationLevel dataInput)
        {
            string strCreateUpdate = "";
            try
            {
                //Prepare empty Data Eductaion Level for Incoming Data
                TblMEducationLevel data = new TblMEducationLevel();

                data.Name = dataInput.Name;
                dataInput.Name = dataInput.Name.Replace(" ","");

                data.IsDelete = false;

                if (dataInput.Id == null || dataInput.Id == 0)
                {
                    strCreateUpdate = "Buat";

                    //Assign each Data Model Column from each Input Data
                    data.CreateBy = 1;
                    data.CreatedOn = DateTime.Now;

                    //Update New data
                    db.Add(data);
                    response.statusCode = HttpStatusCode.Created;

                    //Save Data to Database
                    db.SaveChanges();

                    //Update API Respon
                    response.data = data;
                    response.message = $"Tingkat pendidikan berhasil di {strCreateUpdate}";
                }

                //Update Data to Database
                else
                {
                    strCreateUpdate = "Ubah";

                    VMTblMEducationLevel? existingData = FindById(dataInput.Id);

                    //If Data Exist
                    if(existingData != null)
                    {
                        data.Id = existingData.Id;

                        data.CreateBy = existingData.CreateBy;
                        data.CreatedOn = existingData.CreatedOn;

                        data.ModifiedBy = 1;
                        data.ModifiedOn = DateTime.Now;

                        data.DeletedBy = existingData.DeletedBy;
                        data.DeletedOn = existingData.DeletedOn;


                        db.Update(data);
                        response.statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        throw new Exception($"Tingkat pendidikan gagal {strCreateUpdate}");
                    }
                    //Save Data to Database
                    db.SaveChanges();

                    //Update API Respon
                    response.data = data;
                    response.message = $"Tingkat pendidikan berhasil di {strCreateUpdate}";
                }
                
            }
            catch (Exception ex)
            {
                response.message = $"Tingkat pendidikan gagal di {strCreateUpdate}" + ex.Message;
            }
            return response;
        }
        //Read Data By Id
        private VMTblMEducationLevel? FindById(long? Id)
        {
            return
            (
                from el in db.TblMEducationLevels
                where el.IsDelete == false
                    && el.Id == Id
                select new VMTblMEducationLevel
                {
                    Id = el.Id,
                    Name = el.Name,

                    CreatedOn = el.CreatedOn,
                    CreateBy = el.CreateBy,

                    ModifiedOn = el.ModifiedOn,
                    ModifiedBy = el.ModifiedBy,

                    DeletedOn = el.DeletedOn,
                    DeletedBy = el.DeletedBy,

                    IsDelete = el.IsDelete
                }

            ).FirstOrDefault();
        }

        public VMResponse? GetById(long? id = null)
        {
            try
            {
                VMTblMEducationLevel? data = FindById(id);

                response.data = data;

                response.message = (data == null)
                ? $"Tingkat pendidikan dengan = {id} tidak ditemukan!"
                : $"Tingkat pendidikan dengan = {id} berhasil ditemukan";

                response.statusCode = (data == null)
                ? HttpStatusCode.NoContent
                : HttpStatusCode.OK;
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        //Read Data By Name
        public VMResponse? GetByName(string? name = null)
        {
            try
            {
                //Get All Education Level Data
                List<VMTblMEducationLevel> data =
                (
                    from el in db.TblMEducationLevels
                    where el.IsDelete == false
                        && el.Name.Contains(name ?? "")
                    select new VMTblMEducationLevel
                    {
                        Id = el.Id,
                        Name = el.Name,

                        CreatedOn = el.CreatedOn,
                        CreateBy = el.CreateBy,

                        ModifiedOn = el.ModifiedOn,
                        ModifiedBy = el.ModifiedBy,

                        DeletedOn = el.DeletedOn,
                        DeletedBy = el.DeletedBy,

                        IsDelete = el.IsDelete
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count >= 1)
                ? $"Tingkat Pendidkan {name} ditemukan"
                : $"Tingkat Pendidikan {name} tidak ditemukan";

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

        //Delete Data
        public VMResponse Delete(long id, int userId)
        {
            VMTblMEducationLevel? existingData = FindById(id);
            try
            {
                if(existingData != null)
                {
                    TblMEducationLevel data = new TblMEducationLevel();

                    data.Id = id;
                    data.Name = existingData.Name;

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
                    response.message = "Tingkat pendidikan berhasil ditemukan";
                    response.statusCode = HttpStatusCode.OK;
                }
                else
                {
                    throw new Exception($"Tingkat pendidikan dengan : {existingData.Name} tidak ditemukan");
                }
            }
            catch(Exception ex)
            {
                response.message = $"Tingkat pendidikan dengan : {id} gagal dihapus!" + ex.Message;
            }
            return response;
        }

        //Can't add if existing data
        public VMResponse NameCheck(string name)
        {
            try
            {
                VMTblMEducationLevel? NameCheck = CheckName(name);
                response.data = NameCheck;

                if(response.data != null)
                {
                    if(NameCheck.Name.ToUpper() == name.ToUpper())
                    {
                        response.message = (name == null)
                        ? "Jenjang pendidikan belum digunakan"
                        : "Jenjang pendidikan sudah digunakan";

                        response.statusCode = (name == null)
                        ? HttpStatusCode.NoContent
                        : HttpStatusCode.OK;
                    }
                }
                else
                {
                    response.message = "Jenjang pendidikan belum digunakan";
                }

                //response.data = data;
                //response.message = (data == null)
                //    ? "Jenjang pendidikan belum digunakan"
                //    : "Jenjang pendidikan sudah digunakan";
                //response.statusCode = (data == null)
                //    ? HttpStatusCode.NoContent
                //    : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
    }
}
