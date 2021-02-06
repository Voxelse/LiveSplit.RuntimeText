using LiveSplit.ManualText;
using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.IO;
using System.Reflection;

[assembly: ComponentFactory(typeof(TextComponentFactory))]
namespace LiveSplit.ManualText {
    public class TextComponentFactory : IComponentFactory {
        public string UpdateName => ComponentName;
        public string UpdateURL => Path.Combine("https://raw.githubusercontent.com/Voxelse", Assembly.GetExecutingAssembly().GetName().Name, "main/");
        public string XMLURL => UpdateURL + "Components/ComponentsUpdate.xml";
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public string ComponentName => "Manual Text";
        public string Description => "Displays text that can be modified by code.";
        public ComponentCategory Category => ComponentCategory.Information;
        public IComponent Create(LiveSplitState state) => new ManualTextComponent(state, "Manual Text", "Manual Text");
    }
}