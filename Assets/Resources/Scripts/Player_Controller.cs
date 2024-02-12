using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonEscape
{
    public class Player_Controller : MonoBehaviour
    {
        private List<Area> areasPlayerList = new List<Area>();
        private Image backgroundImage;

        public void GetAreaList(List<Area> areas)
        {
            areasPlayerList = areas;
        }

        public void LoadArea(int index)
        {
            backgroundImage ??= GetComponentInChildren<Canvas>().GetComponentInChildren<Image>(); //Use a null-coalescing assignment to keep the if check simple
            backgroundImage.sprite = areasPlayerList[index].areaSprite;
        }
    }
}
