﻿using HDIClient.DTOs;
using HDIClient.Responses;
using HDIClient.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace HDIClient.Service
{

    public class UserService : IUserService
    {
        HttpClient _cliente;
        TokenDTO _token;
        IMemoryCache _memoryCache;

        public UserService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");

        }

        public async Task<TokenDTO> Login(string user, string pass)
        {
            TokenDTO result = null;
            try
            {
                var loginInfo = new LoginDTO
                {
                    User = user,
                    Password = pass
                };
                var json = new StringContent(JsonConvert.SerializeObject(loginInfo), Encoding.UTF8, "application/json");
                var response = await _cliente.PostAsync("/api/User/LoginEmployee", json);

                var responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<TokenDTO>(responseBody);

                if (response.IsSuccessStatusCode)
                {
                    result = responseObject;
                }

            }
            catch (Exception)
            {
                throw new Exception("Error al conectarse con el servidor");
            }
            return result;
        }
        
    }
}

