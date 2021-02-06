using LiveSplit.UI;

namespace LiveSplit.ManualText {
    public class FontTextComponent : UI.Components.InfoTextComponent {
        public ManualTextSettings Settings { get; set; }

        public FontTextComponent(ManualTextSettings settings): base("", "") {
            Settings = settings;
        }

        public override void PrepareDraw(Model.LiveSplitState state, LayoutMode mode) {
            NameMeasureLabel.Font = Settings.OverrideFont1 ? Settings.Font1 : state.LayoutSettings.TextFont;
            ValueLabel.Font = Settings.OverrideFont2 ? Settings.Font2 : state.LayoutSettings.TextFont;
            NameLabel.Font = Settings.OverrideFont1 ? Settings.Font1 : state.LayoutSettings.TextFont;
        }
    }
}