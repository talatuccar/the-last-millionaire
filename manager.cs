using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;
using System.Linq;

public class manager : MonoBehaviour
{
    public TextMeshProUGUI question_name, cevapa, cevapb, cevapc, cevapd, timer_txt, increase_ask, score_text, highestValue_text;
    AudioSource voice;
    public AudioClip correctVoice;
    public AudioClip wrongVoice;
    public AudioClip askVoice;
    public AudioClip startVoice;
    public GameObject panel;
    public TextMeshProUGUI eleminated_score;
    public int answer;  
    public List<Button> buttons;
    public InputField name_field;
    public Button changebutton;
    public Button extratime;
    public Button delete_choice;
    public Button paid_jokerr;
    public Button[] buttonns;
    int valueholder;
    string nameholder;

    public float timer;
    public int score;
    int increase_number = 0;
    public List<questions> questions;

    public List<bool> asked;
    void Start()
    {
       
        PlayerPrefs.Save();
        Time.timeScale = 0;
        voice = GetComponent<AudioSource>();
        for (int i = 0; i < questions.Count; i++)

            asked.Add(false);


        add_Ask();
    }
    void Update()
    {
        timerfonc();
    }
    public void add_Ask()
    {
        StartCoroutine(ask_voice_time());
        add_deletedbutton();

        int number_ofask = Random.Range(0, asked.Count);
        if (asked[number_ofask] == false)
        {
            asked[number_ofask] = true;
            timer = 30;
            increase_number++;
            increase_ask.text = increase_number.ToString();
            question_name.text = questions[number_ofask].name_ask;
            cevapa.text = questions[number_ofask].answera;
            cevapb.text = questions[number_ofask].answerb;
            cevapc.text = questions[number_ofask].answerc;
            cevapd.text = questions[number_ofask].answerd;
            answer = questions[number_ofask].cevap;
            add_coloredbutton();
            score += questions[number_ofask].scoree;
            score_text.text = score.ToString();
        }
        else
        {
            add_Ask();
        }
    }
    public void iscorrect(int value)
    {
        if (value == answer)

        {
            StartCoroutine(change_color(answer));
            voice.Stop();

            voice.PlayOneShot(correctVoice);

            Invoke("add_Ask", 2f);

        }
        else if (value != answer)
        {


            buttonns[answer - 1].image.color = Color.green;
            buttonns[value - 1].image.color = Color.red;
            voice.Stop();
            voice.PlayOneShot(wrongVoice);


            Invoke("eleminatedpanel_active", 3);
            paid_jokerr.gameObject.SetActive(false);
            delete_choice.gameObject.SetActive(false);

        }


    }
    public void timerfonc()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            timer_txt.text = timer.ToString("00");
        }
        if (timer < 0)
        {
            eleminated_score.text = score.ToString() + "             TIME IS UP.  ";
            panel.gameObject.SetActive(true);
        }
    }

    public void change_ask()
    {
        changebutton.gameObject.SetActive(false);

        add_Ask();
    }
    public void extra_time()
    {
        timer += 60;
        extratime.gameObject.SetActive(false);
        timerfonc();
    }
    public void eleminatedpanel_active()
    {
        if (PlayerPrefs.HasKey("firstScore"))
        {
            if (PlayerPrefs.GetInt("firstScore") < int.Parse(score_text.text))
            {
                highestValue_text.text = name_field.text + "  " + score.ToString();
                eleminated_score.text = name_field.text + "  " + score.ToString();
                panel.gameObject.SetActive(true);
                PlayerPrefs.SetInt("firstScore", int.Parse(score_text.text));
                PlayerPrefs.SetString("name", name_field.text);


            }
            else
            {
                valueholder = PlayerPrefs.GetInt("firstScore");
                nameholder = PlayerPrefs.GetString("name");
                eleminated_score.text = name_field.text + "  " + score.ToString();
                highestValue_text.text = nameholder + "  " + valueholder.ToString();
                panel.gameObject.SetActive(true);

            }
        }
        else
        {
            PlayerPrefs.SetInt("firstScore", int.Parse(score_text.text));
            PlayerPrefs.SetString("name", name_field.text);
            highestValue_text.text = name_field.text + "  " + score.ToString();
            eleminated_score.text = name_field.text + "  " + score.ToString();
            panel.gameObject.SetActive(true);


        }
    }

    public void paid_joker()
    {
        if (score >= 100)
        {
            score -= 100;
            paid_jokerr.gameObject.SetActive(false);

            add_Ask();
        }
    }

    IEnumerator ask_voice_time()
    {
        yield return new WaitForSeconds(4f);
        voice.PlayOneShot(askVoice);
    }

    public void out2choice()
    {
        for (int i = 0; i < 2; i++)
        {
            int nmbr = Random.Range(0, buttonns.Length);
            if (nmbr + 1 != answer)
            {

                buttonns[nmbr].gameObject.SetActive(false);
            }
            if (nmbr + 1 == answer)
            {
                i = 0;
            }

        }
        delete_choice.gameObject.SetActive(false);
    }
    public void add_deletedbutton()
    {

        buttonns[0].gameObject.SetActive(true);
        buttonns[1].gameObject.SetActive(true);
        buttonns[2].gameObject.SetActive(true);
        buttonns[3].gameObject.SetActive(true);
    }

    IEnumerator change_color(int answer)
    {

        buttonns[answer - 1].image.color = Color.green;
        yield return new WaitForSeconds(2);
    }

    public void add_coloredbutton()
    {
        buttonns[0].image.color = Color.white;
        buttonns[1].image.color = Color.white;
        buttonns[2].image.color = Color.white;
        buttonns[3].image.color = Color.white;
    }

    public void close_input()
    {
        name_field.gameObject.SetActive(false);
        Time.timeScale = 1;
        voice.PlayOneShot(startVoice);

    }


}






