using UnityEngine;

namespace UI
{
    public class StartTankTurret : MonoBehaviour
    {
        public float angle;
        public float target;

        private Vector3 _originForward;
        private Vector3 CurForward => transform.forward;
        
        void Start()
        {
            _originForward = CurForward;
        }

        void Update()
        {
            
            var f = Vector3.Angle(_originForward, CurForward);
            if (f >= target)
            {
                angle = -angle;
            }
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles +  new Vector3(0,Time.deltaTime * angle, 0));
        }
    }
}

