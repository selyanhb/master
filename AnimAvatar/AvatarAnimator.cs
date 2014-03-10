using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using AnimAvatar;
using System.IO;


// Require these components when using this script
[RequireComponent(typeof(Animator))]
public class AvatarAnimator : MonoBehaviour
{
    public string filename = @"C:\Users\Tlatoc\Downloads\Motion\Motion\fast_kick.drf";

    private Animator anim;							// a reference to the animator on the character
    private int frame;
    private int initframe;
    private Transformer transformer;
    private TextWriter tr;
	private Vector3 initPosition;

    // Use this for initialization
    public void Start()
    {
        anim = GetComponent<Animator>();
        anim.rootPosition = new Vector3();
        anim.rootRotation = new Quaternion();
        transformer = new Transformer(filename);
        frame = transformer.initFrame;
		initPosition = anim.GetBoneTransform (HumanBodyBones.Hips).position;
    }

    // Update is called once per frame
    public void Update()
    {
//      List<Vector3> listeBoneCoord = transformer.getBoneOrient(frame);
//		List<float[,]> listeBoneMat = transformer.getBoneMat(frame);
		List<Quaternion> listeBoneTrans = transformer.getBoneTransfo (frame);
		Vector3 position = transformer.getPosition (frame);
		if(listeBoneTrans != null)
        {
            List<HumanBodyBones> listeBone = initBoneList();
			transformByQuaternion(listeBone, listeBoneTrans, position);
//          transformByBoneOrientation(listeBone, listeBoneCoord);
//			transformByMat(listeBone, listeBoneMat);
        }

        frame++;
    }

	public void transformByQuaternion(List<HumanBodyBones> listeBone, List<Quaternion> listeBoneTrans, Vector3 position){
		if (listeBone.Count != listeBoneTrans.Count)
		{
			throw new Exception("mismatched numbers of Bones and BonesTrans");
		}

		Transform transform = anim.GetBoneTransform (listeBone [0]);
		transform.position = initPosition + position;

		for (int i = 1; i < 20; i++) {
			transform = anim.GetBoneTransform (listeBone [i]);
			transform.rotation = listeBoneTrans [i];
		}
	}

	/*

    public void transformByBoneOrientation(List<HumanBodyBones> listeBone, List<Vector3> listeBoneCoord)
    {

        if (listeBone.Count != listeBoneCoord.Count)
        {
            throw new Exception("mismatched numbers of Bones and BonesCoord");
        }

		for (int i = 1; i < 20; i++)
        {
	            Transform transform = anim.GetBoneTransform(listeBone[i]);
				Vector3 precVector = transform.up;
				//tr.WriteLine("["+i+"] Loaded transformation :"+ anim.GetBoneTransform(listeBone[i]).rotation.x + "  "+ anim.GetBoneTransform(listeBone[i]).rotation.y+" "+anim.GetBoneTransform(listeBone[i]).rotation.z);
				//tr.WriteLine("["+i+"] PrecVector :"+precVector.ToString());
				transform.rotation = Quaternion.FromToRotation(precVector, listeBoneCoord[i])*transform.rotation;
				//tr.WriteLine("["+i+"] Uploaded transformation :"+ transform.rotation.x + "  "+ transform.rotation.y+" "+transform.rotation.z);
        }
    }

	public void transformByMat(List<HumanBodyBones> listeBone, List<float[,]> listeBoneMat)
	{
		if (listeBone.Count != listeBoneMat.Count)
		{
			throw new Exception("mismatched numbers of Bones and BonesCoord");
		}
		
		for (int i = 1; i < 2; i++)
		{
			Transform transform = anim.GetBoneTransform(listeBone[i]);
			transform.rotation = QuaternionFromMatrix(listeBoneMat[i]);
		}
	}
*/

    List<HumanBodyBones> initBoneList()
    {
        List<HumanBodyBones> listeBone = new List<HumanBodyBones>();
        /*listeBone.Add(HumanBodyBones.Hips);
        listeBone.Add(HumanBodyBones.Spine);
        listeBone.Add(HumanBodyBones.Neck);
        listeBone.Add(HumanBodyBones.Head);
        listeBone.Add(HumanBodyBones.LeftShoulder);
        listeBone.Add(HumanBodyBones.RightShoulder);
        listeBone.Add(HumanBodyBones.LeftUpperArm);
        listeBone.Add(HumanBodyBones.RightUpperArm);
        listeBone.Add(HumanBodyBones.LeftLowerArm);
        listeBone.Add(HumanBodyBones.RightLowerArm);
        listeBone.Add(HumanBodyBones.LeftHand);
        listeBone.Add(HumanBodyBones.RightHand);
        listeBone.Add(HumanBodyBones.LeftUpperLeg);
        listeBone.Add(HumanBodyBones.RightUpperLeg);
		listeBone.Add(HumanBodyBones.LeftUpperLeg);
		listeBone.Add(HumanBodyBones.RightUpperLeg);
		listeBone.Add(HumanBodyBones.LeftLowerLeg);
		listeBone.Add(HumanBodyBones.RightLowerLeg);
		listeBone.Add(HumanBodyBones.LeftFoot);
		listeBone.Add(HumanBodyBones.RightFoot);
		*/
		listeBone.Insert(0, HumanBodyBones.Hips);
		listeBone.Insert(1, HumanBodyBones.Hips);
		listeBone.Insert(2, HumanBodyBones.Spine);
		listeBone.Insert(3, HumanBodyBones.Head);
		listeBone.Insert(4, HumanBodyBones.LeftShoulder);
		listeBone.Insert(5, HumanBodyBones.RightShoulder);
		listeBone.Insert(6, HumanBodyBones.LeftUpperArm);
		listeBone.Insert(7, HumanBodyBones.RightUpperArm);
		listeBone.Insert(8, HumanBodyBones.LeftLowerArm);
		listeBone.Insert(9, HumanBodyBones.RightLowerArm);
		listeBone.Insert(10, HumanBodyBones.LeftHand);
		listeBone.Insert(11, HumanBodyBones.RightHand);
		listeBone.Insert(12, HumanBodyBones.LeftUpperLeg);
		listeBone.Insert(13, HumanBodyBones.RightUpperLeg);
		listeBone.Insert(14, HumanBodyBones.LeftUpperLeg);
		listeBone.Insert(15, HumanBodyBones.RightUpperLeg);
		listeBone.Insert(16, HumanBodyBones.LeftLowerLeg);
		listeBone.Insert(17, HumanBodyBones.RightLowerLeg);
		listeBone.Insert(18, HumanBodyBones.LeftFoot);
		listeBone.Insert(19, HumanBodyBones.RightFoot);
        return listeBone;
    }


    /*
     * Method found on a Unity Forum, not sure it works.

	public static Quaternion QuaternionFromMatrix(float[,] m) 
	{ 
		Vector3 forward = new Vector3 (m [0, 0], m [1, 0], m [2, 0]);
		Vector3 upward = new Vector3 (m [0, 1], m [1, 1], m [2, 1]);
		return Quaternion.LookRotation(forward, upward);
	}

  */
}
