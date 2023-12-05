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
    public class DAAppointmentDone
    {
        private readonly MiniProject329AContext db;
        private readonly DAUser user;
        public DAAppointmentDone(MiniProject329AContext _db)
        {
            db = _db;
            user = new DAUser(db);
        }
        public VMResponse response = new VMResponse();
        public VMTblTAppointmentDone? FindById(long? id = null)
        {
            return (
                from ad in db.TblTAppointmentDones
                where ad.Id == id && ad.IsDelete == false
                select new VMTblTAppointmentDone
                {
                    Id = ad.Id,
                    AppointmentId = ad.AppointmentId,
                    Diagnosis = ad.Diagnosis,

                    CreatedBy = ad.CreatedBy,
                    CreatedOn = ad.CreatedOn,

                    ModifiedBy = ad.ModifiedBy,
                    ModifiedOn = ad.ModifiedOn,

                    DeletedBy = ad.DeletedBy,
                    DeletedOn = ad.DeletedOn,
                }
                ).FirstOrDefault();
        }
        public VMResponse GetById(long? id = null)
        {
            try
            {
                VMTblTAppointmentDone? data = FindById(id);

                response.data = data;
                response.message = (data == null)
                    ? $"Golongan darah dengan id = {id} tidak ada"
                    : $"Golongan darah dengan id = {id} ditemukan";
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
        public VMResponse GetAll()
        {
            try
            {
                List<VMTblTAppointmentDone> data = (
                    from ad in db.TblTAppointmentDones
                    where ad.IsDelete == false
                    select new VMTblTAppointmentDone
                    {
                        Id = ad.Id,
                        AppointmentId = ad.AppointmentId,
                        Diagnosis = ad.Diagnosis,

                        CreatedBy = ad.CreatedBy,
                        CreatedOn = ad.CreatedOn,

                        ModifiedBy = ad.ModifiedBy,
                        ModifiedOn = ad.ModifiedOn,

                        DeletedBy = ad.DeletedBy,
                        DeletedOn = ad.DeletedOn,
                    }
                ).ToList();

                response.data = data;
                response.message = (data.Count > 0)
                    ? $"Data  ditemukan"
                    : $"Data Customer tidak ada";
                response.statusCode = (data.Count > 0)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;
            }
            catch(Exception ex)
            {

            }
            return response;            
        }
    }
}
