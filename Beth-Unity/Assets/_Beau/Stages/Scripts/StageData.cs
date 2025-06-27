using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    [Header("このステージ全体の構成プレハブ（障害物＋壁など）")]
    public GameObject stagePrefab;

    [Header("背景画像(SpriteRenderer用)")]
    public Sprite backgroundSprite;

    [Header("プレハブのY方向生成位置（例：基準Y）")]
    public float baseY = 0f;
}