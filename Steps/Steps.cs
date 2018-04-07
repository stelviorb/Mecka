using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeckaAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace MeckaAutomation.Steps
{
    public class Steps
    {
        private MainPage mainPage;


        private IWebDriver webDriver;
        public Steps(IWebDriver webdriver)
        {
            this.webDriver = webdriver;
        }
        
        
        public void openUrl(string url)
        {
            mainPage = new MainPage(webDriver);
            mainPage.openMainPage(url);
        }

        public void searchByPartNumberOrName(string searchRequest)
        {
            mainPage.searchByPart(searchRequest);
        }

        public void checkSearchRequestInResult(string searchRequest, string productName)
        {
            mainPage
                .verifyBreadcrumb(searchRequest)
                .verifyTitleWitSearchRequest(searchRequest)
                .verifySearchRequestExistInResult(productName);
        }

        public void expandFirstLeafInCategorySection()
        {
            mainPage.expandFirstCategory();
        }

        public void checkResultContainsProduct(string productName)
        {
            mainPage.verifySearchRequestExistInResult(productName);
        }

        public void openFirstItemInBrandMenu()
        {
            mainPage.openFirstBrand();
        }

        public void searchVehicleBy_YearMakeModel_Drive(string year, string make, string model, string drive)
        {
            mainPage
                .selectVehicleByYearMakeAndModel(year, make, model)
                .selectVehicleFirstDriveDisplayed();
        }

        public void checkVehicleSearchText(string vehicleSearch)
        {
            mainPage.verifyVehicleSearchText(vehicleSearch);
        }

        public void searchVehicleBy_YearMakeModel_BodyTypeEngine(string year, string make, string model, string bodyType, string engine)
        {
            mainPage
                .selectVehicleByYearMakeAndModel(year, make, model)
                .selectVehicleFirstBodyTypeDisplayed(bodyType)
                .selectVehicleFirstEngineDisplayed(engine);
        }
    }
}
