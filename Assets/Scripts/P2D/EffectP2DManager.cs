using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class  EffectP2DManager:SingletonMahsa<EffectP2DManager>
{

	List<EffectP2D> effectList = new List<EffectP2D>();

    protected EffectP2DManager() { }

    void Update()
	{

		for (int i = 0; i < effectList.Count; i++)
		{
			effectList[i].Update();

			if (effectList[i].End)
				effectList.Remove(effectList[i]);
		}


	}


	public void AddEffect(EffectP2D effect)
	{
			
		effectList.Add(effect);
	}

    public void RemoveEffect(EffectP2D textureEffect,bool inCamera)
    {
           
        effectList.Remove(textureEffect);
    }


    void OnGUI()
    {

        for (int i = 0; i < effectList.Count; i++)
        {
            effectList[i].Draw();
		}


	}

		
}
