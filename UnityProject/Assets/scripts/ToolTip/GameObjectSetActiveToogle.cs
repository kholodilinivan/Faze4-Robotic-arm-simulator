using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSetActiveToogle : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public void Toogle(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Toogle()
    {
        var result = !gameObjects[0].activeSelf;
        gameObjects.ForEach(x => x.SetActive(result));
    }
}
