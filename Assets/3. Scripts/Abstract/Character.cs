using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public List<Skill> skillList = new();

    public int level;
    public int hp;
    public int speed;
    public List<Icon> iconlist = new();

    public int maxHp;
    public int minSpeed;

    [Header("시스템")]
    public int nowPosition;
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

    public event Action<Icon> OnIconStackChange;
    public event Action<SkillContext> OnAction;
    public event Action OnDamaged;

    [Header("컴포넌트")]
    public Image characterImage;
    public EventTrigger characterTrigger;

    public CharacterTeam characterTeam;
    public Transform characterIcons;
    public IconIcon pre_icon;

    private void OnEnable()
    {
        DefaultSet();
        ReturnToBasic();

        FightManager.Instance.OnActingStart += AnotherSelected;
        FightManager.Instance.OnTargetFinding += CanITargeted;
        FightManager.Instance.OnTargetFinding += Targeting;
        FightManager.Instance.OnTargetFinded += Act;
        FightManager.Instance.OnTurnFinish += ReturnToBasic;
        FightManager.Instance.OnFightFinish += ReturnToBasic;
        //FightManager.Instance.OnFightFinish += RemoveIconList;
    }

    private void OnDisable()
    {
        FightManager.Instance.OnActingStart -= AnotherSelected;
        FightManager.Instance.OnTargetFinding -= CanITargeted;
        FightManager.Instance.OnTargetFinding -= Targeting;
        FightManager.Instance.OnTargetFinded -= Act;
        FightManager.Instance.OnTurnFinish -= ReturnToBasic;
        FightManager.Instance.OnFightFinish -= ReturnToBasic;
        //FightManager.Instance.OnFightFinish -= RemoveIconList;

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
    void AnotherSelected()
    {
        TurnStartedSet();

        Character selectedChar = FightManager.Instance.GetRangeData?.Invoke().nowSelectedChar;
        if (selectedChar.speed != speed)
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
        characterName = characterData.defaultCharacterName;
        skillList = characterData.defaultSkillList;

        maxHp = characterData.defaultHp;
        minSpeed = characterData.defaultMinSpeed;
    }

    void TurnStartedSet()
    {
        characterTeam = GetComponent<CharacterTeam>();
        characterTrigger.triggers.Clear();
        selectingTargets.Clear();

        iTargeting = false;
        iSelecting = false;
        iActChar = false;
    }

    // 자식꺼
    public virtual Skill SkillSetPattern() // 기본적인 오토 스킬세팅 ( 특수한 스킬 세팅법이 있는 enemy는 이거 변경해서 씀 ㅇㅇ )
    {
        int r = UnityEngine.Random.Range(0, skillList.Count);

        return skillList[r];
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

    
    public virtual void AddIcon(IconData data, SkillContext context, int changedStack)
    {
        if (iconlist.Count != 0 && iconlist.Contains(data.myIcon))
        {
            iconlist.Find(x => x == data.myIcon).ChangeStack(changedStack);
        }
        else
        {
            Icon icon = data.myIcon;
            IconContext iconContext = new IconContext
            {
                data = data,
                target = this,
                skill = context.useSkill
            };
            icon.Initialize(iconContext);

            iconlist.Add(icon);
            icon.ChangeStack(changedStack);

            IconIcon iconImage = Instantiate(pre_icon, characterIcons);
            iconImage.Initialize(icon, this);
        }

        OnIconStackChange?.Invoke(data.myIcon);
    }

    
    public virtual void RemoveIcon(Icon icon)
    {
        iconlist.Remove(icon);
    }

    /*
    public void RemoveIconList()
    {
        foreach(Icon icon in iconlist)
        {
            //icon.data.RemoveEvent();
        }
    }
    */
}
