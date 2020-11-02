
using CART_457.EntitySystem.Scripts.Blueprints;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CART_457.EntitySystem
{
    public class InputState
    {
        public KeyState Q = new KeyState(Keys.Q);
        public KeyState W = new KeyState(Keys.W);
        public KeyState E = new KeyState(Keys.E);
        public KeyState R = new KeyState(Keys.R);
        public KeyState T = new KeyState(Keys.T);
        public KeyState Y = new KeyState(Keys.Y);
        public KeyState U = new KeyState(Keys.U);
        public KeyState I = new KeyState(Keys.I);
        public KeyState O = new KeyState(Keys.O);
        public KeyState P = new KeyState(Keys.P);
        public KeyState A = new KeyState(Keys.A);
        public KeyState S = new KeyState(Keys.S);
        public KeyState D = new KeyState(Keys.D);
        public KeyState F = new KeyState(Keys.F);
        public KeyState G = new KeyState(Keys.G);
        public KeyState H = new KeyState(Keys.H);
        public KeyState J = new KeyState(Keys.J);
        public KeyState K = new KeyState(Keys.K);
        public KeyState L = new KeyState(Keys.L);
        public KeyState Z = new KeyState(Keys.Z);
        public KeyState X = new KeyState(Keys.X);
        public KeyState C = new KeyState(Keys.C);
        public KeyState V = new KeyState(Keys.V);
        public KeyState B = new KeyState(Keys.B);
        public KeyState N = new KeyState(Keys.N);
        public KeyState M = new KeyState(Keys.M);
        public KeyState Num0 = new KeyState(Keys.KeyPad0);
        public KeyState Num1 = new KeyState(Keys.KeyPad1);
        public KeyState Num2 = new KeyState(Keys.KeyPad2);
        public KeyState Num3 = new KeyState(Keys.KeyPad3);
        public KeyState Num4 = new KeyState(Keys.KeyPad4);
        public KeyState Num5 = new KeyState(Keys.KeyPad5);
        public KeyState Num6 = new KeyState(Keys.KeyPad6);
        public KeyState Num7 = new KeyState(Keys.KeyPad7);
        public KeyState Num8 = new KeyState(Keys.KeyPad8);
        public KeyState Num9 = new KeyState(Keys.KeyPad9);
        public KeyState Up = new KeyState(Keys.Up);
        public KeyState Down = new KeyState(Keys.Down);
        public KeyState Left = new KeyState(Keys.Left);
        public KeyState Right = new KeyState(Keys.Right);
        public KeyState AltL = new KeyState(Keys.LeftAlt);
        public KeyState AltR = new KeyState(Keys.RightAlt);
        public KeyState ShiftL = new KeyState(Keys.LeftShift);
        public KeyState ShiftR = new KeyState(Keys.RightShift);
        public KeyState Space = new KeyState(Keys.Space);


        // private List<KeyState> _keyStates = new List<KeyState>(50);
   
        public void Update(KeyboardState keyboardState)
        {
            Q.Update(keyboardState);
            W.Update(keyboardState);
            E.Update(keyboardState);
            R.Update(keyboardState);
            T.Update(keyboardState);
            Y.Update(keyboardState);
            U.Update(keyboardState);
            I.Update(keyboardState);
            O.Update(keyboardState);
            P.Update(keyboardState);
            A.Update(keyboardState);
            S.Update(keyboardState);
            D.Update(keyboardState);
            F.Update(keyboardState);
            G.Update(keyboardState);
            H.Update(keyboardState);
            J.Update(keyboardState);
            K.Update(keyboardState);
            L.Update(keyboardState);
            Z.Update(keyboardState);
            X.Update(keyboardState);
            C.Update(keyboardState);
            V.Update(keyboardState);
            B.Update(keyboardState);
            N.Update(keyboardState);
            M.Update(keyboardState);
            Num0.Update(keyboardState);
            Num1.Update(keyboardState);
            Num2.Update(keyboardState);
            Num3.Update(keyboardState);
            Num4.Update(keyboardState);
            Num5.Update(keyboardState);
            Num6.Update(keyboardState);
            Num7.Update(keyboardState);
            Num8.Update(keyboardState);
            Num9.Update(keyboardState);

            ShiftL.Update(keyboardState);
            ShiftR.Update(keyboardState);
            AltL.Update(keyboardState);
            AltR.Update(keyboardState);
            Space.Update(keyboardState);
            
            Up.Update(keyboardState);
            Down.Update(keyboardState);
            Left.Update(keyboardState);
            Right.Update(keyboardState);
   

        }
    }
}