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

        string leaderboardID = FindRequestedLeaderboard();

        BacktoryLeaderBoard topPlayers = new BacktoryLeaderBoard(leaderboardID);

        //// Create a leaderboard object
        Setting.waitingPanel.Show("در حال دریافت اطلاعات");
        // Request for top 100 to backtory
        topPlayers.GetTopPlayersInBackground(100, leaderboardResponse =>
        {
            Setting.waitingPanel.Hide();

            foreach (Transform item in scoreContentPanel.transform)
            {
                Destroy(item.gameObject);
            }
            // Checking if response was fetched successfully
            if (leaderboardResponse.Successful)
            {
                GetComponent<P2DPanel>().Show();


                for (int i = 0; i < leaderboardResponse.Body.UsersProfile.Count; i++)
                {
                    LeaderboardScoreRowScript scoreRow = Instantiate(scoreRowLabel, scoreContentPanel.transform).GetComponent<LeaderboardScoreRowScript>();
                    scoreRow.rankText.text = (i + 1).ToString();
                    scoreRow.nameText.text = leaderboardResponse.Body.UsersProfile[i].UserBriefProfile.FirstName;
                    scoreRow.scoreText.text = leaderboardResponse.Body.UsersProfile[i].Scores[0].ToString();
                    
                    if(i%2 == 1)
                    {
                        scoreRow.GetComponent<Image>().color = new Color(1f, 0.8f, 0.5f,1f);
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

    private string FindRequestedLeaderboard()
    {
        switch (leaderboardType)
        {
            case LeaderboardType.Score:
                switch (timeLimit)
                {
                    case TimeLimit.Total:
                        return "5bb7229ae4b03221c8e0064e";
                    case TimeLimit.Month:
                        return "5bb722aae4b03221c8e0064f";
                    case TimeLimit.Week:
                        return "5bb722b5e4b03221c8e00650";
                    case TimeLimit.Day:
                        return "5bb722bde4b0f623513e0d66";
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


        return "5bb7229ae4b03221c8e0064e";
    }
}
