using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDIClient.Service;
using HDIClient.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    public class ReportTest
    {
        private readonly IPolicyService _service;
        private readonly IReportService _reportService;


        public ReportTest()
        {
          // var configuration = new Mock

        }

        [Fact]
        public void TestMethod()
        {
           
        }

        //[Fact]
        //public void TestGetReportById()
        //{

        //}
    }
}
