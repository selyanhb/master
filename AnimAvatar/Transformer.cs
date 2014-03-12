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
		private Vector3 initPos;

		public Transformer (string filename)
		{
			listesBones = ARTParser.parse (filename);
			initFrame = listesBones.Keys.Min ();
			initPos = listesBones [initFrame] [0].VectPos;
		}

		public List<Quaternion> getBoneTransfo (int frame)
		{
			if (!listesBones.Keys.Contains (frame)) {
				return null;
			}

			List<Quaternion> listeBoneTrans = new List<Quaternion> ();

			for (int i = 0; i < listesBones[frame].Count; i++) {
				listeBoneTrans.Add (transformQuaternion (listesBones [frame] [i]));
			}

			return listeBoneTrans;
		}

		public Vector3 getPosition (int frame)
		{
			if (!listesBones.Keys.Contains (frame)) {
				return new Vector3();
			}

			Vector3 pos = listesBones[frame][0].VectPos - initPos;

			return new Vector3 (-pos.x/500f, pos.z/500f, -pos.y/500f);
		}
	

		private Quaternion transformQuaternion (Bone bone)
		{

			Quaternion QuatAroundX = Quaternion.AngleAxis (bone.VectRot.x, new Vector3 (1f, 0f, 0f));
			Quaternion QuatAroundY = Quaternion.AngleAxis (bone.VectRot.y, QuatAroundX*(new Vector3 (0f, 0f, 1f)));
			Quaternion QuatAroundZ = Quaternion.AngleAxis (bone.VectRot.z, QuatAroundY*QuatAroundX*(new Vector3 (0f, -1f, 0f)));
			if (bone.Number == 5 || bone.Number == 7 || bone.Number == 9 || bone.Number == 11) {
				QuatAroundX = Quaternion.AngleAxis (bone.VectRot.x, new Vector3 (1f, 0f, 0f));
				QuatAroundY = Quaternion.AngleAxis (bone.VectRot.y, QuatAroundX*(new Vector3 (0f, 0f, -1f)));
				QuatAroundZ = Quaternion.AngleAxis (bone.VectRot.z, QuatAroundY*QuatAroundX*(new Vector3 (0f, -1f, 0f)));
				Quaternion QuatInverser = Quaternion.AngleAxis (180, QuatAroundZ*QuatAroundY*QuatAroundX*(new Vector3 (0f, 0f, 1f)));

				return  QuatInverser*QuatAroundZ*QuatAroundY * QuatAroundX;
			}
			return QuatAroundZ * QuatAroundY * QuatAroundX;
		}


		/*

		/*
	     * Method found on a Unity Forum, not sure it works.
		public static Quaternion QuaternionFromMatrix (float[,] m)
		{ 
			Vector3 column1 = new Vector3 (m [0, 0], m [0, 1], m [0, 2]);
			Vector3 column2 = new Vector3 (m [1, 0], m [1, 1], m [1, 2]);
				
			return Quaternion.LookRotation (column2, column1);
		}

		private Vector3 orientationVector (Bone bone)
		{
			float a = bone.VectRot.x * (float)Math.PI / 180f;
			float b = bone.VectRot.y * (float)Math.PI / 180f;
			float c = bone.VectRot.z * (float)Math.PI / 180f;
			Vector3 orientVect = new Vector3 (0, 0, 1);
			if (bone.Number == 4 || bone.Number == 6 || bone.Number == 8 || bone.Number == 10) {
				float x = (float)(Math.Cos (b) * Math.Cos (c) - Math.Cos (b) * Math.Sin (a) * Math.Sin (c));
				float y = (float)(-1f * Math.Sin (b) * Math.Sin (c));
				float z = (float)(-1f * Math.Cos (c) * Math.Sin (a) - Math.Cos (a) * Math.Cos (b) * Math.Sin (c));
				orientVect = new Vector3 (x, y, z);
			} else if (bone.Number == 5 || bone.Number == 7 || bone.Number == 9 || bone.Number == 11) {
				float x = (float)(-1f * Math.Cos (b) * Math.Cos (c) + Math.Cos (b) * Math.Sin (a) * Math.Sin (c));
				float y = (float)(-1f * Math.Sin (b) * Math.Sin (c));
				float z = (float)(Math.Cos (c) * Math.Sin (a) + Math.Cos (a) * Math.Cos (b) * Math.Sin (c));
				orientVect = new Vector3 (x, y, z);
			} else {
				float x = (float)(-Math.Sin (a) * Math.Sin (b));
				float y = (float)(-1f * Math.Cos (b));
				float z = (float)(-1f * Math.Cos (a) * Math.Sin (b));
				orientVect = new Vector3 (x, y, z);
			}

			return orientVect;
		}

		public List<Vector3> getBoneOrient (int frame)
		{
			if (!listesBones.Keys.Contains (frame)) {
				return null;
			}

			List<Vector3> listeBoneOrient = new List<Vector3> ();
			
			for (int i = 0; i < listesBones[frame].Count; i++) {
				listeBoneOrient.Add (orientationVector (listesBones [frame] [i]));
			}

			return listeBoneOrient;
		}


			
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
			*/


	}
}
