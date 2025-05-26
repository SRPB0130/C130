using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Interraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.F; // 상호작용 키
    public float interactionTime = 2f;         // 상호작용에 걸리는 시간
    public string interactionAnimation = "Interact"; // 애니메이션 트리거 이름

    public Slider progressSlider; // UI 연결 (Slider 타입)
    public GameObject uiCanvas;   // UI 전체 그룹 (World or Screen space)

    private bool isPlayerInRange = false;
    private bool isInteracting = false;
    private float timer = 0f;
    private Animator playerAnimator;

    void Update()
    {
        if (isPlayerInRange && !isInteracting)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                StartCoroutine(DoInteraction());
            }
        }
    }

    IEnumerator DoInteraction()
    {
        isInteracting = true;
        timer = 0f;
        uiCanvas.SetActive(true);

        playerAnimator.SetTrigger(interactionAnimation); // 애니메이션 실행

        while (timer < interactionTime)
        {
            timer += Time.deltaTime;
            progressSlider.value = timer / interactionTime;
            yield return null;
        }

        uiCanvas.SetActive(false);
        isInteracting = false;

        // 상호작용 완료 후 처리 (예: 상자 열기)
        OnInteractionComplete();
    }

    void OnInteractionComplete()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Debug.Log("Interaction complete!");
        if (player != null)
        {
            PlayerInventory inventory = player.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddItem(); // 아이템 1개 추가
            }
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerAnimator = other.GetComponent<Animator>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            uiCanvas.SetActive(false);
        }
    }

}
