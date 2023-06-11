using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string LawyerAdded = "Personel eklendi.";
        public static string LawyerDeleted = "Personel silindi.";
        public static string LawyerUpdated = "Personel bilgileri güncellendi.";

        public static string MessageNotExist = "Mesaj bulunamadı.";
        public static string MessageDeleted = "Mesaj silindi.";

        public static string LawyerNotExist = "Avukat mevcut değil";
        public static string LawyerImageIdNotExist = "Fotoğraf bulunamadı.";
        public static string LawyerImageAdded = "Fotoğraf başarıyla yüklendi.";
        public static string LawyerImageDeleted = "Fotoğraf başarıyla silindi.";
        public static string ErrorDeletingImage = "Fotoğraf silinirken hata oluştu.";
        public static string LawyerImageUpdated = "Fotoğraf başarıyla güncellendi.";
        public static string ErrorUpdatingImage = "Fotoğraf güncellenirken hata oluştu.";
        public static string LawyerImageLimitExceeded = "Fotoğraf limiti aşıldı.";
        public static string NoPictureOfTheLawyer = "Bu avukat bir fotoğrafa sahip değil.";

        public static string AuthorizationDenied = "You are not authorized";
        public static string TokenCreated = "Token Created";

        public static string UserNotFound = "User not found";
        public static string UserExists = "User already exists";
        public static string LoginSuccess = "Login Success";
        public static string Registered = "Registered";
        public static string PasswordError = "Wrong password";
    }
}
