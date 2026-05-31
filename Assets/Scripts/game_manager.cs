using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
 // Singleton: acesso global sem precisar arrastar referência no Inspector
 public static GameManager Instance;
 // ── Energia ───────────────────────────────────────────────────────
 public double energia = 0;
 public double energiaPorClique = 1;
 // ── Habilidades (quantidade comprada) ─────────────────────────────
 public int qtdClickDoubler = 0; // máximo: 1000
 public int qtdPainelSolar = 0; // máximo: 10
 public int qtdEsferaDyson = 0; // máximo: 3
 // Custos base (escalam a cada compra via Math.Pow)
 const double CUSTO_CLICK = 50;
 const double CUSTO_PAINEL = 500;
 const double CUSTO_DYSON = 50000;
 // ── Lifecycle ─────────────────────────────────────────────────────
 void Awake()
 {
 // Garante instância única. Se já existir, destrói este objeto.
 if (Instance == null) Instance = this;
 else { Destroy(gameObject); return; }
 CarregarSave();
 }
 void Start()
 {
 // Inicia os geradores de tick passivo
 // nameof() evita bugs silenciosos se o método for renomeado
 InvokeRepeating(nameof(TickPainelSolar), 30f, 30f);
 InvokeRepeating(nameof(TickEsferaDyson), 20f, 20f);
 }

 void OnApplicationQuit() => Salvar();
 // ── Ticks passivos ────────────────────────────────────────────────
 // Chamado a cada 30 segundos. Gera 10 energia por painel comprado.
 void TickPainelSolar()
 {
 if (qtdPainelSolar > 0)
 energia += qtdPainelSolar * 10;
 }
 // Chamado a cada 20 segundos. Gera 500 energia por esfera comprada.
 void TickEsferaDyson()
 {
 if (qtdEsferaDyson > 0)
 energia += qtdEsferaDyson * 500;
 }
 // ── Clique ────────────────────────────────────────────────────────
 public void Clicar()
 {
 energia += energiaPorClique;
 }
 // ── Compras ───────────────────────────────────────────────────────
 public bool ComprarClickDoubler()
 {
 if (qtdClickDoubler >= 1000) return false;
 double custo = CustoClickDoubler();
 if (energia < custo) return false;
 energia -= custo;
 qtdClickDoubler++;
 energiaPorClique *= 2; // dobra o valor do clique
 return true;
 }
 public bool ComprarPainelSolar()
 {
 if (qtdPainelSolar >= 10) return false;
 double custo = CustoPainelSolar();
 if (energia < custo) return false;
 energia -= custo;
 qtdPainelSolar++;
 return true;
 }
 public bool ComprarEsferaDyson()
 {
 if (qtdEsferaDyson >= 3) return false;
 double custo = CustoEsferaDyson();
 if (energia < custo) return false;
 energia -= custo;

 qtdEsferaDyson++;
 return true;
 }
 // ── Custos atuais (usados pelo UIManager) ─────────────────────────
 public double CustoClickDoubler()
 => qtdClickDoubler >= 1000 ? double.MaxValue
 : Math.Floor(CUSTO_CLICK * Math.Pow(2, qtdClickDoubler));
 public double CustoPainelSolar()
 => qtdPainelSolar >= 10 ? double.MaxValue
 : Math.Floor(CUSTO_PAINEL * Math.Pow(3, qtdPainelSolar));
 public double CustoEsferaDyson()
 => qtdEsferaDyson >= 3 ? double.MaxValue
 : Math.Floor(CUSTO_DYSON * Math.Pow(5, qtdEsferaDyson));
 // ── Save / Load ───────────────────────────────────────────────────
 void Salvar()
 {
 PlayerPrefs.SetString("energia", energia.ToString());
 PlayerPrefs.SetString("clique", energiaPorClique.ToString());
 PlayerPrefs.SetInt ("clickDoubler", qtdClickDoubler);
 PlayerPrefs.SetInt ("painelSolar", qtdPainelSolar);
 PlayerPrefs.SetInt ("esferaDyson", qtdEsferaDyson);
 PlayerPrefs.Save();
 }
 void CarregarSave()
 {
 if (PlayerPrefs.HasKey("energia"))
 double.TryParse(PlayerPrefs.GetString("energia"), out energia);
 if (PlayerPrefs.HasKey("clique"))
 double.TryParse(PlayerPrefs.GetString("clique"), out
energiaPorClique);
 qtdClickDoubler = PlayerPrefs.GetInt("clickDoubler", 0);
 qtdPainelSolar = PlayerPrefs.GetInt("painelSolar", 0);
 qtdEsferaDyson = PlayerPrefs.GetInt("esferaDyson", 0);
 }
 // Útil para debug — limpa todo o save
 public void ResetarSave()
 {
 PlayerPrefs.DeleteAll();
 energia = 0; energiaPorClique = 1;
 qtdClickDoubler = 0; qtdPainelSolar = 0; qtdEsferaDyson = 0;
 }
}
