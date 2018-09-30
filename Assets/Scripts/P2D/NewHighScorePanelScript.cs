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

[RequireComponent(typeof(P2DPanel))]
public class NewHighScorePanelScript : MonoBehaviour
{

    [SerializeField]
    GameObject scoreRowLabel, scoreContentPanel;

    P2DPanel myPanel;

    private TimeLimit timeLimit = TimeLimit.Total;
    private LeaderboardType leaderboardType = LeaderboardType.Score;

    public List<CanvasGroup> TimeLimitButtonList;
    public List<CanvasGroup> leaderBoardTypeList;

    void Awake()
    {
        myPanel = GetComponent<P2DPanel>();
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
        if (!myPanel.IsShow)
        {
            myPanel.Show();
        }

        string leaderboardID = FindRequestedLeaderboard();

        BacktoryLeaderBoard topPlayers = new BacktoryLeaderBoard(leaderboardID);

        //// Create a leaderboard object
        Setting.waitingPanel.Show();
        // Request for top 100 to backtory
        topPlayers.GetTopPlayersInBackground(100, leaderboardResponse =>
        {

            foreach (Transform item in scoreContentPanel.transform)
            {
                Destroy(item.gameObject);
            }
            // Checking if response was fetched successfully
            if (leaderboardResponse.Successful)
            {
                Setting.waitingPanel.Hide();
                GetComponent<P2DPanel>().Show();


                for (int i = 0; i < leaderboardResponse.Body.UsersProfile.Count; i++)
                {
                    LeaderboardScoreRowScript scoreRow = Instantiate(scoreRowLabel, scoreContentPanel.transform).GetComponent<LeaderboardScoreRowScript>();
                    scoreRow.rankText.text = (i + 1).ToString();
                    scoreRow.nameText.text = leaderboardResponse.Body.UsersProfile[i].UserBriefProfile.FirstName;
                    scoreRow.scoreText.text = leaderboardResponse.Body.UsersProfile[i].Scores[0].ToString();
                    
                    if(i%2 == 1)
                    {
                        scoreRow.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                    }

                    //scoreRow.goalAgainstText.text = "0";
                    //scoreRow.goalForText.text = "0";
                    //scoreRow.winrateText.text = "0";
                    //scoreRow.playedNumberText.text = "0";
                    //scoreRow.playedNumberText.text = leaderboardResponse.Body.UsersProfile[i].Scores[1].ToString();
                }

            }
            else
            {
                Setting.MessegeBox.SetMessege("ارتباط با سرور برقرار نشد", "", "خطا");
            }
        });


    }

    public void Hide()
    {
        myPanel.Hide();
    }

    private string FindRequestedLeaderboard()
    {
        switch (leaderboardType)
        {
            case LeaderboardType.Score:
                switch (timeLimit)
                {
                    case TimeLimit.Total:
                        return "5afd2e4de4b07d19887a280a";
                    case TimeLimit.Month:
                        return "5afd3045e4b0b0a50c0c1e66";
                    case TimeLimit.Week:
                        return "5afd3058e4b0f2534784976d";
                    case TimeLimit.Day:
                        return "5afd306fe4b0f2534784976e";
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


        return "5afd2e4de4b07d19887a280a";
    }
}
