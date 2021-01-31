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
}
