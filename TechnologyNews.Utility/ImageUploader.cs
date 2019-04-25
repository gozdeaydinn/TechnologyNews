using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TechnologyNews.Utility
{
    public class ImageUploader
    {
        public static string DefaultProfileImagePath = "/Content/Uploads/UserImages/OriginalImages/user_default_image.png";

        public static string DefaultXSmallProfileImage = "/Content/Uploads/UserImages/XSmallImages/user_default_image.png";

        public static string DefaulCruptedProfileImage = "/Content/Uploads/UserImages/CruptedImages/user_default_image.png";

        public static string OriginalProfileImagePath = "~/Content/Uploads/UserImages/OriginalImages/";

        public static List<string> UploadSingleImage(string serverPath, HttpPostedFileBase file, int saveAsParam)//Tek resim yükle - //httppostfilebase: Server'a atılacak olan dosyayı barındıracak olan property-dosya yükleme yolu
        {
            string OriginalImagePath = "~/Content/Uploads/UserImages/OriginalImages/";
            string XSmallImagePath = "~/Content/Uploads/UserImages/XSmallImages/";
            string CruptedImagePath = "~/Content/Uploads/UserImages/CruptedImages/";

            List<string> ImagePaths = new List<string>();

            if (file != null)
            {
                var uniqueName = Guid.NewGuid();//yeni bir guid nesnesi oluşturdu-benzersiz

                serverPath = serverPath.Replace("~", string.Empty);//serverpath de boşlukları ~ ile değiştirir - string.Empty:uzunluğu "sıfır" olan bir stringi represent etmekte olup kendisine "null" değeri atanmış olup, henüz kendisinin refere ettiği bir string örneklemi bulunmadığını belirtmektedir. 

                var fileArray = file.FileName.Split('.');//Dosya ismini . ile böldü
                string extension = fileArray[fileArray.Length - 1].ToLower();//adresteki uzantıyı küçük harfe çevirdi-uzantı

                var fileName = uniqueName + "." + extension;//dosya ismi benzersiz ad ve uzantıdan oluşsun

                if (extension == "jpg" || extension == "jpeg" || extension == "png" || extension == "gif")
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))//Sql'deki dosya yolu hücresinde belirtilen dosyanın local'de olup olmadığını denetler. (Uzak bir sunucudaki dosyaları denetlemez. Sadece kendi local'deki dosyaları denetler.)
                    {
                        ImagePaths.Add("1");

                        return ImagePaths;
                    }
                    else
                    {
                        var filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);//Dosya localde yoksa yeni bir dosya yolu belirtir


                        file.SaveAs(filePath);//dosya yolunu kaydeder

                        /* Görselleri boyutlandırmak için ImageResizer namespace`ini kullanıyorum. Bu işlemin dökümantasyonu imageresizing.net adresinde var(Package Manager Console ile hallettim). */
                        //ResizeSetting:Görüntüyü işlemek için kullanılacak ayarları temsil eder. 
                        //FitMode.Crop:Genişlik ve yükseklik kesin değerler olarak kabul edilir -en boy oranı farkı varsa kırpma kullanılır. 
                        ImageResizer.ResizeSettings ImageSetting = new ImageResizer.ResizeSettings();
                        ImageSetting.Width = 29;
                        ImageSetting.Height = 29;
                        ImageSetting.Mode = ImageResizer.FitMode.Crop;


                        ImageResizer.ImageBuilder.Current.Build(OriginalImagePath + fileName, XSmallImagePath + fileName, ImageSetting);


                        if (saveAsParam == 1)
                        {
                            ImageSetting.Width = 150;
                            ImageSetting.Height = 150;
                            ImageSetting.Mode = ImageResizer.FitMode.Crop;
                        }
                        else
                        {
                            ImageSetting.Width = 213;
                            ImageSetting.Height = 133;
                            ImageSetting.Mode = ImageResizer.FitMode.Crop;
                        }

                        ImageResizer.ImageBuilder.Current.Build(OriginalImagePath + fileName, CruptedImagePath + fileName, ImageSetting);

                        ImagePaths.Add(serverPath + fileName);
                        ImagePaths.Add(XSmallImagePath.Replace("~", string.Empty) + fileName);
                        ImagePaths.Add(CruptedImagePath.Replace("~", string.Empty) + fileName);

                        return ImagePaths;
                    }
                }
                else
                {
                    ImagePaths.Add("2");

                    return ImagePaths;
                }


            }
            ImagePaths.Add("0");

            return ImagePaths;
        }
    }
}

/*HttpHandler kullanımını basitçe anlatacağım. Bazı noktalarda önemli problemleri çözdüğüne şahit olduğum için bu konu hakkında ufak bir makale yazmaya karar verdim. Örnek bir problemden bahsetmek gerekirse; web sitelerinde çok farklı sebeplerden resim kullanılabilir. Tanıtım, profil, reklam vb. amaçlarla yoğun olarak resim dosyalarını web sayflarında göstermek isteriz. Fakat bazen web sunucusu url olarak girilen resim dosyasını dosya sistemi üzerinde bulamayabilir. Bu problemin başlıca sebepleri; dosyanın sunucuda silinmiş olması ya da yanlış resim url’ sinin girilmesi ki dinamik olarak oluşruruluyorsa sık karşılaşılabilir. Bu durumda sunucumuzdan istenen her resim dosyasının önce var olup olmadığını kontrol edip varsa devam edilmesini yoksa bizim belirlediğimiz default bir resmi göstermesini sağlamak web sayfamızda görüntü kirliliği oluşmasını engelleyecektir. Bu yöntemi resimlerinizi çektiğiniz kod üzerinden de "if else" mantığı ile yapabilirsiniz fakat büyük projelerde her seferinde bu işlemleri yapmak pratik olmayacaktır. Bunun yerine bir handler oluşturup, belirli kriterlerdeki dosya ya da istekleri tutup, kendi istediğiniz cevabı vermesini sağlayabilirsiniz.*/
