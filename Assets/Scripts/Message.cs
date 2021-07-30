using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Message
{
    private static Queue<string> texts = new Queue<string>();
    //��������L���[�ɉ�����
    public static void add(string m)
    {
        texts.Enqueue(m);
    }
    //�L���[���當��������o��
    public static string get()
    {
        if (texts.Count > 0)
        {
            return texts.Dequeue();
        }
        return null;
    }
    //�L���[�Ɋi�[����Ă��镶����̐���Ԃ�
    public static int getCount()
    {
        return texts.Count;
    }
}
