using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Collections;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dialogueAudioMP3;
    [SerializeField][Range(0f, 1f)] private float soundVolume = 0.5f;
    [SerializeField] private float textDisplayDuration = 10f;
    [SerializeField] private float pauseDuration = 3f;
    [SerializeField] private float fadeOutDuration = 1f;
    private Story story;
    private bool isDialogueActive = false;
    private Coroutine typingCoroutine;
    private string currentSentence;
    private float timer;
    private bool isDisplaying = false;

    void Start()
    {
        if (inkJSON == null)
        {
            Debug.LogError("Ink JSON belum diassign di Inspector!");
            return;
        }

        dialoguePanel.SetActive(false);
        story = new Story(inkJSON.text);

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.volume = soundVolume;
        audioSource.loop = false;
        audioSource.clip = dialogueAudioMP3;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Tersentuh oleh: " + other.name);
        if (other.CompareTag("Player") && !isDialogueActive)
        {
            ShowDialogue();
        }
    }

    void ShowDialogue()
    {
        if (story == null)
        {
            Debug.LogError("Story tidak ditemukan! Pastikan Ink JSON terhubung.");
            return;
        }

        if (story.canContinue)
        {

            dialoguePanel.SetActive(true);
            currentSentence = story.Continue();
            isDialogueActive = true;
            isDisplaying = true;
            timer = 0f;

            if (dialogueAudioMP3 != null && audioSource != null)
            {
                audioSource.time = 0;
                audioSource.volume = soundVolume;
                audioSource.Play();
                Debug.Log("Playing audio: " + dialogueAudioMP3.name);
            }
            else
            {
                Debug.LogWarning("Audio clip or audio source is missing!");
            }

            StartTyping();
        }
        else
        {
            Debug.LogWarning("Story tidak dapat dilanjutkan!");
        }
    }

    void StartTyping()
    {
        dialogueText.text = "";
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeDialogue());
    }

    IEnumerator TypeDialogue()
    {
        foreach (char letter in currentSentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.09f);
        }
    }

    public void StartDialogueExternally()
    {
        if (!isDialogueActive && story.canContinue)
        {
            ShowDialogue();
        }
    }

    private void Update()
    {
        if (isDisplaying)
        {
            timer += Time.deltaTime;

            if (timer >= textDisplayDuration + pauseDuration && audioSource.isPlaying)
            {
                StartCoroutine(FadeOutAudio());
            }

            if (timer >= textDisplayDuration + pauseDuration + fadeOutDuration)
            {
                HideDialogue();
            }
        }

        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueDialogue();
        }
    }

    void ContinueDialogue()
    {
        if (story.canContinue)
        {
            currentSentence = story.Continue();
            StartTyping();
            Debug.Log("Next Dialogue: " + dialogueText.text);
        }
        else
        {
            HideDialogue();
        }
    }

    void HideDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        isDisplaying = false;

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        Debug.Log("Dialogue Ended");
    }

    IEnumerator FadeOutAudio()
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeOutDuration);
            yield return null;
        }

        audioSource.Stop();
    }
}
