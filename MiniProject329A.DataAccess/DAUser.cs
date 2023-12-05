using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using System.Data;
using System.Net;

namespace MiniProject329A.DataAccess
{
    public class DAUser
    {
        private readonly MiniProject329AContext db;
        public DAUser(MiniProject329AContext _db)
        {
            db = _db;
        }
        private VMResponse response = new VMResponse();

        public VMResponse GetAll()
        {
            try
            {
                //Get All Category Data
                List<VMTblMUser> data = (
                    from user in db.TblMUsers
                    join bio in db.TblMBiodata
                    on user.BiodataId equals bio.Id
                    where user.IsDelete == false
                    select new VMTblMUser
                    {
                        Id = user.Id,
                        BiodataId = bio.Id,
                        RoleId = user.RoleId,
                        Email = user.Email,
                        Password = user.Password,

                        LoginAttempt = user.LoginAttempt,
                        IsLocked = user.IsLocked,
                        LastLogin = user.LastLogin,

                        Image_Path = bio.ImagePath,

                        CreateBy = user.CreateBy,
                        CreatedOn = user.CreatedOn,

                        ModifiedBy = user.ModifiedBy,
                        ModifiedOn = user.ModifiedOn,

                        DeletedBy = user.DeletedBy,
                        DeletedOn = user.DeletedOn,

                        IsDelete = user.IsDelete,
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count < 1) ? "User data cannot be fatched" : "User data Successfully fetched";
                response.statusCode = (data.Count < 1) ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMTblMUser? FindById(long id)
        {
            return (
                from user in db.TblMUsers
                join bio in db.TblMBiodata
                on user.BiodataId equals bio.Id
                where user.IsDelete == false && user.Id == id
                select new VMTblMUser
                {
                    Id = user.Id,
                    BiodataId = bio.Id,
                    Image_Path= bio.ImagePath,

                    RoleId = user.RoleId,
                    Email = user.Email,
                    Password = user.Password,


                    LoginAttempt = user.LoginAttempt,
                    IsLocked = user.IsLocked,
                    LastLogin = user.LastLogin,

                    CreateBy = user.CreateBy,
                    CreatedOn = user.CreatedOn,

                    ModifiedBy = user.ModifiedBy,
                    ModifiedOn = user.ModifiedOn,

                    DeletedBy = user.DeletedBy,
                    DeletedOn = user.DeletedOn,

                    IsDelete = user.IsDelete,
                }
            ).FirstOrDefault();
        }
        public VMResponse GetById(long id)
        {
            try
            {
                VMTblMUser? data = FindById(id);
                response.data = data;
                response.message = (data == null)
                    ? $"User with id = {id} is not exist"
                    : $"User with id = {id} successfully fatched";
                response.statusCode = (data == null)
                    ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse Login(string email, string password)
        {
            try
            {
                VMTblMUser? data = (
                   from user in db.TblMUsers
                   join bio in db.TblMBiodata
                   on user.BiodataId equals bio.Id
                   where user.IsDelete == false
                   && user.Email == email && user.Password == password
                   select new VMTblMUser
                   {
                       Id = user.Id,
                       BiodataId = bio.Id,
                       RoleId = user.RoleId,
                       Email = user.Email,
                       Password = user.Password,

                       LoginAttempt = user.LoginAttempt,
                       IsLocked = user.IsLocked,
                       LastLogin = DateTime.Now,

                       CreateBy = user.CreateBy,
                       CreatedOn = user.CreatedOn,

                       ModifiedBy = user.Id,
                       ModifiedOn = DateTime.Now,

                       DeletedBy = user.DeletedBy,
                       DeletedOn = user.DeletedOn,

                       IsDelete = user.IsDelete,
                   }
               ).FirstOrDefault();

                response.data = data;
                response.message = (data == null) ? "Your email and password does not match" : "Login success";
                response.statusCode = (data == null) ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse Register(VMTblMUser dataUser)
        {
            try
            {
                long IdBiodata = db.TblMBiodata.OrderByDescending(
                    bio => bio.Id).Select(bio => bio.Id).FirstOrDefault();
                TblMUser data = new TblMUser();

                //assign each datamodel column from each data input
                data.BiodataId = IdBiodata;
                data.RoleId = dataUser.RoleId;

                data.Email = dataUser.Email;
                data.Password = dataUser.Password;

                data.LoginAttempt = 5;
                data.IsLocked = false;

                data.LastLogin = dataUser.LastLogin;

                data.CreateBy = IdBiodata;


                //prosess create new data in table from data model
                db.Add(data);
                db.SaveChanges();

                //update api response
                response.data = data;
                response.message = "Proses register berhasil";
                response.statusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public VMResponse EmailCheck(string email)
        {
            try
            {
                VMTblMUser? data = (

                    from user in db.TblMUsers
                    where user.Email == email && user.IsDelete == false
                    select new VMTblMUser
                    {
                        Id = user.Id,
                        RoleId = user.RoleId,
                        Email = user.Email,
                        Password = user.Password,

                        LoginAttempt = user.LoginAttempt,
                        IsLocked = user.IsLocked,
                        LastLogin = user.LastLogin,

                        CreateBy = user.CreateBy,
                        CreatedOn = user.CreatedOn,

                        ModifiedBy = user.ModifiedBy,
                        ModifiedOn = user.ModifiedOn,

                        DeletedBy = user.DeletedBy,
                        DeletedOn = user.DeletedOn,

                        IsDelete = user.IsDelete,
                    }
                    ).FirstOrDefault();

                response.data = data;
                response.message = (data == null)
                    ? "Email belum digunakan"
                    : "Email sudah digunakan";
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
        public VMResponse LoginAttemp(VMTblMUser dataUser)
        {
            try
            {
                VMTblMUser? dataExist = FindById(dataUser.Id);

                if (dataExist != null)
                {
                    TblMUser data = new TblMUser();
                    data.Id = dataExist.Id;
                    data.BiodataId = dataExist.BiodataId;
                    data.RoleId = dataExist.RoleId;

                    data.Email = dataExist.Email;
                    data.Password = dataExist.Password;


                    if(dataExist.LoginAttempt > 0)
                    {
                        data.LoginAttempt = dataExist.LoginAttempt - 1;
                    }
                    else
                    {
                        data.LoginAttempt = 0;
                    }


                    if(data.LoginAttempt == 0)
                    {
                        data.IsLocked = true;
                    }
                    else
                    {
                        data.IsLocked = false;
                    }
                    
                    data.LastLogin = dataExist.LastLogin;

                    data.CreateBy = dataExist.CreateBy;
                    data.CreatedOn = dataExist.CreatedOn;

                    db.Update(data);
                    db.SaveChanges();

                    //update api response
                    response.data = data;
                    response.message = $"Kesempatan login tinggal {data.LoginAttempt}";
                    response.statusCode = HttpStatusCode.Created;
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
        public VMResponse ResetLoginAttemp(VMTblMUser dataUser)
        {
            try
            {
                VMResponse resetResponse = EmailCheck(dataUser.Email);
                long idEmail = ((VMTblMUser)resetResponse.data).Id;
                VMTblMUser? dataExist = FindById(idEmail);

                if (dataExist != null)
                {
                    TblMUser data = new TblMUser();
                    data.Id = dataExist.Id;
                    data.BiodataId = dataExist.BiodataId;
                    data.RoleId = dataExist.RoleId;

                    data.Email = dataExist.Email;
                    data.Password = dataExist.Password;

                    data.IsLocked = false;

                    data.LoginAttempt = 5;

                    data.LastLogin = DateTime.Now;

                    data.CreateBy = dataExist.CreateBy;
                    data.CreatedOn = dataExist.CreatedOn;

                    data.ModifiedBy = dataExist.ModifiedBy;
                    data.ModifiedOn = DateTime.Now;

                    db.Update(data);
                    db.SaveChanges();

                    //update api response
                    response.data = data;
                    response.message = "Login Sukses";
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
        public VMResponse UpdatePassword(VMTblMUser dataUser)
        {
            try
            {
                VMResponse resetResponse = EmailCheck(dataUser.Email);
                long idEmail = ((VMTblMUser)resetResponse.data).Id;
                VMTblMUser? dataExist = FindById(idEmail);

                if (dataExist != null)
                {
                    TblMUser data = new TblMUser();
                    data.Id = dataExist.Id;
                    data.BiodataId = dataExist.BiodataId;
                    data.RoleId = dataExist.RoleId;

                    data.Email = dataExist.Email;
                    data.Password = dataUser.Password;

                    data.IsLocked = false;

                    data.LoginAttempt = 5;

                    data.LastLogin = DateTime.Now;

                    data.CreateBy = dataExist.CreateBy;
                    data.CreatedOn = dataExist.CreatedOn;

                    data.ModifiedBy = idEmail;
                    data.ModifiedOn = DateTime.Now;

                    data.IsDelete = false;

                    db.Update(data);
                    db.SaveChanges();

                    //update api response
                    response.data = data;
                    response.message = "Login Sukses";
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