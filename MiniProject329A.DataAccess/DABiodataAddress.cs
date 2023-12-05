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
    public class DABiodataAddress
    {
        private readonly MiniProject329AContext db;
        private VMResponse response = new VMResponse();
        public DABiodataAddress(MiniProject329AContext _db)
        {
            db = _db;
        }
        public VMResponse GetByName(string? name=null)
        {
            try
            {
                List<VMTblMBiodataAddress> data = (
                    from ba in db.TblMBiodataAddresses
                    join b in db.TblMBiodata
                    on ba.BiodataId equals b.Id
                    join l in db.TblMLocations on ba.LocationId equals l.Id
                    where ba.IsDelete == false && ba.Label.Contains(name??"")
                    select new VMTblMBiodataAddress
                    {
                        Id= ba.Id,
                        BiodataId=b.Id,
                        Label = ba.Label,
                        Recipient = ba.Recipient,
                        RecipientPhoneNumber = ba.RecipientPhoneNumber,
                        LocationId=l.Id,
                        PostalCode = ba.PostalCode,
                        Address = ba.Address,
                        CreateBy = ba.CreateBy,
                        CreateOn = ba.CreateOn,
                        ModifiedBy = ba.ModifiedBy,
                        ModifiedOn = ba.ModifiedOn,
                        DeletedBy = ba.DeletedBy,
                        DeletedOn = ba.DeletedOn,
                        IsDelete = ba.IsDelete
                       
                    }).ToList();
                response.data = data;
                response.message = (data.Count < 1) ?
                    $"Label {name} Tidak Ada" :
                    $"Label {name} Berhasil dicari!";
                response.statusCode = (data.Count < 1) ?
                    HttpStatusCode.NoContent : HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

    }
}
