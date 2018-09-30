using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lesson", menuName = "", order = 1)]
public class Lesson : ScriptableObject
{
    public List<Question> questions;
    public QuestionMode Lmode;
    public QuestionType Ltype;
    public string title;
}