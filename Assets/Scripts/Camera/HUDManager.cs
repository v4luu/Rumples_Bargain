using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("Textos del HUD")]
    public TextMeshProUGUI Lives;
    public TextMeshProUGUI Wheat;
    public TextMeshProUGUI Gems;
    public TextMeshProUGUI Coins;
    public TextMeshProUGUI Rock;
    public TextMeshProUGUI Necklace;

    // Prefijos fijos, se capturan UNA sola vez al inicio
    private string livesPrefix;
    private string wheatPrefix;
    private string gemsPrefix;
    private string coinsPrefix;
    private string rockPrefix;
    private string necklacePrefix;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Captura el prefijo original del texto configurado en el Inspector
        if (Lives != null) livesPrefix = Lives.text.Split(' ')[0];
        if (Wheat != null) wheatPrefix = Wheat.text.Split(' ')[0];
        if (Gems != null) gemsPrefix = Gems.text.Split(' ')[0];
        if (Coins != null) coinsPrefix = Coins.text.Split(' ')[0];
        if (Rock != null) rockPrefix = Rock.text.Split(' ')[0];
        if (Necklace != null) necklacePrefix = Necklace.text.Split(' ')[0];

        UpdateAll();
    }

    public void UpdateAll()
    {
        UpdateVidas();
        UpdatePaja();
        UpdateGemas();
        UpdateMonedas();
        UpdatePiedra();
        UpdateCollar();
    }

    public void UpdateVidas()
    {
        if (Lives != null && PlayerStats.Instance != null)
            Lives.text = livesPrefix + " " + PlayerStats.Instance.currentLives;
    }

    public void UpdatePaja()
    {
        if (Wheat != null)
            Wheat.text = wheatPrefix + " " + PlayerStats.wheatCount + "/4";
    }

    public void UpdateGemas()
    {
        if (Gems != null)
            Gems.text = gemsPrefix + " " + PlayerStats.gemCount + "/5";
    }

    public void UpdateMonedas()
    {
        if (Coins != null)
            Coins.text = coinsPrefix + " " + PlayerStats.coinCount;
    }

    public void UpdatePiedra(bool tiene = false)
    {
        if (Rock != null)
            Rock.text = rockPrefix + " " + (tiene ? "Sí" : "No");
    }

    public void UpdateCollar(bool tiene = false)
    {
        if (Necklace != null)
            Necklace.text = necklacePrefix + " " + (tiene ? "Collar listo" : "Sin collar");
    }
}
