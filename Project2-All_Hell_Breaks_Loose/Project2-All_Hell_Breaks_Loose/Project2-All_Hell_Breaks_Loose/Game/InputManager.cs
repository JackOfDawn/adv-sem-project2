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
        public event DirectionPressedDelegate Event_MovementPressed;
        public event ButtonPressedDelegate Event_SwitchWeapons;
        public event ButtonPressedDelegate Event_Shoot;
        public event MousePositionDelegate Event_UpdateCursorLoc;

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
        
        public void Update(GameTime gameTime)
        {
            shootActionPressed = false;
            switchWeaponActionPressed = false;

            movementVector = Vector2.Zero;


            HandleKeyInput();
            HandleMouseInput();

            //call events
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

        private void HandleMouseInput()
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

        private void HandleKeyInput()
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
