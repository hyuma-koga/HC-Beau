using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    [Header("���̃X�e�[�W�S�̂̍\���v���n�u�i��Q���{�ǂȂǁj")]
    public GameObject stagePrefab;

    [Header("�w�i�摜(SpriteRenderer�p)")]
    public Sprite backgroundSprite;

    [Header("�v���n�u��Y���������ʒu�i��F�Y�j")]
    public float baseY = 0f;
}