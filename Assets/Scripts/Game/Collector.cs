using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] GameObject CoinPF;
    float gameOverSeconds = 1;
    private void OnTriggerEnter(Collider other)
    {
        Brick brick = other.GetComponent<Brick>();
        if (brick)
        {
            InsMoneyAndGotoTop(other.transform.position);
            Destroy(brick.gameObject);
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameOverSeconds -= Time.deltaTime;
            if (gameOverSeconds < 0)
            {
                Destroy(other.gameObject);
                GameController.Instance.GameOver();
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Brick brick = other.gameObject.GetComponent<Brick>();
        if (brick)
        {
            InsMoneyAndGotoTop(other.transform.position);
            Destroy(brick.gameObject);
        }
    }
    private void InsMoneyAndGotoTop(Vector3 pos)
    {
        GameObject money = Instantiate(CoinPF, pos, Quaternion.identity);
        // RectTransformUtility.ScreenPointToWorldPointInRectangle(CanvasController.Instance.hud.GetComponent<RectTransform>(), CanvasController.Instance.hudCoin.GetComponent<RectTransform>().position, CameraController.Instance.GetCamera(), out Vector3 worldPoint);

        // RectTransformUtility.ScreenPointToWorldPointInRectangle(CanvasController.Instance.GetComponent<RectTransform>(), CanvasController.Instance.hudCoin.GetComponent<RectTransform>().position, CameraController.Instance.GetCamera(), out Vector3 worldPoint);

        // print(worldPoint);
        StartCoroutine(localCoroutine());
        IEnumerator localCoroutine()
        {
            float time = 0;
            float duration = 1;
            float t = 1;
            yield return new WaitForSeconds(1);
            while (time < duration)
            {
                time += Time.deltaTime;
                t = time / duration;
                Vector3 toPoint = CameraController.Instance.GetCamera().ScreenToWorldPoint(CanvasController.Instance.hudCoin.transform.position + new Vector3(0, 0, 10));
                money.transform.position = Vector3.Lerp(money.transform.position, toPoint, t);
                // money.transform.localScale = Vector3.Lerp(money.transform.localScale, Vector3.one * 0.4f, t);
                // Mathf.Lerp(0, 1, t);
                yield return null;
            }
            GameController.Instance.Coin++;
            Destroy(money.gameObject);
        }
    }
}
