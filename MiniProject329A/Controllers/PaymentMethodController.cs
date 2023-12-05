using Microsoft.AspNetCore.Mvc;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;
using System.Net;
using MiniProject329A.AddOns;

namespace MiniProject329A.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly PaymentMethodModel paymentMethod;
        private readonly int pageSize;
        private VMResponse? response;

        public PaymentMethodController(IConfiguration _config)
        {
            paymentMethod = new PaymentMethodModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
        }

        public async Task<IActionResult> Index(VMPageParam pageParam)
        {

            if (pageParam.filter == null)
            {
                response = await paymentMethod.GetAll();
            }
            else
                response = await paymentMethod.GetByName(pageParam.filter);
            List<VMTblMPaymentMethod> data = (List<VMTblMPaymentMethod>)response.data;
            switch (pageParam.orderBy)
            {
                case "name_desc":
                    data = data.OrderByDescending(pm => pm.Name).ToList();
                    break;
                default:
                    data = data.OrderBy(pm => pm.Name).ToList();
                    break;
            }
            ViewBag.Title = "Metode Pembayaran";
            ViewBag.Filter = pageParam.filter;
            ViewBag.OrderBy = pageParam.orderBy;
            ViewBag.PageNumber = pageParam.pageNumber;

            ViewBag.OrderName = pageParam.orderBy == "name" ? "name_desc" : "name";
            return View(Pagination<VMTblMPaymentMethod>.Create(data, pageParam.pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Edit(long id)
        {
            VMTblMPaymentMethod? data = await paymentMethod.GetById(id);
            ViewBag.Title = "Edit Metode Pembayaran";
            return View(data);
        }
        [HttpPost]
        public async Task<VMResponse?> Edit(VMTblMPaymentMethod data)
        {
            VMResponse response = await paymentMethod.Edit(data);
            return response;
        }

        public async Task<IActionResult> Delete(long id)
        {
            VMTblMPaymentMethod? data = await paymentMethod.GetById(id);
            ViewBag.Title = "Hapus Metode Pembayaran";
            return View(data);
        }
        [HttpPost]
        public async Task<VMResponse?> Delete(long id, long userId)
        {
            VMResponse response = await paymentMethod.Delete(id, userId);
            return response;
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<VMResponse> Add(VMTblMPaymentMethod formData)
        {
            VMResponse response = await paymentMethod.Add(formData);
            return response;
        }
    }
}
