using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyNews.Model.Option;
using TechnologyNews.Service.Option;
using TechnologyNews.UI.Areas.Admin.Models.DTO;
using TechnologyNews.Utility;

namespace TechnologyNews.UI.Areas.Admin.Controllers
{
    public class AppUserController : Controller
    {
        AppUserService _appUserService;
        public AppUserController()
        {
            _appUserService = new AppUserService();
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(AppUser data, HttpPostedFileBase Image)//httppostfilebase:Server'a atılacak olan resmi barındıracak olan property-resim yükleme yolu
        {
            List<string> UploadedImagePaths = new List<string>();

            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            data.UserImage = UploadedImagePaths[0];

            if (data.UserImage == "0" || data.UserImage == "1" || data.UserImage == "2")
            {
                data.UserImage = ImageUploader.DefaultProfileImagePath;
                data.XSmallUserImage = ImageUploader.DefaultXSmallProfileImage;
                data.CruptedUserImage = ImageUploader.DefaulCruptedProfileImage;
            }
            else
            {
                data.XSmallUserImage = UploadedImagePaths[1];
                data.CruptedUserImage = UploadedImagePaths[2];
            }

            data.Status = Core.Enum.Status.Active;

            _appUserService.Add(data);

            return Redirect("/Admin/AppUser/List");
        }
        public ActionResult Update(Guid id)
        {
            AppUser appuser = _appUserService.GetById(id);
            AppUserDTO model = new AppUserDTO();
            model.ID = appuser.ID;
            model.FirstName = appuser.FirstName;
            model.LastName = appuser.LastName;
            model.UserName = appuser.UserName;
            model.Password = appuser.Password;
            model.Email = appuser.Email;
            model.Role = appuser.Role;
            model.Gender = appuser.Gender;
            model.Address = appuser.Address;
            model.PhoneNumber = appuser.PhoneNumber;
            model.ImagePath = appuser.ImagePath;
            return View(model);

        }
        [HttpPost]
        public ActionResult Update(AppUserDTO data)
        {
            AppUser appuser = _appUserService.GetById(data.ID);
            appuser.FirstName = data.FirstName;
            appuser.LastName = data.LastName;
            appuser.UserName = data.UserName;
            appuser.Password = data.Password;
            appuser.Email = data.Email;
            appuser.Role = data.Role;
            appuser.Gender = data.Gender;
            appuser.Address = data.Address;
            appuser.PhoneNumber = data.PhoneNumber;
            appuser.ImagePath = data.ImagePath;
            _appUserService.Update(appuser);

            return Redirect("/Admin/AppUser/List");
        }
        public ActionResult List()
        {
            List<AppUser> model = _appUserService.GetActive();
            return View(model);
        }

        public RedirectResult Delete(Guid id)
        {
            _appUserService.Remove(id);
            return Redirect("/Admin/AppUser/List");
        }
    }
}