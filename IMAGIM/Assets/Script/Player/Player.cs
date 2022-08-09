using DKH.SkillSystem;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Player : Unit, ISkillable
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 4f;
    [SerializeField] private float decceleration = 6f;
    [SerializeField] private float velPower = 0.9f;
    [HideInInspector] public float xInput; 
    private float xScale;
    [SerializeField] private float jumpSpeed = 10f;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    private PlayerDash playerDash;
    private bool inDash;
    Dictionary<SkillBase, SkillData> skillData = new Dictionary<SkillBase, SkillData>();
    [SerializeField] SkillBase skill;
    [SerializeField] BUFF buff;
    [SerializeField] TMP_Text cooldownTimer;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        xScale = transform.localScale.x;
        boxCollider = GetComponent<BoxCollider2D>();
        playerDash = GetComponent<PlayerDash>();
        DemoAtkBuffData data = (DemoAtkBuffData)GetSkillData(skill);
        data.buff = buff;
    }
    private void Start()
    {
        SetMaxHealth(100);
    }

    private void Update() {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded()) {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ExecuteSkill(skill);
        }
        AnimationHandler(); 
        FlipPlayer();
        DashHandler();
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(2);
        }
        float skillCooldown = 0;
        foreach (var skillData in skillData.Values)
        {
            ISkillDataCooldown cooldownSkill = (ISkillDataCooldown)skillData;
            cooldownSkill.Cooldown -= Time.deltaTime;
            skillCooldown = cooldownSkill.Cooldown;
        }
        cooldownTimer.text = (skillCooldown <= 0) ? "Ready" : $"{Mathf.Round(skillCooldown * 10f) / 10f}s";
    }

    private void FixedUpdate() {
        if (!inDash)
        {
            float targetSpeed = xInput * moveSpeed;
            float speedDif = targetSpeed - rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01) ? acceleration : decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
            
            rb.AddForce(movement * Vector2.right);
        }
    }

    private void FlipPlayer(){
        if (xInput != 0) {
            Vector3 scale = transform.localScale;
            scale.x = xScale * xInput;
            transform.localScale = scale;
        }
    }

    private void AnimationHandler(){
        if (xInput != 0 & rb.velocity.y == 0) {
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
    }

    public bool isGrounded(){
        Vector3 downChecker = new Vector3(0, -0.2f, 0);
        return Physics2D.OverlapBoxAll(boxCollider.bounds.center + downChecker, boxCollider.bounds.size, 0, LayerMask.GetMask("Ground")).Length > 0;
    }

    private void DashHandler(){
        if (isGrounded()) {
            playerDash.dashCharge = 1;
        }

        inDash = playerDash.inDash;
    }

    public void ExecuteSkill(SkillBase skill)
    {
        skill.ExecuteAs(this);
    }

    public SkillData GetSkillData(SkillBase skill)
    {
        SkillData data;
        if (!skillData.TryGetValue(skill, out data))
        {
            // Basically
            // data = new DemoDebugLogSkillData()
            data = Activator.CreateInstance(skill.SkillData) as SkillData;
            skillData.Add(skill, data);
        }
        return data;
    }
}
