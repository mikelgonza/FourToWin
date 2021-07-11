using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FourToWin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FourToWin.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _environment;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public string UserImage { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [PersonalData]
            [Display(Name = "Nickname")]
            public string Nickname { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
            UserImage = user.UserImage;

            Input = new InputModel
            {
                Nickname = user.Nickname,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile imageFile)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (imageFile != null)
            {
                // If img folder not exist, create it
                string path = Path.Combine(_environment.WebRootPath, "img");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // get the file name and upload to img folder
                string fileName = Path.GetFileName(imageFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    imageFile.CopyTo(stream);

                user.UserImage = fileName;
                var setImageResult = await _userManager.UpdateAsync(user);
                if (!setImageResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set Image.";
                    return RedirectToPage();
                }
            }

            if (Input.Nickname != user.Nickname)
            {
                user.Nickname = Input.Nickname;
                var setNicknameResult = await _userManager.UpdateAsync(user);
                if (!setNicknameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set Nickname.";
                    return RedirectToPage();
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
