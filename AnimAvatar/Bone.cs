using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AnimAvatar
{
    public class Bone
    {
        public int Number { get; set; }
        public Vector3 VectPos;
        public Vector3 VectRot;
        public float[,] Mat { get; set; }

		public Bone(int number){
			Number = number;
			VectPos = new Vector3 ();
			if (number < 4) {
				VectRot = new Vector3 (0f, 0f, 1f);
			} else if (number < 12) {
				VectRot = new Vector3 (1f, 0f, 0f);
			} else {
				VectRot = new Vector3 (-1f, 0f, 0f);
			}
		}

        public string getString()
        {
            string result = "Numéro : " + Number + " Vecteur Position : [";
            for (int i = 0; i < 3; i++)
            {
                result += " " + VectPos[i];
            }
            result = "] Vecteur Rotation : [";
            for (int i = 0; i < 3; i++)
            {
                result += " " + VectRot[i];
            }
            result += "] Matrice = [";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result += " " + Mat[i,j];
                }
            }
            return result + "];";
        }


    }
}
