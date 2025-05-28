using System.ComponentModel.DataAnnotations;

namespace Do_an.ViewModel
{
    public class EditProfileViewModel
    {
        [Required]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Phone { get; set; }

        // Optional password change
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không khớp.")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string? ConfirmPassword { get; set; }
    }
}
