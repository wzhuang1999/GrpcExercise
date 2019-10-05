using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Common;

namespace Extensibility
{
    // Nuget: System.ComponentModel.Composition
    // https://msdn.microsoft.com/en-us/magazine/ee291628.aspx
    // 
    class Program
    {
        private static CompositionContainer container;

        [Import(typeof(IPlugin1))]
        public IPlugin1 Plugin1;

        static void Main(string[] args)
        {
            var instance = new Program();

            instance.Init();

            var result = instance.Plugin1.UpperCaseAndConcatStrings("Karl", "Sepp", "Joe");

            Console.WriteLine(result);

            Console.ReadKey();
        }

        private void Init()
        {
            var pluginDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            Console.WriteLine($"Searching for plugins ins '{pluginDirectory}' ...");

            var catalog = new DirectoryCatalog(pluginDirectory, "*.dll");

            // Create the CompositionContainer with the parts in the catalog.
            container = new CompositionContainer(catalog);

            DebugMetadata();

            // Fill the imports of this object.
            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        private static void DebugMetadata()
        {
            foreach (var parts in container.Catalog.Parts)
            {
                foreach (var export in parts.ExportDefinitions)
                {
                    var metadata = string.Join(", ", export.Metadata.Select(i => $"{i.Key} -> {i.Value}"));

                    Console.WriteLine($"{export.ContractName}: {metadata}");
                }
            }
        }
    }
}
