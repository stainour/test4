using ApplicationServices;
using ApplicationServices.IoC;
using Castle.Windsor;
using NUnit.Framework;
using System.IO;

namespace Test
{
    [TestFixture]
    internal class MainServiceTests
    {
        private readonly IWindsorContainer _container;
        private readonly DirectoryInfo _directoryInfo;
        private readonly MainService _mainService;

        public MainServiceTests()
        {
            _container = new WindsorContainer();
            _container.Install(new Installer());
            _directoryInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "TestFiles"));
            _mainService = _container.Resolve<MainService>();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            foreach (var fileInfo in _directoryInfo.GetFiles("*result.txt"))
            {
                fileInfo.Delete();
            }
        }

        [Test]
        public void ProcessFolder_Ok()
        {
            _mainService.ProcessFolder(_directoryInfo.GetFiles("*.txt"));
        }
    }
}