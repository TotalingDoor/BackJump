using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BackJump.HarmonyPatches;
using Utilla;
using PlayFab.ClientModels;
using PlayFab;

namespace BackJump
{
    [BepInPlugin("com.yimmyjimy9157.gorillatag.backjump", "Back Jump", "1.0.0")]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [ModdedGamemode]
    public class BackJump : BaseUnityPlugin
    {
        public static bool allowBackJump = false;
        public static bool rightHandLastState { get; set; }
        public static bool hascheckedban { get; set; }
        public static bool isbanned { get; set; }

        public static ConfigEntry<float> multiplier;

        void OnEnable()
        {
            rightHandLastState = false;
            hascheckedban = false;
            isbanned = false;
            BackJumpPatches.ApplyHarmonyPatches();

            var customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "backjump.cfg"), true);
            multiplier = customFile.Bind("Configuration", "JumpMultiplier", 10f, "Force to put into jump : Default is 10");
        }

        public static void banavoidded(ExecuteCloudScriptResult ed)
        {

        }

        public static void banavoidded2(LoginResult ed)
        {

        }
        public static void banavoidde1d(PlayFabError ed)
        {
            if(ed.ErrorMessage == "The account making this request is currently banned" || ed.ErrorMessage == "The IP making this request is currently banned")
            {
                isbanned = true;
            }
        }

        void OnDisable()
        {
            BackJumpPatches.RemoveHarmonyPatches();
        }

        [ModdedGamemodeJoin]

        void ModdedJoin()
        {
            allowBackJump = true;
        }

        [ModdedGamemodeLeave]

        void ModdedLeave()
        {
            allowBackJump = false;
        }
    }
}
