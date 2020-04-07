using UnityEngine;

public class Panel : MonoBehaviour {
    public PotaTweenPreset animEnable, animDisable;

    public virtual void OnPanelEnable() {
      PotaTween tween = animEnable.Initialize(gameObject);
      tween.Play(()=> {
        GetComponent<CanvasGroup>().interactable = true;
      });
    }

    public virtual void OnPanelDisable() {
      PotaTween tween = animDisable.Initialize(gameObject);
      tween.Play(()=> {
        gameObject.SetActive(false);
      });
      GetComponent<CanvasGroup>().interactable = false;
    }
}
