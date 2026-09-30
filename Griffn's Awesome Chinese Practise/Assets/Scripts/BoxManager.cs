using UnityEngine;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private GameObject FlashBox;
    [SerializeField] private GameObject FlashBoxUI;
    
    [SerializeField] private VocabManager vocabManager;
    [SerializeField] private GameObject worldCanvas;
    
    [SerializeField] private Transform flashBoxSpawn;
    


    public bool[] including;

    private string[,] myVocab;

    public void StartBoxes()
    {
        myVocab = vocabManager.GenerateFullVocabList(including);

        Debug.Log(myVocab.GetLength(0));
        for (int i = 0; i < myVocab.GetLength(0); i++)
        {
            GameObject newFlashBox = Instantiate(FlashBox, flashBoxSpawn);
            newFlashBox.transform.position = new Vector3(newFlashBox.transform.position.x, newFlashBox.transform.position.y + i * 3, newFlashBox.transform.position.z);

            FlashBoxUI newFlashBoxUI = Instantiate(FlashBoxUI, newFlashBox.transform.position, newFlashBox.transform.rotation, worldCanvas.transform).GetComponent<FlashBoxUI>();

            newFlashBoxUI.pinyinText.text = myVocab[i, 0];
            newFlashBoxUI.characterText.text = myVocab[i, 1];
            newFlashBoxUI.meaningText.text = myVocab[i, 2];
        }

    }
}
