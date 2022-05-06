using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using HarmonyLib;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.XR;

namespace BackJump.HarmonyPatches
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    internal class JumpPatch
    {
        public static bool ResetSpeed = false;
        private static void Prefix()
        {
            if (!BackJump.hascheckedban)
            {
                
                BackJump.hascheckedban = true;
            }
            if (BackJump.allowBackJump || BackJump.isbanned)
            {
                float Force = BackJump.multiplier.Value;
                GorillaLocomotion.Player player = GorillaLocomotion.Player.Instance;

                bool JumpButtonPressed = false;

                //Click Detection

                InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonValue);
                if (buttonValue != BackJump.rightHandLastState)
                {
                    if (buttonValue == true)
                    {
                        JumpButtonPressed = true;
                    }
                    else
                    {
                        JumpButtonPressed = false;
                    }
                    BackJump.rightHandLastState = buttonValue;
                }

                //Click Detection

                if (JumpButtonPressed)
                {
                    Vector3 Direction = player.rightHandTransform.transform.TransformDirection(Vector3.back).normalized;
                    player.bodyCollider.attachedRigidbody.velocity = player.bodyCollider.attachedRigidbody.velocity + (Direction * Force);
                }
            }
            
        }
    }
}
