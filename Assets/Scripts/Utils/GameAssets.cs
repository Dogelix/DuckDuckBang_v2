﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class of assets that can be used to spawn in to the game.
/// Usage: GameAssests.i.{AssetRequired}
/// Example: GameAssets.i.PointsPopup
/// </summary>
public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if ( _i == null )
            {
                var sManager = GameObject.FindGameObjectWithTag(StringUtils.SceneManager);
                if ( sManager == null)
                {
                    _i = Instantiate(Resources.Load(StringUtils.SceneManager) as GameObject).GetComponent<GameAssets>();

                }
                else
                {
                    _i = sManager.GetComponent<GameAssets>();
                }

            }
            return _i;
        }
    }


    /// <summary>
    /// Points Pop Up Text
    /// </summary>
    public Transform PointsPopup;

    /// <summary>
    /// For adding sprites as children to a game object
    /// </summary>
    public Transform InWorldSprite;

    public Transform Basket;

    public Transform Bullet;

    /// <summary>
    /// Basic Testing Duck
    /// </summary>
    public Transform RegularFlyingDuck;

    /// <summary>
    /// Zombie Reginald
    /// </summary>
    public Transform ZombieWalkingDuck;

    /// <summary>
    /// Zombie Reginald
    /// </summary>
    public Transform RegularWalkingDuck;

    public Transform HeavyReginald;

    /// <summary>
    /// For anything that has hearts
    /// </summary>
    public Sprite EmptyHeart;

    /// <summary>
    /// For anything that has hearts
    /// </summary>
    public Sprite FullHeart;

}
