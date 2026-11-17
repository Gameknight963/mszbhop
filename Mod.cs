using MelonLoader;
using UnityEngine;

namespace mszbhop
{
    public class Mod : MelonMod
    {
        GameObject? kiri;

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName != "Version 1.9 POST") return;
            kiri = GameObject.Find("Kiri");
            Component comp = kiri.GetComponent<kiriMoveBasic>();
            UnityEngine.Object.Destroy(comp);
            kiri.AddComponent<BhopMovement>();
        }
    }
}
 