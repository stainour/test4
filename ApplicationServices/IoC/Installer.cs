using ApplicationServices.Infrastructure;
using ApplicationServices.MatrixOperation;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Domain;
using System.IO;

namespace ApplicationServices.IoC
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();

            container.Register(Component.For<IMatrixOperationFactory>().AsFactory(configuration => configuration.SelectedWith(new NamedSelector())));

            container.Register(Component.For<IMatrixOperation>().ImplementedBy<MultiplyMatrixOperation>().Named(Operations.Multiply).LifestyleSingleton());
            container.Register(Component.For<IMatrixOperation>().ImplementedBy<AddMatrixOperation>().Named(Operations.Add).LifestyleSingleton());
            container.Register(Component.For<IMatrixOperation>().ImplementedBy<SubtractMatrixOperation>().Named(Operations.Subtract).LifestyleSingleton());
            container.Register(Component.For<IMatrixOperation>().ImplementedBy<TransposeMatrixOperation>().Named(Operations.Transpose).LifestyleSingleton());

            container.Register(Component.For<IMatrixOperationCommandDeserializer<FileInfo>>().ImplementedBy<MatrixOperationCommandTextFileDeserializer>().LifestyleSingleton());
            container.Register(Component.For<MainService>().ImplementedBy<MainService>().LifestyleSingleton());
            container.Register(Component.For<IMatrixSerializer<FileInfo>>().ImplementedBy<MatrixTextFileSerializer>().LifestyleSingleton());
        }
    }
}