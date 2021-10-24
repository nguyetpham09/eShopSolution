using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        
        public UserController(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
         
        }
        
        public async Task<IActionResult> Index(string keyword="1", int pageIndex = 1, int pageSize = 1)
        {
            var sessions = HttpContext.Session.GetString("Token");
            var request = new GetUserListRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUserListAsync(request);
            return View(data.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            return View(result.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Create(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userApiClient.RegisterUser(registerRequest);
            
            if (result.IsSuccessed) return RedirectToAction("Index");

            ModelState.AddModelError("", result.Message);
            return View(registerRequest);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result =await _userApiClient.GetById(id);
            if (result != null)
            {
                var user = result.ResultObj;
                var updateRequest = new UserUpdateRequest()
                {
                    Dob = user.Dob,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber
                };
                return View(updateRequest);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userApiClient.UpdateUser(updateRequest.Id, updateRequest);

            if (result.IsSuccessed) return RedirectToAction("Index");

            ModelState.AddModelError("", result.Message);
            return View(updateRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(new DeleteRequest()
            {
                Id = id
            }) ;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userApiClient.Delete(request.Id);

            if (result.IsSuccessed) return RedirectToAction("Index");

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
    }
}
