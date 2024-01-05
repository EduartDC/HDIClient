using HDIClient.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HDIClient.Service.Interface
{
    public interface IAccidentService
    {
        Task<List<AccidentDTO>> GetAccidents();
        Task<List<AccidentDTO>> GetAccidentsWithoutAdjuster();
        Task<bool> AssignAdjusterToAccident([FromBody] AdjusterWithAccidentDTO element);
    }
}
