using Backtory.Core.Public;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public enum TimeLimit
{
    Total = 0,
    Month = 1,
    Week = 2,
    Day = 3
}
public enum LeaderboardType
{
    Score = 0,
    GoalAgainst = 1,
    GoalFor = 2,
    Winrate = 3,
    playedGame = 4
}

public class NewHighScorePanelScript : MonoBehaviour
{

    [SerializeField]
    GameObject scoreRowLabel, scoreContentPanel;

    private TimeLimit timeLimit = TimeLimit.Total;
    private LeaderboardType leaderboardType = LeaderboardType.Score;

    public List<CanvasGroup> TimeLimitButtonList;
    public List<CanvasGroup> leaderBoardTypeList;

    void Awake()
    {
    }

    private void Start()
    {
        ChangeTimeLimit(2);
    }

    public void ChangeTimeLimit(int index)
    {
        timeLimit = (TimeLimit)index;

        for (int i = 0; i < TimeLimitButtonList.Count; i++)
        {
            if (i == index)
            {
                TimeLimitButtonList[i].DOFade(1, 0.3f);
            }
            else
            {
                TimeLimitButtonList[i].DOFade(0.5f, 0.2f);

            }
        }

        ShowHighScore();

    }

    public void ChangeLeaderBoardType(int index)
    {
        leaderboardType = (LeaderboardType)index;

        for (int i = 0; i < leaderBoardTypeList.Count; i++)
        {
            if (i == index)
            {
                leaderBoardTypeList[i].DOFade(1, 0.3f);
            }
            else
            {
                leaderBoardTypeList[i].DOFade(0.6f, 0.2f);

            }
        }

        ShowHighScore();
    }

    public void ShowHighScore()
    {

        string leaderboardName = FindRequestedLeaderboard();

        Setting.waitingPanel.Show("در حال دریافت اطلاعات");
        new GameSparks.Api.Requests.LeaderboardDataRequest().SetLeaderboardShortCode(leaderboardName).SetEntryCount(100).Send((response) => {
            Setting.waitingPanel.Hide();
            if (!response.HasErrors)
            {
                foreach (Transform item in scoreContentPanel.transform)
                {
                    Destroy(item.gameObject);
                }

                int i = 0;
                foreach (GameSparks.Api.Responses.LeaderboardDataResponse._LeaderboardData entry in response.Data)
                {
                    int rank = (int)entry.Rank;
                    string playerName = entry.UserName;
                    string score = entry.JSONData["Score"].ToString();
                    Debug.Log("Rank:" + rank + " Name:" + playerName + " \n Score:" + score);

                    LeaderboardScoreRowScript scoreRow = Instantiate(scoreRowLabel, scoreContentPanel.transform).GetComponent<LeaderboardScoreRowScript>();
                    scoreRow.rankText.text = rank.ToString();
                    scoreRow.nameText.text = playerName;
                    scoreRow.scoreText.text = score.ToString();

                    if (i % 2 == 1)
                    {
                        scoreRow.GetComponent<Image>().color = new Color(1f, 0.8f, 0.5f, 1f);
                    }

                    i++;

                }
            }
            else
            {
                Setting.MessegeBox.SetMessege("ارتباط با سرور برقرار نشد", "", "خطا");
                Debug.Log("Error Retrieving Leaderboard Data...");
            }
        });


    }

    private string FindRequestedLeaderboard()
    {
        switch (leaderboardType)
        {
            case LeaderboardType.Score:
                switch (timeLimit)
                {
                    case TimeLimit.Total:
                        return "ScoreTotal";
                    case TimeLimit.Month:
                        return "ScoreMonthly";
                    case TimeLimit.Week:
                        return "ScoreWeakly";
                    case TimeLimit.Day:
                        return "ScoreDaily";
                    default:
                        break;
                }
                break;
            case LeaderboardType.GoalAgainst:
                switch (timeLimit)
                {
                    case TimeLimit.Total:
                        break;
                    case TimeLimit.Month:
                        break;
                    case TimeLimit.Week:
                        break;
                    case TimeLimit.Day:
                        break;
                    default:
                        break;
                }
                break;
            case LeaderboardType.GoalFor:
                switch (timeLimit)
                {
                    case TimeLimit.Total:
                        break;
                    case TimeLimit.Month:
                        break;
                    case TimeLimit.Week:
                        break;
                    case TimeLimit.Day:
                        break;
                    default:
                        break;
                }
                break;
            case LeaderboardType.Winrate:
                switch (timeLimit)
                {
                    case TimeLimit.Total:
                        break;
                    case TimeLimit.Month:
                        break;
                    case TimeLimit.Week:
                        break;
                    case TimeLimit.Day:
                        break;
                    default:
                        break;
                }
                break;
            case LeaderboardType.playedGame:
                switch (timeLimit)
                {
                    case TimeLimit.Total:
                        break;
                    case TimeLimit.Month:
                        break;
                    case TimeLimit.Week:
                        break;
                    case TimeLimit.Day:
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }


        return "ScoreTotal";
    }
}
