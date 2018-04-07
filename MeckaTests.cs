using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MeckaAutomation.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;



namespace MeckaAutomation
{
    //[TestFixture]
    [TestClass]
    class MeckaTests
    {
        private IWebDriver webDriver;
        private Steps.Steps step;


        [OneTimeSetUp]
        //[SetUp]
        public void startTest() // This method will be fired at the start of the test
        {
            //driver = new ChromeDriver();
            //driver.Url = "https://google.ca";
            webDriver = Browsers.Init();
            step = new Steps.Steps(webDriver);


            //var dir = Path.GetDirectoryName(typeof(TestingWithReferencedFiles).Assembly.Location);
            //if (dir != null)
            //{
            //    Environment.CurrentDirectory = dir;
            //    Directory.SetCurrentDirectory(dir);
            //}
            //else
            //    throw new Exception("Path.GetDirectoryName(typeof(TestingWithReferencedFiles).Assembly.Location) returned null");

        }

        [OneTimeTearDown]
        //[TearDown]
        public void endTest() // This method will be fired at the end of the test
        {
            //driver.Quit();
            Browsers.Close();
            

        }
        public static IEnumerable<TestCaseData> SearchBy_Brand_TestCase => new TestParameterBuilder()
            .Add("URL", "url")
            .Add("Product Name", "Product Name")
            .GetTestCases();
        [TestCaseSource(nameof(SearchBy_Brand_TestCase))]
        public void SearchBy_Brand_Test(string url,  string productName)
        {
            Console.WriteLine("URL - " + url);            
            Console.WriteLine("Product Name - " + productName);

            step.openUrl(url);
            step.openFirstItemInBrandMenu();
            step.checkResultContainsProduct(productName);
        }

        public static IEnumerable<TestCaseData> SearchBy_Category_TestCase => new TestParameterBuilder()
            .Add("URL", "url")
            .Add("Product Name", "Product Name")
            .GetTestCases();
        [TestCaseSource(nameof(SearchBy_Category_TestCase))]
        public void SearchBy_Category_Test(string url, string productName)
        {
            Console.WriteLine("URL - " + url);
            Console.WriteLine("Product Name - " + productName);

            step.openUrl(url);
            step.expandFirstLeafInCategorySection();
            step.checkResultContainsProduct(productName);
        }

        public static IEnumerable<TestCaseData> SearchBy_PartNumberAndName_TestCase => new TestParameterBuilder()
            .Add("URL", "url")
            .Add("Search Request", "040")
            .Add("Product Name", "Product Name")
            .GetTestCases();
        [TestCaseSource(nameof(SearchBy_PartNumberAndName_TestCase))]
        public void SearchBy_PartNumberAndName_Test(string url, string searchRequest, string productName)
        {
            Console.WriteLine("URL - " + url);
            Console.WriteLine("Search Request - " + searchRequest);
            Console.WriteLine("Product Name - " + productName);

            step.openUrl(url);
            step.searchByPartNumberOrName(searchRequest);
            step.checkSearchRequestInResult(searchRequest, productName);            
        }

        public static IEnumerable<TestCaseData> SearchVehicleBy_YearMakeModelDrive_TestCase => new TestParameterBuilder()
            .Add("URL", "url")
            .Add("Year", "2016")
            .Add("Make", "Audi")
            .Add("Model", "A6")
            .Add("Drive", "Quatro")
            .Add("Vehicle Search", "2016 Audi A6 4-Door Sedan 2.0L 1984CC 121Cu. In. I4 GAS DOHC Turbocharged FWD")
            .Add("Product Name", "Product Name")
            .GetTestCases();
        [TestCaseSource(nameof(SearchVehicleBy_YearMakeModelDrive_TestCase))]
        public void SearchVehicleBy_YearMakeModelDrive_Test(string url, string year, string make, string model, string drive, string vehicleSearch, 
            string productName)
        {
            Console.WriteLine("URL - " + url);
            Console.WriteLine(year + " " + make + " " + model + " " + drive);
            Console.WriteLine("Product Name - " + productName);

            step.openUrl(url);
            step.searchVehicleBy_YearMakeModel_Drive(year, make, model, drive);
            step.checkVehicleSearchText(vehicleSearch);
            step.checkResultContainsProduct(productName);
        }

        public static IEnumerable<TestCaseData> SearchVehicleBy_YearMakeModel_BodyTypeEngine_TestCase => new TestParameterBuilder()
            .Add("URL", "url")
            .Add("Year", "2016")
            .Add("Make", "Subaru")
            .Add("Model", "Impreza")
            .Add("Body Type", "4-Door Sedan")
            .Add("Engine", "2.5L 2458CC H4 GAS DOHC Turbocharged")
            .Add("Vehicle Search", "2016 Audi A6 4-Door Sedan 2.0L 1984CC 121Cu. In. I4 GAS DOHC Turbocharged FWD")
            .Add("Product Name", "Product Name")
            .GetTestCases();
        [TestCaseSource(nameof(SearchVehicleBy_YearMakeModel_BodyTypeEngine_TestCase))]
        public void SearchVehicleBy_YearMakeModel_BodyTypeEngine_Test(string url, string year, string make, string model, string bodyType, string engine, string vehicleSearch,
            string productName)
        {
            Console.WriteLine("URL - " + url);
            Console.WriteLine(year + " " + make + " " + model + " " + bodyType + " " + engine);
            Console.WriteLine("Product Name - " + productName);

            step.openUrl(url);
            step.searchVehicleBy_YearMakeModel_BodyTypeEngine(year, make, model, bodyType, engine);
            step.checkVehicleSearchText(vehicleSearch);
            step.checkResultContainsProduct(productName);
        }

    }
}
