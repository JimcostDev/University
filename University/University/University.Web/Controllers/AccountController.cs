using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Services.Implements;
using University.Web.App_Start;

namespace University.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Account
        [HttpGet]//mostrar o cargar la vista
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]//recibe los datos
        public async Task<ActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SendEmailConfirmationTokenAsync(user.Id, user.UserName, user.Email, "Por favor, valida tu cuenta de University");

                    ViewBag.Message = "Check your email and confirm your account, you must be confirmed "
                        + "before you can log in.";

                    //ViewBag.Message = string.Format("User {0} was created successfully!", user.UserName);

                    return View("Info", "_LayoutAccount");
                }

                AddErrors(result);
            }

            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email,
                model.Password,
                model.RememberMe,
                shouldLockout: false);

            // Require the user to have a confirmed email before they can log on.
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user != null)
            {
                if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    await SendEmailConfirmationTokenAsync(user.Id,
                        user.UserName,
                        user.Email,
                        "Por favor, valida tu cuenta de University");

                    ViewBag.errorMessage = "You must have a confirmed email to log on. "
                                         + "The confirmation token has been resent to your email account.";
                    return View("Error", "_LayoutAccount");
                }
            }
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.LockedOut:
                    return View("Lockout", "_LayoutAccount");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        [HttpGet]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return View("Error");

            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [HttpPost]
        public ActionResult Logoff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        #region Helpers
        private async Task SendEmailConfirmationTokenAsync(string userID,
            string userName,
            string userEmail,
            string subject)
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userID, code = code }, protocol: Request.Url.Scheme);

            EmailService emailService = new EmailService();
            var data = new { UserName = userName, CallbackUrl = callbackUrl };
            string basePathTemplate = Server.MapPath("~/Resources/Templates/ConfirmEmail.html");
            var content = emailService.GetHtml(basePathTemplate, data);
            List<Documents> documents = new List<Documents>();
            documents.Add(new Documents
            {
                ContentId = "angular-logo",
                Disposition = Disposition.inline.ToString(),
                Type = "image/png",
                Filename = "angular-logo",
                Path = Server.MapPath("~/Resources/Templates/Images/angular-logo.png")
            });

            await emailService.SendNotification(documents, userEmail, subject, content);
        }
        #endregion
    }
}
