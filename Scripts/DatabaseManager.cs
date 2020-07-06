using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;

public class DatabaseManager : MonoBehaviour        //제이슨을 이용한 게임 내용 저장
{
    SaveData save = new SaveData();
    [SerializeField] StageMenu theStage;

    string SAVE_DATA_DIRECTORY;     //저장 경로
    string SAVE_FILENAME = "/SaveFile.txt";  //파일 이름

    int currentSong = 0;        //데이터베이스에 넘길 곡의 인덱스

    public void SetCurrentSong(int num)
    {
        currentSong = num;
    }

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";     //현재게임 폴더
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))                 //경로내에 디렉토리가없으면
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);         //새로 생성
    }

    public void SaveData(int score)
    {
        ScoreManager theScore = FindObjectOfType<ScoreManager>();
        save.maxScores[currentSong] = theScore.GetScore();

        string json = JsonUtility.ToJson(save);                 //데이터 저장 클래스의 데이터들을 제이슨화
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);   //기존 지정 디렉토리에 제이슨화 되었던 정보들을 기록(물리적인 저장)

        Debug.Log("저장완료");

    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))       //저장된 데이터가 있는 상태에서만 실행
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);        //디렉토리 경로에있는 정보를  제이슨에 저장
            save = JsonUtility.FromJson<SaveData>(loadJson);    //역순으로 제이슨화된 정보들을 세이브데이터에 저장


            
            theStage.SetScore(string.Format("{0:#,##0}", save.maxScores[currentSong]));

            Debug.Log("로드완료");
        }
    }


}

[System.Serializable]
public class SaveData
{
    public int[] maxScores=new int[3];
}
