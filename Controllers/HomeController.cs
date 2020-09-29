using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReservationSystem.Models;
using ReservationSystem.Repository;
using ReservationSystem.Service;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReservationRepository _repository;
        private readonly SharedService _sharedService;

        public HomeController(ILogger<HomeController> logger, ReservationRepository repository, SharedService sharedService)
        {
            _logger = logger;
            _repository = repository;
            _sharedService = sharedService;
        }

        public IActionResult Index()
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel();

            reservationViewModel.Services = new List<SelectListItem>
            {
                new SelectListItem {Text = "維修服務", Value = "Repair"},
                new SelectListItem {Text = "腳踏車保養", Value = "Maintenance"}
            };

            return View(reservationViewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Reservation(ReservationViewModel submitData)
        {
            if (!ModelState.IsValid)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel();

                reservationViewModel.Services = new List<SelectListItem>
                {
                    new SelectListItem {Text = "維修服務", Value = "Repair"},
                    new SelectListItem {Text = "腳踏車保養", Value = "Maintenance"}
                };
                return View("Index", reservationViewModel);
            }

            UserInfoViewModel userInfo = _sharedService.GetUserInfoWithToken(submitData.TokenId);

            BooksInfoModel booksInfo = new BooksInfoModel()
            {
                UID = userInfo.userId,
                UserName = submitData.UserName,
                Service = submitData.Service,
                ToStoreDateTime = submitData.ToStoreDateTime,
                CreateDateTime = DateTime.Now
            };

            int resultInt = _repository.AddBookingInfo(booksInfo);

            ViewBag.Result = resultInt != 0 ? true : false;

            return View();
        }

        [HttpPost]
        public IActionResult GetUserBookingData(string TokenId)
        {
            #region 去LINE API取得個人資料
            UserInfoViewModel userInfo = _sharedService.GetUserInfoWithToken(TokenId);
            #endregion

            BooksInfoModel booksInfo = _repository.GetBookingInfo(userInfo.userId);

            bool result = booksInfo != null ? true : false;


            if (result)
            {
                return Json(new
                {
                    result = result
                            ,
                    username = booksInfo.UserName
                            ,
                    service = booksInfo.Service
                            ,
                    tostoredatetime = booksInfo.ToStoreDateTime.ToString()
                });
            }
            else
            {
                return Json(new { result = result });
            }

        }

        [HttpPost]
        public IActionResult Canel(string CanelTokenId)
        {
            #region 去LINE API取得個人資料
            UserInfoViewModel userInfo = _sharedService.GetUserInfoWithToken(CanelTokenId);
            #endregion

            int resultInt = _repository.DeleteBookingInfo(userInfo.userId);

            ViewBag.Result = resultInt != 0 ? true : false;

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
