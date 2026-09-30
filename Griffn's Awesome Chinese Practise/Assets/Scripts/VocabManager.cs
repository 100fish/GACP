using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;
using System;
public class VocabManager : MonoBehaviour
{
    //create array of file paths
    public string[] vocabFilePaths;

    //array of 2 dimensional arrays
    //each array is a set of vocabs
    //with a new word for each row
    //Then each array (each vocab list) has 3 columns
    //pinyin, character(s), and meaning (yisi)
    private string[][,] vocabLists;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReadFiles();
    }

    void ReadFiles()
    {
        //create list of different vocab lists
        string[][,] stagingVocabLists = new string[vocabFilePaths.Length][,];
        // Assuming 'myArray' is your System.Array variable or method output
        vocabLists = (string[][,])stagingVocabLists;


        //for as many file paths there are vocabs

        for (int i = 0; i < vocabFilePaths.Length; i++)
        {
            int lineCount = 0;

            using (StreamReader reader = new StreamReader(vocabFilePaths[i]))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }

            Debug.Log(lineCount);

            //Create lists of the actual vocab
            vocabLists[i] = new string[lineCount, 3];

            using (StreamReader reader = new StreamReader(vocabFilePaths[i]))
            {
                for (int ii = 0; ii < vocabLists[i].Length; ii++)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        continue;

                    //Debug.Log(line);
                    string[] lineParts;
                    lineParts = line.Split(',');
                    vocabLists[i][ii, 0] = lineParts[0];
                    vocabLists[i][ii, 1] = lineParts[1];
                    vocabLists[i][ii, 2] = lineParts[2];
                }
            }
        }
        //DELETE
        bool[] newBool = new bool[2] { true, true };
        GenerateFullVocabList(newBool);
    }

    public string[,] GenerateFullVocabList(bool[] including)
    {
        int vocabItemCount = 0;

        //for each vocab list in vocabLists
        for (int di = 0; di < vocabLists.Length; di++)
        {
            if (including[di] == false)
                continue;

            //for each row in the vocab list
            for (int dii = 0; dii < vocabLists[di].GetLength(0); dii++)
            {
                //count it
                vocabItemCount++;
            }
        }

        string[,] fullVocabList = new string[vocabItemCount, 3];

        int mi = 0;

        for (int di = 0; di < vocabLists.Length; di++)
        {
            if (including[di] == false)
                continue;


            for (int dii = 0; dii < vocabLists[di].GetLength(0); dii++)
            {
                fullVocabList[mi, 0] = vocabLists[di][dii, 0];
                fullVocabList[mi, 1] = vocabLists[di][dii, 1];
                fullVocabList[mi, 2] = vocabLists[di][dii, 2];

                mi++;
            }
        }

        for (int i = 0; i < fullVocabList.GetLength(0); i++)
        {
            Debug.Log(fullVocabList[i, 0]);
            Debug.Log(fullVocabList[i, 1]);
            Debug.Log(fullVocabList[i, 2]);
        }

        return fullVocabList;
    }

    void DebugListAllArrayContents()
    {

        for (int di = 0; di < vocabLists.Length; di++)
        {
            for (int dii = 0; dii < vocabLists[di].Length; dii++)
            {
                Debug.Log(vocabLists[di][dii, 0]);
                Debug.Log(vocabLists[di][dii, 1]);
                Debug.Log(vocabLists[di][dii, 2]);
            }
        }
    }
}
