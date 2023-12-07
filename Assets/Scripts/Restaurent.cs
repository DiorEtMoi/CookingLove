using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Restaurent 
{
    public string id;
    public string restaurantName;
    public int conversTimeValue;
    public int nowRubyTime;
    public int nowAdsTime;
    public string sceneName;
    public int keyRequired;
    public string shopUpgradeProgress;
    public List<int> levelProgressLock;
    public List<int> levelRewardProgress;
    public List<Ingredients> ingredients;

}
[System.Serializable]
public class Ingredients
{
    public int id;
    public string ingredientName;
    public int level;
    public int unlock;
    public List<int> golds;
    public List<int> upgradeTimes;
    public List<int> upgradePrices;
    public List<int> rewardExps;
}
