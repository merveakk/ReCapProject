using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string EntityAdded = "Ekleme başarılı.";
        public static string EntityNameInvalid = "Ürün ismi geçersiz";
        public static string InvalidValue = "Geçersiz değer girdiniz.";
        public static string MaintenanceTime = "Sistem bakımda.";
        public static string EntityListed = "Listeleme islemi başarılı";
        public static string EntityUpdated="Güncelleme başarılı.";
        public static string EntityDeleted="Silme işlemi başarılı.";
        public static string Error="ERROR !";
        public static string CarImageLimitExceeded="En fazla 5 resim yüklenebilir.";
        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string UserRegistered = "Kayıt oldu.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Parola hatası.";
        public static string SuccessfulLogin = "Başarılı giriş.";
        public static string UserAlreadyExists = "Kullanıcı zaten var.";
        public static string AccessTokenCreated = "AccessToken oluşturuldu.";
        public static string TransactionCanceled = "The transaction has been canceled!";

    }
}
