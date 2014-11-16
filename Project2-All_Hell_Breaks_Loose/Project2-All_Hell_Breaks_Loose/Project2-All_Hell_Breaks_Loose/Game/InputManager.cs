using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project2_All_Hell_Breaks_Loose.Game
{

    public class InputManager
    {
        //delegates
        public delegate void DirectionPressedDelegate(Vector2 moveVector);
        public delegate void ButtonPressedDelegate();
        public delegate void MousePositionDelegate(Vector2 mousePos);

        //Defining events
        public event DirectionPressedDelegate event_MovementPressed;
        public event ButtonPressedDelegate event_SwitchWeapons;
        public event ButtonPressedDelegate event_Shoot;
        public event MousePositionDelegate event_UpdateCursorLoc;

        KeyboardState lastKeyboardState;
        MouseState lastMouseState;

        bool shootActionPressed;
        bool switchWeaponActionPressed;

        bool leftClickDown;
        bool qKeyDown;

        Vector2 movementVector;
        

        public InputManager()
        {
            shootActionPressed = false;
            switchWeaponActionPressed = false;
            movementVector = Vector2.Zero;
            qKeyDown = false;
            leftClickDown = false;
        }
        
        public void update(GameTime gameTime)
        {
            shootActionPressed = false;
            switchWeaponActionPressed = false;

            movementVector = Vector2.Zero;


            handleKeyInput();
            handleMouseInput();

            //call events
            if(shootActionPressed && event_Shoot != null)
            {
                event_Shoot();
            }

            if(switchWeaponActionPressed && event_SwitchWeapons != null)
            {
                event_SwitchWeapons();
            }

            if(movementVector != Vector2.Zero && event_MovementPressed != null)
            {
                event_MovementPressed(movementVector);
            }

            if(event_UpdateCursorLoc != null)
            {
                Vector2 mouseLoc = new Vector2(lastMouseState.X, lastMouseState.Y);
                event_UpdateCursorLoc(mouseLoc);
            }


        }

        private void handleMouseInput()
        {
            //Handle mousestate
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

        private void handleKeyInput()
        {
            //handle debounced Q press
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
            }

            //handle directions
            if (currentKeyState.IsKeyDown(Keys.W)) movementVector.Y = -1;
            if (currentKeyState.IsKeyDown(Keys.S)) movementVector.Y = 1;
            if (currentKeyState.IsKeyDown(Keys.A)) movementVector.X = -1;
            if (currentKeyState.IsKeyDown(Keys.D)) movementVector.X = 1;

            lastKeyboardState = Keyboard.GetState();
        }
    }
}
