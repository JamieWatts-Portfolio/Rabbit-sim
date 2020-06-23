// Internal view logic generated from "ButtonsExample.xml"
#region Using Statements
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace Delight
{
    public partial class ButtonsExample : LayoutRoot
    {
        #region Constructors

        public ButtonsExample(View parent, View layoutParent = null, string id = null, Template template = null, bool deferInitialization = false) :
            base(parent, layoutParent, id, template ?? ButtonsExampleTemplates.Default, deferInitialization)
        {
            if (deferInitialization)
                return;

            // constructing Group (Group1)
            Group1 = new Group(this, this, "Group1", Group1Template);
            Group2 = new Group(this, Group1.Content, "Group2", Group2Template);
            Group3 = new Group(this, Group2.Content, "Group3", Group3Template);
            CheckBox1 = new CheckBox(this, Group3.Content, "CheckBox1", CheckBox1Template);
            CheckBox2 = new CheckBox(this, Group3.Content, "CheckBox2", CheckBox2Template);
            CheckBox3 = new CheckBox(this, Group3.Content, "CheckBox3", CheckBox3Template);
            Group4 = new Group(this, Group2.Content, "Group4", Group4Template);
            RadioButton1 = new RadioButton(this, Group4.Content, "RadioButton1", RadioButton1Template);
            RadioButton2 = new RadioButton(this, Group4.Content, "RadioButton2", RadioButton2Template);
            RadioButton3 = new RadioButton(this, Group4.Content, "RadioButton3", RadioButton3Template);
            Group5 = new Group(this, Group1.Content, "Group5", Group5Template);
            Button1 = new Button(this, Group5.Content, "Button1", Button1Template);
            Button1.Click.RegisterHandler(this, "ButtonClick");
            Button2 = new Button(this, Group5.Content, "Button2", Button2Template);
            Label1 = new Label(this, Button2.Content, "Label1", Label1Template);
            Image1 = new Image(this, Button2.Content, "Image1", Image1Template);
            Label2 = new Label(this, Group1.Content, "Label2", Label2Template);

            // binding <Label Text="Click count: {ClickCount}">
            Bindings.Add(new Binding(new List<BindingPath> { new BindingPath(new List<string> { "ClickCount" }, new List<Func<BindableObject>> { () => this }) }, new BindingPath(new List<string> { "Label2", "Text" }, new List<Func<BindableObject>> { () => this, () => Label2 }), () => Label2.Text = String.Format("Click count: {0}", ClickCount), () => { }, false));
            ComboBox = new ComboBox(this, Group1.Content, "ComboBox", ComboBoxTemplate);
            ComboBox.ItemSelected.RegisterHandler(this, "ItemSelected");

            // binding <ComboBox Items="{player in @Players}">
            Bindings.Add(new Binding(new List<BindingPath> { new BindingPath(new List<string> {  }, new List<Func<BindableObject>> {  }) }, new BindingPath(new List<string> { "ComboBox", "Items" }, new List<Func<BindableObject>> { () => this, () => ComboBox }), () => ComboBox.Items = Models.Players, () => { }, false));

            // templates for ComboBox
            ComboBox.ContentTemplates.Add(new ContentTemplate(tiPlayer => 
            {
                var comboBoxContent = new ComboBoxListItem(this, ComboBox.Content, "ComboBoxContent", ComboBoxContentTemplate);
                var label3 = new Label(this, comboBoxContent.Content, "Label3", Label3Template);

                // binding <Label Text="{player.Name}">
                comboBoxContent.Bindings.Add(new Binding(new List<BindingPath> { new BindingPath(new List<string> { "Item", "Name" }, new List<Func<BindableObject>> { () => tiPlayer, () => (tiPlayer.Item as Delight.Player) }) }, new BindingPath(new List<string> { "Text" }, new List<Func<BindableObject>> { () => label3 }), () => label3.Text = (tiPlayer.Item as Delight.Player).Name, () => { }, false));
                comboBoxContent.IsDynamic = true;
                comboBoxContent.SetContentTemplateData(tiPlayer);
                return comboBoxContent;
            }, typeof(ComboBoxListItem), "ComboBoxContent"));
            this.AfterInitializeInternal();
        }

        public ButtonsExample() : this(null)
        {
        }

        static ButtonsExample()
        {
            var dependencyProperties = new List<DependencyProperty>();
            DependencyProperties.Add(ButtonsExampleTemplates.Default, dependencyProperties);

            dependencyProperties.Add(ClickCountProperty);
            dependencyProperties.Add(Group1Property);
            dependencyProperties.Add(Group1TemplateProperty);
            dependencyProperties.Add(Group2Property);
            dependencyProperties.Add(Group2TemplateProperty);
            dependencyProperties.Add(Group3Property);
            dependencyProperties.Add(Group3TemplateProperty);
            dependencyProperties.Add(CheckBox1Property);
            dependencyProperties.Add(CheckBox1TemplateProperty);
            dependencyProperties.Add(CheckBox2Property);
            dependencyProperties.Add(CheckBox2TemplateProperty);
            dependencyProperties.Add(CheckBox3Property);
            dependencyProperties.Add(CheckBox3TemplateProperty);
            dependencyProperties.Add(Group4Property);
            dependencyProperties.Add(Group4TemplateProperty);
            dependencyProperties.Add(RadioButton1Property);
            dependencyProperties.Add(RadioButton1TemplateProperty);
            dependencyProperties.Add(RadioButton2Property);
            dependencyProperties.Add(RadioButton2TemplateProperty);
            dependencyProperties.Add(RadioButton3Property);
            dependencyProperties.Add(RadioButton3TemplateProperty);
            dependencyProperties.Add(Group5Property);
            dependencyProperties.Add(Group5TemplateProperty);
            dependencyProperties.Add(Button1Property);
            dependencyProperties.Add(Button1TemplateProperty);
            dependencyProperties.Add(Button2Property);
            dependencyProperties.Add(Button2TemplateProperty);
            dependencyProperties.Add(Label1Property);
            dependencyProperties.Add(Label1TemplateProperty);
            dependencyProperties.Add(Image1Property);
            dependencyProperties.Add(Image1TemplateProperty);
            dependencyProperties.Add(Label2Property);
            dependencyProperties.Add(Label2TemplateProperty);
            dependencyProperties.Add(ComboBoxProperty);
            dependencyProperties.Add(ComboBoxTemplateProperty);
            dependencyProperties.Add(Label3Property);
            dependencyProperties.Add(Label3TemplateProperty);
            dependencyProperties.Add(ComboBoxContentProperty);
            dependencyProperties.Add(ComboBoxContentTemplateProperty);
        }

        #endregion

        #region Properties

        public readonly static DependencyProperty<System.Int32> ClickCountProperty = new DependencyProperty<System.Int32>("ClickCount");
        public System.Int32 ClickCount
        {
            get { return ClickCountProperty.GetValue(this); }
            set { ClickCountProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Group> Group1Property = new DependencyProperty<Group>("Group1");
        public Group Group1
        {
            get { return Group1Property.GetValue(this); }
            set { Group1Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> Group1TemplateProperty = new DependencyProperty<Template>("Group1Template");
        public Template Group1Template
        {
            get { return Group1TemplateProperty.GetValue(this); }
            set { Group1TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Group> Group2Property = new DependencyProperty<Group>("Group2");
        public Group Group2
        {
            get { return Group2Property.GetValue(this); }
            set { Group2Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> Group2TemplateProperty = new DependencyProperty<Template>("Group2Template");
        public Template Group2Template
        {
            get { return Group2TemplateProperty.GetValue(this); }
            set { Group2TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Group> Group3Property = new DependencyProperty<Group>("Group3");
        public Group Group3
        {
            get { return Group3Property.GetValue(this); }
            set { Group3Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> Group3TemplateProperty = new DependencyProperty<Template>("Group3Template");
        public Template Group3Template
        {
            get { return Group3TemplateProperty.GetValue(this); }
            set { Group3TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<CheckBox> CheckBox1Property = new DependencyProperty<CheckBox>("CheckBox1");
        public CheckBox CheckBox1
        {
            get { return CheckBox1Property.GetValue(this); }
            set { CheckBox1Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> CheckBox1TemplateProperty = new DependencyProperty<Template>("CheckBox1Template");
        public Template CheckBox1Template
        {
            get { return CheckBox1TemplateProperty.GetValue(this); }
            set { CheckBox1TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<CheckBox> CheckBox2Property = new DependencyProperty<CheckBox>("CheckBox2");
        public CheckBox CheckBox2
        {
            get { return CheckBox2Property.GetValue(this); }
            set { CheckBox2Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> CheckBox2TemplateProperty = new DependencyProperty<Template>("CheckBox2Template");
        public Template CheckBox2Template
        {
            get { return CheckBox2TemplateProperty.GetValue(this); }
            set { CheckBox2TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<CheckBox> CheckBox3Property = new DependencyProperty<CheckBox>("CheckBox3");
        public CheckBox CheckBox3
        {
            get { return CheckBox3Property.GetValue(this); }
            set { CheckBox3Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> CheckBox3TemplateProperty = new DependencyProperty<Template>("CheckBox3Template");
        public Template CheckBox3Template
        {
            get { return CheckBox3TemplateProperty.GetValue(this); }
            set { CheckBox3TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Group> Group4Property = new DependencyProperty<Group>("Group4");
        public Group Group4
        {
            get { return Group4Property.GetValue(this); }
            set { Group4Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> Group4TemplateProperty = new DependencyProperty<Template>("Group4Template");
        public Template Group4Template
        {
            get { return Group4TemplateProperty.GetValue(this); }
            set { Group4TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<RadioButton> RadioButton1Property = new DependencyProperty<RadioButton>("RadioButton1");
        public RadioButton RadioButton1
        {
            get { return RadioButton1Property.GetValue(this); }
            set { RadioButton1Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> RadioButton1TemplateProperty = new DependencyProperty<Template>("RadioButton1Template");
        public Template RadioButton1Template
        {
            get { return RadioButton1TemplateProperty.GetValue(this); }
            set { RadioButton1TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<RadioButton> RadioButton2Property = new DependencyProperty<RadioButton>("RadioButton2");
        public RadioButton RadioButton2
        {
            get { return RadioButton2Property.GetValue(this); }
            set { RadioButton2Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> RadioButton2TemplateProperty = new DependencyProperty<Template>("RadioButton2Template");
        public Template RadioButton2Template
        {
            get { return RadioButton2TemplateProperty.GetValue(this); }
            set { RadioButton2TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<RadioButton> RadioButton3Property = new DependencyProperty<RadioButton>("RadioButton3");
        public RadioButton RadioButton3
        {
            get { return RadioButton3Property.GetValue(this); }
            set { RadioButton3Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> RadioButton3TemplateProperty = new DependencyProperty<Template>("RadioButton3Template");
        public Template RadioButton3Template
        {
            get { return RadioButton3TemplateProperty.GetValue(this); }
            set { RadioButton3TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Group> Group5Property = new DependencyProperty<Group>("Group5");
        public Group Group5
        {
            get { return Group5Property.GetValue(this); }
            set { Group5Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> Group5TemplateProperty = new DependencyProperty<Template>("Group5Template");
        public Template Group5Template
        {
            get { return Group5TemplateProperty.GetValue(this); }
            set { Group5TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Button> Button1Property = new DependencyProperty<Button>("Button1");
        public Button Button1
        {
            get { return Button1Property.GetValue(this); }
            set { Button1Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> Button1TemplateProperty = new DependencyProperty<Template>("Button1Template");
        public Template Button1Template
        {
            get { return Button1TemplateProperty.GetValue(this); }
            set { Button1TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Button> Button2Property = new DependencyProperty<Button>("Button2");
        public Button Button2
        {
            get { return Button2Property.GetValue(this); }
            set { Button2Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> Button2TemplateProperty = new DependencyProperty<Template>("Button2Template");
        public Template Button2Template
        {
            get { return Button2TemplateProperty.GetValue(this); }
            set { Button2TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Label> Label1Property = new DependencyProperty<Label>("Label1");
        public Label Label1
        {
            get { return Label1Property.GetValue(this); }
            set { Label1Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> Label1TemplateProperty = new DependencyProperty<Template>("Label1Template");
        public Template Label1Template
        {
            get { return Label1TemplateProperty.GetValue(this); }
            set { Label1TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Image> Image1Property = new DependencyProperty<Image>("Image1");
        public Image Image1
        {
            get { return Image1Property.GetValue(this); }
            set { Image1Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> Image1TemplateProperty = new DependencyProperty<Template>("Image1Template");
        public Template Image1Template
        {
            get { return Image1TemplateProperty.GetValue(this); }
            set { Image1TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Label> Label2Property = new DependencyProperty<Label>("Label2");
        public Label Label2
        {
            get { return Label2Property.GetValue(this); }
            set { Label2Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> Label2TemplateProperty = new DependencyProperty<Template>("Label2Template");
        public Template Label2Template
        {
            get { return Label2TemplateProperty.GetValue(this); }
            set { Label2TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<ComboBox> ComboBoxProperty = new DependencyProperty<ComboBox>("ComboBox");
        public ComboBox ComboBox
        {
            get { return ComboBoxProperty.GetValue(this); }
            set { ComboBoxProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> ComboBoxTemplateProperty = new DependencyProperty<Template>("ComboBoxTemplate");
        public Template ComboBoxTemplate
        {
            get { return ComboBoxTemplateProperty.GetValue(this); }
            set { ComboBoxTemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Label> Label3Property = new DependencyProperty<Label>("Label3");
        public Label Label3
        {
            get { return Label3Property.GetValue(this); }
            set { Label3Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> Label3TemplateProperty = new DependencyProperty<Template>("Label3Template");
        public Template Label3Template
        {
            get { return Label3TemplateProperty.GetValue(this); }
            set { Label3TemplateProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<ComboBoxListItem> ComboBoxContentProperty = new DependencyProperty<ComboBoxListItem>("ComboBoxContent");
        public ComboBoxListItem ComboBoxContent
        {
            get { return ComboBoxContentProperty.GetValue(this); }
            set { ComboBoxContentProperty.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> ComboBoxContentTemplateProperty = new DependencyProperty<Template>("ComboBoxContentTemplate");
        public Template ComboBoxContentTemplate
        {
            get { return ComboBoxContentTemplateProperty.GetValue(this); }
            set { ComboBoxContentTemplateProperty.SetValue(this, value); }
        }

        #endregion
    }

    #region Data Templates

    public static class ButtonsExampleTemplates
    {
        #region Properties

        public static Template Default
        {
            get
            {
                return ButtonsExample;
            }
        }

        private static Template _buttonsExample;
        public static Template ButtonsExample
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExample == null || _buttonsExample.CurrentVersion != Template.Version)
#else
                if (_buttonsExample == null)
#endif
                {
                    _buttonsExample = new Template(LayoutRootTemplates.LayoutRoot);
#if UNITY_EDITOR
                    _buttonsExample.Name = "ButtonsExample";
#endif
                    Delight.ButtonsExample.Group1TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleGroup1);
                    Delight.ButtonsExample.Group2TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleGroup2);
                    Delight.ButtonsExample.Group3TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleGroup3);
                    Delight.ButtonsExample.CheckBox1TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleCheckBox1);
                    Delight.ButtonsExample.CheckBox2TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleCheckBox2);
                    Delight.ButtonsExample.CheckBox3TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleCheckBox3);
                    Delight.ButtonsExample.Group4TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleGroup4);
                    Delight.ButtonsExample.RadioButton1TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleRadioButton1);
                    Delight.ButtonsExample.RadioButton2TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleRadioButton2);
                    Delight.ButtonsExample.RadioButton3TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleRadioButton3);
                    Delight.ButtonsExample.Group5TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleGroup5);
                    Delight.ButtonsExample.Button1TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleButton1);
                    Delight.ButtonsExample.Button2TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleButton2);
                    Delight.ButtonsExample.Label1TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleLabel1);
                    Delight.ButtonsExample.Image1TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleImage1);
                    Delight.ButtonsExample.Label2TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleLabel2);
                    Delight.ButtonsExample.ComboBoxTemplateProperty.SetDefault(_buttonsExample, ButtonsExampleComboBox);
                    Delight.ButtonsExample.ComboBoxContentTemplateProperty.SetDefault(_buttonsExample, ButtonsExampleComboBoxContent);
                    Delight.ButtonsExample.Label3TemplateProperty.SetDefault(_buttonsExample, ButtonsExampleLabel3);
                }
                return _buttonsExample;
            }
        }

        private static Template _buttonsExampleGroup1;
        public static Template ButtonsExampleGroup1
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleGroup1 == null || _buttonsExampleGroup1.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleGroup1 == null)
#endif
                {
                    _buttonsExampleGroup1 = new Template(GroupTemplates.Group);
#if UNITY_EDITOR
                    _buttonsExampleGroup1.Name = "ButtonsExampleGroup1";
#endif
                    Delight.Group.OrientationProperty.SetDefault(_buttonsExampleGroup1, Delight.ElementOrientation.Vertical);
                    Delight.Group.SpacingProperty.SetDefault(_buttonsExampleGroup1, new ElementSize(10f, ElementSizeUnit.Pixels));
                    Delight.Group.ContentAlignmentProperty.SetDefault(_buttonsExampleGroup1, Delight.ElementAlignment.Left);
                }
                return _buttonsExampleGroup1;
            }
        }

        private static Template _buttonsExampleGroup2;
        public static Template ButtonsExampleGroup2
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleGroup2 == null || _buttonsExampleGroup2.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleGroup2 == null)
#endif
                {
                    _buttonsExampleGroup2 = new Template(GroupTemplates.Group);
#if UNITY_EDITOR
                    _buttonsExampleGroup2.Name = "ButtonsExampleGroup2";
#endif
                    Delight.Group.OrientationProperty.SetDefault(_buttonsExampleGroup2, Delight.ElementOrientation.Horizontal);
                    Delight.Group.SpacingProperty.SetDefault(_buttonsExampleGroup2, new ElementSize(50f, ElementSizeUnit.Pixels));
                }
                return _buttonsExampleGroup2;
            }
        }

        private static Template _buttonsExampleGroup3;
        public static Template ButtonsExampleGroup3
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleGroup3 == null || _buttonsExampleGroup3.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleGroup3 == null)
#endif
                {
                    _buttonsExampleGroup3 = new Template(GroupTemplates.Group);
#if UNITY_EDITOR
                    _buttonsExampleGroup3.Name = "ButtonsExampleGroup3";
#endif
                    Delight.Group.SpacingProperty.SetDefault(_buttonsExampleGroup3, new ElementSize(10f, ElementSizeUnit.Pixels));
                    Delight.Group.ContentAlignmentProperty.SetDefault(_buttonsExampleGroup3, Delight.ElementAlignment.Left);
                }
                return _buttonsExampleGroup3;
            }
        }

        private static Template _buttonsExampleCheckBox1;
        public static Template ButtonsExampleCheckBox1
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox1 == null || _buttonsExampleCheckBox1.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox1 == null)
#endif
                {
                    _buttonsExampleCheckBox1 = new Template(CheckBoxTemplates.CheckBox);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox1.Name = "ButtonsExampleCheckBox1";
#endif
                    Delight.CheckBox.CheckBoxGroupTemplateProperty.SetDefault(_buttonsExampleCheckBox1, ButtonsExampleCheckBox1CheckBoxGroup);
                    Delight.CheckBox.CheckBoxImageViewTemplateProperty.SetDefault(_buttonsExampleCheckBox1, ButtonsExampleCheckBox1CheckBoxImageView);
                    Delight.CheckBox.CheckBoxLabelTemplateProperty.SetDefault(_buttonsExampleCheckBox1, ButtonsExampleCheckBox1CheckBoxLabel);
                }
                return _buttonsExampleCheckBox1;
            }
        }

        private static Template _buttonsExampleCheckBox1CheckBoxGroup;
        public static Template ButtonsExampleCheckBox1CheckBoxGroup
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox1CheckBoxGroup == null || _buttonsExampleCheckBox1CheckBoxGroup.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox1CheckBoxGroup == null)
#endif
                {
                    _buttonsExampleCheckBox1CheckBoxGroup = new Template(CheckBoxTemplates.CheckBoxCheckBoxGroup);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox1CheckBoxGroup.Name = "ButtonsExampleCheckBox1CheckBoxGroup";
#endif
                }
                return _buttonsExampleCheckBox1CheckBoxGroup;
            }
        }

        private static Template _buttonsExampleCheckBox1CheckBoxImageView;
        public static Template ButtonsExampleCheckBox1CheckBoxImageView
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox1CheckBoxImageView == null || _buttonsExampleCheckBox1CheckBoxImageView.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox1CheckBoxImageView == null)
#endif
                {
                    _buttonsExampleCheckBox1CheckBoxImageView = new Template(CheckBoxTemplates.CheckBoxCheckBoxImageView);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox1CheckBoxImageView.Name = "ButtonsExampleCheckBox1CheckBoxImageView";
#endif
                }
                return _buttonsExampleCheckBox1CheckBoxImageView;
            }
        }

        private static Template _buttonsExampleCheckBox1CheckBoxLabel;
        public static Template ButtonsExampleCheckBox1CheckBoxLabel
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox1CheckBoxLabel == null || _buttonsExampleCheckBox1CheckBoxLabel.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox1CheckBoxLabel == null)
#endif
                {
                    _buttonsExampleCheckBox1CheckBoxLabel = new Template(CheckBoxTemplates.CheckBoxCheckBoxLabel);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox1CheckBoxLabel.Name = "ButtonsExampleCheckBox1CheckBoxLabel";
#endif
                    Delight.Label.TextProperty.SetDefault(_buttonsExampleCheckBox1CheckBoxLabel, "Option 1");
                }
                return _buttonsExampleCheckBox1CheckBoxLabel;
            }
        }

        private static Template _buttonsExampleCheckBox2;
        public static Template ButtonsExampleCheckBox2
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox2 == null || _buttonsExampleCheckBox2.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox2 == null)
#endif
                {
                    _buttonsExampleCheckBox2 = new Template(CheckBoxTemplates.CheckBox);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox2.Name = "ButtonsExampleCheckBox2";
#endif
                    Delight.CheckBox.CheckBoxGroupTemplateProperty.SetDefault(_buttonsExampleCheckBox2, ButtonsExampleCheckBox2CheckBoxGroup);
                    Delight.CheckBox.CheckBoxImageViewTemplateProperty.SetDefault(_buttonsExampleCheckBox2, ButtonsExampleCheckBox2CheckBoxImageView);
                    Delight.CheckBox.CheckBoxLabelTemplateProperty.SetDefault(_buttonsExampleCheckBox2, ButtonsExampleCheckBox2CheckBoxLabel);
                }
                return _buttonsExampleCheckBox2;
            }
        }

        private static Template _buttonsExampleCheckBox2CheckBoxGroup;
        public static Template ButtonsExampleCheckBox2CheckBoxGroup
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox2CheckBoxGroup == null || _buttonsExampleCheckBox2CheckBoxGroup.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox2CheckBoxGroup == null)
#endif
                {
                    _buttonsExampleCheckBox2CheckBoxGroup = new Template(CheckBoxTemplates.CheckBoxCheckBoxGroup);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox2CheckBoxGroup.Name = "ButtonsExampleCheckBox2CheckBoxGroup";
#endif
                }
                return _buttonsExampleCheckBox2CheckBoxGroup;
            }
        }

        private static Template _buttonsExampleCheckBox2CheckBoxImageView;
        public static Template ButtonsExampleCheckBox2CheckBoxImageView
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox2CheckBoxImageView == null || _buttonsExampleCheckBox2CheckBoxImageView.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox2CheckBoxImageView == null)
#endif
                {
                    _buttonsExampleCheckBox2CheckBoxImageView = new Template(CheckBoxTemplates.CheckBoxCheckBoxImageView);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox2CheckBoxImageView.Name = "ButtonsExampleCheckBox2CheckBoxImageView";
#endif
                }
                return _buttonsExampleCheckBox2CheckBoxImageView;
            }
        }

        private static Template _buttonsExampleCheckBox2CheckBoxLabel;
        public static Template ButtonsExampleCheckBox2CheckBoxLabel
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox2CheckBoxLabel == null || _buttonsExampleCheckBox2CheckBoxLabel.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox2CheckBoxLabel == null)
#endif
                {
                    _buttonsExampleCheckBox2CheckBoxLabel = new Template(CheckBoxTemplates.CheckBoxCheckBoxLabel);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox2CheckBoxLabel.Name = "ButtonsExampleCheckBox2CheckBoxLabel";
#endif
                    Delight.Label.TextProperty.SetDefault(_buttonsExampleCheckBox2CheckBoxLabel, "Option 2");
                }
                return _buttonsExampleCheckBox2CheckBoxLabel;
            }
        }

        private static Template _buttonsExampleCheckBox3;
        public static Template ButtonsExampleCheckBox3
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox3 == null || _buttonsExampleCheckBox3.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox3 == null)
#endif
                {
                    _buttonsExampleCheckBox3 = new Template(CheckBoxTemplates.CheckBox);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox3.Name = "ButtonsExampleCheckBox3";
#endif
                    Delight.CheckBox.CheckBoxGroupTemplateProperty.SetDefault(_buttonsExampleCheckBox3, ButtonsExampleCheckBox3CheckBoxGroup);
                    Delight.CheckBox.CheckBoxImageViewTemplateProperty.SetDefault(_buttonsExampleCheckBox3, ButtonsExampleCheckBox3CheckBoxImageView);
                    Delight.CheckBox.CheckBoxLabelTemplateProperty.SetDefault(_buttonsExampleCheckBox3, ButtonsExampleCheckBox3CheckBoxLabel);
                }
                return _buttonsExampleCheckBox3;
            }
        }

        private static Template _buttonsExampleCheckBox3CheckBoxGroup;
        public static Template ButtonsExampleCheckBox3CheckBoxGroup
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox3CheckBoxGroup == null || _buttonsExampleCheckBox3CheckBoxGroup.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox3CheckBoxGroup == null)
#endif
                {
                    _buttonsExampleCheckBox3CheckBoxGroup = new Template(CheckBoxTemplates.CheckBoxCheckBoxGroup);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox3CheckBoxGroup.Name = "ButtonsExampleCheckBox3CheckBoxGroup";
#endif
                }
                return _buttonsExampleCheckBox3CheckBoxGroup;
            }
        }

        private static Template _buttonsExampleCheckBox3CheckBoxImageView;
        public static Template ButtonsExampleCheckBox3CheckBoxImageView
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox3CheckBoxImageView == null || _buttonsExampleCheckBox3CheckBoxImageView.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox3CheckBoxImageView == null)
#endif
                {
                    _buttonsExampleCheckBox3CheckBoxImageView = new Template(CheckBoxTemplates.CheckBoxCheckBoxImageView);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox3CheckBoxImageView.Name = "ButtonsExampleCheckBox3CheckBoxImageView";
#endif
                }
                return _buttonsExampleCheckBox3CheckBoxImageView;
            }
        }

        private static Template _buttonsExampleCheckBox3CheckBoxLabel;
        public static Template ButtonsExampleCheckBox3CheckBoxLabel
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleCheckBox3CheckBoxLabel == null || _buttonsExampleCheckBox3CheckBoxLabel.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleCheckBox3CheckBoxLabel == null)
#endif
                {
                    _buttonsExampleCheckBox3CheckBoxLabel = new Template(CheckBoxTemplates.CheckBoxCheckBoxLabel);
#if UNITY_EDITOR
                    _buttonsExampleCheckBox3CheckBoxLabel.Name = "ButtonsExampleCheckBox3CheckBoxLabel";
#endif
                    Delight.Label.TextProperty.SetDefault(_buttonsExampleCheckBox3CheckBoxLabel, "Option 3");
                }
                return _buttonsExampleCheckBox3CheckBoxLabel;
            }
        }

        private static Template _buttonsExampleGroup4;
        public static Template ButtonsExampleGroup4
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleGroup4 == null || _buttonsExampleGroup4.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleGroup4 == null)
#endif
                {
                    _buttonsExampleGroup4 = new Template(GroupTemplates.Group);
#if UNITY_EDITOR
                    _buttonsExampleGroup4.Name = "ButtonsExampleGroup4";
#endif
                    Delight.Group.SpacingProperty.SetDefault(_buttonsExampleGroup4, new ElementSize(10f, ElementSizeUnit.Pixels));
                    Delight.Group.ContentAlignmentProperty.SetDefault(_buttonsExampleGroup4, Delight.ElementAlignment.Left);
                }
                return _buttonsExampleGroup4;
            }
        }

        private static Template _buttonsExampleRadioButton1;
        public static Template ButtonsExampleRadioButton1
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton1 == null || _buttonsExampleRadioButton1.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton1 == null)
#endif
                {
                    _buttonsExampleRadioButton1 = new Template(RadioButtonTemplates.RadioButton);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton1.Name = "ButtonsExampleRadioButton1";
#endif
                    Delight.RadioButton.RadioButtonGroupTemplateProperty.SetDefault(_buttonsExampleRadioButton1, ButtonsExampleRadioButton1RadioButtonGroup);
                    Delight.RadioButton.RadioButtonImageViewTemplateProperty.SetDefault(_buttonsExampleRadioButton1, ButtonsExampleRadioButton1RadioButtonImageView);
                    Delight.RadioButton.RadioButtonLabelTemplateProperty.SetDefault(_buttonsExampleRadioButton1, ButtonsExampleRadioButton1RadioButtonLabel);
                }
                return _buttonsExampleRadioButton1;
            }
        }

        private static Template _buttonsExampleRadioButton1RadioButtonGroup;
        public static Template ButtonsExampleRadioButton1RadioButtonGroup
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton1RadioButtonGroup == null || _buttonsExampleRadioButton1RadioButtonGroup.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton1RadioButtonGroup == null)
#endif
                {
                    _buttonsExampleRadioButton1RadioButtonGroup = new Template(RadioButtonTemplates.RadioButtonRadioButtonGroup);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton1RadioButtonGroup.Name = "ButtonsExampleRadioButton1RadioButtonGroup";
#endif
                }
                return _buttonsExampleRadioButton1RadioButtonGroup;
            }
        }

        private static Template _buttonsExampleRadioButton1RadioButtonImageView;
        public static Template ButtonsExampleRadioButton1RadioButtonImageView
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton1RadioButtonImageView == null || _buttonsExampleRadioButton1RadioButtonImageView.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton1RadioButtonImageView == null)
#endif
                {
                    _buttonsExampleRadioButton1RadioButtonImageView = new Template(RadioButtonTemplates.RadioButtonRadioButtonImageView);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton1RadioButtonImageView.Name = "ButtonsExampleRadioButton1RadioButtonImageView";
#endif
                }
                return _buttonsExampleRadioButton1RadioButtonImageView;
            }
        }

        private static Template _buttonsExampleRadioButton1RadioButtonLabel;
        public static Template ButtonsExampleRadioButton1RadioButtonLabel
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton1RadioButtonLabel == null || _buttonsExampleRadioButton1RadioButtonLabel.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton1RadioButtonLabel == null)
#endif
                {
                    _buttonsExampleRadioButton1RadioButtonLabel = new Template(RadioButtonTemplates.RadioButtonRadioButtonLabel);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton1RadioButtonLabel.Name = "ButtonsExampleRadioButton1RadioButtonLabel";
#endif
                    Delight.Label.TextProperty.SetDefault(_buttonsExampleRadioButton1RadioButtonLabel, "Option 1");
                }
                return _buttonsExampleRadioButton1RadioButtonLabel;
            }
        }

        private static Template _buttonsExampleRadioButton2;
        public static Template ButtonsExampleRadioButton2
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton2 == null || _buttonsExampleRadioButton2.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton2 == null)
#endif
                {
                    _buttonsExampleRadioButton2 = new Template(RadioButtonTemplates.RadioButton);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton2.Name = "ButtonsExampleRadioButton2";
#endif
                    Delight.RadioButton.RadioButtonGroupTemplateProperty.SetDefault(_buttonsExampleRadioButton2, ButtonsExampleRadioButton2RadioButtonGroup);
                    Delight.RadioButton.RadioButtonImageViewTemplateProperty.SetDefault(_buttonsExampleRadioButton2, ButtonsExampleRadioButton2RadioButtonImageView);
                    Delight.RadioButton.RadioButtonLabelTemplateProperty.SetDefault(_buttonsExampleRadioButton2, ButtonsExampleRadioButton2RadioButtonLabel);
                }
                return _buttonsExampleRadioButton2;
            }
        }

        private static Template _buttonsExampleRadioButton2RadioButtonGroup;
        public static Template ButtonsExampleRadioButton2RadioButtonGroup
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton2RadioButtonGroup == null || _buttonsExampleRadioButton2RadioButtonGroup.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton2RadioButtonGroup == null)
#endif
                {
                    _buttonsExampleRadioButton2RadioButtonGroup = new Template(RadioButtonTemplates.RadioButtonRadioButtonGroup);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton2RadioButtonGroup.Name = "ButtonsExampleRadioButton2RadioButtonGroup";
#endif
                }
                return _buttonsExampleRadioButton2RadioButtonGroup;
            }
        }

        private static Template _buttonsExampleRadioButton2RadioButtonImageView;
        public static Template ButtonsExampleRadioButton2RadioButtonImageView
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton2RadioButtonImageView == null || _buttonsExampleRadioButton2RadioButtonImageView.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton2RadioButtonImageView == null)
#endif
                {
                    _buttonsExampleRadioButton2RadioButtonImageView = new Template(RadioButtonTemplates.RadioButtonRadioButtonImageView);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton2RadioButtonImageView.Name = "ButtonsExampleRadioButton2RadioButtonImageView";
#endif
                }
                return _buttonsExampleRadioButton2RadioButtonImageView;
            }
        }

        private static Template _buttonsExampleRadioButton2RadioButtonLabel;
        public static Template ButtonsExampleRadioButton2RadioButtonLabel
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton2RadioButtonLabel == null || _buttonsExampleRadioButton2RadioButtonLabel.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton2RadioButtonLabel == null)
#endif
                {
                    _buttonsExampleRadioButton2RadioButtonLabel = new Template(RadioButtonTemplates.RadioButtonRadioButtonLabel);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton2RadioButtonLabel.Name = "ButtonsExampleRadioButton2RadioButtonLabel";
#endif
                    Delight.Label.TextProperty.SetDefault(_buttonsExampleRadioButton2RadioButtonLabel, "Option 2");
                }
                return _buttonsExampleRadioButton2RadioButtonLabel;
            }
        }

        private static Template _buttonsExampleRadioButton3;
        public static Template ButtonsExampleRadioButton3
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton3 == null || _buttonsExampleRadioButton3.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton3 == null)
#endif
                {
                    _buttonsExampleRadioButton3 = new Template(RadioButtonTemplates.RadioButton);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton3.Name = "ButtonsExampleRadioButton3";
#endif
                    Delight.RadioButton.RadioButtonGroupTemplateProperty.SetDefault(_buttonsExampleRadioButton3, ButtonsExampleRadioButton3RadioButtonGroup);
                    Delight.RadioButton.RadioButtonImageViewTemplateProperty.SetDefault(_buttonsExampleRadioButton3, ButtonsExampleRadioButton3RadioButtonImageView);
                    Delight.RadioButton.RadioButtonLabelTemplateProperty.SetDefault(_buttonsExampleRadioButton3, ButtonsExampleRadioButton3RadioButtonLabel);
                }
                return _buttonsExampleRadioButton3;
            }
        }

        private static Template _buttonsExampleRadioButton3RadioButtonGroup;
        public static Template ButtonsExampleRadioButton3RadioButtonGroup
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton3RadioButtonGroup == null || _buttonsExampleRadioButton3RadioButtonGroup.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton3RadioButtonGroup == null)
#endif
                {
                    _buttonsExampleRadioButton3RadioButtonGroup = new Template(RadioButtonTemplates.RadioButtonRadioButtonGroup);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton3RadioButtonGroup.Name = "ButtonsExampleRadioButton3RadioButtonGroup";
#endif
                }
                return _buttonsExampleRadioButton3RadioButtonGroup;
            }
        }

        private static Template _buttonsExampleRadioButton3RadioButtonImageView;
        public static Template ButtonsExampleRadioButton3RadioButtonImageView
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton3RadioButtonImageView == null || _buttonsExampleRadioButton3RadioButtonImageView.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton3RadioButtonImageView == null)
#endif
                {
                    _buttonsExampleRadioButton3RadioButtonImageView = new Template(RadioButtonTemplates.RadioButtonRadioButtonImageView);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton3RadioButtonImageView.Name = "ButtonsExampleRadioButton3RadioButtonImageView";
#endif
                }
                return _buttonsExampleRadioButton3RadioButtonImageView;
            }
        }

        private static Template _buttonsExampleRadioButton3RadioButtonLabel;
        public static Template ButtonsExampleRadioButton3RadioButtonLabel
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleRadioButton3RadioButtonLabel == null || _buttonsExampleRadioButton3RadioButtonLabel.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleRadioButton3RadioButtonLabel == null)
#endif
                {
                    _buttonsExampleRadioButton3RadioButtonLabel = new Template(RadioButtonTemplates.RadioButtonRadioButtonLabel);
#if UNITY_EDITOR
                    _buttonsExampleRadioButton3RadioButtonLabel.Name = "ButtonsExampleRadioButton3RadioButtonLabel";
#endif
                    Delight.Label.TextProperty.SetDefault(_buttonsExampleRadioButton3RadioButtonLabel, "Option 3");
                }
                return _buttonsExampleRadioButton3RadioButtonLabel;
            }
        }

        private static Template _buttonsExampleGroup5;
        public static Template ButtonsExampleGroup5
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleGroup5 == null || _buttonsExampleGroup5.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleGroup5 == null)
#endif
                {
                    _buttonsExampleGroup5 = new Template(GroupTemplates.Group);
#if UNITY_EDITOR
                    _buttonsExampleGroup5.Name = "ButtonsExampleGroup5";
#endif
                    Delight.Group.OrientationProperty.SetDefault(_buttonsExampleGroup5, Delight.ElementOrientation.Horizontal);
                    Delight.Group.SpacingProperty.SetDefault(_buttonsExampleGroup5, new ElementSize(15f, ElementSizeUnit.Pixels));
                }
                return _buttonsExampleGroup5;
            }
        }

        private static Template _buttonsExampleButton1;
        public static Template ButtonsExampleButton1
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleButton1 == null || _buttonsExampleButton1.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleButton1 == null)
#endif
                {
                    _buttonsExampleButton1 = new Template(ButtonTemplates.Button);
#if UNITY_EDITOR
                    _buttonsExampleButton1.Name = "ButtonsExampleButton1";
#endif
                    Delight.Button.LabelTemplateProperty.SetDefault(_buttonsExampleButton1, ButtonsExampleButton1Label);
                }
                return _buttonsExampleButton1;
            }
        }

        private static Template _buttonsExampleButton1Label;
        public static Template ButtonsExampleButton1Label
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleButton1Label == null || _buttonsExampleButton1Label.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleButton1Label == null)
#endif
                {
                    _buttonsExampleButton1Label = new Template(ButtonTemplates.ButtonLabel);
#if UNITY_EDITOR
                    _buttonsExampleButton1Label.Name = "ButtonsExampleButton1Label";
#endif
                    Delight.Label.TextProperty.SetDefault(_buttonsExampleButton1Label, "Click Me");
                }
                return _buttonsExampleButton1Label;
            }
        }

        private static Template _buttonsExampleButton2;
        public static Template ButtonsExampleButton2
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleButton2 == null || _buttonsExampleButton2.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleButton2 == null)
#endif
                {
                    _buttonsExampleButton2 = new Template(ButtonTemplates.Button);
#if UNITY_EDITOR
                    _buttonsExampleButton2.Name = "ButtonsExampleButton2";
#endif
                    Delight.Button.IsToggleButtonProperty.SetDefault(_buttonsExampleButton2, true);
                    Delight.Button.WidthProperty.SetDefault(_buttonsExampleButton2, new ElementSize(100f, ElementSizeUnit.Pixels));
                    Delight.Button.LabelTemplateProperty.SetDefault(_buttonsExampleButton2, ButtonsExampleButton2Label);
                }
                return _buttonsExampleButton2;
            }
        }

        private static Template _buttonsExampleButton2Label;
        public static Template ButtonsExampleButton2Label
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleButton2Label == null || _buttonsExampleButton2Label.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleButton2Label == null)
#endif
                {
                    _buttonsExampleButton2Label = new Template(ButtonTemplates.ButtonLabel);
#if UNITY_EDITOR
                    _buttonsExampleButton2Label.Name = "ButtonsExampleButton2Label";
#endif
                }
                return _buttonsExampleButton2Label;
            }
        }

        private static Template _buttonsExampleLabel1;
        public static Template ButtonsExampleLabel1
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleLabel1 == null || _buttonsExampleLabel1.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleLabel1 == null)
#endif
                {
                    _buttonsExampleLabel1 = new Template(LabelTemplates.Label);
#if UNITY_EDITOR
                    _buttonsExampleLabel1.Name = "ButtonsExampleLabel1";
#endif
                    Delight.Label.TextProperty.SetDefault(_buttonsExampleLabel1, "Toggle");
                    Delight.Label.OffsetProperty.SetDefault(_buttonsExampleLabel1, new ElementMargin(new ElementSize(35f, ElementSizeUnit.Pixels), new ElementSize(0f, ElementSizeUnit.Pixels), new ElementSize(0f, ElementSizeUnit.Pixels), new ElementSize(0f, ElementSizeUnit.Pixels)));
                    Delight.Label.AlignmentProperty.SetDefault(_buttonsExampleLabel1, Delight.ElementAlignment.Left);
                    Delight.Label.AutoSizeProperty.SetDefault(_buttonsExampleLabel1, Delight.AutoSize.Default);
                }
                return _buttonsExampleLabel1;
            }
        }

        private static Template _buttonsExampleImage1;
        public static Template ButtonsExampleImage1
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleImage1 == null || _buttonsExampleImage1.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleImage1 == null)
#endif
                {
                    _buttonsExampleImage1 = new Template(ImageTemplates.Image);
#if UNITY_EDITOR
                    _buttonsExampleImage1.Name = "ButtonsExampleImage1";
#endif
                    Delight.Image.SpriteProperty.SetDefault(_buttonsExampleImage1, Assets.Sprites["RainbowSquare"]);
                    Delight.Image.AlignmentProperty.SetDefault(_buttonsExampleImage1, Delight.ElementAlignment.Left);
                    Delight.Image.OffsetProperty.SetDefault(_buttonsExampleImage1, new ElementMargin(new ElementSize(5f, ElementSizeUnit.Pixels), new ElementSize(0f, ElementSizeUnit.Pixels), new ElementSize(0f, ElementSizeUnit.Pixels), new ElementSize(0f, ElementSizeUnit.Pixels)));
                }
                return _buttonsExampleImage1;
            }
        }

        private static Template _buttonsExampleLabel2;
        public static Template ButtonsExampleLabel2
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleLabel2 == null || _buttonsExampleLabel2.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleLabel2 == null)
#endif
                {
                    _buttonsExampleLabel2 = new Template(LabelTemplates.Label);
#if UNITY_EDITOR
                    _buttonsExampleLabel2.Name = "ButtonsExampleLabel2";
#endif
                    Delight.Label.AutoSizeProperty.SetDefault(_buttonsExampleLabel2, Delight.AutoSize.Default);
                    Delight.Label.FontColorProperty.SetDefault(_buttonsExampleLabel2, new UnityEngine.Color(1f, 1f, 1f, 1f));
                    Delight.Label.TextProperty.SetHasBinding(_buttonsExampleLabel2);
                }
                return _buttonsExampleLabel2;
            }
        }

        private static Template _buttonsExampleComboBox;
        public static Template ButtonsExampleComboBox
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBox == null || _buttonsExampleComboBox.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBox == null)
#endif
                {
                    _buttonsExampleComboBox = new Template(ComboBoxTemplates.ComboBox);
#if UNITY_EDITOR
                    _buttonsExampleComboBox.Name = "ButtonsExampleComboBox";
#endif
                    Delight.ComboBox.IsDropUpProperty.SetDefault(_buttonsExampleComboBox, false);
                    Delight.ComboBox.ComboBoxButtonTemplateProperty.SetDefault(_buttonsExampleComboBox, ButtonsExampleComboBoxComboBoxButton);
                    Delight.ComboBox.ComboBoxListCanvasTemplateProperty.SetDefault(_buttonsExampleComboBox, ButtonsExampleComboBoxComboBoxListCanvas);
                    Delight.ComboBox.ComboBoxListTemplateProperty.SetDefault(_buttonsExampleComboBox, ButtonsExampleComboBoxComboBoxList);
                }
                return _buttonsExampleComboBox;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxButton;
        public static Template ButtonsExampleComboBoxComboBoxButton
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxButton == null || _buttonsExampleComboBoxComboBoxButton.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxButton == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxButton = new Template(ComboBoxTemplates.ComboBoxComboBoxButton);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxButton.Name = "ButtonsExampleComboBoxComboBoxButton";
#endif
                    Delight.Button.LabelTemplateProperty.SetDefault(_buttonsExampleComboBoxComboBoxButton, ButtonsExampleComboBoxComboBoxButtonLabel);
                }
                return _buttonsExampleComboBoxComboBoxButton;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxButtonLabel;
        public static Template ButtonsExampleComboBoxComboBoxButtonLabel
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxButtonLabel == null || _buttonsExampleComboBoxComboBoxButtonLabel.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxButtonLabel == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxButtonLabel = new Template(ComboBoxTemplates.ComboBoxComboBoxButtonLabel);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxButtonLabel.Name = "ButtonsExampleComboBoxComboBoxButtonLabel";
#endif
                }
                return _buttonsExampleComboBoxComboBoxButtonLabel;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxListCanvas;
        public static Template ButtonsExampleComboBoxComboBoxListCanvas
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxListCanvas == null || _buttonsExampleComboBoxComboBoxListCanvas.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxListCanvas == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxListCanvas = new Template(ComboBoxTemplates.ComboBoxComboBoxListCanvas);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxListCanvas.Name = "ButtonsExampleComboBoxComboBoxListCanvas";
#endif
                }
                return _buttonsExampleComboBoxComboBoxListCanvas;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxList;
        public static Template ButtonsExampleComboBoxComboBoxList
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxList == null || _buttonsExampleComboBoxComboBoxList.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxList == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxList = new Template(ComboBoxTemplates.ComboBoxComboBoxList);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxList.Name = "ButtonsExampleComboBoxComboBoxList";
#endif
                    Delight.List.ItemsProperty.SetHasBinding(_buttonsExampleComboBoxComboBoxList);
                    Delight.List.ScrollableRegionTemplateProperty.SetDefault(_buttonsExampleComboBoxComboBoxList, ButtonsExampleComboBoxComboBoxListScrollableRegion);
                }
                return _buttonsExampleComboBoxComboBoxList;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxListScrollableRegion;
        public static Template ButtonsExampleComboBoxComboBoxListScrollableRegion
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxListScrollableRegion == null || _buttonsExampleComboBoxComboBoxListScrollableRegion.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxListScrollableRegion == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxListScrollableRegion = new Template(ComboBoxTemplates.ComboBoxComboBoxListScrollableRegion);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxListScrollableRegion.Name = "ButtonsExampleComboBoxComboBoxListScrollableRegion";
#endif
                    Delight.ScrollableRegion.ContentRegionTemplateProperty.SetDefault(_buttonsExampleComboBoxComboBoxListScrollableRegion, ButtonsExampleComboBoxComboBoxListScrollableRegionContentRegion);
                    Delight.ScrollableRegion.HorizontalScrollbarTemplateProperty.SetDefault(_buttonsExampleComboBoxComboBoxListScrollableRegion, ButtonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar);
                    Delight.ScrollableRegion.VerticalScrollbarTemplateProperty.SetDefault(_buttonsExampleComboBoxComboBoxListScrollableRegion, ButtonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar);
                }
                return _buttonsExampleComboBoxComboBoxListScrollableRegion;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxListScrollableRegionContentRegion;
        public static Template ButtonsExampleComboBoxComboBoxListScrollableRegionContentRegion
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionContentRegion == null || _buttonsExampleComboBoxComboBoxListScrollableRegionContentRegion.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionContentRegion == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxListScrollableRegionContentRegion = new Template(ComboBoxTemplates.ComboBoxComboBoxListScrollableRegionContentRegion);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxListScrollableRegionContentRegion.Name = "ButtonsExampleComboBoxComboBoxListScrollableRegionContentRegion";
#endif
                }
                return _buttonsExampleComboBoxComboBoxListScrollableRegionContentRegion;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar;
        public static Template ButtonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar == null || _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar = new Template(ComboBoxTemplates.ComboBoxComboBoxListScrollableRegionHorizontalScrollbar);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar.Name = "ButtonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar";
#endif
                    Delight.Scrollbar.BarTemplateProperty.SetDefault(_buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar, ButtonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarBar);
                    Delight.Scrollbar.HandleTemplateProperty.SetDefault(_buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar, ButtonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarHandle);
                }
                return _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbar;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarBar;
        public static Template ButtonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarBar
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarBar == null || _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarBar.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarBar == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarBar = new Template(ComboBoxTemplates.ComboBoxComboBoxListScrollableRegionHorizontalScrollbarBar);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarBar.Name = "ButtonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarBar";
#endif
                }
                return _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarBar;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarHandle;
        public static Template ButtonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarHandle
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarHandle == null || _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarHandle.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarHandle == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarHandle = new Template(ComboBoxTemplates.ComboBoxComboBoxListScrollableRegionHorizontalScrollbarHandle);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarHandle.Name = "ButtonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarHandle";
#endif
                }
                return _buttonsExampleComboBoxComboBoxListScrollableRegionHorizontalScrollbarHandle;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar;
        public static Template ButtonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar == null || _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar = new Template(ComboBoxTemplates.ComboBoxComboBoxListScrollableRegionVerticalScrollbar);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar.Name = "ButtonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar";
#endif
                    Delight.Scrollbar.BarTemplateProperty.SetDefault(_buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar, ButtonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarBar);
                    Delight.Scrollbar.HandleTemplateProperty.SetDefault(_buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar, ButtonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarHandle);
                }
                return _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbar;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarBar;
        public static Template ButtonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarBar
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarBar == null || _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarBar.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarBar == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarBar = new Template(ComboBoxTemplates.ComboBoxComboBoxListScrollableRegionVerticalScrollbarBar);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarBar.Name = "ButtonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarBar";
#endif
                }
                return _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarBar;
            }
        }

        private static Template _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarHandle;
        public static Template ButtonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarHandle
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarHandle == null || _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarHandle.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarHandle == null)
#endif
                {
                    _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarHandle = new Template(ComboBoxTemplates.ComboBoxComboBoxListScrollableRegionVerticalScrollbarHandle);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarHandle.Name = "ButtonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarHandle";
#endif
                }
                return _buttonsExampleComboBoxComboBoxListScrollableRegionVerticalScrollbarHandle;
            }
        }

        private static Template _buttonsExampleComboBoxContent;
        public static Template ButtonsExampleComboBoxContent
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleComboBoxContent == null || _buttonsExampleComboBoxContent.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleComboBoxContent == null)
#endif
                {
                    _buttonsExampleComboBoxContent = new Template(ComboBoxListItemTemplates.ComboBoxListItem);
#if UNITY_EDITOR
                    _buttonsExampleComboBoxContent.Name = "ButtonsExampleComboBoxContent";
#endif
                }
                return _buttonsExampleComboBoxContent;
            }
        }

        private static Template _buttonsExampleLabel3;
        public static Template ButtonsExampleLabel3
        {
            get
            {
#if UNITY_EDITOR
                if (_buttonsExampleLabel3 == null || _buttonsExampleLabel3.CurrentVersion != Template.Version)
#else
                if (_buttonsExampleLabel3 == null)
#endif
                {
                    _buttonsExampleLabel3 = new Template(LabelTemplates.Label);
#if UNITY_EDITOR
                    _buttonsExampleLabel3.Name = "ButtonsExampleLabel3";
#endif
                    Delight.Label.WidthProperty.SetDefault(_buttonsExampleLabel3, new ElementSize(1f, ElementSizeUnit.Percents));
                    Delight.Label.MarginProperty.SetDefault(_buttonsExampleLabel3, new ElementMargin(new ElementSize(10f, ElementSizeUnit.Pixels), new ElementSize(0f, ElementSizeUnit.Pixels), new ElementSize(0f, ElementSizeUnit.Pixels), new ElementSize(0f, ElementSizeUnit.Pixels)));
                    Delight.Label.TextProperty.SetHasBinding(_buttonsExampleLabel3);
                }
                return _buttonsExampleLabel3;
            }
        }

        #endregion
    }

    #endregion
}
