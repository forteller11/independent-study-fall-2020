using System.Collections.Generic;
using System.Data;
using CART_457.EntitySystem.Scripts.EntityPrefab;
using OpenTK.Input;

namespace CART_457.EntitySystem
{
    public class InputState
    {
        public KeyState Q = new KeyState(Key.Q);
        public KeyState W = new KeyState(Key.W);
        public KeyState E = new KeyState(Key.E);
        public KeyState R = new KeyState(Key.R);
        public KeyState T = new KeyState(Key.T);
        public KeyState Y = new KeyState(Key.Y);
        public KeyState U = new KeyState(Key.U);
        public KeyState I = new KeyState(Key.I);
        public KeyState O = new KeyState(Key.O);
        public KeyState P = new KeyState(Key.P);
        public KeyState A = new KeyState(Key.A);
        public KeyState S = new KeyState(Key.S);
        public KeyState D = new KeyState(Key.D);
        public KeyState F = new KeyState(Key.F);
        public KeyState G = new KeyState(Key.G);
        public KeyState H = new KeyState(Key.H);
        public KeyState J = new KeyState(Key.J);
        public KeyState K = new KeyState(Key.K);
        public KeyState L = new KeyState(Key.L);
        public KeyState Z = new KeyState(Key.Z);
        public KeyState X = new KeyState(Key.X);
        public KeyState C = new KeyState(Key.C);
        public KeyState V = new KeyState(Key.V);
        public KeyState B = new KeyState(Key.B);
        public KeyState N = new KeyState(Key.N);
        public KeyState M = new KeyState(Key.M);
        public KeyState Num0 = new KeyState(Key.Number0);
        public KeyState Num1 = new KeyState(Key.Number1);
        public KeyState Num2 = new KeyState(Key.Number2);
        public KeyState Num3 = new KeyState(Key.Number3);
        public KeyState Num4 = new KeyState(Key.Number4);
        public KeyState Num5 = new KeyState(Key.Number5);
        public KeyState Num6 = new KeyState(Key.Number6);
        public KeyState Num7 = new KeyState(Key.Number7);
        public KeyState Num8 = new KeyState(Key.Number8);
        public KeyState Num9 = new KeyState(Key.Number9);
        public KeyState Up = new KeyState(Key.Up);
        public KeyState Down = new KeyState(Key.Down);
        public KeyState Left = new KeyState(Key.Left);
        public KeyState Right = new KeyState(Key.Right);
        public KeyState AltL = new KeyState(Key.AltLeft);
        public KeyState AltR = new KeyState(Key.AltRight);
        public KeyState ShiftL = new KeyState(Key.ShiftLeft);
        public KeyState ShiftR = new KeyState(Key.ShiftRight);
        public KeyState Space = new KeyState(Key.Space);


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