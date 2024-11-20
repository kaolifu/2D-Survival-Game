using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
  public void NoteAppear()
  {
    gameObject.SetActive(true);
  }

  public void NoteDisappearDelay(float delay)
  {
    Invoke(nameof(NoteDisappear), delay);
  }

  public void NoteDisappear()
  {
    gameObject.SetActive(false);
  }
}