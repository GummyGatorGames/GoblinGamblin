using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GamblingLogic
{



    static RewardTiers[] rewardData = new RewardTiers[]{
            new RewardTiers{ TierMax= 30, MaxRange= 10, Multipliers= new[]{ 1,2,3,4,5,6,7,8,9,10 }},
            new RewardTiers{ TierMax = 45, MaxRange= 5, Multipliers= new[]{ 12, 16, 24, 32, 48, 64 }},
            new RewardTiers{ TierMax= 50, MaxRange= 4, Multipliers= new[]{ 100, 200, 300, 400, 500 }},
        };

    public static float ChestPrizeAmount(int currentChestNumber, float winPool)
    {
        if (currentChestNumber == 8 || winPool <= .05f)
        {
            return winPool;
        }

        int chestPrize = Random.Range(1, (int) (winPool/.05f));
        float trueChestPrize = chestPrize * 0.05f;



        return trueChestPrize;
    }

    private class RewardTiers
    {
        public int TierMax;
        public int MaxRange;
        public int[] Multipliers;
}
    public static float CalculateWin(float denomination)
    {
        int rand = Random.Range(1, 100);
        var rewardTier = rewardData.FirstOrDefault( d => rand <= d.TierMax);
        if(rewardTier==null)
        {
            return 0;
        }
        int randMult = Random.Range(1, rewardTier.MaxRange);
        var winPool = denomination * rewardTier.Multipliers[randMult];

        return winPool;
    }
}
