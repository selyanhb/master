using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace AnimAvatar
{
    public class ARTParser
    {
        public static Regex frames = new Regex(".*fr ([0-9]+)");
        public static Regex times = new Regex("ts ([0-9.,]+)");

        public static Dictionary<Int32, List<Bone>> parse(string filename)
        {
            //Stopwatch sw = new Stopwatch();

            //sw.Start();

            TextReader tr;
            try
            {
                tr = new StreamReader(filename);
            }
            catch (Exception e)
            {
                throw new Exception("Error while opening file : " + e.Message);
            }

            Dictionary<Int32, List<Bone>> listesBones = new Dictionary<int, List<Bone>>();
            string line;

            while ((line = tr.ReadLine()) != null) 
            {
                int frame;
                try
                {
                    if (!line.Contains("fr"))  // "fr 258753"
                    {
                        line = tr.ReadLine();
                    }
                    frame = parseFrame(line);
                }
                catch (Exception e)
                {
                    throw new Exception("Error while reading frame : " + e.Message);
                }

                line = tr.ReadLine();  // "ts 34214.215192"

                try
                {
                    double time = parseTime(line);
                }
                catch (Exception e)
                {
                    throw new Exception("Error while reading time : " + e.Message);
                }

                line = tr.ReadLine(); // "6dcal 20" useless
                line = tr.ReadLine(); // "6d 17 [0 1.000][..." useless
                line = tr.ReadLine(); // "3d 0" useless
				if(line.Contains("3d 0")){
                	line = tr.ReadLine(); // "6dj 1 1 [0 20][0 1.000][-8.0677 -173.6933 995.9617 -0.0000 0.0000 -5.9987][0.994524 -0.104505 0.000000 0.104505 0.994524 -0.000000 0.000000 0.000000 1.000000][1 1.000][..."
				}

                try
                {
                    listesBones.Add(frame, parseData(line));
                }
                catch (Exception e)
                {
                    throw new Exception("Error while reading data : " + e.Message);
                }
            }

            //Console.WriteLine(sw.Elapsed);

            // close the stream
            tr.Close();

            return listesBones;

        }

        public static int parseFrame(string line)
        {
            Match m = frames.Match(line);
            int result = Int32.Parse(m.Groups[1].Value);
            return result;
        }

        public static double parseTime(string line)
        {
            Match m = times.Match(line);
            string value = m.Groups[1].Value;
            double result = Double.Parse(value);
            return result;
        }

        public static List<Bone> parseData(string line)
        {

            string[] liste = line.Split('[');

            //string index = liste[1].Replace("]", ""); // "0 20"
            //int numberOfBones = Int32.Parse(index.Split(' ')[1]);

            List<Bone> Bones = new List<Bone>();


            for (int i = 2; i < liste.Length; i++)
            {
                Bone bone = new Bone(0);

                string inputBoneNumber = liste[i]; // "0 1.000]"
                bone.Number = Int32.Parse(inputBoneNumber.Split(' ')[0]);

                i++;

                string inputBone6dVect = liste[i].Replace("]", ""); // "-8.0677 -173.6933 995.9617 -0.0000 0.0000 -5.9987"
                string[] stringBone6dVect = inputBone6dVect.Split(' ');
                if (stringBone6dVect.Length != 6)
                {
                    throw new IOException("6d Vector whith : " + stringBone6dVect.Length + "dimensions");
                }
                float[] boneVect = new float[6];

                for (int j = 0; j < 6; j++)
                {
                    string value = stringBone6dVect[j];
                    boneVect[j] = float.Parse(value);
                }

                bone.VectPos = new Vector3(boneVect[0], boneVect[1], boneVect[2]);
                bone.VectRot = new Vector3(boneVect[3], boneVect[4], boneVect[5]);

                i++;

                string inputBoneMat = liste[i].Replace("]", ""); // "0.994524 -0.104505 0.000000 0.104505 0.994524 -0.000000 0.000000 0.000000 1.000000"
                string[] stringBoneMat = inputBoneMat.Split(' ');
                if (stringBoneMat.Length != 9)
                {
                    throw new IOException("Mat whith : " + stringBoneMat.Length + "values");
                }
                float[,] boneMat =  new float[3,3];

                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        string value = stringBoneMat[3*j+k];
                        boneMat[j, k] = float.Parse(value);
                    }
                }

                bone.Mat = boneMat;
                Bones.Add(bone);
            }

            return Bones;
        }
    }



}
