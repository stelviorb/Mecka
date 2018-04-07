using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace MeckaAutomation.Pages
{
    class MainPage
    {
        private IWebDriver webDriver;

        private By fld_SearchPart = By.XPath("//*[@id='ctl00__Header_HeaderLogoArea_SearchTextBox']");
        private By btn_Search = By.Id("ctl00__Header_HeaderLogoArea_SearchButton");
        
        private By btn_AddToCart = By.Name("addToCartSubmit");
        private By btn_DisplayGrid = By.XPath("//*[@id='content']/div[5]/div/a");
        private By btn_DisplayList = By.XPath("//*[@id='content']/div[5]/div/span");
        private By btn_FilterByBrand_Item1 = By.XPath("//div[@class='category-list-with-image']/descendant::div[1]");
        private By btn_FilterByBrand_Item2 = By.XPath("//div[@class='category-list-with-image']/descendant::div[5]");
        private By btn_FilterByCategory_Item1 = By.XPath("//div[@class='category-list']/descendant::div[1]");
        private By btn_FilterByCategory_Item2 = By.XPath("//div[@class='category-list']/descendant::div[4]");
        private By brcrmb_Search = By.XPath("id('PageContent__BreadcrumbPanel')/a[2]");

        private By firstItemInResult = By.XPath("//*[@id='PageContent__TooltipZone']/div[1]/div[1]/div/div[2]/div[1]/a/span");
        private By fld_SearchByPartNumber = By.XPath("//*[@id='ctl00__Header_HeaderLogoArea_SearchTextBox']");
        private By hdr_FilterByBrand = By.XPath("//h2[contains(.,'Filter by Brand')]");
        private By hdr_FilterByCategory = By.XPath("//h2[contains(.,'Filter by Category')]");
        private By sctn_DescriptionOnProductPage = By.XPath("//a[@href='#tab-description']");
        private By sctn_FirstProductInResult = By.XPath("//*[@class='product-list']/div[1]");
        private By ttl_Search = By.XPath("id('PageContent__H1')/span[1]");
        private By ttl_VehicleSearch = By.XPath("//h1[@id='PageContent__H1']/span");
        private By txt_nameOfFirstItemInResult = By.XPath("//*[@id='PageContent__TooltipZone']/div[1]/div[1]/div/div[2]/div[1]/a/span");
        private By txt_ProductNumber = By.XPath("//div[@class='rdpWrap'][3]");
        private By sctn_ProductList = By.ClassName("product-list");
        private By sctn_ProductList_FirstItem = By.XPath("//*[@class='product-list']/div[1]");
        private By sctn_ProductList_FirstItem_Name = By.XPath("//*[@class='product-list']/div[1]//div[@class='name']/a");

        //Category        
        private By categoryName = By.XPath("//*[@class='box-content box-category']/ul[1]/li[1]");
        private By CategorySubName1 = By.XPath("//*[@class='box-content box-category']/ul[1]/li[1]/ul[1]/li[1]");
        private By CategorySubName1Name = By.XPath("//*[@class='box-content box-category']/ul[1]/li[1]/ul[1]/li[1]/div");
        private By CategorySubName1Leaf1 = By.XPath("//*[@id='ColumnLeft_ColumnLeftCategories_categoryList']/li[1]/ul[1]/li[1]/ul/li[1]/a");

        //Menu
        private By btn_TopMenu_Brands = By.XPath("//*[@id='TopNavigation__MenuList']//a[contains(text(), 'Brands')]");
        private By btn_TopSubMenu_FirstBrand = By.XPath("//*[@class='menu_brands sub']/div/div/div[1]/a");

        //Search by vehicle section
        private By txt_SearchByVehicleField = By.Id("ctl00__Header_HeaderLogoArea__VehicleSearchControl__ComboBox_Input");
        private By itm_FirstItemInSuggestionVehicleList = By.XPath("//div[@id='ctl00__Header_HeaderLogoArea__VehicleSearchControl__ComboBox_DropDown']/div/ul/li[1]");
        private By itm_FirstItemInSuggestionDriveList = By.XPath("//div[@id='ctl00__Header_HeaderLogoArea__VehicleSearchControl__ComboBox_DropDown']/div/ul/li[3]");
        private By itm_FirstItemInSuggestionBodyTypeList = By.XPath("//div[@id='ctl00__Header_HeaderLogoArea__VehicleSearchControl__ComboBox_DropDown']/div/ul/li[3]");
        private By itm_FirstItemInSuggestionEngineList = By.XPath("//div[@id='ctl00__Header_HeaderLogoArea__VehicleSearchControl__ComboBox_DropDown']/div/ul/li[3]");
        //private By xxx = By.XPath("xxx");
        //private By xxx = By.XPath("xxx");



        public MainPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public MainPage openMainPage(String url)
        {
            webDriver.Url = url;

            return getMainnPageWithTimeout();
        }

        private MainPage getMainnPageWithTimeout()
        {
            Thread.Sleep(1000 * 0);
            return this;
        }


        public MainPage searchByPart(string searchRequest)
        {
            webDriver.FindElement(fld_SearchPart).SendKeys(searchRequest);
            webDriver.FindElement(btn_Search).Click();
            return this;
        }

        public MainPage verifyBreadcrumb(string searchRequest)
        {
            Assert.That(webDriver.FindElement(brcrmb_Search).Displayed, Is.EqualTo(true), "Breadcrumb is not displayed");
            string getSearchRequest = webDriver.FindElement(brcrmb_Search).Text;
            Assert.That(getSearchRequest, Is.EqualTo("Search " + searchRequest), $"Returned breadcrumb content is {getSearchRequest}");
            //Console.WriteLine("BreadCrumbs = " + getSearchRequest);
            return this;
        }

        public MainPage verifyTitleWitSearchRequest(string searchRequest)
        {
            Assert.That(webDriver.FindElement(ttl_Search).Displayed, Is.EqualTo(true), "Title with Search Request is not displayed");
            string getTitleWithSearchRequest = webDriver.FindElement(ttl_Search).Text;
            Assert.That(getTitleWithSearchRequest, Is.EqualTo("Search: " + searchRequest), $"Returned breadcrumb content is {getTitleWithSearchRequest}");
            //Console.WriteLine("Title with Search Request = " + getTitleWithSearchRequest);            
            return this;
        }

        private List<By> getAllProductNamesInProductList()
        {
            bool ifElementDisplayed = true;
            List<By> allItems = new List<By>();
            for (int i = 1; i < 20; i++)
            {
                    try
                {
                    ifElementDisplayed = webDriver.FindElement(By.XPath($"//*[@class='product-list']/div[{i}]//div[@class='name']/a")).Displayed;
                    if (ifElementDisplayed)
                    {
                        By temp = By.XPath($"//*[@class='product-list']/div[{i}]//div[@class='name']/a");
                        allItems.Add(temp);
                    }
                }
                catch (Exception e)
                {
                    break;                 
                }
            }
            Console.WriteLine("allItems size = " + allItems.Capacity);
            return allItems;
        }

        private void getTextFromWebElementsCollection(List<By> collectionList)
        {
            Console.WriteLine("\n Name - ");
            foreach (var webElement in collectionList)
            {
                Console.WriteLine(webDriver.FindElement(webElement).Text);
            }
        }

        public MainPage printProductName()
        {
            getTextFromWebElementsCollection(getAllProductNamesInProductList());
            return this;
        }

        public MainPage verifySearchRequestExistInResult(string searchRequest)
        {
            var productNamesList = getAllProductNamesInProductList();
            bool matchSearchRequest = false;
            foreach (var productNameElement in productNamesList)
            {
                if (matchSearchRequest) break;
                string productName = webDriver.FindElement(productNameElement).Text;
                if (productName == searchRequest)
                {
                    Assert.That(productName, Is.EqualTo(searchRequest));
                    matchSearchRequest = true;                                        
                }
                          
            }
            if (!matchSearchRequest)
            {
                Assert.That(matchSearchRequest, Is.EqualTo(true), $"Search request '{searchRequest}' doesn't exsist in result");
            }
            return this;
        }

        public MainPage expandFirstCategory()
        {
            webDriver.FindElement(categoryName).Click();
            webDriver.FindElement(CategorySubName1).Click();
            webDriver.FindElement(CategorySubName1Leaf1).Click();
            return this;
        }

        public MainPage openFirstBrand()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("document.body.style.zoom = '35%';");


            //WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            //var element = wait.Until(ExpectedConditions.ElementIsVisible(btn_TopMenu_Brands));

            //Actions action = new Actions(webDriver);
            //action.MoveToElement(element).Perform();

            //Thread.Sleep(5000);
            IWebElement element1 = webDriver.FindElement(btn_TopSubMenu_FirstBrand);
            js.ExecuteScript("arguments[0].click()", element1);

            //webDriver.FindElement(btn_TopSubMenu_FirstBrand).Click();

            //JavascriptExecutor js = (JavascriptExecutor)driver;
            //js.executeScript("arguments[0].click()", yourElement);
            //Thread.Sleep(3000);

            js.ExecuteScript("document.body.style.zoom = '100%';");

            return this;
        }


        public MainPage selectVehicleByYearMakeAndModel(string year, string make, string model)
        {
            webDriver.FindElement(txt_SearchByVehicleField).SendKeys(year + " " + make + " " + model);
            Thread.Sleep(1000);

            IWebElement element = webDriver.FindElement(itm_FirstItemInSuggestionVehicleList);
            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("arguments[0].click()", element);

            //webDriver.FindElement(itm_FirstItemInSuggestionVehicleList).Click();
            //Thread.Sleep(3000);
            return this;
        }

        public MainPage selectVehicleFirstDriveDisplayed()
        {
            Thread.Sleep(1000);
            webDriver.FindElement(itm_FirstItemInSuggestionDriveList).Click();

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            //var element = wait.Until(ExpectedConditions.ElementIsVisible(itm_FirstItemInSuggestionDriveList));
            Actions action = new Actions(webDriver);
            //action.MoveToElement(element).Click().Build().Perform();


            //var element = wait.Until(ExpectedConditions.ElementIsVisible(sctn_FirstProductInResult));
            //action = new Actions(webDriver);
            //action.MoveToElement(element).Perform();
            return this;
        }

        public MainPage verifyVehicleSearchText(string vehicleSearch)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(ttl_VehicleSearch));
            Actions action = new Actions(webDriver);
            action.MoveToElement(element).Click().Build().Perform();

            string vehicleSearchText = webDriver.FindElement(ttl_VehicleSearch).Text;
            Assert.That(vehicleSearchText, Is.EqualTo("Vehicle Search: " + vehicleSearch), "Title vehicle search didn't match");
            return this;
        }

        public MainPage selectVehicleFirstBodyTypeDisplayed(string bodyType)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(itm_FirstItemInSuggestionBodyTypeList));
            Actions action = new Actions(webDriver);
            action.MoveToElement(element).Click().Build().Perform();

            Thread.Sleep(1000);

            return this;
        }

        public MainPage selectVehicleFirstEngineDisplayed(string engine)
        {
            //WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            //var element = wait.Until(ExpectedConditions.ElementIsVisible(itm_FirstItemInSuggestionEngineList));
            //Actions action = new Actions(webDriver);
            //action.MoveToElement(element).Click().Build().Perform();
            //try
            //{
            //    action.MoveToElement(element).Click().Build().Perform();
            //}
            //catch (StaleElementReferenceException e)
            //{
            //    Console.WriteLine(e);
            //    action.MoveToElement(element).Click().Build().Perform();
            //}

            webDriver.FindElement(itm_FirstItemInSuggestionEngineList).Click();

            return this;
        }
    }
}
