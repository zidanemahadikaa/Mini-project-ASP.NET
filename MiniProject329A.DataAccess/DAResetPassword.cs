using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.DataAccess
{
    public class DAResetPassword
    {
        private readonly MiniProject329AContext db;
        private readonly DAUser user;
        public DAResetPassword(MiniProject329AContext _db)
        {
            db = _db;
            user = new DAUser(db);
        }
        private VMResponse response = new VMResponse();

        public VMResponse GetAll()
        {
            try
            {
                List<VMTblTResetPassword> data = (
                    from rp in db.TblTResetPasswords
                    where rp.IsDelete == false
                    select new VMTblTResetPassword
                    {
                        Id = rp.Id,
                        OldPassword = rp.OldPassword,
                        NewPassword = rp.NewPassword,
                        ResetFor = rp.ResetFor,

                        CreatedBy = rp.CreatedBy,
                        CreatedOn = rp.CreatedOn,

                        ModifiedBy = rp.ModifiedBy,
                        ModifiedOn = rp.ModifiedOn,

                        DeletedBy = rp.DeletedBy,
                        DeletedOn = rp.DeletedOn,

                        IsDelete = rp.IsDelete,
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count < 1) ? "ResetPassword cannot be fatched" : "ResetPassword Successfully fetched";
                response.statusCode = (data.Count < 1) ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse ResetOldPassword(VMTblTResetPassword dataPassword)
        {
            try
            {

                //assign each datamodel column from each data input
                TblTResetPassword data = new TblTResetPassword();

                data.OldPassword = dataPassword.OldPassword;
                data.NewPassword = dataPassword.NewPassword;
                data.ResetFor = dataPassword.ResetFor;

                data.CreatedBy = dataPassword.CreatedBy;
                data.CreatedOn = DateTime.Now;

                data.IsDelete = false;


                //prosess create new data in table from data model
                db.Add(data);
                db.SaveChanges();

                //update api response
                response.data = data;
                response.message = "Your password successfully reset";
                response.statusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
    }
}
