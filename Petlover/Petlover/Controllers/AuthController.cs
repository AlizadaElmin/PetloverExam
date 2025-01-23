using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Petlover.Enums;
using Petlover.Models;
using Petlover.ViewModels.UserVMs;

namespace Petlover.Controllers;

public class AuthController(UserManager<User> _userManager,SignInManager<User> _signInManager,IMapper _mapper):Controller
{
    private bool isAuthenticated => HttpContext.User.Identity?.IsAuthenticated ?? false;

    public IActionResult Register()
    {
        if (isAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM register)
    {
        if(isAuthenticated) return RedirectToAction("Index","Home");
        if(!ModelState.IsValid) return View();
        User user = _mapper.Map<User>(register);

        var userCreate =await _userManager.CreateAsync(user,register.Password);

        if (!userCreate.Succeeded)
        {
            foreach(var err in userCreate.Errors)
            {
                ModelState.AddModelError("",err.Description);
            }
            return View();
        }
        var roleResult = await _userManager.AddToRoleAsync(user, nameof(Roles.User));
        if (!roleResult.Succeeded) 
        {
            foreach (var err in roleResult.Errors) {
                ModelState.AddModelError("", err.Description);
            }
            return View();
        }
        return RedirectToAction("Login","Auth");
    }

    public async Task<IActionResult> Login()
    {
        if (isAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM login,string? returnUrl=null)
    {
        if (isAuthenticated) return RedirectToAction("Index", "Home");
        if (!ModelState.IsValid) return View();
        User? user = null;

        if(login.UsernameOrEmail.Contains("@"))
            user = await _userManager.FindByEmailAsync(login.UsernameOrEmail);
        else
            user = await _userManager.FindByNameAsync(login.UsernameOrEmail);

        if (user == null) {
            ModelState.AddModelError("", "Incorrect username or email");
            return View();
        }

        var loginAccount =await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
        if (!loginAccount.Succeeded)
        {
            if (loginAccount.IsLockedOut)
            {
                ModelState.AddModelError("","Wait until "+user.LockoutEnd!.Value.ToString("yyyy - MM - dd HH: mm:ss"));
            }
            if (loginAccount.IsNotAllowed)
            {
                ModelState.AddModelError("", "Username or email incorrect");
            }
            return View();
        }

        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            if(await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("Index",new {area ="Admin",controller="Dashboard" });
            }
            return RedirectToAction("Index", "Home");
        }
        return LocalRedirect(returnUrl);
    }
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
}
