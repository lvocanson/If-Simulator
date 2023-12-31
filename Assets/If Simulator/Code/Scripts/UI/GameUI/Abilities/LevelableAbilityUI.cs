﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelableAbilityUI : AbilityIconUI
{
    [SerializeField] private LayoutGroup _levelStarLayoutGroup;
    [SerializeField] private GameObject _levelStarPrefab;
    
    private List<LevelStarUI> _levelStars = new();
    
    private void Awake()
    {
        _levelStars.Clear();
        
        for (int i = 0; i < _levelStarLayoutGroup.transform.childCount; i++)
        {
            _levelStars.Add(_levelStarLayoutGroup.transform.GetChild(i).GetComponent<LevelStarUI>());
        }
    }

    public override void HideAbility()
    {
        base.HideAbility();

        _levelStarLayoutGroup.gameObject.SetActive(false);
    }

    public override void ShowAbility()
    {
        base.ShowAbility();
        
        _levelStarLayoutGroup.gameObject.SetActive(true);
    }

    public void InitStars(int maxLevel)
    {
        int levelStarInstantiated = _levelStarLayoutGroup.transform.childCount;
        if (levelStarInstantiated > maxLevel)
        {
            for (int i = maxLevel; i < levelStarInstantiated; i++)
            {
                Destroy(_levelStarLayoutGroup.transform.GetChild(i).gameObject);
                if (i < _levelStars.Count) break; // Temporary fix of a bug where the whole UpdateSpell event is called twice
                _levelStars.RemoveAt(i);
            }
        }
        else if (levelStarInstantiated < maxLevel)
        {
            for (int i = levelStarInstantiated; i < maxLevel; i++)
            {
                LevelStarUI star = Instantiate(_levelStarPrefab, _levelStarLayoutGroup.transform).GetComponent<LevelStarUI>();
                _levelStars.Add(star);
            }
        }
    }
    
    public void EnableStars(int level)
    {
        for (int i = 0; i < _levelStars.Count; i++)
        {
            _levelStars[i].EnableStar(i < level);
        }
    } 
}
