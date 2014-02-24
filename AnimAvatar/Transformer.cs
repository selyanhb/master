using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

namespace AnimAvatar
{
	public class Transformer
	{
		private Dictionary<Int32, List<Bone>> listesBones;

		public int initFrame { get; set; }

		public Transformer (string filename)
		{
			listesBones = ARTParser.parse (filename);
			initFrame = listesBones.Keys.Min ();

		}

		public List<Quaternion> getBoneTransfo (int frame)
		{
			if (!listesBones.Keys.Contains (frame)) {
				return null;
			}

			List<Quaternion> listeBoneTrans = new List<Quaternion> ();

			for (int i = 0; i < listesBones[frame].Count; i++) {
				listeBoneTrans.Add (transformQuaternion (listesBones[frame][i]));
			}

			return listeBoneTrans;
		}


        /*
         * Doesn't give the correct result
         */ 
		private Quaternion transformQuaternion (Bone bone)
		{
			float[,] mat = new float[3, 3] { { 1f, 0f, 0f }, { 0f, 0f, -1f }, { 0f, -1f, 0f}};
			if (bone.Number < 4 || bone.Number > 11) {
				mat = new float[3, 3] { { -1f, 0f, 0f }, { 0f, 0f, -1f }, { 0f, 1f, 0f}};
			}
			Quaternion CSChanger = QuaternionFromMatrix (mat);

			return CSChanger * Quaternion.Euler (bone.VectRot) * CSChanger ;
		}

        /*
         * Method found on a Unity Forum, not sure it works.
         */
		public static Quaternion QuaternionFromMatrix(float[,] m) 
		{ 
			Vector3 column1 = new Vector3 (m [0, 0], m [0, 1], m [0, 2]);
			Vector3 column2 = new Vector3 (m [1, 0], m [1, 1], m [1, 2]);
			
			return Quaternion.LookRotation(column2, column1);
		}

		public List<Vector3> getBoneRot (int frame)
		{
			if (!listesBones.Keys.Contains (frame)) {
				return null;
			}

			List<Vector3> listeBoneRot = new List<Vector3> ();

			for (int i = 0; i < listesBones[frame].Count; i++) {
				listeBoneRot.Add (transformRot (listesBones[frame][i]));
			}

			return listeBoneRot;
		}

        /*
		public List<float[,]> getBoneMat (int frame)
		{
			if (!listesBones.Keys.Contains (frame)) {
				return null;
			}
			
			List<float[,]> listeBoneMat = new List<float[,]> ();
			
			for (int i = 0; i < listesBones[frame].Count; i++) {
				listeBoneMat.Add (transformMat (listesBones[frame][i]));
			}
			
			return listeBoneMat;
		}
		
		private float[,] multiplyMat(float[,] m1, float[,] m2)
		{
			float[,] mat = new float[3,3];
			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 3; j++) {
					float sum = 0f;
					for (int k = 0; k < 3; k++) {
						sum+=m1[i,k]+m2[k,j];
					}
					mat[i,j] = sum;
				}
			}
			return mat;
		}
        */

		private Vector3 transformRot (Bone bone)
		{
			Vector3 rotation = new Vector3 ();
			switch (bone.Number) {
			case 0:
				rotation = new  Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 1:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 2:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 3:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 4:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 5:
				rotation = new Vector3 (bone.VectRot.x, -bone.VectRot.z, -bone.VectRot.y);
				break;
			case 6:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 7:
				rotation = new Vector3 (bone.VectRot.x, -bone.VectRot.z, -bone.VectRot.y);
				break;
			case 8:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 9:
				rotation = new Vector3 (bone.VectRot.x, -bone.VectRot.z, -bone.VectRot.y);
				break;
			case 10:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 11:
				rotation = new Vector3 (bone.VectRot.x, -bone.VectRot.z, -bone.VectRot.y);
				break;
			case 12:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 13:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 14:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 15:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 16:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 17:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 18:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			case 19:
				rotation = new Vector3 (-bone.VectRot.x, bone.VectRot.z, -bone.VectRot.y);
				break;
			default:
				break;
			}
			return rotation;
		}


	}
}
