using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MiniProject329A.DataAccess
{
    public class DARole
    {
        private readonly MiniProject329AContext db;
        public DARole(MiniProject329AContext _db)
        {
            db = _db;
        }
        private VMResponse response = new VMResponse();

        public VMResponse GetAll()
        {
            try
            {
                //Get All Category Data
                List<VMTblMRole> data = (
                    from role in db.TblMRoles
                    where role.IsDelete == false
                    select new VMTblMRole
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Code = role.Code,

                        CreateBy = role.CreateBy,
                        CreatedOn = role.CreatedOn,

                        ModifiedBy = role.ModifiedBy,
                        ModifiedOn = role.ModifiedOn,

                        DeletedBy = role.DeletedBy,
                        DeletedOn = role.DeletedOn,

                        IsDelete = role.IsDelete,
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count < 1) ? "Role cannot be fatched" : "Role Successfully fetched";
                response.statusCode = (data.Count < 1) ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public long GetAllIdData()
        {
            List<VMTblMRole> data = new List<VMTblMRole>();
            try
            {
                //Get All Category Data
                data = (
                from role in db.TblMRoles
                select new VMTblMRole
                { Id = role.Id }).ToList();

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return data.Count;
        }
        public long GetIdGenerator(long totalData)
        {
            totalData = GetAllIdData();
            long currentId = (totalData > 0) ? (totalData + 1) : 0;

            return currentId;
        }
        public VMResponse CreateRole(VMTblMRole formData)
        {
            try
            {
                TblMRole data = new TblMRole();

                //assign each datamodel column from each data input
                data.Name = formData.Name;
                data.Code = formData.Code;

                data.CreateBy = GetIdGenerator(data.Id);
                data.CreatedOn = DateTime.Now;

                data.IsDelete = false;


                //prosess create new data in table from data model
                db.Add(data);
                db.SaveChanges();

                //update api response
                response.data = data;
                response.message = "New Role has been succesfully created";
                response.statusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            finally { db.Dispose(); }
            return response;
        }
    }
}
