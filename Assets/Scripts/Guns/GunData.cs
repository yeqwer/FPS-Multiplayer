using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{   
    public int id;
    public string gunName;
    public Image gunImage;
    public int maxBulletCount;
    public int currentBulletCount;
    public int gunMagazineCount;
    public float fireRate;
    public int gunDamage;
    public TypeFire fireType;
    public EGuns gunType;
}   