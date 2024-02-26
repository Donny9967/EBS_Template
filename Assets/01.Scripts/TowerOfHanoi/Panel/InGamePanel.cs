using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanel : Panel
{
    [SerializeField] SoundManager soundManager;

    [SerializeField]
    PopupManager popupManager;
    [SerializeField]
    CountTimer countTimer;
    [SerializeField]
    EndingPanel endingPanel;

    [SerializeField]
    GameObject Dim_Dark;

    [Space(20)]
    [SerializeField]
    List<Stack<int>> stacks = new List<Stack<int>>();

    public List<Transform> Posts = new List<Transform>();

    [SerializeField]
    private List<Disk> disks = new List<Disk>();
    [SerializeField]
    private List<EmptyDisk> emptyDisks = new List<EmptyDisk>();

    public int SelectDiskNumber;
    public bool isPop;
    public int prevPostNumber;
    public bool onGameover;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponentsInChildren(disks);
        GetComponentsInChildren(emptyDisks);

        Dim_Dark.GetComponent<Image>().DOFade(1, 0);
    }

    public void Init()
    {
        countTimer.Init();
        onGameover = false;
    }

    private void OnEnable()
    {
        Init();
        ResetPlay(GameManager.Instance.Stage);
    }

    public void ResetPlay(int stageNumber)
    {
        stacks.Clear();

        for (int i = 0; i < 3; i++)
        {
            stacks.Add(new Stack<int>());
        }

        for (int i = 0; i < 3; i++)
        {
            stacks[i].Clear();
        }

        for (int i = 0; i < disks.Count; i++)
        {
            disks[i].gameObject.SetActive(false);
            Vector2 pos = new Vector2(Posts[0].position.x, Posts[0].localPosition.y + (float)(80 * i));
            disks[i].transform.position = pos;
        }

        for (int i = 0; i < stageNumber; i++)
        {
            stacks[0].Push(i);
            disks[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < emptyDisks.Count; i++)
        {
            emptyDisks[i].gameObject.SetActive(false);
        }

        isPop = false;
        countTimer.Init();
        countTimer.StartTime();
        GameManager.Instance.SetStartDate();
    }

    private void Update()
    {
        if (onGameover) return;
        else
        {
            if (Input.GetButtonDown("Alpha1") || Input.GetButtonDown("Keypad1"))
                PopPushDisk(0);
            if (Input.GetButtonDown("Alpha2") || Input.GetButtonDown("Keypad2"))
                PopPushDisk(1);
            if (Input.GetButtonDown("Alpha3") || Input.GetButtonDown("Keypad3"))
                PopPushDisk(2);
        }
    }

    // 빼지 않았으면 빼고 꽂지 않았으면 꽂는다
    public void PopPushDisk(int postNumber)
    {
        if (onGameover) return;

        if (!isPop)
        {
            if (stacks[postNumber].Count == 0)
                return;

            PopDisk(postNumber);
            isPop = true;
        }
        else
        {
            PushDisk(postNumber);
            isPop = false;
        }
    }

    // 디스크 빼내기
    public void PopDisk(int origin)
    {
        soundManager.CorrectSound();
        SelectDiskNumber = stacks[origin].Pop();
        disks[SelectDiskNumber].transform.SetParent(null);
        emptyDisks[SelectDiskNumber].gameObject.SetActive(true);
        emptyDisks[SelectDiskNumber].transform.position = disks[SelectDiskNumber].transform.position;
        prevPostNumber = origin;


        // TODO : 이동 모션

    }

    // 디스크 꽂기
    public void PushDisk(int target)
    {
        if (stacks[target].Count > 0 && SelectDiskNumber < stacks[target].Peek())
        {
            ResetPushDisk(prevPostNumber);
            soundManager.WrongSound();
            return;
        }
        else if (target == prevPostNumber)
        {
            ResetPushDisk(prevPostNumber);
            soundManager.CorrectSound();
            return;
        }

        soundManager.CorrectSound();
        stacks[target].Push(SelectDiskNumber);
        disks[SelectDiskNumber].transform.SetParent(Posts[target].transform);
        emptyDisks[SelectDiskNumber].gameObject.SetActive(false);
        Vector2 pos = new Vector2(Posts[target].position.x, Posts[target].localPosition.y + (float)(80 * (stacks[target].Count - 1)));
        disks[SelectDiskNumber].transform.position = pos;

        countTimer.CountUp();
        CheckCorrect();

    }

    // 틀렸을 때
    public void ResetPushDisk(int prevPostNmuber)
    {
        stacks[prevPostNmuber].Push(SelectDiskNumber);
        disks[SelectDiskNumber].transform.SetParent(Posts[prevPostNmuber].transform);
        emptyDisks[SelectDiskNumber].gameObject.SetActive(false);
        Vector2 pos = new Vector2(Posts[prevPostNmuber].position.x, Posts[prevPostNmuber].localPosition.y + (float)(80 * (stacks[prevPostNmuber].Count - 1)));
        disks[SelectDiskNumber].transform.position = pos;

    }


    // 정답 체크
    public void CheckCorrect()
    {
        // 마지막 기둥의 stack count 가 스테이지넘버의 수 와 같다면 모두 옮긴것으로 간주
        if (stacks[2].Count == GameManager.Instance.Stage)
        {
            if (onGameover) return;
            onGameover = true;
            soundManager.ClearSound();
            popupManager.DimFadeIn();
            // 성공
            GameManager.Instance.GameClear(true);
            gameObject.SetActive(false);
        }
    }


}
