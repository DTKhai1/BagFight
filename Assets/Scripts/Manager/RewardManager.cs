using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    GameManager _gameManager;
    public PlayerData _playerData;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _playerData = _gameManager._playerData;
    }
    public void AddGold(int amount)
    {
        _playerData._gold += amount;
    }
    public void AddProgressXP(int amount)
    {
        _playerData._progressTrackXP += amount;
    }
    public void AddWeaponPiece(WeaponData weaponData, int amount)
    {
        weaponData.AddWeaponPiece(amount);
    }
}
public class Gold : Reward
{
    public override string _displayName => "Coin";
    public Gold(int value) : base(value)
    {
    }
}
public class ProgressXP: Reward
{
    public override string _displayName => "ProgressXP";
    public ProgressXP(int value) : base(value)
    {
    }
}
public class WeaponPieceReward: Reward
{
    public WeaponData _weaponData { get; set; }
    public override string _displayName => _weaponData._weaponName.ToString();
    public WeaponPieceReward(int value) : base(value)
    {
    }
}
public class Reward
{
    public int _value { get; set; }
    public virtual string _displayName { get; }
    public Reward(int value)
    {
        _value = value;
    }
}
