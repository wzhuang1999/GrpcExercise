using System.ComponentModel.Composition;
using System.Linq;
using Common;

namespace Plugin1
{
    [Export(typeof(IPlugin1))]
    [ExportMetadata("PluginName", "MySuperPlugin1")]
    public class Plugin1 : IPlugin1
    {
        public string UpperCaseAndConcatStrings(params string[] values)
        {
            return string.Join(", ", values.Select(i => i.ToUpper()));
        }
    }
}
