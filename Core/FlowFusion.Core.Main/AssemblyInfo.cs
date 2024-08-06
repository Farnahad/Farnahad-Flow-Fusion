using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,            //where theme specific resource dictionaries are located
                                                //(used if a resource is not found in the page,
                                                // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly   //where the generic resource dictionary is located
                                                //(used if a resource is not found in the page,
                                                // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsDefinition("FlowFusion", "FlowFusion.Core.Main.Application")]
[assembly: XmlnsDefinition("FlowFusion", "FlowFusion.Core.Main.Command")]
[assembly: XmlnsDefinition("FlowFusion", "FlowFusion.Core.Main.Converter")]
[assembly: XmlnsDefinition("FlowFusion", "FlowFusion.Core.Main.Mvvm")]