using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public abstract class LocationView : MonoBehaviour
    {

        public BoardSideView BoardSideView { get; private set; }
        public Location Location { get; private set; }
    }
}
