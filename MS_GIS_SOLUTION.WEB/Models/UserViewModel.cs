using MS_GIS_SOLUTION.WEB.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MS_GIS_SOLUTION.Core;

namespace MS_GIS_SOLUTION.WEB.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            UserRegionIds = new List<int>();
            ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = false };
        }


        public int Id { get; set; }

        [DisplayName("Əməkdaş")]
        public int EmployeeId { get; set; }

        [DisplayName("Açılış ekranı")]
        public int? DefaultOperationId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("İstifadəçi adı")]
        public string Username { get; set; }

        [StringLength(50)]
        [DisplayName("Soyad")]
        public string UserLastName { get; set; }

        [StringLength(50)]
        [DisplayName("Ad")]
        public string UserFirstName { get; set; }

        [StringLength(50)]
        [DisplayName("Ata adı")]
        public string UserMiddleName { get; set; }

        [StringLength(150, MinimumLength = 5, ErrorMessage = "Şifrə ən az 5 simvol olmalıdır")]
        [DisplayName("Şifrə")]
        [Required(ErrorMessage = "Şifrə boş ola bilməz")]
        public string Pass { get; set; }

        [DisplayName("Köhnə şifrə")]
        [Required]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Email ünvanı daxil edin")]
        [RegularExpression(@"^[\d\w\._\-]+@([\d\w\._\-]+\.)+[\w]+$", ErrorMessage = "Email standarta uyğun deyil")]
        [StringLength(50)]
        [DisplayName("E-poçt")]
        public string Mail { get; set; }

        [StringLength(500)]
        [DisplayName("Avatar")]
        public string ImagePath { get; set; }

        [DisplayName("Təsdiqlənib")]
        public bool IsApproved { get; set; }

        [StringLength(2000)]
        [DisplayName("Qeyd")]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Pass", ErrorMessage = "'Şifrə' və 'Şifrənin təkrarı' uyğun deyil.")]
        [DisplayName("Şifrənin təkrarı")]
        [Required(ErrorMessage = "Şifrənin təkrarı şifrə ilə eyni olmalıdır")]
        public string ConfirmPassword { get; set; }

        public string UserRegionIdsJson { get; set; }
        public List<int> UserRegionIds { get; set; }

        public string UserLayerIdsJson { get; set; }
        public List<int> UserLayerIds { get; set; }

        //public ButtonPanelVM ButtonPanel { get; set; }

        public ButtonPanelViewModel ButtonPanel { get; set; }
    }
}
