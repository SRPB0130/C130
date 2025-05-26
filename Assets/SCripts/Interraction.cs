using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Interraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.F; // ��ȣ�ۿ� Ű
    public float interactionTime = 2f;         // ��ȣ�ۿ뿡 �ɸ��� �ð�
    public string interactionAnimation = "Interact"; // �ִϸ��̼� Ʈ���� �̸�

    public Slider progressSlider; // UI ���� (Slider Ÿ��)
    public GameObject uiCanvas;   // UI ��ü �׷� (World or Screen space)

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

        playerAnimator.SetTrigger(interactionAnimation); // �ִϸ��̼� ����

        while (timer < interactionTime)
        {
            timer += Time.deltaTime;
            progressSlider.value = timer / interactionTime;
            yield return null;
        }

        uiCanvas.SetActive(false);
        isInteracting = false;

        // ��ȣ�ۿ� �Ϸ� �� ó�� (��: ���� ����)
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
                inventory.AddItem(); // ������ 1�� �߰�
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
