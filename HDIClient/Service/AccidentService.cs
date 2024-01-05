using AseguradoraApp.Models;
using HDIClient.DTOs;
using HDIClient.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HDIClient.Service
{
    public class AccidentService : IAccidentService
    {
        HttpClient _cliente;
        public AccidentService(IHttpClientFactory httpClientFactory)
        {
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");
        }

        public async Task<bool> AssignAdjusterToAccident([FromBody] AdjusterWithAccidentDTO element)
        {
            try
            {
                var elementToSend = new AdjusterWithAccidentDTO
                {
                    IdAccident = element.IdAccident,
                    IdAdjuster = element.IdAdjuster
                };

                var json = new StringContent(JsonConvert.SerializeObject(elementToSend), Encoding.UTF8, "application/json");

                var response = await _cliente.PostAsync("/api/Accident/AssignAdjusterToAccident", json);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
         
        public async Task<List<AccidentDTO>> GetAccidents()
        {
            List<AccidentDTO> accidentList = new List<AccidentDTO>();
            try
            {
                var response = await _cliente.GetAsync($"/api/Accident/GetAccidents");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    accidentList = JsonConvert.DeserializeObject<List<AccidentDTO>>(content);
                }else
                {
                    return null;
                }
            }   
            catch (Exception ex)
            {
                return null;
            }
            return accidentList;
        }

        public async Task<List<AccidentDTO>> GetAccidentsWithoutAdjuster()
        {
            List<AccidentDTO> accidentList = new List<AccidentDTO>();
            try
            {
                var response = await _cliente.GetAsync($"/api/Accident/GetAccidentsWithoutAdjuster");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    accidentList = JsonConvert.DeserializeObject<List<AccidentDTO>>(content);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return accidentList;
        }
    }
}
