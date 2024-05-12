using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IdleButtons : MonoBehaviour
{
    public Button LongButton;
    public Button SizeButton;
    public int LongPrice = 10;
    public int SizePrice = 10;
    // Start is called before the first frame update
    void Start()
    {
        LongButton.onClick.AddListener(() =>
        {
            Longer();
        });
        SizeButton.onClick.AddListener(() =>
        {
            Bigger();
        });
        GameController.Instance.OnCoinChanged += OnCoinChanged;
        PopulatePrice();
        OnCoinChanged(this, 0);
    }

    private void PopulatePrice()
    {
        LongButton.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = LongPrice.ToString();
        SizeButton.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = SizePrice.ToString();
    }

    private void OnCoinChanged(object sender, int e)
    {
        LongButton.interactable = GameController.Instance.Coin > LongPrice;
        SizeButton.interactable = GameController.Instance.Coin > SizePrice;
    }

    private void Longer()
    {
        if (GameController.Instance.Coin > LongPrice)
        {
            // A.Player.Long();
            GameController.Instance.Coin -= LongPrice;
            LongPrice += 20;
            PopulatePrice();
        }
    }

    private void Bigger()
    {
        if (GameController.Instance.Coin > SizePrice)
        {
            // A.Player.Size();
            GameController.Instance.Coin -= SizePrice;
            SizePrice += 40;
            PopulatePrice();
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
