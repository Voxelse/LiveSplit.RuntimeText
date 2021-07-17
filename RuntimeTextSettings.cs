using LiveSplit.Model;
using LiveSplit.UI;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.RuntimeText {
    public partial class RuntimeTextSettings : UserControl {
        public Color Text1Color { get; set; }
        public bool OverrideText1Color { get; set; }
        public Color Text2Color { get; set; }
        public bool OverrideText2Color { get; set; }

        public Color BackgroundColor1 { get; set; }
        public Color BackgroundColor2 { get; set; }
        public GradientType BackgroundGradient { get; set; }
        public string GradientString {
            get => BackgroundGradient.ToString();
            set => BackgroundGradient = (GradientType)Enum.Parse(typeof(GradientType), value);
        }

        public string ComponentName { get; set; }

        public bool UseText { get; set; }

        public string Text1 { get; set; }
        public Font Font1 { get; set; }
        public string Font1String { get => String.Format("{0} {1}", Font1.FontFamily.Name, Font1.Style); }
        public bool OverrideFont1 { get; set; }

        public string Text2 { get; set; }
        public Font Font2 { get; set; }
        public string Font2String { get => String.Format("{0} {1}", Font2.FontFamily.Name, Font2.Style); }
        public bool OverrideFont2 { get; set; }

        public LayoutMode Mode { get; set; }
        public bool Display2Rows { get; set; }

        public LiveSplitState CurrentState { get; set; }

        public RuntimeTextSettings(string componentName, string name, bool isLocked) {
            InitializeComponent();

            ComponentName = componentName;
            UseText = !isLocked;
            Text1 = name;
            Text2 = "";

            Text1Color = Color.FromArgb(255, 255, 255);
            OverrideText1Color = false;
            Text2Color = Color.FromArgb(255, 255, 255);
            OverrideText2Color = false;
            BackgroundColor1 = Color.Transparent;
            BackgroundColor2 = Color.Transparent;
            BackgroundGradient = GradientType.Plain;
            OverrideFont1 = false;
            OverrideFont2 = false;
            
            Font1 = new Font("Segoe UI", 13, FontStyle.Regular, GraphicsUnit.Pixel);
            Font2 = new Font("Segoe UI", 13, FontStyle.Regular, GraphicsUnit.Pixel);

            chkOverrideText1Color.DataBindings.Add("Checked", this, "OverrideText1Color", false, DataSourceUpdateMode.OnPropertyChanged);
            chkOverrideTest2Color.DataBindings.Add("Checked", this, "OverrideText1Color", false, DataSourceUpdateMode.OnPropertyChanged);
            btnText1Color.DataBindings.Add("BackColor", this, "Text1Color", false, DataSourceUpdateMode.OnPropertyChanged);
            btnText2Color.DataBindings.Add("BackColor", this, "Text2Color", false, DataSourceUpdateMode.OnPropertyChanged);
            lblFont.DataBindings.Add("Text", this, "Font1String", false, DataSourceUpdateMode.OnPropertyChanged);
            lblFont2.DataBindings.Add("Text", this, "Font2String", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor1.DataBindings.Add("BackColor", this, "BackgroundColor1", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor2.DataBindings.Add("BackColor", this, "BackgroundColor2", false, DataSourceUpdateMode.OnPropertyChanged);
            txtOne.DataBindings.Add("Text", this, "Text1");
            txtOne.DataBindings.Add("Enabled", this, "UseText", false, DataSourceUpdateMode.OnPropertyChanged);
            chkFont1.DataBindings.Add("Checked", this, "OverrideFont1", false, DataSourceUpdateMode.OnPropertyChanged);
            chkFont2.DataBindings.Add("Checked", this, "OverrideFont2", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbGradientType.SelectedIndexChanged += CmbGradientType_SelectedIndexChanged;
            cmbGradientType.DataBindings.Add("SelectedItem", this, "GradientString", false, DataSourceUpdateMode.OnPropertyChanged);
            
        }

        void ChkFont2_CheckedChanged(object sender, EventArgs e) {
            label7.Enabled = lblFont2.Enabled = btnFont2.Enabled = chkFont2.Checked;
        }

        void ChkFont_CheckedChanged(object sender, EventArgs e) {
            label5.Enabled = lblFont.Enabled = btnFont.Enabled = chkFont1.Checked;
        }

        void ChkOverrideTimeColor_CheckedChanged(object sender, EventArgs e) {
            label2.Enabled = btnText2Color.Enabled = chkOverrideTest2Color.Checked;
        }

        void ChkOverrideTextColor_CheckedChanged(object sender, EventArgs e) {
            label1.Enabled = btnText1Color.Enabled = chkOverrideText1Color.Checked;
        }

        void TextComponentSettings_Load(object sender, EventArgs e) {
            ChkOverrideTextColor_CheckedChanged(null, null);
            ChkOverrideTimeColor_CheckedChanged(null, null);
            ChkFont_CheckedChanged(null, null);
            ChkFont2_CheckedChanged(null, null);
            if(Mode == LayoutMode.Horizontal) {
                chkTwoRows.Enabled = false;
                chkTwoRows.DataBindings.Clear();
                chkTwoRows.Checked = true;
            } else {
                chkTwoRows.Enabled = true;
                chkTwoRows.DataBindings.Clear();
                chkTwoRows.DataBindings.Add("Checked", this, "Display2Rows", false, DataSourceUpdateMode.OnPropertyChanged);
            }
        }

        void CmbGradientType_SelectedIndexChanged(object sender, EventArgs e) {
            btnColor1.Visible = cmbGradientType.SelectedItem.ToString() != "Plain";
            btnColor2.DataBindings.Clear();
            btnColor2.DataBindings.Add("BackColor", this, btnColor1.Visible ? "BackgroundColor2" : "BackgroundColor1", false, DataSourceUpdateMode.OnPropertyChanged);
            GradientString = cmbGradientType.SelectedItem.ToString();
        }

        public void SetSettings(XmlNode node) {
            var element = (XmlElement)node;
            Text1 = SettingsHelper.ParseString(element["Text1"]);
            Text2 = SettingsHelper.ParseString(element["Text2"]);
            ComponentName = SettingsHelper.ParseString(element["ComponentName"]);
            Text1Color = SettingsHelper.ParseColor(element["Text1Color"]);
            Text2Color = SettingsHelper.ParseColor(element["Text2Color"]);
            OverrideText1Color = SettingsHelper.ParseBool(element["OverrideText1Color"]);
            OverrideText2Color = SettingsHelper.ParseBool(element["OverrideText2Color"]);
            BackgroundColor1 = SettingsHelper.ParseColor(element["BackgroundColor1"]);
            BackgroundColor2 = SettingsHelper.ParseColor(element["BackgroundColor2"]);
            GradientString = SettingsHelper.ParseString(element["BackgroundGradient"]);
            UseText = SettingsHelper.ParseBool(element["UseText"]);
            Font1 = SettingsHelper.GetFontFromElement(element["Font1"]);
            Font2 = SettingsHelper.GetFontFromElement(element["Font2"]);
            OverrideFont1 = SettingsHelper.ParseBool(element["OverrideFont1"]);
            OverrideFont2 = SettingsHelper.ParseBool(element["OverrideFont2"]);
            Display2Rows = SettingsHelper.ParseBool(element["Display2Rows"], false);
        }

        public XmlNode GetSettings(XmlDocument document) {
            var parent = document.CreateElement("Settings");
            SettingsHelper.CreateSetting(document, parent, "Text1", Text1);
            SettingsHelper.CreateSetting(document, parent, "Text2", Text2);
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode() => CreateSettingsNode(null, null);

        private int CreateSettingsNode(XmlDocument document, XmlElement parent) {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.0.1") ^
            SettingsHelper.CreateSetting(document, parent, "ComponentName", ComponentName) ^
            SettingsHelper.CreateSetting(document, parent, "TextColor", Text1Color) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideTextColor", OverrideText1Color) ^
            SettingsHelper.CreateSetting(document, parent, "TimeColor", Text2Color) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideTimeColor", OverrideText2Color) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundColor", BackgroundColor1) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundColor2", BackgroundColor2) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundGradient", BackgroundGradient) ^
            SettingsHelper.CreateSetting(document, parent, "UseText", UseText) ^
            SettingsHelper.CreateSetting(document, parent, "Font1", Font1) ^
            SettingsHelper.CreateSetting(document, parent, "Font2", Font2) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideFont1", OverrideFont1) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideFont2", OverrideFont2) ^
            SettingsHelper.CreateSetting(document, parent, "Display2Rows", Display2Rows);
        }

        private void ColorButtonClick(object sender, EventArgs e) {
            SettingsHelper.ColorButtonClick((Button)sender, this);
        }

        private void BtnFont1_Click(object sender, EventArgs e) {
            var dialog = SettingsHelper.GetFontDialog(Font1, 7, 20);
            dialog.FontChanged += (s, ev) => Font1 = ((CustomFontDialog.FontChangedEventArgs)ev).NewFont;
            dialog.ShowDialog(this);
            lblFont.Text = Font1String;
        }

        private void BtnFont2_Click(object sender, EventArgs e) {
            var dialog = SettingsHelper.GetFontDialog(Font2, 7, 20);
            dialog.FontChanged += (s, ev) => Font2 = ((CustomFontDialog.FontChangedEventArgs)ev).NewFont;
            dialog.ShowDialog(this);
            lblFont.Text = Font2String;
        }
    }
}