using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    private int score = 0;

    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;

    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] MemoryCard originalCard;
    [SerializeField] Sprite[] images;

    public bool canReveal
    {
        get { return secondRevealed == null; }
    }

    public void Restart() {
        SceneManager.LoadScene("Scene");
    }
    public void CardRevealed(MemoryCard card)
    {
        if(firstRevealed == null) {
            firstRevealed = card;
        } else {
            secondRevealed = card;
            //Debug.Log("Match? " + (firstRevealed.ID == secondRevealed.ID));
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch() {
        if(firstRevealed.ID == secondRevealed.ID) {
            score++;
            //Debug.Log($"Score: {score}");
            scoreLabel.text = $"Score: {score}";
        } else {
            yield return new WaitForSeconds(0.5f);
            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }

        firstRevealed = null;
        secondRevealed = null;
    }
    // Start is called before the first frame update
    void Start()
    {
       // int id = Random.Range(0, images.Length);
       // originalCard.SetCard(id, images[id]);

        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;

                if(i == 0 && j == 0)
                {
                    card = originalCard;
                } else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    // Method to shuffle an array of numbers
    // This is an implmentation of the Knuth shuffle algorithm
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int temp = newArray[i];
            int rand = Random.Range(i, newArray.Length);
            newArray[i] = newArray[rand];
            newArray[rand] = temp;
        }
        return newArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
