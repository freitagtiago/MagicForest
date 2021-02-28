using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    bool active = true;

    [SerializeField] float timeToLoad = 2f;
    [SerializeField] int nextScene;
    [SerializeField] bool isNear = false;
    [SerializeField] GameObject visualFX;

    private void Update()
    {
        if (!active) { return; }

        if (isNear && Input.GetKey(KeyCode.N))
        {
            active = false;
            StartCoroutine(ReachedAtEnd());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isNear = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isNear = false;
    }

    private IEnumerator ReachedAtEnd()
    {
        FindObjectOfType<Mover>().SetCanMove(false);
        GameObject fx = Instantiate(visualFX, transform.position, Quaternion.identity);
        fx.transform.parent = transform;
        yield return new WaitForSeconds(timeToLoad);
        SceneLoader.instance.LoadSceneNow(nextScene);
        FindObjectOfType<GameSession>().UpdateDisplays();
    }
}
