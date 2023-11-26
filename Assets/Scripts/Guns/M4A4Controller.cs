using UnityEngine;

public class M4A4Controller : Gun , IGun
{
    private void Start() 
    {
        SetStats();
    }

    public void SetStats() 
    {
        SelectGunActions.OnSelectM4A4 += ActiveGun;
        SelectGunActions.OnSelectPistol += DisactiveGun;
        
        cam = FindObjectOfType<Camera>();
    }
    
    private void OnDisable()
    {
        GunActions.OnGunFire -= FireCalulate;
        GunActions.OnGunReload -= Reload;
        GunActions.OnGunHide -= Hide;
        GunActions.OnGunShow -= Show;

        SelectGunActions.OnSelectM4A4 -= ActiveGun;
        SelectGunActions.OnSelectPistol -= DisactiveGun;
    }
}
