using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.ViewModels
{
    public class ReservationViewModel
    {
        [Required(ErrorMessage = "找不到使用者資料")]
        public string TokenId { get; set; }

        [Required(ErrorMessage = "姓名為必填")]
        [MaxLength(20, ErrorMessage ="姓名不能超過20個字")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "請選擇服務")]
        public string Service { get; set; }

        public List<SelectListItem> Services { get; set; }

        [Required(ErrorMessage = "請選擇到店時間")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime ToStoreDateTime { get; set; } = DateTime.Now;
    }
}
