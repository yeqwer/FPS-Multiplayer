using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    [SerializeField] private Gun gun;
    
    private void Awake()
    {
        gun = GetComponentInParent<Gun>();
    }

    public void ReadyToFire() 
    {
        if(gun)
        {
            gun.readyToFire = true;
        }
    }
}