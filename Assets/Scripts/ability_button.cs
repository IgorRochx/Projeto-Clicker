using UnityEngine;
// Enum identifica qual habilidade este botão representa
public enum TipoHabilidade { ClickDoubler, PainelSolar, EsferaDyson }
public class AbilityButton : MonoBehaviour
{
 [Tooltip("Selecione a habilidade correspondente a este botão")]
 public TipoHabilidade tipo;
 // Chamado pelo evento OnClick do componente Button no Inspector
 public void OnClicar()
 {
 switch (tipo)
 {
 case TipoHabilidade.ClickDoubler:
 GameManager.Instance.ComprarClickDoubler();
 break;
 case TipoHabilidade.PainelSolar:
 GameManager.Instance.ComprarPainelSolar();
 break;
 case TipoHabilidade.EsferaDyson:
 GameManager.Instance.ComprarEsferaDyson();
 break;
 }
 }
}