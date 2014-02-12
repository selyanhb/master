using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Parser;


// Require these components when using this script
[RequireComponent(typeof(Animator))]
public class AvatarAnimator : MonoBehaviour
{

    private Animator anim;							// a reference to the animator on the character
    private int frame;
    private int initframe;

    private Dictionary<Int32, List<Bone>> listesBones;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.rootPosition = new Vector3();
        anim.rootRotation = new Quaternion();
        listesBones = ARTParser.parse(@"C:\Users\Tlatoc\Downloads\Motion\Motion\fast_kick.drf");
        initframe = listesBones.Keys.Min();
        frame = initframe;
    }

    // Update is called once per frame
    void Update()
    {
        if(listesBones.Keys.Contains(frame))
        {
            List<Bone> listeBoneCoord = listesBones[frame];
            List<HumanBodyBones> listeBone = initBoneList();

            transformByRotation(listeBone, listeBoneCoord);
        }

        frame++;
    }

    void transformByRotation(List<HumanBodyBones> listeBone, List<Bone> listeBoneCoord)
    {
        Transform transform = null;

        if (listeBone.Count != listeBoneCoord.Count)
        {
            throw new Exception("mismatched numbers of Bones and BonesCoord");
        }

        for (int i = 0; i < listeBone.Count; i++)
        {
            transform = anim.GetBoneTransform(listeBone[i]);
            transform.rotation = Quaternion.Euler(listeBoneCoord[i].VectRot);
        }
    }


    List<HumanBodyBones> initBoneList()
    {
        List<HumanBodyBones> listeBone = new List<HumanBodyBones>();
        listeBone.Add(HumanBodyBones.Hips);
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
        listeBone.Add(HumanBodyBones.LeftLowerLeg);
        listeBone.Add(HumanBodyBones.RightLowerLeg);
        listeBone.Add(HumanBodyBones.LeftFoot);
        listeBone.Add(HumanBodyBones.RightFoot);
        listeBone.Add(HumanBodyBones.LeftToes);
        listeBone.Add(HumanBodyBones.RightToes);
        return listeBone;
    }


}
