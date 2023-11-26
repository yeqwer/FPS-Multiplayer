using UnityEngine;

public class PistolController : Gun , IGun
{
    private void Start() 
    {
        SetStats();
    }

    public void SetStats() 
    {
        SelectGunActions.OnSelectPistol += ActiveGun;
        SelectGunActions.OnSelectM4A4 += DisactiveGun;
        
        cam = FindObjectOfType<Camera>();
    }
    
    private void OnDisable()
    {
        GunActions.OnGunFire -= FireCalulate;
        GunActions.OnGunReload -= Reload;
        GunActions.OnGunHide -= Hide;
        GunActions.OnGunShow -= Show;

        SelectGunActions.OnSelectPistol -= ActiveGun;
        SelectGunActions.OnSelectM4A4 -= DisactiveGun;
    }
}