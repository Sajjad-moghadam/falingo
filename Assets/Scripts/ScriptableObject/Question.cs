using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Question", menuName = "", order = 1)]
public class Question : ScriptableObject {

    [HideInInspector]
    public int QuestionNum, answerType;

    public string Title;
    public string Que;
    public QuestionMode Mode;
    public QuestionType type;
    public QuestionStruct structure;
    public WordGameSize wordGameSize = WordGameSize.Size_2_2;
    public int correctAnswer;
    public List<string> options;
    public List<Sprite> pics;

    public string[] words;
    public List<Vector2> words1Positions;
    public List<Vector2> words2Positions;
    public List<Vector2> words3Positions;
    public List<Vector2> words4Positions;
    public List<Vector2> words5Positions;
    public List<Vector2> words6Positions;
    public List<Vector2> words7Positions;


}


