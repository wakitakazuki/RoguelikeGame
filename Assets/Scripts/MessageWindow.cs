using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessageWindow : MonoBehaviour
{
    public Text text;
    public float maxPerFrameH = 0.5f;
    public float maxPerFrameV = 1.0f;
    private bool isAdding = false;
    private bool isFalling = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAdding)
        {
            MessageAnimation anim;
            if (!isFalling)
            {
                anim = transform.GetChild(transform.childCount - 1).GetComponent<MessageAnimation>();
                isAdding = !anim.MoveMessage(transform.position + new Vector3(0, 0, 0), maxPerFrameH);
                Debug.Log("OK");
                return;
            }
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                anim = transform.GetChild(i).GetComponent<MessageAnimation>();
                if (anim.IsDeleting()) continue;
                isFalling = !anim.MoveMessage(transform.position + new Vector3(0, -100 * (transform.childCount - i - 1), 0), maxPerFrameV);
            }
        }
        else ShowMessage();
    }
    //メッセージを追加表示する
    private void ShowMessage()
    {
        if (Message.getCount() > 0)
        {
            isAdding = true;
            isFalling = transform.childCount > 0;
            string m = Message.get();
            Text msg = Instantiate(text, transform);
            msg.transform.position = transform.position + new Vector3(-2000, 0, 0);
            msg.text = m;
        }
    }
}
