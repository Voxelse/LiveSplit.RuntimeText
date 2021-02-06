using LiveSplit.RuntimeText;
using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.IO;
using System.Reflection;

[assembly: ComponentFactory(typeof(RuntimeTextFactory))]
namespace LiveSplit.RuntimeText {
    public class RuntimeTextFactory : IComponentFactory {
        public string UpdateName => ComponentName;
        public string UpdateURL => Path.Combine("https://raw.githubusercontent.com/Voxelse", Assembly.GetExecutingAssembly().GetName().Name, "main/");
        public string XMLURL => UpdateURL + "Components/ComponentsUpdate.xml";
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public string ComponentName => "Runtime Text";
        public string Description => "Displays text that can be modified by code at runtime.";
        public ComponentCategory Category => ComponentCategory.Information;
        public IComponent Create(LiveSplitState state) => new RuntimeTextComponent(state, "Runtime Text", "Runtime Text");
    }
}