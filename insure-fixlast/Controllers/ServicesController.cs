using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using insure_fixlast.Data;
using insure_fixlast.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace insure_fixlast.Controllers
{
    public class ServicesController : Controller
    {
        private readonly insure_fixlastContext _context;

        public ServicesController(insure_fixlastContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            return View(await _context.Service.ToListAsync());
        }
        public async Task<IActionResult> UserService()
        {
            return View(await _context.Service.ToListAsync());
        }
        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Service
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Name,Price,Claim,Time")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Service.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Name,Price,Claim,Time")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Service
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Service.FindAsync(id);
            if (service != null)
            {
                _context.Service.Remove(service);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Service.Any(e => e.Id == id);
        }
        public IActionResult ReviewOrder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReviewOrder(List<int> selectedServices, Dictionary<int, int> quantities)
        {
            if (selectedServices == null || !selectedServices.Any())
            {
                TempData["ErrorMessage"] = "Please select at least one service.";
                return RedirectToAction(nameof(Index));
            }

            var selectedServiceDetails = new List<Service>();
            foreach (var serviceId in selectedServices)
            {
                var service = _context.Service.FirstOrDefault(s => s.Id == serviceId);
                if (service != null)
                {
                    selectedServiceDetails.Add(service);
                }
            }

            HttpContext.Session.SetString("SelectedServices", JsonSerializer.Serialize(selectedServiceDetails));
            HttpContext.Session.SetString("ServiceQuantities", JsonSerializer.Serialize(quantities));

            return RedirectToAction(nameof(Checkout));
        }

        public IActionResult Checkout()
        {
            var selectedServicesJson = HttpContext.Session.GetString("SelectedServices");
            var serviceQuantitiesJson = HttpContext.Session.GetString("ServiceQuantities");

            if (string.IsNullOrEmpty(selectedServicesJson) || string.IsNullOrEmpty(serviceQuantitiesJson))
            {
                TempData["ErrorMessage"] = "No services found in the order. Please select services again.";
                return RedirectToAction(nameof(Index));
            }

            var selectedServices = JsonSerializer.Deserialize<List<Service>>(selectedServicesJson);
            var serviceQuantities = JsonSerializer.Deserialize<Dictionary<int, int>>(serviceQuantitiesJson);

            var checkoutViewModel = new CheckoutViewModel
            {
                SelectedServices = selectedServices,
                ServiceQuantities = serviceQuantities,
            };

            var companyId = HttpContext.Session.GetString("CompanyId");
            if (!string.IsNullOrEmpty(companyId))
            {
                var company = _context.Company.FirstOrDefault(c => c.Id == Convert.ToInt32(companyId));
                if (company != null)
                {
                    checkoutViewModel.CompanyName = company.Name;
                    checkoutViewModel.CompanyEmail = company.Email;
                    checkoutViewModel.CompanyPhone = company.Phone;
                    checkoutViewModel.CompanyAddress = company.Address;
                }
            }


            return View(checkoutViewModel);
        }
        public IActionResult PlaceOrder()
        {
            var selectedServicesJson = HttpContext.Session.GetString("SelectedServices");
            var serviceQuantitiesJson = HttpContext.Session.GetString("ServiceQuantities");

            if (string.IsNullOrEmpty(selectedServicesJson) || string.IsNullOrEmpty(serviceQuantitiesJson))
            {
                TempData["ErrorMessage"] = "No services found in the order. Please select services again.";
                return RedirectToAction(nameof(Index));
            }

            var selectedServices = JsonSerializer.Deserialize<List<Service>>(selectedServicesJson);
            var serviceQuantities = JsonSerializer.Deserialize<Dictionary<int, int>>(serviceQuantitiesJson);

            // Tạo mới đối tượng Order
            var order = new Order
            {
                PriceTotal = selectedServices.Sum(s => s.Price * serviceQuantities.GetValueOrDefault(s.Id, 1)),
                CompanyId = Convert.ToInt32(HttpContext.Session.GetString("CompanyId")), // Lấy CompanyId từ session
                Details = new List<OrderDetail>()
            };

            // Tạo mới các đối tượng OrderDetail và thêm dịch vụ đã chọn
            foreach (var service in selectedServices)
            {
                var quantity = serviceQuantities.GetValueOrDefault(service.Id, 1);
                var orderDetail = new OrderDetail
                {
                    Services = new List<Service> { service }
                };
                order.Details.Add(orderDetail);
            }

            // Lưu đối tượng Order và các OrderDetail vào cơ sở dữ liệu
            _context.Order.Add(order);
            _context.SaveChanges();

            // Xóa session để làm mới đơn hàng
            HttpContext.Session.Remove("SelectedServices");
            HttpContext.Session.Remove("ServiceQuantities");

            TempData["SuccessMessage"] = "Order placed successfully!";
            return RedirectToAction(nameof(Index));
        }
    }

}

