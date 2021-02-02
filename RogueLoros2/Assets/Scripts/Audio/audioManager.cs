using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager instance { get; private set; }

    public AudioSource FootstepAudio;

    public AudioSource attackEnemy;

    public AudioSource getMoney;

    public AudioSource getLife;

    public AudioSource loseLife;

    public AudioSource lose;

    public AudioSource buildAudio;

    public AudioSource cantBuildAudio;

    public AudioSource getNewTiles;

    public AudioSource getTile;

    public AudioSource spendMoney;

    public AudioSource musica1;

    public AudioSource musica2;
    private void Awake()
    {
        //lida com duplicatas de instancia
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void playFootstep()
    {
        FootstepAudio.PlayOneShot(FootstepAudio.clip,FootstepAudio.volume);
    }
    public void enemyAttackAudio()
    {
        attackEnemy.PlayOneShot(attackEnemy.clip, attackEnemy.volume);
    }
    public void getMoneyAudio()
    {
        getMoney.PlayOneShot(getMoney.clip, getMoney.volume);
    }
    public void getLifeAudio()
    {
        getLife.PlayOneShot(getLife.clip, getLife.volume);
    }
    public void loseLifeAudio()
    {
        loseLife.PlayOneShot(loseLife.clip, loseLife.volume);
    }
    public void loseAudio()
    {
        lose.PlayOneShot(lose.clip, lose.volume);
    }
    public void audioBuild()
    {
        buildAudio.PlayOneShot(buildAudio.clip, buildAudio.volume);
    }
    public void audioBuildFail()
    {
        cantBuildAudio.PlayOneShot(cantBuildAudio.clip, cantBuildAudio.volume);
    }
    public void refreshTilesAudio()
    {
        getNewTiles.PlayOneShot(getNewTiles.clip, getNewTiles.volume);
    }
    public void chooseTilePreview()
    {
        getTile.PlayOneShot(getTile.clip, getTile.volume);
    }
    public void loseMoney()
    {
        spendMoney.PlayOneShot(spendMoney.clip, spendMoney.volume);
    }
    public void changeToBossMusic()
    {
        //musica1.FadeOut(1);
        //musica2.FadeIn(1,0.5f);
        musica1.Stop();
        musica2.Play();
    }
}
namespace UnityEngine
{
    public static class AudioSourceExtensions
    {
        public static void FadeOut(this AudioSource a, float duration)
        {
            a.GetComponent<MonoBehaviour>().StartCoroutine(FadeOutCore(a, duration));
        }

        private static IEnumerator FadeOutCore(AudioSource a, float duration)
        {
            float startVolume = a.volume;

            while (a.volume > 0)
            {
                a.volume -= startVolume * Time.deltaTime / duration;
                yield return new WaitForEndOfFrame();
            }

            a.Stop();
            a.volume = startVolume;
        }

        public static void FadeIn(this AudioSource a, float duration, float finalVolume2)
        {
            a.GetComponent<MonoBehaviour>().StartCoroutine(FadeInCore(a, duration, finalVolume2));
        }

        private static IEnumerator FadeInCore(AudioSource a, float duration, float finalVolume)
        {
            float startVolume = a.volume;

            while (a.volume < finalVolume)
            {
                a.volume += startVolume * Time.deltaTime / duration;
                yield return new WaitForEndOfFrame();
            }

            a.Stop();
            a.volume = startVolume;
        }
    }
}

