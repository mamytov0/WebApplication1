using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.StoredProcedure;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
       
            UserAccesslayer accesslayer = null;

            public AccountController(UserAccesslayer u)
            {
                accesslayer = u;
            }

            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginModel model)
            {
                Crypt crypt = new Crypt();
                if (ModelState.IsValid)
                {

                    //IEnumerable<User> u = accesslayer.GetAllData();
                    User user = await Task.Run(() => accesslayer.GetAllData().FirstOrDefault(u => u.Email == model.Email && u.Password == crypt.GetMD5(model.Password)));
                    if (user != null)
                    {
                        await Authenticate(model.Email); // аутентификация

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");

                }
                return View(model);
            }
            private async Task Authenticate(string userName)
            {
                // создаем один claim
                var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
                // создаем объект ClaimsIdentity
                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                // установка аутентификационных куки
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Account");
            }

            public IActionResult Index()
            {
                IEnumerable<User> u = accesslayer.GetAll();

                return View(u);
            }

            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(User user)
            {
                try
                {
                    accesslayer.Add(user);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception)
                {
                    return View();
                }
            }

            public ActionResult Delete(int ID)
            {
                User user = accesslayer.Details(ID);

                return View(user);
            }
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult Delete(User user)
            {
                try
                {
                    // TODO: Add delete logic here  
                    accesslayer.Delete(user.ID);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            public ActionResult Edit(int ID)
            {
                User user = accesslayer.Details(ID);
                user.Password = null;
                return View(user);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(User user)
            {
                try
                {
                    accesslayer.Update(user);
                    // TODO: Add update logic here  
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

        }
    }
