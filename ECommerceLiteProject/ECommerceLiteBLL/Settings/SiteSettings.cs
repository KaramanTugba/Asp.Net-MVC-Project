using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceLiteBLL.Settings
{
   public class SiteSettings
    {
        // ToDo: Mail adresini webconfig dosyasından çekmeyi dde öğrenelim.

        public static string SiteMail { get; set; } = "nayazilim303@gmail.com";
        public static string SiteMailPassword { get; set; } = "betul303303";
        public static string SiteMailSmtpHost { get; set; } = "smtp.gmail.com";
        public static string SiteMailSmtpPost { get; set; } = "587";
        public static bool SiteMailEnableSSL = true;

        //buraya geri döneceğiz


    }
}
