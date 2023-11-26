using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    public Animator animator; 
    public ParticleSystem firePartical;
    public GameObject gun;
    [HideInInspector] public Camera cam;
    [HideInInspector] public bool readyToFire;
    private float _lastShootTime = 0;

    public void FireCalulate()
    {
        if (gunData.currentBulletCount > 0) 
        {
            switch (gunData.fireType)
            {
                case TypeFire.Auto:
                    Fire();
                    break;

                case TypeFire.Birst:
                    Debug.Log("NOT WORKING");
                    break;

                case TypeFire.Single:
                    Debug.Log("NOT WORKING");
                    break;
            }
        }
        else
        {
            Reload();
        }
    }
    
    private void Fire()
    {
        if (readyToFire & Time.time > _lastShootTime + gunData.fireRate) 
        {  
            animator.SetTrigger("Shoot");
            
            firePartical.Play();

            FireRay();

            gunData.currentBulletCount--;

            _lastShootTime = Time.time; 
        }
        
    }

    public void FireRay()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject obj = hit.collider.gameObject;

            if (obj.CompareTag("Enemy") & obj.transform.parent != null)
            {
                
                obj.GetComponentInParent<Enemy>().Damage(gunData.gunDamage, hit.collider);
            }
        }
    }

    public void Reload()
    {
        if(gunData.gunMagazineCount > 0 &  gunData.currentBulletCount != gunData.maxBulletCount)
        { 
            readyToFire = false;

            animator.SetTrigger("Reload");
            gunData.currentBulletCount = gunData.maxBulletCount;

            gunData.gunMagazineCount--;
        }
        else
        {
            Debug.Log("ANIMOBULE!");
        }
    }
    
    public void Hide()
    {   
        gun.SetActive(false);

        if (readyToFire)
        {
            readyToFire = false;
            animator.SetTrigger("Hide");
        }
    }

    public void Show()
    {
        gun.SetActive(true);

        if (!readyToFire)
        {
            animator.SetTrigger("Show");
        }  
    }

    private void OnEnable()
    {
        animator.SetTrigger("Show");
        
        GunActions.OnGunFire += FireCalulate;
        GunActions.OnGunReload += Reload;
        GunActions.OnGunHide += Hide;
        GunActions.OnGunShow += Show;
    }

    public void ActiveGun()
    {
        //gun.SetActive(true);
        Show();
    }

    public void DisactiveGun()
    {
        // gun.SetActive(false);
        Hide();
    }
}