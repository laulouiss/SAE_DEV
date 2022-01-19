using System;
using System.Collections.Generic;
using System.Text;

namespace SAE1._01_2.Core
{
    public class ChoixPerso
    {
        public static int CouleurPlayer = 0;
        public static String CouleurPerso;

        public static int direction = 0;
        public static String ChoixCouleur(int choix)
        {
            String couleur = "";
            switch (choix)
            {
                case 0:
                    couleur = "red";
                    break;
                case 1:
                    couleur = "blue";
                    break;
                case 2:
                    couleur = "green";
                    break;
                case 3:
                    couleur = "yellow";
                    break;
            }
            return couleur;
        }

        public static String ChoixDirection(int choix)
        {
            String direction = "";
            if (choix == 0)
            {
                direction = "Left";
            }
            else
                direction = "Right";
            return direction;
        }
    }
}
