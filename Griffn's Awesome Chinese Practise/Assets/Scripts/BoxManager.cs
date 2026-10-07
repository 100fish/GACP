using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private GameObject FlashBox;
    [SerializeField] private GameObject FlashBoxUI;

    [SerializeField] private MenuManager menuManager;
    [SerializeField] private VocabManager vocabManager;
    [SerializeField] private GameObject worldCanvas;
    
    [SerializeField] private Transform flashBoxSpawn;

    [SerializeField] private Camera myCamera;

    public bool[] including;



    private string[,] myVocab;


    public enum BoxState
    { 
        Pinyin,
        Characters,
        Meaning
    }

    private BoxState boxState;
    [SerializeField] private BoxState startingState;

    [SerializeField] Transform[] boxPoints = new Transform[3];


    #region Inputsetup
    private InputAsset playerInput;
    private void Awake()
    {
        
        boxState = startingState;

        playerInput = new InputAsset();
    }

    private void OnEnable()
    {
        playerInput.Enable();

        playerInput.Standard.Pinyin.started += InputPinyin;
        playerInput.Standard.Character.started += InputCharacter;
        playerInput.Standard.Meaning.started += InputMeaning;
        playerInput.Standard.Left.started += InputLeft;
        playerInput.Standard.Right.started += InputRight;
        playerInput.Standard.Next.started += InputNext;
    }

    private void OnDisable()
    {
        playerInput.Disable(); 
        
        playerInput.Standard.Pinyin.started -= InputPinyin;
        playerInput.Standard.Character.started -= InputCharacter;
        playerInput.Standard.Meaning.started -= InputMeaning;
        playerInput.Standard.Left.started -= InputLeft;
        playerInput.Standard.Right.started -= InputRight;
        playerInput.Standard.Next.started -= InputNext;
    }
    #endregion Inputsetup

    private void InputPinyin(InputAction.CallbackContext context)
    {
        GoTo(BoxState.Pinyin);
    }
    private void InputCharacter(InputAction.CallbackContext context)
    {
        GoTo(BoxState.Characters);
    }
    private void InputMeaning(InputAction.CallbackContext context)
    {
        GoTo(BoxState.Meaning);
    }
    private void InputLeft(InputAction.CallbackContext context)
    {
        int newState = (int)boxState - 1;

        if (newState >= 3)
        {
            newState -= 3;
        }

        if (newState < 0)
        {
            newState += 3;
        }

        GoTo((BoxState)newState);
    }
    private void InputRight(InputAction.CallbackContext context)
    {
        int newState = (int)boxState + 1;

        if (newState >= 3)
        {
            newState -= 3;
        }

        if (newState < 0)
        {
            newState += 3;
        }

        GoTo((BoxState)newState);
    }
    private void InputNext(InputAction.CallbackContext context)
    {
        currentBox++;
        progressText.text = $"{currentBox}/{boxCount}";
        backProgressText.text = $"{currentBox}/{boxCount}";

        GoTo(startingState);
    }

    private int currentBox = 0;
    private int boxCount = 0;
    private void GoTo(BoxState newState)
    {
        menuManager.AddLerp(
            myCamera.transform, 
            new Vector3(
                boxPoints[(int)newState].position.x,
                boxPoints[(int)newState].position.y + currentBox * 3,
                boxPoints[(int)newState].position.z),
            boxPoints[(int)newState].rotation,
            .5f);

        boxState = newState;
    }

    [SerializeField] TextMeshProUGUI progressText;
    [SerializeField] TextMeshProUGUI backProgressText;

    public void StartBoxes()
    {
        myVocab = vocabManager.GenerateFullVocabList(including);
        boxCount = myVocab.GetLength(0);

        List<int> randomIndex = new List<int>();

        for (int i = 0; i < myVocab.GetLength(0); i++)
        {
            randomIndex.Add(i);
        }



        Debug.Log(myVocab.GetLength(0));
        for (int i = 0; i < myVocab.GetLength(0); i++)
        {
            int randomPos = UnityEngine.Random.Range(0, randomIndex.Count);

            GameObject newFlashBox = Instantiate(FlashBox, flashBoxSpawn);
            newFlashBox.transform.position = new Vector3(newFlashBox.transform.position.x, newFlashBox.transform.position.y + randomIndex[randomPos] * 3, newFlashBox.transform.position.z);
            randomIndex.RemoveAt(randomPos);

            FlashBoxUI newFlashBoxUI = Instantiate(FlashBoxUI, newFlashBox.transform.position, newFlashBox.transform.rotation, worldCanvas.transform).GetComponent<FlashBoxUI>();

            newFlashBoxUI.pinyinText.text = myVocab[i, 0];
            newFlashBoxUI.characterText.text = myVocab[i, 1];
            newFlashBoxUI.meaningText.text = myVocab[i, 2];
        }

    }
}
