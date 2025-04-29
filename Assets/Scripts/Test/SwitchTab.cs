using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SwitchTab : MonoBehaviour
{
    public RectTransform _UIpos;
    public async void SwitchingTab()
    {
        _UIpos.position = new Vector2(_UIpos.position.x - 1080f, _UIpos.position.y);
        await Task.Delay(1000);
        _UIpos.position = new Vector2(_UIpos.position.x + 1080f, _UIpos.position.y);
        await Task.Delay(1000);
    }
}
