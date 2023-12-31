﻿using HDIClient.DTOs;
using HDIClient.Models;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Net;
using System.Reflection;
using System.Security.Claims;

namespace HDIClient.Controllers
{
    [Authorize]
    public class PolicyController : Controller
    {
        readonly IPolicyService _service;

        public PolicyController(IPolicyService service, IMemoryCache memoryCache)
        {
            _service = service;

        }
        [Authorize(Roles = "conductor")]
        public async Task<IActionResult> ViewPolicy()
        {
            var token = User.FindFirst("token").Value;
            var idUser = User.FindFirst(ClaimTypes.Sid).Value;
            var listPolicy = new List<PolicyDTO>();
            var model = new PolicyViewModel();
            try
            {
                (listPolicy, var code) = await _service.GetPolicyByDriver(token, idUser);
                if (code == HttpStatusCode.InternalServerError)
                {
                    return RedirectToAction("ErrorServer", "Home");
                }
                else if(code == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("LoginView", "Account");
                }
                else if (code == HttpStatusCode.OK)
                {
                    model.PolicyList = listPolicy;
                }
                else
                {
                    return RedirectToAction("ErrorNotFound", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorServer", "Home");
            }

            return View(model);
        }
    }
}
