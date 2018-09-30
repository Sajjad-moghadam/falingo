using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "Question", menuName = "", order = 1)]
public class Question : ScriptableObject {
    
    public int QuestionNum;
    public string Title;
    public string Que;
    public QuestionMode Mode;
    public QuestionType type;
    public QuestionStruct structure;
    public int correctAnswer;
    public List<string> options;
    public List<Sprite> pics;
}


