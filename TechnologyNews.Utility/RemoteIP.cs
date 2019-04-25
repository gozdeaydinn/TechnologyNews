using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyNews.Utility
{
    public class RemoteIP
    {
        public static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());//İlgili adres hakıında bilgi için DNS veri tabanıı sorgular ve yerel bilgisayarın hostname ini alır
         //Dns Sınıfı: Internet etki alanı adı sistemi (DNS) gelen belirli bir ana bilgisayar hakkındaki bilgileri alır statik bir sınıftır.
         //GetHostEntry:ana bilgisayar ilgili adres hakkında bilgi için DNS veritabanını sorgular ve bilgileri döndürür 
         //GetHostName: yerel bilgisayarın ana bilgisayar adını(Host Name) alma yöntemidir.

            foreach (var ip in host.AddressList)//host.AddressList:ana bilgisayarla ilişkilendirilmiş IP adreslerinin listesini alır veya ayarlar
            {
                if (ip.AddressFamily==AddressFamily.InterNetwork)//IPAddress.AddressFamily :IP adresinin adres ailesini alır. Sunucu tarafından desteklenen adres ailesinin türünü görüntüler
                {
                    return ip.ToString();
                }
            }
            return "Local Ip Address Not Found!";
        }
        public static string IpAddress { get { return GetIpAddress(); } }

    }
}

//System Net:Local ve global IP!leri loglama
//Syste.Net.Socket:Client ve Server arasındaki bağlantılar:sunucu tarafında destekelenen ip ailesi ,server ile ilişkilendirilmiş ip adresleri vs