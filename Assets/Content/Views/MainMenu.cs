#region Using Statements
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using WorldGeneration;
#endregion

namespace Delight
{
    public partial class MainMenu
    {
        public static MainMenu instance;

        private PlayerCameraController connectedCamera = null;

        private GameObject rootLayout = null;

        protected override void AfterLoad()
        { //Override load with custom load
            base.AfterLoad();                    //parse normal load
            instance = this;                     // Store static reference for global use

            validateConfiguration();             //configure,
            connectedCamera.MenuPosition();      //move camera to menu.
        }

        private void validateConfiguration()
        {
            connectedCamera = GameObject.Find(Literals.OBJECT_PLAYER_CAM).GetComponent(typeof(PlayerCameraController)) as PlayerCameraController;
            Assert.IsNotNull(connectedCamera, "Main menu missing camera connection");

            rootLayout = GameObject.Find(Literals.OBJECT_DESIRE_ROOT);
            Assert.IsNotNull(rootLayout, "Unable to locate Desire menu root");
        }

        public void Play()
        {
            connectedCamera.PlayPosition();
            hide();
        }

        public void Quit()
        {
            Application.Quit(0);
        }


        public void enterMenu()
        {
            connectedCamera.MenuPosition();
            show();
        }

        private void hide()
        {
            rootLayout.SetActive(false);
        }

        private void show()
        {
            rootLayout.SetActive(true);
        }

        public void Regen()
        {
            WorldFactory factory = GameObject.Find("World").GetComponent<WorldFactory>();
            factory.Start();
        }


    }
}
