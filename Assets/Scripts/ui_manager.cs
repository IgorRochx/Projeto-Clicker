using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Textos principais")]
    public TMP_Text textoEnergia;
    public TMP_Text textoClique;

    [Header("Textos dos botões de habilidade")]
    public TMP_Text textoClickDoubler;
    public TMP_Text textoPainelSolar;
    public TMP_Text textoEsferaDyson;

    void Update()
    {
        AtualizarUI();
    }

    void AtualizarUI()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;

        // -- Textos principais
        textoEnergia.text = Fmt(gm.energia) + " ⚡";
        textoClique.text = "Clique: +" + Fmt(gm.energiaPorClique) + " ⚡";

        // -- Dobrar Clique
        bool maxClick = gm.qtdClickDoubler >= 1000;
        bool podeClik = !maxClick && gm.energia >= gm.CustoClickDoubler();
        textoClickDoubler.text = maxClick
            ? "🖱️ Dobrar Clique\nMAX (1000/1000)"
            : $"🖱️ Dobrar Clique\nCusto: {Fmt(gm.CustoClickDoubler())}\n{gm.qtdClickDoubler}/1000";
        textoClickDoubler.color = podeClik ? Color.white : new Color(1, 1, 1, 0.35f);

        // -- Painel Solar
        bool maxPainel = gm.qtdPainelSolar >= 10;
        bool podePain = !maxPainel && gm.energia >= gm.CustoPainelSolar();
        textoPainelSolar.text = maxPainel
            ? "☀️ Painel Solar\nMAX (10/10)\n+10 ⚡ a cada 30s"
            : $"☀️ Painel Solar\nCusto: {Fmt(gm.CustoPainelSolar())}\n{gm.qtdPainelSolar}/10 → +{gm.qtdPainelSolar * 10}/30s";
        textoPainelSolar.color = podePain ? Color.white : new Color(1, 1, 1, 0.35f);

        // -- Esfera de Dyson
        bool maxDyson = gm.qtdEsferaDyson >= 3;
        bool podeDyson = !maxDyson && gm.energia >= gm.CustoEsferaDyson();
        textoEsferaDyson.text = maxDyson
            ? "⭐ Esfera de Dyson\nMAX (3/3)\n+500 ⚡ a cada 20s"
            : $"⭐ Esfera de Dyson\nCusto: {Fmt(gm.CustoEsferaDyson())}\n{gm.qtdEsferaDyson}/3 → +{gm.qtdEsferaDyson * 500}/20s";
        textoEsferaDyson.color = podeDyson ? Color.white : new Color(1, 1, 1, 0.35f);
    }

    // Formata números grandes: 1500 = "1.5K", 1500000 = "1.5M", etc.
    string Fmt(double v)
    {
        if (v >= 1_000_000_000) return (v / 1_000_000_000d).ToString("F1") + "B";
        if (v >= 1_000_000) return (v / 1_000_000d).ToString("F1") + "M";
        if (v >= 1_000) return (v / 1_000d).ToString("F1") + "K";
        return Mathf.FloorToInt((float)v).ToString();
    }
}