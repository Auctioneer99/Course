using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class Interactor : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Interact();
            }
        }

        private void Interact()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100000f))
            {
                if (hit.collider != null)
                {
                    ILoggable l = hit.collider.GetComponent<ILoggable>();
                    if (l != null)
                    {
                        l.Log();
                    }
                }
            }
        }
    }
}
