using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

using Random = UnityEngine.Random;

public enum CollectType
{
    Linear,
    RandomStart
}

public class P2DCollectManager : MonoBehaviour
{

    const int maxImage = 24;
    public List<Image> collectImages = new List<Image>(maxImage);
    public List<Image> collectImages2 = new List<Image>(maxImage);

    [SerializeField]
    GameObject collectObject;

    private void Awake()
    {
        for (int i = 0; i < maxImage; i++)
        {
            collectImages.Add(Instantiate(collectObject, transform).GetComponent<Image>());
            collectImages2.Add(Instantiate(collectObject, transform).GetComponent<Image>());
        }
    }

    void Start()
    {
       
    }

    public void Collect(Sprite sprite, Vector3 startCenter, Vector3 target, int number, AudioClip collectClip, CollectType type = CollectType.RandomStart, float startScale = 1, float endScale = 0.75f, int group = 1)
    {
        number = Mathf.Clamp(number, 1, maxImage);
        List<Image> currentGroup = group == 1 ? collectImages : collectImages2;
        Image temp;
        for (int i = 0; i < number; i++)
        {
            temp = currentGroup[i];

            try
            {
                temp.gameObject.SetActive(true);
                temp.sprite = sprite;
                temp.transform.localPosition = startCenter;
                if (type == CollectType.RandomStart)
                    temp.transform.localPosition += new Vector3(Random.Range(-250, 250), Random.Range(-400, 100), 0);

                temp.transform.DOKill(true);
                float randTime = Random.Range(0.5f, 1.2f);
                float delayTime = type == CollectType.RandomStart ? Random.Range(0.1f, 0.5f) : i / 15f;
                float scaleTelorance = Random.Range(-0.3f, 0.3f);
                temp.transform.localScale = new Vector3(startScale, startScale, startScale) + new Vector3(scaleTelorance, scaleTelorance, scaleTelorance);
                temp.transform.DOScale(endScale, randTime).SetDelay(delayTime);
                temp.transform.DOLocalMove(target, randTime).SetDelay(delayTime).OnComplete(() =>
                {
                    temp.gameObject.SetActive(false);
                    if (collectClip != null)
                        Setting.AudioPlayer.PlayOneShot(collectClip,1);
                });
                //collectImages[i].rectTransform.position
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }

            StartCoroutine(DeActiveAll(group,3.2f));
        }
    }


    IEnumerator DeActiveAll(int group,float second)
    {
        yield return new WaitForSeconds(second);

        List<Image> currentGroup = group == 1 ? collectImages : collectImages2;

        for (int i = 0; i < currentGroup.Count; i++)
        {
            currentGroup[i].gameObject.SetActive(false);
        }
    }
}
