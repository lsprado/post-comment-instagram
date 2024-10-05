using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumMSTest
{
    [TestClass]
    public class SeleniumTests
    {
        private IWebDriver driver;
        private string[] _LISTA_URLS;

        [TestInitialize]
        public void Setup()
        {
            // Inicializa o WebDriver do Chrome
            driver = new ChromeDriver();

            // Array de URLs
            _LISTA_URLS = new string[]
            {
                "https://www.instagram.com/p/C_u5NhnO2zi/",
                "https://www.instagram.com/p/C-sFahKp6yn/"
            };

        }

        [TestMethod]
        public void TestGoogleSearch()
        {
            // Navega até o Google
            driver.Navigate().GoToUrl("https://www.google.com");

            // Encontra o campo de busca e digita "Selenium"
            IWebElement searchBox = driver.FindElement(By.Name("q"));
            searchBox.SendKeys("Selenium");
            searchBox.Submit();

            // Verifica se o título da página contém "Selenium"
            Assert.IsTrue(driver.Title.Contains("Selenium"));
        }

        [TestMethod]
        public void PostComment()
        {
            // Obter o nome de usuário da variável de ambiente
            string username = "<USER>";
            string password = "<PASS>";

            // Navega até o Instagram
            driver.Navigate().GoToUrl("https://www.instagram.com");
            driver.Manage().Window.Maximize();

            // Espera até que o campo de username esteja presente
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement usernameBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("username")));
            usernameBox.SendKeys(username);

            // Espera até que o campo de senha esteja presente
            IWebElement passwordBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("password")));
            passwordBox.SendKeys(password);

            // botao de login
            IWebElement loginButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='loginForm']/div/div[3]/button")));
            loginButton.Click();

            // fechar save login info
            IWebElement saveLoginInfo = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(), 'Save info')]")));
            saveLoginInfo.Click();

            // fechar save login info
            IWebElement notificationNotNow = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(), 'Not Now')]")));
            notificationNotNow.Click();

            // Itera sobre o array de URLs
            foreach (string url in _LISTA_URLS)
            {
                // Navega até o post
                driver.Navigate().GoToUrl(url);

                // Escrever uma mensagem no post
                IWebElement commentBox = null;
                try
                {
                    commentBox = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//textarea[@aria-label='Add a comment…' and @placeholder='Add a comment…']")));
                    commentBox.SendKeys("Teste de comentario");
                }
                catch (StaleElementReferenceException)
                {
                    commentBox = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//textarea[@aria-label='Add a comment…' and @placeholder='Add a comment…']")));
                    commentBox.SendKeys("Teste de comentario");
                }

                // Localizar e clicar no botão "Post" usando a classe fornecida
                IWebElement postButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='x1i10hfl xjqpnuy xa49m3k xqeqjp1 x2hbi6w xdl72j9 x2lah0s xe8uvvx xdj266r x11i5rnm xat24cr x1mh8g0r x2lwn1j xeuugli x1hl2dhg xggy1nq x1ja2u2z x1t137rt x1q0g3np x1lku1pv x1a2a7pz x6s0dn4 xjyslct x1ejq31n xd10rxx x1sy0etr x17r0tee x9f619 x1ypdohk x1f6kntn xwhw2v2 xl56j7k x17ydfre x2b8uid xlyipyv x87ps6o x14atkfc xcdnw81 x1i0vuye xjbqb8w xm3z3ea x1x8b98j x131883w x16mih1h x972fbf xcfux6l x1qhh985 xm0m39n xt0psk2 xt7dq6l xexx8yu x4uap5 x18d9i69 xkhd6sd x1n2onr6 x1n5bzlp x173jzuc x1yc6y37' and @role='button' and @tabindex='0']")));
                postButton.Click();
            }
        }

        [TestCleanup]
        public void Teardown()
        {
            // Fecha o WebDriver
            driver.Quit();
        }
    }
}
