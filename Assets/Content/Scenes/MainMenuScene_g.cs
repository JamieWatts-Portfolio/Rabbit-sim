// Internal view logic generated from "MainMenuScene.xml"
#region Using Statements
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace Delight
{
    public partial class MainMenuScene : UIView
    {
        #region Constructors

        public MainMenuScene(View parent, View layoutParent = null, string id = null, Template template = null, bool deferInitialization = false) :
            base(parent, layoutParent, id, template ?? MainMenuSceneTemplates.Default, deferInitialization)
        {
            if (deferInitialization)
                return;

            // constructing MainMenu (MainMenu1)
            MainMenu1 = new MainMenu(this, this, "MainMenu1", MainMenu1Template);
            this.AfterInitializeInternal();
        }

        public MainMenuScene() : this(null)
        {
        }

        static MainMenuScene()
        {
            var dependencyProperties = new List<DependencyProperty>();
            DependencyProperties.Add(MainMenuSceneTemplates.Default, dependencyProperties);

            dependencyProperties.Add(MainMenu1Property);
            dependencyProperties.Add(MainMenu1TemplateProperty);
        }

        #endregion

        #region Properties

        public readonly static DependencyProperty<MainMenu> MainMenu1Property = new DependencyProperty<MainMenu>("MainMenu1");
        public MainMenu MainMenu1
        {
            get { return MainMenu1Property.GetValue(this); }
            set { MainMenu1Property.SetValue(this, value); }
        }

        public readonly static DependencyProperty<Template> MainMenu1TemplateProperty = new DependencyProperty<Template>("MainMenu1Template");
        public Template MainMenu1Template
        {
            get { return MainMenu1TemplateProperty.GetValue(this); }
            set { MainMenu1TemplateProperty.SetValue(this, value); }
        }

        #endregion
    }

    #region Data Templates

    public static class MainMenuSceneTemplates
    {
        #region Properties

        public static Template Default
        {
            get
            {
                return MainMenuScene;
            }
        }

        private static Template _mainMenuScene;
        public static Template MainMenuScene
        {
            get
            {
#if UNITY_EDITOR
                if (_mainMenuScene == null || _mainMenuScene.CurrentVersion != Template.Version)
#else
                if (_mainMenuScene == null)
#endif
                {
                    _mainMenuScene = new Template(UIViewTemplates.UIView);
#if UNITY_EDITOR
                    _mainMenuScene.Name = "MainMenuScene";
#endif
                    Delight.MainMenuScene.MainMenu1TemplateProperty.SetDefault(_mainMenuScene, MainMenuSceneMainMenu1);
                }
                return _mainMenuScene;
            }
        }

        private static Template _mainMenuSceneMainMenu1;
        public static Template MainMenuSceneMainMenu1
        {
            get
            {
#if UNITY_EDITOR
                if (_mainMenuSceneMainMenu1 == null || _mainMenuSceneMainMenu1.CurrentVersion != Template.Version)
#else
                if (_mainMenuSceneMainMenu1 == null)
#endif
                {
                    _mainMenuSceneMainMenu1 = new Template(MainMenuTemplates.MainMenu);
#if UNITY_EDITOR
                    _mainMenuSceneMainMenu1.Name = "MainMenuSceneMainMenu1";
#endif
                    Delight.MainMenu.Image1TemplateProperty.SetDefault(_mainMenuSceneMainMenu1, MainMenuSceneMainMenu1Image1);
                    Delight.MainMenu.Group1TemplateProperty.SetDefault(_mainMenuSceneMainMenu1, MainMenuSceneMainMenu1Group1);
                    Delight.MainMenu.Button1TemplateProperty.SetDefault(_mainMenuSceneMainMenu1, MainMenuSceneMainMenu1Button1);
                    Delight.MainMenu.Button2TemplateProperty.SetDefault(_mainMenuSceneMainMenu1, MainMenuSceneMainMenu1Button2);
                    Delight.MainMenu.Button3TemplateProperty.SetDefault(_mainMenuSceneMainMenu1, MainMenuSceneMainMenu1Button3);
                }
                return _mainMenuSceneMainMenu1;
            }
        }

        private static Template _mainMenuSceneMainMenu1Image1;
        public static Template MainMenuSceneMainMenu1Image1
        {
            get
            {
#if UNITY_EDITOR
                if (_mainMenuSceneMainMenu1Image1 == null || _mainMenuSceneMainMenu1Image1.CurrentVersion != Template.Version)
#else
                if (_mainMenuSceneMainMenu1Image1 == null)
#endif
                {
                    _mainMenuSceneMainMenu1Image1 = new Template(MainMenuTemplates.MainMenuImage1);
#if UNITY_EDITOR
                    _mainMenuSceneMainMenu1Image1.Name = "MainMenuSceneMainMenu1Image1";
#endif
                }
                return _mainMenuSceneMainMenu1Image1;
            }
        }

        private static Template _mainMenuSceneMainMenu1Group1;
        public static Template MainMenuSceneMainMenu1Group1
        {
            get
            {
#if UNITY_EDITOR
                if (_mainMenuSceneMainMenu1Group1 == null || _mainMenuSceneMainMenu1Group1.CurrentVersion != Template.Version)
#else
                if (_mainMenuSceneMainMenu1Group1 == null)
#endif
                {
                    _mainMenuSceneMainMenu1Group1 = new Template(MainMenuTemplates.MainMenuGroup1);
#if UNITY_EDITOR
                    _mainMenuSceneMainMenu1Group1.Name = "MainMenuSceneMainMenu1Group1";
#endif
                }
                return _mainMenuSceneMainMenu1Group1;
            }
        }

        private static Template _mainMenuSceneMainMenu1Button1;
        public static Template MainMenuSceneMainMenu1Button1
        {
            get
            {
#if UNITY_EDITOR
                if (_mainMenuSceneMainMenu1Button1 == null || _mainMenuSceneMainMenu1Button1.CurrentVersion != Template.Version)
#else
                if (_mainMenuSceneMainMenu1Button1 == null)
#endif
                {
                    _mainMenuSceneMainMenu1Button1 = new Template(MainMenuTemplates.MainMenuButton1);
#if UNITY_EDITOR
                    _mainMenuSceneMainMenu1Button1.Name = "MainMenuSceneMainMenu1Button1";
#endif
                    Delight.Button.LabelTemplateProperty.SetDefault(_mainMenuSceneMainMenu1Button1, MainMenuSceneMainMenu1Button1Label);
                }
                return _mainMenuSceneMainMenu1Button1;
            }
        }

        private static Template _mainMenuSceneMainMenu1Button1Label;
        public static Template MainMenuSceneMainMenu1Button1Label
        {
            get
            {
#if UNITY_EDITOR
                if (_mainMenuSceneMainMenu1Button1Label == null || _mainMenuSceneMainMenu1Button1Label.CurrentVersion != Template.Version)
#else
                if (_mainMenuSceneMainMenu1Button1Label == null)
#endif
                {
                    _mainMenuSceneMainMenu1Button1Label = new Template(MainMenuTemplates.MainMenuButton1Label);
#if UNITY_EDITOR
                    _mainMenuSceneMainMenu1Button1Label.Name = "MainMenuSceneMainMenu1Button1Label";
#endif
                }
                return _mainMenuSceneMainMenu1Button1Label;
            }
        }

        private static Template _mainMenuSceneMainMenu1Button2;
        public static Template MainMenuSceneMainMenu1Button2
        {
            get
            {
#if UNITY_EDITOR
                if (_mainMenuSceneMainMenu1Button2 == null || _mainMenuSceneMainMenu1Button2.CurrentVersion != Template.Version)
#else
                if (_mainMenuSceneMainMenu1Button2 == null)
#endif
                {
                    _mainMenuSceneMainMenu1Button2 = new Template(MainMenuTemplates.MainMenuButton2);
#if UNITY_EDITOR
                    _mainMenuSceneMainMenu1Button2.Name = "MainMenuSceneMainMenu1Button2";
#endif
                    Delight.Button.LabelTemplateProperty.SetDefault(_mainMenuSceneMainMenu1Button2, MainMenuSceneMainMenu1Button2Label);
                }
                return _mainMenuSceneMainMenu1Button2;
            }
        }

        private static Template _mainMenuSceneMainMenu1Button2Label;
        public static Template MainMenuSceneMainMenu1Button2Label
        {
            get
            {
#if UNITY_EDITOR
                if (_mainMenuSceneMainMenu1Button2Label == null || _mainMenuSceneMainMenu1Button2Label.CurrentVersion != Template.Version)
#else
                if (_mainMenuSceneMainMenu1Button2Label == null)
#endif
                {
                    _mainMenuSceneMainMenu1Button2Label = new Template(MainMenuTemplates.MainMenuButton2Label);
#if UNITY_EDITOR
                    _mainMenuSceneMainMenu1Button2Label.Name = "MainMenuSceneMainMenu1Button2Label";
#endif
                }
                return _mainMenuSceneMainMenu1Button2Label;
            }
        }

        private static Template _mainMenuSceneMainMenu1Button3;
        public static Template MainMenuSceneMainMenu1Button3
        {
            get
            {
#if UNITY_EDITOR
                if (_mainMenuSceneMainMenu1Button3 == null || _mainMenuSceneMainMenu1Button3.CurrentVersion != Template.Version)
#else
                if (_mainMenuSceneMainMenu1Button3 == null)
#endif
                {
                    _mainMenuSceneMainMenu1Button3 = new Template(MainMenuTemplates.MainMenuButton3);
#if UNITY_EDITOR
                    _mainMenuSceneMainMenu1Button3.Name = "MainMenuSceneMainMenu1Button3";
#endif
                    Delight.Button.LabelTemplateProperty.SetDefault(_mainMenuSceneMainMenu1Button3, MainMenuSceneMainMenu1Button3Label);
                }
                return _mainMenuSceneMainMenu1Button3;
            }
        }

        private static Template _mainMenuSceneMainMenu1Button3Label;
        public static Template MainMenuSceneMainMenu1Button3Label
        {
            get
            {
#if UNITY_EDITOR
                if (_mainMenuSceneMainMenu1Button3Label == null || _mainMenuSceneMainMenu1Button3Label.CurrentVersion != Template.Version)
#else
                if (_mainMenuSceneMainMenu1Button3Label == null)
#endif
                {
                    _mainMenuSceneMainMenu1Button3Label = new Template(MainMenuTemplates.MainMenuButton3Label);
#if UNITY_EDITOR
                    _mainMenuSceneMainMenu1Button3Label.Name = "MainMenuSceneMainMenu1Button3Label";
#endif
                }
                return _mainMenuSceneMainMenu1Button3Label;
            }
        }

        #endregion
    }

    #endregion
}
