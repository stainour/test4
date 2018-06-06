using ApplicationServices;
using ApplicationServices.IoC;
using Castle.Windsor;
using Domain;
using NUnit.Framework;
using System.IO;

namespace Test
{
    [TestFixture]
    internal class IoC
    {
        private readonly IWindsorContainer _container;

        public IoC()
        {
            _container = new WindsorContainer();
            _container.Install(new Installer());
        }

        [Test]
        public void MatrixOperationFactory_Resolve_MatchOperationType()
        {
            var operationFactory = _container.Resolve<IMatrixOperationFactory>();

            Assert.True(operationFactory.Create(Operations.Transpose).GetType().Name == "TransposeMatrixOperation");
        }

        [Test]
        public void ResolveAllMatrixOperations_MatchCount()
        {
            var matrixOperations = _container.ResolveAll<IMatrixOperation>();
            Assert.AreEqual(matrixOperations.Length, 4);
        }

        [Test]
        public void ResolveMainService_Ok()
        {
            Assert.NotNull(_container.ResolveAll<MainService>());
        }

        [Test]
        public void ResolveMatrixOperationCommandTextFileDeserializer_Ok()
        {
            Assert.NotNull(_container.ResolveAll<IMatrixOperationCommandDeserializer<FileInfo>>());
        }

        [Test]
        public void ResolveMatrixSerializer_Ok()
        {
            Assert.NotNull(_container.ResolveAll<IMatrixSerializer<FileInfo>>());
        }
    }
}