using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositions : MonoBehaviour
{
    [SerializeField] private Vector3[] positions;
    private int index;

    [SerializeField] private float timeToChangePositions = 1f;
    private bool isMoving = false;

    [SerializeField] private AnimationCurve moveAnimationCurve;

    private Animator anim;

    private void Awake()
    {
        if (this.positions.Length <= 0)
            this.positions = new Vector3[] { new Vector3(-1, 0, 0), new Vector3 (0, 0, 0), new Vector3(1, 0, 0) };

        this.anim = GetComponent<Animator>();
        if (!this.anim)
            Debug.LogError("Could not resolve Animator reference.");

        this.anim.SetFloat("MoveSpeed", 1f / this.timeToChangePositions);

        SwitchPosition(Mathf.FloorToInt(this.positions.Length / 2));
    }

    public void SwitchPosition(int i)
    {
        if (this.isMoving)
            return;

        if (i < 0 || i >= this.positions.Length)
            return;

        if (this.index > i)
            this.anim.SetTrigger("MoveLeft");
        else if (this.index < i)
            this.anim.SetTrigger("MoveRight");

        this.index = i;
        StartCoroutine(MoveToPosition(this.index));
    }

    public void MovePositionUp()
    {
        SwitchPosition(this.index + 1);
    }

    public void MovePositionDown()
    {
        SwitchPosition(this.index - 1);
    }

    private IEnumerator MoveToPosition(int i)
    {
        this.isMoving = true;

        Vector3 startPosition = this.transform.position;
        float startTime = Time.time - Time.deltaTime;
        float interpolationValue = 0f;
        while (interpolationValue <= 1f)
        {
            interpolationValue = (Time.time - startTime) / this.timeToChangePositions;
            this.transform.position = Vector3.Lerp(startPosition, this.positions[i], this.moveAnimationCurve.Evaluate(interpolationValue));
            yield return null;
        }
        
        this.isMoving = false;
    }
}
