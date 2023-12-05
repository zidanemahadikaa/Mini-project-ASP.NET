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
    public class DAMenuRole
    {
        private readonly MiniProject329AContext db;
        public DAMenuRole(MiniProject329AContext _db)
        {
            db = _db;
        }
        private VMResponse response = new VMResponse();

        public VMResponse? GetAll()
        {
            try
            {
                List<VMTblMMenuRole> data = (
                    from menuRole in db.TblMMenuRoles
                    join role in db.TblMRoles
                    on menuRole.RoleId equals role.Id
                    join menu in db.TblMMenus
                    on menuRole.MenuId equals menu.Id
                    where menuRole.IsDelete == false
                    select new VMTblMMenuRole
                    {
                        Id= menuRole.Id,

                        MenuId= menu.Id,
                        NamaMenu=menu.Name,
                        url=menu.Url,

                        RoleId= role.Id,

                        CreateBy=menuRole.CreateBy,
                        CreatedOn=menuRole.CreatedOn,
                        ModifiedBy=menuRole.ModifiedBy,
                        ModifiedOn=menuRole.ModifiedOn,

                        DeletedBy=menuRole.DeletedBy,
                        DeletedOn=menuRole.DeletedOn,
                        
                        IsDelete=menuRole.IsDelete,

                    }
                    ).ToList();

                response.data = data;
                response.message = (data.Count < 1) ? "Menu role data cannot be fatched" : "Menu role data Successfully fetched";
                response.statusCode = (data.Count < 1) ? HttpStatusCode.NoContent : HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;

        }

        public List<VMTblMMenuRole?> FindByRoleId(long id)
        {
            return (
                from menuRole in db.TblMMenuRoles
                join role in db.TblMRoles
                on menuRole.RoleId equals role.Id
                join menu in db.TblMMenus
                on menuRole.MenuId equals menu.Id
                where menuRole.IsDelete == false
                && role.Id == id
                select new VMTblMMenuRole
                {
                    Id = menuRole.Id,

                    MenuId = menu.Id,
                    NamaMenu = menu.Name,
                    url = menu.Url,

                    RoleId = role.Id,

                    CreateBy = menuRole.CreateBy,
                    CreatedOn = menuRole.CreatedOn,
                    ModifiedBy = menuRole.ModifiedBy,
                    ModifiedOn = menuRole.ModifiedOn,

                    DeletedBy = menuRole.DeletedBy,
                    DeletedOn = menuRole.DeletedOn,

                    IsDelete = menuRole.IsDelete,
                }
                ).ToList();
        }

        public VMResponse GetByRoleId( long Id)
        {
            try
            {
                List<VMTblMMenuRole> data = (
                    from menuRole in db.TblMMenuRoles
                    join role in db.TblMRoles
                    on menuRole.RoleId equals role.Id
                    join menu in db.TblMMenus
                    on menuRole.MenuId equals menu.Id
                    where menuRole.IsDelete == false
                    && role.Id == Id
                    select new VMTblMMenuRole
                    {
                        Id = menuRole.Id,

                        MenuId = menu.Id,
                        NamaMenu = menu.Name,
                        url = menu.Url,

                        RoleId = role.Id,

                        CreateBy = menuRole.CreateBy,
                        CreatedOn = menuRole.CreatedOn,
                        ModifiedBy = menuRole.ModifiedBy,
                        ModifiedOn = menuRole.ModifiedOn,

                        DeletedBy = menuRole.DeletedBy,
                        DeletedOn = menuRole.DeletedOn,

                        IsDelete = menuRole.IsDelete,
                    }
                ).ToList();
                response.data = data;
                response.message = (data == null) ? $"Menu whit roleId {Id} is not Exixt": $"Menu whit roleId {Id} successfully fatched";
                response.statusCode = (data == null) ? HttpStatusCode.NoContent : HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
    
    }
}
