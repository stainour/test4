using ApplicationServices;
using ApplicationServices.IoC;
using Castle.Windsor;
using ShellProgressBar;
using System;
using System.IO;
using System.Linq;

namespace TestApp
{
    internal class Program
    {
        private static IWindsorContainer InitContainer()
        {
            IWindsorContainer windsorContainer = new WindsorContainer();
            windsorContainer.Install(new Installer());
            return windsorContainer;
        }

        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage:dotnet TestApp.dll <full folder path>");
                return;
            }

            try
            {
                var directoryInfo = new DirectoryInfo(args.First());

                if (!directoryInfo.Exists)
                {
                    Console.WriteLine($"Folder {directoryInfo.FullName} doesn't exist");
                    return;
                }

                var windsorContainer = InitContainer();

                var mainService = windsorContainer.Resolve<MainService>();
                var fileInfos = directoryInfo.GetFiles("*.txt");

                var options = new ProgressBarOptions
                {
                    ProgressCharacter = '#',
                    ProgressBarOnBottom = true,
                    DisplayTimeInRealTime = true
                };
                var pbar = new ProgressBar(fileInfos.Length, $"of {fileInfos.Length} files processed", options);
                mainService.OnFileProcessed += (sender, eventArgs) => pbar.Tick();
                mainService.ProcessFolder(fileInfos);
            }
            catch (Exception e)
            {
                Console.WriteLine($"There is an error processing your command: {e.Message}");
            }
        }
    }
}