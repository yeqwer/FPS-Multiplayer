using System;


public static class SelectGunActions
{
    public static Action OnSelectM4A4;
    public static Action OnSelectPistol;
}

public static class GunActions
{
    public static Action OnGunFire;
    public static Action OnGunReload;
    public static Action OnGunHide;
    public static Action OnGunShow;
}
public static class PlayerActions
{
    public static Action GameOver;
    public static Action GameWin;
    public static Action StartGame;
    public static Action PauseGame;
}
public static class EnemyActions
{
    public static Action EnemyKilled;
}