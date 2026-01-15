using UnityEngine;
using TMPro;

public class CoinsManager : MonoBehaviour
{

    public int CoinsCounter = 0;
    [SerializeField] private TMP_Text coinText;
    private void Update()
    {
        coinText.text =CoinsCounter.ToString() + " :" ;
    }
}
