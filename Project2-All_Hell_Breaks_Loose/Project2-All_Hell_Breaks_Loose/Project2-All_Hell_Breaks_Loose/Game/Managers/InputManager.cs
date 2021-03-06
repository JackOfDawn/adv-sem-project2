﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project2_All_Hell_Breaks_Loose.Game.Managers
{

    public class InputManager
    {
        //delegates
        public delegate void DirectionPressedDelegate(Vector2 moveVector);
        public delegate void ButtonPressedDelegate();
        public delegate void MousePositionDelegate(Vector2 mousePos);
        public delegate void ButtonParamDelegate(int val);

        //Defining events
        public event DirectionPressedDelegate Event_MovementPressed;
        public event ButtonPressedDelegate Event_SwitchWeapons;
        public event ButtonPressedDelegate Event_Shoot;
        public event MousePositionDelegate Event_UpdateCursorLoc;
        public event ButtonPressedDelegate Event_CloseShop;
        public event ButtonPressedDelegate Event_UpgradePistol;
        public event ButtonPressedDelegate Event_UpgradeShotgun;
        public event ButtonParamDelegate Event_GiveAmmo;
        public event ButtonPressedDelegate Event_Respawn;

        KeyboardState lastKeyboardState;
        MouseState lastMouseState;

        bool shootActionPressed;
        bool switchWeaponActionPressed;

        bool leftClickDown;
        bool qKeyDown;
        bool eKeyDown;
        bool oneKeyDown;
        bool twoKeyDown;
        bool spaceKeyDown;

        bool shopExitPressed;
        bool pistolUpgradePressed;
        bool shotgunUpgradePressed;

        bool respawnPressed;

        Vector2 movementVector;

        public InputManager()
        {
            shootActionPressed = false;
            switchWeaponActionPressed = false;
            movementVector = Vector2.Zero;
            qKeyDown = false;
            leftClickDown = false;
            oneKeyDown = false;
            twoKeyDown = false;
            shopExitPressed = false;
            pistolUpgradePressed = false;
            shotgunUpgradePressed = false;
        }
        
        public void Update(GameTime gameTime)
        {
            shootActionPressed = false;
            switchWeaponActionPressed = false;

            movementVector = Vector2.Zero;

            HandleKeyInput();
            HandleMouseInput();

            if(shootActionPressed && Event_Shoot != null)
            {
                Event_Shoot();
            }

            if(switchWeaponActionPressed && Event_SwitchWeapons != null)
            {
                Event_SwitchWeapons();
            }

            if(movementVector != Vector2.Zero && Event_MovementPressed != null)
            {
                Event_MovementPressed(movementVector);
            }

            if(Event_UpdateCursorLoc != null)
            {
                Vector2 mouseLoc = new Vector2(lastMouseState.X, lastMouseState.Y);
                Event_UpdateCursorLoc(mouseLoc);
            }
        }

        public void shopUpdate(GameTime gameTime)
        {
            shopExitPressed = false;
            pistolUpgradePressed = false;
            shotgunUpgradePressed = false;

            HandleKeyInput();

            if(shopExitPressed && Event_GiveAmmo != null && Event_CloseShop != null)
            {
                Event_GiveAmmo(20);
                Event_CloseShop();
            }
            if (pistolUpgradePressed && Event_UpgradePistol != null && Event_CloseShop != null)
            {
                Event_UpgradePistol();
                Event_CloseShop();
            }
            if (shotgunUpgradePressed && Event_UpgradeShotgun != null && Event_CloseShop != null)
            {
                Event_UpgradeShotgun();
                Event_CloseShop();
            }
        }

        public void deathUpdate(GameTime gameTime)
        {
            respawnPressed = false;

            HandleKeyInput();

            if(respawnPressed && Event_Respawn != null)
            {
                Event_Respawn();
            }
        }

        private void HandleMouseInput()
        {
            MouseState currentMouseState = Mouse.GetState();
            if (lastMouseState != currentMouseState)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && !leftClickDown)
                {
                    shootActionPressed = true;
                    leftClickDown = true;
                }
                else if (!(currentMouseState.LeftButton == ButtonState.Pressed) && leftClickDown)
                {
                    leftClickDown = false;
                }
            }
            lastMouseState = Mouse.GetState();
        }

        private void HandleKeyInput()
        {
            KeyboardState currentKeyState = Keyboard.GetState();
            if (lastKeyboardState != currentKeyState)
            {
                if (currentKeyState.IsKeyDown(Keys.Q) && !qKeyDown)
                {
                    switchWeaponActionPressed = true;
                    qKeyDown = true;
                }
                else if (!currentKeyState.IsKeyDown(Keys.Q) && qKeyDown)
                {
                    qKeyDown = false;
                }

                if(currentKeyState.IsKeyDown(Keys.E) && !eKeyDown)
                {
                    shopExitPressed = true;
                    eKeyDown = true;
                }
                else if(!currentKeyState.IsKeyDown(Keys.E) && eKeyDown)
                {
                    eKeyDown = false;
                }

                if(currentKeyState.IsKeyDown(Keys.D1) && !oneKeyDown)
                {
                    pistolUpgradePressed = true;
                    oneKeyDown = true;
                }
                else if(!currentKeyState.IsKeyDown(Keys.D1) && oneKeyDown)
                {
                    oneKeyDown = false;
                }

                if (currentKeyState.IsKeyDown(Keys.D2) && !twoKeyDown)
                {
                    shotgunUpgradePressed = true;
                    twoKeyDown = true;
                }
                else if (!currentKeyState.IsKeyDown(Keys.D2) && twoKeyDown)
                {
                    twoKeyDown = false;
                }

                if(currentKeyState.IsKeyDown(Keys.Space) && !spaceKeyDown)
                {
                    respawnPressed = true;
                    spaceKeyDown = true;
                }
                else if(!currentKeyState.IsKeyDown(Keys.Space) && spaceKeyDown)
                {
                    spaceKeyDown = false;
                }
            }

            if (currentKeyState.IsKeyDown(Keys.W)) movementVector.Y = -1;
            if (currentKeyState.IsKeyDown(Keys.S)) movementVector.Y = 1;
            if (currentKeyState.IsKeyDown(Keys.A)) movementVector.X = -1;
            if (currentKeyState.IsKeyDown(Keys.D)) movementVector.X = 1;

            lastKeyboardState = Keyboard.GetState();
        }
    }
}
