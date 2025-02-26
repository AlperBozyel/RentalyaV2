using System.ComponentModel.DataAnnotations;

namespace RentalyaAPI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [MinLength(2, ErrorMessage = "Ad en az 2 karakter olmalıdır")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [MinLength(2, ErrorMessage = "Soyad en az 2 karakter olmalıdır")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur")]
        [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public string PhoneNumber { get; set; }
    }
} 