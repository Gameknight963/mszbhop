using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace mszbhop
{

    public class Mod : MelonMod
    {
        public static GameObject Kiri;

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName != "Version 1.9 POST") return;
            Kiri = GameObject.Find("Kiri");
            Component comp = Kiri.GetComponent<kiriMoveBasic>();
            UnityEngine.Object.Destroy(comp);
            Kiri.AddComponent<BhopMovement>();

            OOBProtection oob = GameObject.FindObjectOfType<OOBProtection>();
            oob.limit = -300;
        }
    }
}
