using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaCalculation : MonoBehaviour
{

    /// <summary>
    /// ������ ��ġ�� ��ȯ
    /// </summary>
    /// <param name="count"></param>
    /// <param name="width"></param>
    /// <param name="force"></param>
    /// <param name="radDir"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    private List<Vector2> ReturnParabolaPos(int count, float width, float force, float radDir, float time)
    {
        List<Vector2> results = new List<Vector2>(count);
        float[] objLerps = new float[count];
        float[] timeLerps = new float[count];
        float interbal = 1f / (count - 1 > 0 ? count - 1 : 1);
        float timeInterbal = time / (count - 1 > 0 ? count - 1 : 1);
        for (int i = 0; i < count; i++)
        {
            objLerps[i] = interbal * i;
            timeLerps[i] = timeInterbal * i;
        }

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = Vector3.Lerp((Vector2)transform.position, new Vector2(transform.position.x - width, 0), objLerps[i]);
            pos.y = Caculated_TimeToPos(force, radDir, timeLerps[i]);

            //if (i > 0)
            //{
            //    if (pos.x >= _stageData.max_Range || pos.x <= -_stageData.max_Range)
            //    {
            //        pos = results[i - 1];
            //    }
            //}

            results.Add(pos);
        }

        return results;
    }
    /// <summary>
     /// t�� ���� ��ġ
     /// </summary>
     /// <param name="v">�ʱ� ����</param>
     /// <param name="sin">���� �Լ��� ���Ȱ�</param>
     /// <param name="time">�ð�</param>
     /// <returns></returns>
    static public float Caculated_TimeToPos(float v, float sin, float time)
    {
        return (v * time * Mathf.Sin(sin)) - (Mathf.Abs(Physics2D.gravity.y / 2) * (time * time));
    }
}
