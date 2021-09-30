﻿using System;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using System.Reflection;
using UnityEngine.XR;
using Photon.Pun;
using System.IO;
using System.Net;
using Photon.Realtime;
using UnityEngine.Rendering;

namespace clippie
{
    [BepInPlugin("org.runtis165.monke.clippie", "clippie", "1.0.0.0")]
    public class MyPatcher : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("com.runtis165.monke.clippie");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]
    public class Class1
    {
      
        static bool down = false;
        static bool no = false;
        static bool yes = true;
        private static void Postfix(GorillaLocomotion.Player __instance)
        {

            if (!PhotonNetwork.CurrentRoom.IsVisible || !PhotonNetwork.InRoom)
            {
                List<InputDevice> list = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller, list);
                list[0].TryGetFeatureValue(CommonUsages.primaryButton, out down);

                if (down)
                {
                    if (!no)
                    {
                        foreach (MeshCollider mc in Resources.FindObjectsOfTypeAll<MeshCollider>())
                            mc.transform.localScale = mc.transform.localScale / 10000;
                        no = true;
                        yes = false;
                    }
                }
                else
                {
                    if (!yes)
                    {
                        foreach (MeshCollider mc in Resources.FindObjectsOfTypeAll<MeshCollider>())
                            mc.transform.localScale = mc.transform.localScale * 10000;
                        yes = true;
                        no = false;
                    }
                }
            }
            
        }
    }
}