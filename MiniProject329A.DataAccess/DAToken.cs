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
    public class DAToken
    {
        private readonly MiniProject329AContext db;
        private readonly DAUser user;
        public DAToken(MiniProject329AContext _db)
        {
            db = _db;
            user = new DAUser(_db);
        }
        private VMResponse response = new VMResponse();

        public VMResponse GetAll()
        {
            try
            {
                List<VMTblTToken> data = (
                    from t in db.TblTTokens
                    join u in db.TblMUsers
                    on t.UserId equals u.Id
                    where t.IsDelete == false
                    select new VMTblTToken
                    {
                        Id = t.Id,
                        Email = t.Email,
                        UserId = u.Id,
                        Token = t.Token,

                        ExpiredOn = t.ExpiredOn,
                        IsExpired = t.IsExpired,
                        UsedFor = t.UsedFor,

                        CreatedBy = t.CreatedBy,
                        CreatedOn = t.CreatedOn,

                        ModifiedBy = t.ModifiedBy,
                        ModifiedOn = t.ModifiedOn,

                        DeletedBy = t.DeletedBy,
                        DeletedOn = t.DeletedOn,

                        IsDelete = t.IsDelete,
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count < 1) ? "Token tidak ditemukan" : "Token ditemukan";
                response.statusCode = (data.Count < 1) ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMTblTToken? FindById(long id)
        {
            return (
                from t in db.TblTTokens
                join u in db.TblMUsers
                on t.UserId equals u.Id
                where t.IsDelete == false
                select new VMTblTToken
                {
                    Id = t.Id,
                    Email = t.Email,
                    UserId = u.Id,
                    Token = t.Token,

                    ExpiredOn = t.ExpiredOn,
                    IsExpired = t.IsExpired,
                    UsedFor = t.UsedFor,

                    CreatedBy = t.CreatedBy,
                    CreatedOn = t.CreatedOn,

                    ModifiedBy = t.ModifiedBy,
                    ModifiedOn = t.ModifiedOn,

                    DeletedBy = t.DeletedBy,
                    DeletedOn = t.DeletedOn,

                    IsDelete = t.IsDelete,
                }
            ).FirstOrDefault();
        }
        public VMResponse GetById(long id)
        {
            try
            {
                VMTblTToken? data = FindById(id);
                response.data = data;
                response.message = (data == null)
                    ? $"Token with id = {id} is not exist"
                    : $"Token with id = {id} successfully fatched";
                response.statusCode = (data == null)
                    ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse GetByEmailToken(string email, string token)
        {
            try
            {
                VMTblTToken? data = (

                    from t in db.TblTTokens
                    join u in db.TblMUsers
                    on t.UserId equals u.Id
                    where t.IsDelete == false && t.Email == email && t.Token == token
                    select new VMTblTToken
                    {
                        Id = t.Id,
                        Email = t.Email,
                        UserId = u.Id,
                        Token = t.Token,

                        ExpiredOn = t.ExpiredOn,
                        IsExpired = t.IsExpired,
                        UsedFor = t.UsedFor,

                        CreatedBy = t.CreatedBy,
                        CreatedOn = t.CreatedOn,

                        ModifiedBy = t.ModifiedBy,
                        ModifiedOn = t.ModifiedOn,

                        DeletedBy = t.DeletedBy,
                        DeletedOn = t.DeletedOn,

                        IsDelete = t.IsDelete,
                    }
                    ).FirstOrDefault();

                response.data = data;
                response.message = (data == null)
                    ? $"Email {email} mendapatkan token"
                    : $"Email {email} belum mendapatkan token";
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
        public VMResponse AddToken(VMTblTToken dataToken)
        {
            try
            {
                long IdUser = db.TblMUsers.OrderByDescending(
                    u => u.Id).Select(u => u.Id).FirstOrDefault();
                TblTToken data = new TblTToken();

                data.Email = dataToken.Email;
                data.UserId = IdUser;
                data.Token = dataToken.Token;

                data.ExpiredOn = DateTime.Now.AddMinutes(10);
                data.IsExpired = false;
                data.UsedFor = dataToken.UsedFor;

                data.CreatedBy = 999;
                data.CreatedOn = DateTime.Now;
                data.IsDelete = false;


                //prosess create new data in table from data model
                db.Add(data);
                db.SaveChanges();

                //update api response
                response.data = data;
                response.message = "Token telah ditambahkan";
                response.statusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse UpdateByOtpCode(VMTblTToken dataToken)
        {
            try
            {
                VMResponse emailResponse = GetByEmailToken(dataToken.Email, dataToken.Token);

                VMTblTToken? dataExist = (VMTblTToken)emailResponse.data;

                if (dataExist != null)
                {
                    TblTToken data = new TblTToken();

                    data.Id = dataExist.Id;
                    data.Email = dataExist.Email;
                    data.UserId = dataExist.UserId;
                    data.Token = dataExist.Token;

                    data.ExpiredOn = DateTime.Now;
                    data.IsExpired = true;
                    data.UsedFor = dataExist.UsedFor;

                    data.CreatedBy = 1;
                    data.CreatedOn = dataExist.CreatedOn;

                    data.ModifiedBy = 1;
                    data.ModifiedOn = DateTime.Now;

                    data.IsDelete = false;

                    db.Update(data);
                    db.SaveChanges();

                    //update api response
                    response.data = data;
                    response.message = "Token sudah digunakan";
                    response.statusCode = HttpStatusCode.OK;
                }
                else
                {
                    throw new Exception(response.message);
                }


            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }
        public VMResponse UpdateOtpEx(VMTblTToken dataToken)
        {
            try
            {
                VMResponse emailResponse = GetByEmailToken(dataToken.Email, dataToken.Token);

                VMTblTToken? dataExist = (VMTblTToken)emailResponse.data;

                if (dataExist != null)
                {
                    TblTToken data = new TblTToken();

                    data.Id = dataExist.Id;
                    data.Email = dataExist.Email;
                    data.UserId = dataExist.UserId;
                    data.Token = dataExist.Token;

                    data.ExpiredOn = dataExist.ExpiredOn;
                    data.IsExpired = true;
                    data.UsedFor = dataExist.UsedFor;

                    data.CreatedBy = 1;
                    data.CreatedOn = dataExist.CreatedOn;

                    data.ModifiedBy = 1;
                    data.ModifiedOn = DateTime.Now;

                    data.IsDelete = false;

                    db.Update(data);
                    db.SaveChanges();

                    //update api response
                    response.data = data;
                    response.message = "Token sudah digunakan";
                    response.statusCode = HttpStatusCode.OK;
                }
                else
                {
                    throw new Exception(response.message);
                }


            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }
        public VMResponse EmailUser(string email)
        {
            try
            {
                List<VMTblTToken?> data = (

                    from t in db.TblTTokens
                    join u in db.TblMUsers
                    on t.UserId equals u.Id
                    where t.IsDelete == false && t.Email == email
                    select new VMTblTToken
                    {
                        Id = t.Id,
                        Email = t.Email,
                        UserId = u.Id,
                        Token = t.Token,

                        ExpiredOn = t.ExpiredOn,
                        IsExpired = t.IsExpired,
                        UsedFor = t.UsedFor,

                        CreatedBy = t.CreatedBy,
                        CreatedOn = t.CreatedOn,

                        ModifiedBy = t.ModifiedBy,
                        ModifiedOn = t.ModifiedOn,

                        DeletedBy = t.DeletedBy,
                        DeletedOn = t.DeletedOn,

                        IsDelete = t.IsDelete,
                    }
                    ).ToList();

                response.data = data;
                response.message = (data == null)
                    ? $"List Email {email} ditemukan"
                    : $"List Email {email} tidak ditemukan";
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
        public VMResponse UpdateUserID(VMTblTToken dataToken)
        {
            try
            {
                VMResponse emailResponse = EmailUser(dataToken.Email);
                VMResponse userResponse = user.EmailCheck(dataToken.Email);


                List<VMTblTToken?> dataEx = (List<VMTblTToken>)emailResponse.data;

                if (dataEx != null)
                {
                    foreach (VMTblTToken dataExist in dataEx)
                    {
                        
                        TblTToken data = new TblTToken();

                        data.Id = dataExist.Id;
                        data.Email = dataExist.Email;
                        data.UserId = ((VMTblMUser)userResponse.data).Id;
                        data.Token = dataExist.Token;

                        data.ExpiredOn = DateTime.Now;
                        data.IsExpired = true;
                        data.UsedFor = dataExist.UsedFor;

                        data.CreatedBy = 1;
                        data.CreatedOn = dataExist.CreatedOn;

                        data.ModifiedBy = 1;
                        data.ModifiedOn = DateTime.Now;

                        data.IsDelete = false;

                        db.Update(data);
                        db.SaveChanges();

                        //List<TblTToken> newData = data;

                        response.data = data;
                    }

                    //update api response
                    response.message = "Token sudah digunakan";
                    response.statusCode = HttpStatusCode.OK;
                }
                else
                {
                    throw new Exception(response.message);
                }


            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }
    }
    
}
