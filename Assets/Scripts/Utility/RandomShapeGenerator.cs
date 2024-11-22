using System;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteShapeController))]
public class RandomShapeGenerator : MonoBehaviour
{
  public int pointCount = 8; // 控制点数量
  public float baseRadiusMin = 1f;
  public float baseRadiusMax = 3f;
  private float baseRadius; // 基础圆形半径
  public float randomOffset = 2f; // 每个点的随机偏移量
  public bool isClosedShape = true; // 是否生成闭合形状

  private SpriteShapeController spriteShapeController;

  void Start()
  {
    // 获取 SpriteShapeController 组件
    spriteShapeController = GetComponent<SpriteShapeController>();

    // 清空当前 Spline
    spriteShapeController.spline.Clear();

    // 生成随机形状
    GenerateRandomShape();
  }

  void GenerateRandomShape()
  {
    Spline spline = spriteShapeController.spline;

    Vector3[] points = new Vector3[pointCount];

    // 计算所有点的位置
    for (int i = 0; i < pointCount; i++)
    {
      float angle = (360f / pointCount) * i * Mathf.Deg2Rad;

      baseRadius = Random.Range(baseRadiusMin, baseRadiusMax);

      // 基础圆形的坐标
      float x = Mathf.Cos(angle) * baseRadius;
      float y = Mathf.Sin(angle) * baseRadius;

      // 增加随机偏移
      x += Random.Range(-randomOffset, randomOffset);
      y += Random.Range(-randomOffset, randomOffset);

      points[i] = new Vector3(x, y, 0);
    }

    // 添加控制点并计算切线
    for (int i = 0; i < pointCount; i++)
    {
      // 当前点
      Vector3 current = points[i];

      // 获取左右点（闭合形状考虑循环索引）
      Vector3 previous = points[(i - 1 + pointCount) % pointCount];
      Vector3 next = points[(i + 1) % pointCount];

      // 计算弧线切线：基于当前点和前后点的位置，使用向量的角度
      Vector3 leftTangent = CalculateTangent(previous, current, next, true); // 左切线
      Vector3 rightTangent = CalculateTangent(previous, current, next, false); // 右切线

      // 插入控制点
      spline.InsertPointAt(i, current);

      // 设置切线模式和切线
      spline.SetTangentMode(i, ShapeTangentMode.Continuous);
      spline.SetLeftTangent(i, leftTangent);
      spline.SetRightTangent(i, rightTangent);
    }

    // 设置是否为闭合图形
    spriteShapeController.spline.isOpenEnded = !isClosedShape;
  }

  // 计算弧线切线
  Vector3 CalculateTangent(Vector3 previous, Vector3 current, Vector3 next, bool isLeftTangent)
  {
    // 计算前后点的方向向量
    Vector3 prevDir = (current - previous).normalized; // 当前点到前一个点的方向
    Vector3 nextDir = (next - current).normalized; // 当前点到下一个点的方向

    // 计算两个方向的平均方向，来生成切线的方向
    Vector3 tangentDir = Vector3.Lerp(prevDir, nextDir, 0.5f).normalized;

    // 如果是左切线，则反转方向
    if (isLeftTangent)
    {
      tangentDir = -tangentDir;
    }

    // 根据弧度来调节切线长度，这里使用 0.5f 来控制弧度的平滑程度
    return tangentDir * baseRadius * 0.5f; // 使用半径作为切线的长度控制
  }
}