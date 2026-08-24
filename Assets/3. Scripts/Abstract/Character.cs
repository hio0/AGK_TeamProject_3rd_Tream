using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static System.Collections.Specialized.BitVector32;

public abstract class Character : MonoBehaviour
{
    [Header("기본 정보")]
    public CharacterData characterData;
    public string characterName;
    public List<SkillData> skillList = new();

    public int levelCount;
    public int level;
    public int maxLevel;
    public int hp;
    public int speed;
    public List<Icon> iconlist = new();
    public List<Item> itemList = new();

    public int maxHp;
    public int minSpeed;
    public int maxSpeed;

    [Header("시스템")]
    public int nowPosition;
    public int nowTurnCount;
    public bool isapproval;

    public bool iActChar;
    public bool iOurUnit;
    public bool iTargeting;
    public bool iSelecting; // 이건 이벤트버스로 해도 되긴하는데,,,어차피 이거 관여하는 쪽에서 이미 날 알고 있어서, 모르는 채로 정보 교환이라는 이벤트 버스 방식일 필요가 없어서,,,
    public List<Character> selectingTargets = new();

    public Action OnActingStart;
    public Action OnCanITargeted;
    public Action OnTargetFinding;
    public Action OnDied;

    public Action OnTriggerEnter;
    public Action OnTriggerClick;
    public Action OnTriggerExit;

    public event Action OnLevelUp;
    public event Action<int> OnLevelChanged;
    public event Action<Icon> OnIconStackChange;
    public event Action<SkillContext> OnAction;
    public event Action OnDamaged;
    public event Action<int> OnHeal;

    [Header("컴포넌트")]
    public Image characterImage;
    public EventTrigger characterTrigger;
    public CharacterTeam characterTeam;
    public TMP_Text characterOpinion;
    public CharacterEmotion characterEmotion;

    public Transform body;
    public Transform characterIcons;

    public IconText pre_iconText;
    public IconIcon pre_icon;

    private void Awake()
    {
        DefaultSet();
        ReturnToBasic();

    }

    private void OnEnable()
    {
        FightManager.Instance.OnActingStart += AnotherSelected;
        FightManager.Instance.OnTargetFinding += CanITargeted;
        FightManager.Instance.OnTargetFinding += Targeting;
        FightManager.Instance.OnTargetFinded += Act;
        FightManager.Instance.OnTurnFinish += ReturnToBasic;
        FightManager.Instance.OnFightFinish += ReturnToBasic;
        FightManager.Instance.OnFightFinish += RemoveIconList;
        FightManager.Instance.OnActingFinished += ActFinish;
    }

    private void OnDisable()
    {
        FightManager.Instance.OnActingStart -= AnotherSelected;
        FightManager.Instance.OnTargetFinding -= CanITargeted;
        FightManager.Instance.OnTargetFinding -= Targeting;
        FightManager.Instance.OnTargetFinded -= Act;
        FightManager.Instance.OnTurnFinish -= ReturnToBasic;
        FightManager.Instance.OnFightFinish -= ReturnToBasic;
        FightManager.Instance.OnFightFinish -= RemoveIconList;
        FightManager.Instance.OnActingFinished -= ActFinish;

        OnActingStart = null;
        OnCanITargeted = null;
        OnTargetFinding = null;
        OnDied = null;

        OnTriggerEnter = null;
        OnTriggerClick = null;
        OnTriggerExit = null;

        OnAction = null;
        OnDamaged = null;
    }

    // 시스템
    public void SetImage(MotionData data)
    {
        Sprite sprite = data.image;

        characterImage.sprite = sprite;
        characterImage.SetNativeSize();
    }

    public void SetSpeed()
    {
        int value = UnityEngine.Random.Range(minSpeed, maxSpeed + 1);
        speed = value;
    }

    void AnotherSelected()
    {
        TurnStartedSet();

        Character selectedChar = FightManager.Instance.GetRangeData?.Invoke().nowSelectedChar;
        if (selectedChar.nowTurnCount != nowTurnCount)
        {
            characterImage.color = new Color32(116, 116, 116, 200);
            return;
        }
        else
        {
            ReturnToBasic();
            iActChar = true;
            OnActingStart?.Invoke();
        }
    }

    public void ReturnToBasic()
    {
        characterImage.color = new Color32(255, 255, 255, 255);
    }

    void CanITargeted()
    {
        if (iActChar)
        {
            OnCanITargeted?.Invoke();
        }
    }

    void Targeting()
    {
        if (iActChar)
        {
            OnTargetFinding?.Invoke();
        }
    }

    void DefaultSet()
    {
        maxHp = characterData.defaultHp;
        minSpeed = characterData.defaultMinSpeed;
        maxSpeed = characterData.defaultMaxSpeed;

        characterName = characterData.defaultCharacterName;
        SetImage(characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));
        skillList = characterData.defaultSkillList;

        hp = maxHp;
        level = 0;
        levelCount = 0;
        maxLevel = 15;

        Templet.AddEvent(characterTrigger, EventTriggerType.Drop, OnDrop);
    }

    void TurnStartedSet()
    {
        characterTrigger.triggers.RemoveAll(entry => entry.eventID == EventTriggerType.PointerEnter || entry.eventID == EventTriggerType.PointerExit || entry.eventID == EventTriggerType.PointerClick);
        selectingTargets.Clear();

        iTargeting = false;
        iSelecting = false;
        iActChar = false;
    }

    void ActFinish()
    {
        SetImage(characterData.motionData.Find(x => x.type == MotionData.MotionType.standing));
    }

    // 자식꺼
    public virtual Skill SkillSetPattern() // 기본적인 오토 스킬세팅 ( 특수한 스킬 세팅법이 있는 enemy는 이거 변경해서 씀 ㅇㅇ )
    {
        int r = UnityEngine.Random.Range(0, skillList.Count);

        Skill skill = skillList[r].mySkill;
        skill.Initialize(skillList[r]);
        return skill;
    }

    public virtual void Act() // 스킬컨텍스트 받고 계산은 스킬 쪽에서 다함 ㅇ
    {
        if (iActChar)
        {
            SkillContext skillContext = characterTeam.RetrunContext();

            OnAction?.Invoke(skillContext);
            StartCoroutine(skillContext.useSkill.Effected(skillContext));
            iActChar = false;
        }
    }

    public virtual void Damaged(Action skillEffect)
    {
        skillEffect?.Invoke();
        SetImage(characterData.motionData.Find(x => x.type == MotionData.MotionType.hit));

        IEnumerator Cor()
        {
            Color32 bcol = characterImage.color;
            characterImage.color = new Color32(192, 68, 89, 255);

            yield return new WaitForSeconds(0.5f);

            characterImage.color = bcol;
        }

        StartCoroutine(Cor());

        HpToZero();

        OnDamaged?.Invoke();
    }

    public virtual void HpToZero()
    {
        if (hp <= 0)
        {
            OnDied?.Invoke();
        }
    }

    public virtual void Heal(int healCount)
    {
        hp += healCount;

        OnHeal.Invoke(healCount);
    }

    public virtual void AddIcon(IconData data, int changedStack)
    {
        Icon icon = data.myIcon;

        if (iconlist.Contains(icon))
        {
            iconlist.Find(x => x == icon).ChangeStack(changedStack);
        }
        else
        {
            IconContext iconContext = new IconContext
            {
                data = data,
                target = this
            };
            icon.Initialize(iconContext);

            iconlist.Add(icon);
            icon.ChangeStack(changedStack);

            IconIcon iconImage = Instantiate(pre_icon, characterIcons);
            iconImage.Initialize(icon, this);
        }

        IconText text = Instantiate(pre_iconText, body);
        text.Initialize(data.iconImage, data.iconName, data.textColor);

        OnIconStackChange?.Invoke(icon);
    }


    public virtual void RemoveIcon(Icon icon)
    {
        iconlist.Remove(icon);
    }

    public void RemoveIconList()
    {
        foreach (Icon icon in iconlist)
        {
            icon.RemoveEvent();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item drag = eventData.pointerDrag.GetComponent<ItemIcon>().myItem.myItem;

        ItemContext context = new ItemContext
        {
            data = eventData.pointerDrag.GetComponent<ItemIcon>().myItem,
            target = this
        };
        drag.Initialize(context);

        IconText text = Instantiate(pre_iconText, body);
        text.Initialize(drag.data.itemImage, drag.data.itemName, new Color32(255, 255, 255, 255));

        ItemManager.Instance.OnRemoveItem.Invoke(eventData.pointerDrag.GetComponent<ItemIcon>().myItem, 1);

        SchoolManager.instance.OnNoticedSomething?.Invoke($"{characterData.name}에게\n{drag.data.itemName}을 사용했다.");
    }

    public void GetExp(int exp)
    {
        level = level + exp;
        if (level >= maxLevel)
        {
            LevelUp();
        }

        OnLevelChanged?.Invoke(exp);
    }

    void LevelUp()
    {
        LevelPlus();

        IconText text = Instantiate(pre_iconText, body);
        text.Initialize(null, "레벨 업!", new Color32(90, 108, 166, 255));
    }

    void LevelPlus()
    {
        maxLevel = maxLevel * 2 / ImportantData.dayCount / 3;
        level = 0;
        levelCount++;

        LevelStatChanged(levelCount);
        OnLevelUp.Invoke();
    }

    public void LevelStatChanged(int lv)
    {
        maxHp += UnityEngine.Random.Range(3, 5);
        hp = maxHp;
    }

    public void EmotionUp()
    {
        IconText text = Instantiate(pre_iconText, body);
        text.Initialize(null, "감정 격양!", new Color32(227, 118, 46, 255));
    }
}
