using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction
{
    public class GrabEvent : MonoBehaviour, ITransformer
    {
        public void BeginTransform()
        {
            Destroy(gameObject);
        }

        public void EndTransform()
        {
            
        }

        public void Initialize(IGrabbable grabbable)
        {
            
        }

        public void UpdateTransform()
        {
            
        }
    }
}
