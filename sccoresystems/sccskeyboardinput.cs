using System;
using SharpDX.DirectInput;

namespace sccoresystems.sccsconsole
{
    public class sccsconsolekeyboardinput
    {
        public SharpDX.DirectInput.Keyboard Keyboard;
        DirectInput directInput = new DirectInput();
        public KeyboardState KeyboardState;
        int InitializeKeyboardAuth = 0;

        public sccsconsolekeyboardinput()
        {
            InitializeKeyboard();
            KeyboardState = new KeyboardState();
        }

        public int InitializeKeyboard()
        {
            InitializeKeyboardAuth = 1;
            try
            {
                directInput = new DirectInput();
                Keyboard = new SharpDX.DirectInput.Keyboard(directInput);
                Keyboard.Properties.BufferSize = 128;
                Keyboard.Acquire();
            }
            catch
            {

                InitializeKeyboardAuth = 0;
            }
            return InitializeKeyboardAuth;
        }


        public bool ReadKeyboard()
        {
            var resultCode = SharpDX.DirectInput.ResultCode.Ok;

            try
            {
                Keyboard.GetCurrentState(ref KeyboardState);
            }
            catch (SharpDX.SharpDXException ex)
            {
                resultCode = ex.Descriptor;
            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "cannot get keyboard info 00: " + ex.ToString() + "", "Oculus error", 0);
                return false;
            }

            if (resultCode == SharpDX.DirectInput.ResultCode.InputLost || resultCode == SharpDX.DirectInput.ResultCode.NotAcquired)
            {
                try
                {
                    Keyboard.Acquire();
                }
                catch (Exception ex)
                {
                    Program.MessageBox((IntPtr)0, "cannot get keyboard info 01: " + ex.ToString() + "", "Oculus error", 0);
                }

                return true;
            }

            if (resultCode == SharpDX.DirectInput.ResultCode.Ok)
            {
                return true;
            }

            return false;
        }
    }
}
