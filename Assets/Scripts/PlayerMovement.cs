using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public EDir direction = EDir.Up;
    public float speed = 0.9f;
    public float speedDamptime = 0.1f;

    public int maxframe = 100;
    //fps���l������ꍇ�͉��L��s��L�����A��L��s���폜
    //public float maxPerFrame = 1.67f;
    //private float complementFrame;

    private int currentFrame = 0;
    private Pos2D newGrid = null;

    private readonly int hashspeedPara = Animator.StringToHash("Speed");
    public Pos2D grid = new Pos2D();
    // Start is called before the first frame update
    void Start()
    {
        //fps���l������ꍇ�͉��L��s��L����
        //complementFrame = maxPerFrame / Time.deltaTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentFrame == 0)
        {
            EDir d = DirUtil.KeyToDir();
            if (d == EDir.Pause)
            {
                animator.SetFloat(hashspeedPara, 0.0f, speedDamptime, Time.deltaTime);
            }
            else
            {
                direction = d;
                Message.add(direction.ToString());
                transform.rotation = DirUtil.DirToRotation(direction);
                newGrid = DirUtil.Move(GetComponentInParent<Field>(),grid, direction);
                grid = Move(grid, newGrid, ref currentFrame);
            }
        }
        else
        {
            grid = Move(grid, newGrid, ref currentFrame);
        }
    }
    /*private EDir KeyToDir()
    {
        if (!Input.anyKey)
        {
            return EDir.Pause;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return EDir.Left;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return EDir.Up;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            return EDir.Right;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            return EDir.Down;
        }
        return EDir.Pause;
    }*/
    /*private Quaternion DirToRotation(EDir d)
    {
        Quaternion r = Quaternion.Euler(0, 0, 0);
        switch (d)
        {
            case EDir.Left:
                r = Quaternion.Euler(0, 270, 0);
                break;
            case EDir.Up:
                r = Quaternion.Euler(0, 0, 0);
                break;
            case EDir.Right:
                r = Quaternion.Euler(0, 90, 0);
                break;
            case EDir.Down:
                r = Quaternion.Euler(0, 180, 0);
                break;
        }
        return r;
    }*/
    //�O���b�h���W�����[���h���W�ɕϊ�
    /*private float ToworldX(int xgrid)
    {
        return xgrid * 2;
    }*/
    /*private float ToworldZ(int zgrid)
    {
        return zgrid * 2;
    }*/
    //���[���h���W���O���b�h���W�ɕϊ�
    /*private int TogridX(float xworld)
    {
        return Mathf.FloorToInt(xworld / 2);
    }*/
    /*private int TogridZ(float zworld)
    {
        return Mathf.FloorToInt(zworld / 2);
    }*/
    //�⊮�Ōv�Z���Đi��
    private Pos2D Move(Pos2D currentPos,Pos2D newPos,ref int frame)
    {
        float px1 = Field.ToWorldX(currentPos.x);
        float pz1 = Field.ToWorldZ(currentPos.z);
        float px2 = Field.ToWorldX(newPos.x);
        float pz2 = Field.ToWorldZ(newPos.z);
        frame += 1;

        float t = (float)frame / maxframe;
        //fps���l�����Ȃ��ꍇ�͉��L��s��L�����A��L��s���폜
        //float t = frame / complementFrame;

        float newX = px1 + (px2 - px1) * t;
        float newZ = pz1 + (pz2 - pz1) * t;
        transform.position = new Vector3(newX, 0, newZ);
        animator.SetFloat(hashspeedPara, speed, speedDamptime, Time.deltaTime);
        if (maxframe == frame)
        //fps���l�����Ȃ��ꍇ�͉��L��s��L�����A��L��s���폜
        //if(maxPerFrame<=frame)
        {
            frame = 0;
            //fps�l�����Ɏg�p
            //transform.position = new Vector3(px2, 0, pz2);
            return newPos;
        }
        return currentPos;
    }
    //���݂̍��W�iPosition)�ƈړ������������i���j��n����
    //�ړ���̍��W���擾
    /*private Pos2D GetNewGrid(Pos2D position,EDir d)
    {
        Pos2D newP = new Pos2D();
        newP.x = position.x;
        newP.z = position.z;
        switch (d)
        {
            case EDir.Left:
                newP.x -= 1;
                break;
            case EDir.Up:
                newP.z += 1;
                break;
            case EDir.Right:
                newP.x += 1;
                break;
            case EDir.Down:
                newP.z -= 1;
                break;
        }
        return newP;
    }*/
    //�C���X�y�N�^�[�̒l���ύX���ꂽ��Ăяo�����
    private void OnValidate()
    {
        if (grid.x != Field.ToGridX(transform.position.x) || grid.z != Field.ToGridZ(transform.position.z))
        {
            transform.position = new Vector3(Field.ToWorldX(grid.x), 0, Field.ToWorldZ(grid.z));
        }
        if (direction != DirUtil.RotationToDir(transform.rotation))
        {
            transform.rotation = DirUtil.DirToRotation(direction);
        }
    }
    //�����ŗ^����ꂽ��]�̃x�N�g���ɑΉ�����������Ԃ�
   /* private EDir RotationToDir(Quaternion r)
    {
        float y = r.eulerAngles.y;
        if (y < 45)
        {
            return EDir.Up;
        }
        else if (y < 135)
        {
            return EDir.Right;
        }
        else if (y < 225)
        {
            return EDir.Down;
        }
        else if (y < 315)
        {
            return EDir.Left;
        }
        return EDir.Up;
    }*/
   public void SetPosition(int xgrid,int zgrid)
    {
        grid.x = xgrid;
        grid.z = zgrid;
        transform.position = new Vector3(Field.ToWorldX(xgrid), 0, Field.ToWorldZ(zgrid));
    }
    //�w�肵�������ɍ��킹�ĉ�]�x�N�g�����ύX����
    public void SetDirection(EDir d)
    {
        direction = d;
        transform.rotation = DirUtil.DirToRotation(d);
    }
}
