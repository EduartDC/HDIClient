using HDIClient.DTOs;
using HDIClient.Service;
using Moq;
using System.Net;

namespace TestProject
{
    public  class ReportServiceTest
    {
        private readonly ReportService _service;

        public ReportServiceTest()
        {
            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler);

            // Configura la propiedad BaseAddress del HttpClient directamente
            httpClient.BaseAddress = new Uri("https://localhost:7026/");

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _service = new(httpClientFactory.Object);
        }

        //Jonathan
        [Fact]
        public async Task GetReportListByIdEmployeeSuccess()
        {
            var result = await _service.GetPreviewReportsList("e1");
            Xunit.Assert.Equal("200", result.Item1.ToString());
        }

        //Jonathan
        [Fact]
        public async Task GetReportListByIdEmployeeConnectionError()
        {
            var result = await _service.GetPreviewReportsList("e1");
            Xunit.Assert.Equal("500", result.Item1.ToString());
        }

        //Eduart
        [Fact]
        public async Task GetReportByIdSuccess()
        {
            var result = await _service.GetReportById("", "a1");
            Assert.Equal(HttpStatusCode.OK , result.Item2);
        }

        //Eduart
        [Fact]
        public async Task GetReportByIdNotFound()
        {
            var result = await _service.GetReportById("", "a11");
            Assert.Equal(HttpStatusCode.NotFound, result.Item2);
        }

        //Eduart
        [Fact]
        public async Task GetReportByIdInternalServerError()
        {
            var result = await _service.GetReportById("", "a11");
            Assert.Equal(HttpStatusCode.InternalServerError, result.Item2);
        }

        //Eduart
        [Fact]
        public async Task TestPostOpinionSuccess()
        {
            var opinion = new NewOpinionadjusterDTO()
            {
                Description = "Test",
                IdAccident = "gdfg",
                IdOpinionAdjuster = "e1",
                CreationDate = DateTime.Now

            };
            var result = await _service.PostOpionion(opinion, "");
            Assert.Equal(HttpStatusCode.OK, result);
        }

        //Eduart
        [Fact]
        public async Task TestPostOpinionBadRequest()
        {
            var opinion = new NewOpinionadjusterDTO()
            {
                Description = "Test",
                IdAccident = "gdfg",
                IdOpinionAdjuster = "e1",
                CreationDate = DateTime.Now

            };
            var result = await _service.PostOpionion(opinion, "");
            Assert.Equal(HttpStatusCode.BadRequest, result);
        }

        //Eduart
        [Fact]
        public async Task TestPostOpinionInternalServerError()
        {
            var opinion = new NewOpinionadjusterDTO()
            {
                Description = "Test",
                IdAccident = "123123",
                IdOpinionAdjuster = "e1",
                CreationDate = DateTime.Now

            };
            var result = await _service.PostOpionion(opinion, "");
            Assert.Equal(HttpStatusCode.InternalServerError, result);
        }

        //Eduart
        [Fact]
        public async Task TestPutOpinionSuccess()
        {
            var opinion = new NewOpinionadjusterDTO()
            {
                Description = "Tests",
                IdAccident = "gdfg",
                IdOpinionAdjuster = "d9629175-ad7c-4560-83bd-ae93b3df1df1",
                CreationDate = DateTime.Now

            };
            var result = await _service.PutOpionion(opinion, "");
            Assert.Equal(HttpStatusCode.OK, result);
        }

        //Eduart
        [Fact]
        public async Task TestPutOpinionBadRequest()
        {
            var opinion = new NewOpinionadjusterDTO()
            {
                Description = "Test",
                IdAccident = "gdfg",
                IdOpinionAdjuster = "e1asd",
                CreationDate = DateTime.Now

            };
            var result = await _service.PutOpionion(opinion, "");
            Assert.Equal(HttpStatusCode.BadRequest, result);
        }

        //Eduart
        [Fact]
        public async Task TestPutOpinionInternalServerError()
        {
            var opinion = new NewOpinionadjusterDTO()
            {
                Description = "Test",
                IdAccident = "123123",
                IdOpinionAdjuster = "e1",
                CreationDate = DateTime.Now

            };
            var result = await _service.PostOpionion(opinion, "");
            Assert.Equal(HttpStatusCode.InternalServerError, result);
        }

        //Eduart
        [Fact]
        public async Task TestPostReportSuccess()
        {
            var report = new NewReport();

            report.Location = "Test";
            report.AccidentDate = DateTime.Now;
            report.Latitude = "0";
            report.Longitude = "0";
            report.IdDriverClient = "c1";
            report.ReportStatus = "Pendiente";
            report.IdVehicleClient = "v1";

            var images = new List<string>();
            var image = "";
            images.Add(image);

            var involveds = new List<InvolvedDTO>();
            var involved = new InvolvedDTO();
            involved.NameInvolved = "Test";
            involved.LastNameInvolved = "Test";
            involved.LicenseNumber = "Test";
            involveds.Add(involved);

            report.Involveds = involveds;
            report.Images = images;
            
            var result = await _service.PostReport(report, "");
            Assert.Equal(HttpStatusCode.OK, result);
        }


        //Eduart
        [Fact]
        public async Task TestPostReportError()
        {
            var report = new NewReport();

            report.Location = "Test";
            report.AccidentDate = DateTime.Now;
            report.Latitude = "0";
            report.Longitude = "0";
            report.IdDriverClient = "c1";
            report.ReportStatus = "Pendiente";
            report.IdVehicleClient = "";

            var images = new List<string>();
            var image = "";
            images.Add(image);

            var involveds = new List<InvolvedDTO>();
            var involved = new InvolvedDTO();
            involved.NameInvolved = "Test";
            involved.LastNameInvolved = "Test";
            involved.LicenseNumber = "Test";
            involveds.Add(involved);

            report.Involveds = involveds;
            report.Images = images;

            var result = await _service.PostReport(report, "");
            Assert.Equal(HttpStatusCode.BadRequest, result);
        }

        //Eduart
        [Fact]
        public async Task TestPostReportInternalServerError()
        {
            var report = new NewReport();

            report.Location = "Test";
            report.AccidentDate = DateTime.Now;
            report.Latitude = "0";
            report.Longitude = "0";
            report.IdDriverClient = "c1";
            report.ReportStatus = "Pendiente";
            report.IdVehicleClient = "";

            var images = new List<string>();
            var image = "";
            images.Add(image);

            var involveds = new List<InvolvedDTO>();
            var involved = new InvolvedDTO();
            involved.NameInvolved = "Test";
            involved.LastNameInvolved = "Test";
            involved.LicenseNumber = "Test";
            involveds.Add(involved);

            report.Involveds = involveds;
            report.Images = images;

            var result = await _service.PostReport(report, "");
            Assert.Equal(HttpStatusCode.InternalServerError, result);
        }
    }
}
